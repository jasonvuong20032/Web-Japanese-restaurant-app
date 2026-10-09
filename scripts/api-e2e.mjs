#!/usr/bin/env node
/**
 * Kiểm thử đầu–cuối API Sakura Tei: thêm / đọc / huỷ đơn hàng và thêm / đọc / sửa / xoá đặt bàn,
 * gọi thẳng vào API đang chạy rồi đối chiếu kết quả. Không cần cài thêm gói nào (Node 18+).
 *
 *   cd backend/SakuraTei.Api && dotnet run        # terminal 1
 *   node scripts/api-e2e.mjs                      # terminal 2, mặc định http://localhost:5187
 *
 * Script tạo đơn và lượt đặt bàn thử nên chỉ chạy với API ở máy. Muốn chạy với địa chỉ khác
 * thì phải thêm cờ --allow-remote.
 */

const args = process.argv.slice(2)
const base = (args.find((a) => !a.startsWith('--')) ?? 'http://localhost:5187').replace(/\/$/, '')

if (!/^https?:\/\/(localhost|127\.0\.0\.1)(:\d+)?$/.test(base) && !args.includes('--allow-remote')) {
  console.error(`Từ chối chạy với ${base}: script sẽ tạo dữ liệu thử. Thêm --allow-remote nếu chắc chắn.`)
  process.exit(1)
}

let passed = 0
let failed = 0

async function call(method, path, body) {
  const response = await fetch(base + path, {
    method,
    headers: body ? { 'Content-Type': 'application/json' } : {},
    body: body ? JSON.stringify(body) : undefined,
  })
  const text = await response.text()
  let data = text
  try {
    data = text ? JSON.parse(text) : null
  } catch {
    /* không phải JSON — giữ nguyên chuỗi */
  }
  return { status: response.status, data }
}

function check(name, ok, detail = '') {
  if (ok) {
    passed++
    console.log(`  PASS  ${name}`)
  } else {
    failed++
    console.log(`  FAIL  ${name}  ${typeof detail === 'string' ? detail : JSON.stringify(detail)}`)
  }
}

// Ngày theo giờ Việt Nam (UTC+7) cộng 3 ngày, dạng yyyy-MM-dd — chắc chắn nằm trong 60 ngày cho phép.
const day = new Date(Date.now() + (7 + 3 * 24) * 3600 * 1000).toISOString().slice(0, 10)

console.log(`Kiểm thử ${base}\n\n== Sức khoẻ và thực đơn ==`)
{
  const { status, data } = await call('GET', '/health')
  check('health: SQLite, 250 món, 12 nhóm', status === 200 && data.database === 'SQLite' && data.dishCount === 250 && data.categoryCount === 12, data)
  const search = await call('GET', '/api/dishes?q=ca%20hoi&pageSize=1')
  check("tìm không dấu 'ca hoi' ra kết quả", search.status === 200 && search.data.totalItems > 0, search.data)
}

const { data: menu } = await call('GET', '/api/dishes?pageSize=2')
const [dishA, dishB] = menu.items

console.log('\n== Đơn hàng: thêm / đọc / huỷ ==')
let orderCode
{
  const bad = await call('POST', '/api/orders', { customerName: 'A', phone: '123', address: 'x', items: [] })
  check('dữ liệu sai → 400 kèm lỗi từng trường', bad.status === 400 && ['CustomerName', 'Phone', 'Address', 'Items'].every((k) => k in bad.data.errors), bad.data)

  const created = await call('POST', '/api/orders', {
    customerName: 'Khách Kiểm Thử',
    phone: '0901234567',
    address: '12 Đường Thử, Quận 1',
    items: [
      { dishId: dishA.id, quantity: 2 },
      { dishId: dishB.id, quantity: 1 },
    ],
  })
  check('tạo đơn → 201, mã ST-', created.status === 201 && created.data.code.startsWith('ST-'), created.data)
  check('server tự tính tạm tính từ giá thực đơn', created.data.subtotal === dishA.price * 2 + dishB.price, created.data.subtotal)
  orderCode = created.data.code

  const found = await call('GET', `/api/orders/${orderCode.toLowerCase()}`)
  check('tra cứu được bằng mã chữ thường', found.status === 200 && found.data.items.length === 2, found.status)

  const cancelled = await call('PATCH', `/api/orders/${orderCode}/cancel`)
  check('huỷ đơn → 200, trạng thái Cancelled', cancelled.status === 200 && cancelled.data.status === 'Cancelled', cancelled.data)

  const again = await call('PATCH', `/api/orders/${orderCode}/cancel`)
  check('huỷ lần nữa → 409', again.status === 409, again.data)

  const missing = await call('PATCH', '/api/orders/ST-KHONGCO/cancel')
  check('huỷ đơn không tồn tại → 404', missing.status === 404, missing.data)
}

console.log('\n== Đặt bàn: thêm / đọc / sửa / xoá ==')
{
  const body = (time, partySize, customerName = 'Khách Đặt Bàn Thử') => ({ customerName, phone: '0912345678', date: day, time, partySize })
  const remaining = async (time) => {
    const { data } = await call('GET', `/api/reservations/availability?date=${day}`)
    return data.slots.find((s) => s.time === time).remainingSeats
  }

  // Bắt đầu từ trạng thái biết trước: khung 19:00 và 19:30 của ngày thử phải còn trống.
  const before = { '19:00': await remaining('19:00'), '19:30': await remaining('19:30') }

  const first = await call('POST', '/api/reservations', body('19:00', 4))
  check('đặt bàn → 201, mã RS-', first.status === 201 && first.data.code.startsWith('RS-') && !('updatedAt' in first.data), first.data)
  check('khung 19:00 bớt 4 chỗ', (await remaining('19:00')) === before['19:00'] - 4)

  const moved = await call('PUT', `/api/reservations/${first.data.code}`, body('19:30', 6, 'Khách Đã Đổi Lịch'))
  check('đổi lịch → 200, có updatedAt', moved.status === 200 && moved.data.time === '19:30' && moved.data.partySize === 6 && 'updatedAt' in moved.data, moved.data)
  check('đổi giờ trả lại chỗ cho 19:00', (await remaining('19:00')) === before['19:00'])
  check('khung 19:30 bớt 6 chỗ', (await remaining('19:30')) === before['19:30'] - 6)

  const tooMany = await call('PUT', `/api/reservations/${first.data.code}`, body('19:30', 30))
  check('sửa thành 30 khách → 400 PartySize', tooMany.status === 400 && 'PartySize' in tooMany.data.errors, tooMany.data)

  const closed = await call('PUT', `/api/reservations/${first.data.code}`, body('23:00', 2))
  check('sửa sang giờ đóng cửa → 400 Time', closed.status === 400 && 'Time' in closed.data.errors, closed.data)

  const deleted = await call('DELETE', `/api/reservations/${first.data.code}`)
  check('huỷ đặt bàn → 204', deleted.status === 204, deleted.status)

  const gone = await call('GET', `/api/reservations/${first.data.code}`)
  check('lượt đã huỷ tra không ra → 404', gone.status === 404, gone.status)
  check('huỷ trả lại chỗ cho 19:30', (await remaining('19:30')) === before['19:30'])

  const twice = await call('DELETE', `/api/reservations/${first.data.code}`)
  check('huỷ lần nữa → 404', twice.status === 404, twice.status)
}

console.log('\n== Khác ==')
{
  const wrong = await call('GET', '/api/khong-ton-tai')
  check('đường dẫn API sai → 404 JSON, không phải HTML', wrong.status === 404 && typeof wrong.data === 'object', wrong.data)
}

console.log(`\n${passed} passed, ${failed} failed`)
process.exit(failed ? 1 : 0)
