import { useState } from 'react'
import type { FormEvent } from 'react'
import { Link } from 'react-router-dom'
import type { Reservation, ReservationRequest } from '@/types/api'
import { ApiError, api } from '@/lib/api'
import { BRAND } from '@/lib/brand'
import { cn } from '@/lib/cn'
import { addDays, formatDate, shortDayLabel, todayIso } from '@/lib/format'
import { useAsync } from '@/hooks/useAsync'
import { Button } from '@/components/ui/Button'
import { ErrorState, Skeleton, Spinner } from '@/components/ui/Feedback'
import { SelectField, TextAreaField, TextField } from '@/components/ui/Field'

/** Dải ngày nhanh: hôm nay và 13 ngày kế tiếp. Backend nhận đặt trước tối đa 60 ngày. */
const QUICK_DAYS = 14

/** Khớp quy tắc của backend: 1–12 khách một lượt, nhóm đông hơn gọi hotline. */
const MAX_PARTY_SIZE = 12

interface ReservationFormProps {
  /** Lượt đặt đang được đổi lịch; bỏ trống khi đặt bàn mới. */
  initial?: Reservation
  submitLabel: string
  /** Gửi dữ liệu lên server. Lỗi `ApiError` ném ra sẽ được hiện đúng dưới ô nhập tương ứng. */
  onSubmit: (payload: ReservationRequest) => Promise<void>
  /** Có thì hiện thêm nút "Thôi" — dùng khi đổi lịch. */
  onCancel?: () => void
}

/** Form chọn ngày → khung giờ → thông tin khách, dùng chung cho đặt bàn mới và đổi lịch. */
export function ReservationForm({ initial, submitLabel, onSubmit, onCancel }: ReservationFormProps) {
  const [date, setDate] = useState(initial?.date ?? todayIso)
  const [time, setTime] = useState(initial?.time ?? '')
  const [partySize, setPartySize] = useState(initial?.partySize ?? 2)
  const [customerName, setCustomerName] = useState(initial?.customerName ?? '')
  const [phone, setPhone] = useState(initial?.phone ?? '')
  const [email, setEmail] = useState(initial?.email ?? '')
  const [note, setNote] = useState(initial?.note ?? '')

  const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({})
  const [formError, setFormError] = useState<string>()
  const [isSubmitting, setIsSubmitting] = useState(false)

  const availability = useAsync((signal) => api.getAvailability(date, signal), [date])

  const today = todayIso()
  const quickDays = Array.from({ length: QUICK_DAYS }, (_, index) => addDays(today, index))
  const errorFor = (field: string) => fieldErrors[field]?.[0]

  /**
   * Khi đổi lịch, khung giờ cũ đang tính cả chỗ của chính lượt này. Cộng trả lại để khách thấy
   * đúng số chỗ mình có — server cũng bỏ qua lượt này khi kiểm tra.
   */
  const ownSeats = (slotTime: string) =>
    initial && date === initial.date && slotTime === initial.time ? initial.partySize : 0

  const pickDate = (next: string) => {
    setDate(next)
    // Khung giờ của ngày cũ có thể đã hết chỗ ở ngày mới, nên bỏ chọn cho chắc.
    setTime('')
  }

  const submit = async (event: FormEvent) => {
    event.preventDefault()
    setIsSubmitting(true)
    setFieldErrors({})
    setFormError(undefined)

    try {
      await onSubmit({
        customerName: customerName.trim(),
        phone: phone.trim(),
        email: email.trim() || undefined,
        date,
        time,
        partySize,
        note: note.trim() || undefined,
      })
    } catch (cause) {
      if (cause instanceof ApiError) {
        setFieldErrors(cause.fieldErrors)
        setFormError(cause.fieldErrors.time?.[0] ?? cause.message)
      } else {
        setFormError('Không gửi được yêu cầu đặt bàn. Vui lòng thử lại.')
      }

      // Hết chỗ thì bảng khung giờ đã lỗi thời — nạp lại để khách thấy ngay.
      availability.reload()
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <div className="container-page grid gap-10 py-10 lg:grid-cols-[1fr_20rem] lg:py-14">
      {/* min-w-0: không có thì cột lưới không co nhỏ hơn dải 14 ngày (~1000px) và đẩy phần tóm tắt
          bên phải ra khỏi màn hình; có rồi thì dải ngày tự cuộn ngang trong cột của nó. */}
      <form onSubmit={submit} noValidate className="min-w-0 space-y-8">
        {formError && (
          <p className="rounded-xl border border-shu-500/30 bg-shu-500/8 px-4 py-3 text-sm text-brand" role="alert">
            {formError}
          </p>
        )}

        <section>
          <h2 className="text-lg font-semibold text-ink">1. Chọn ngày</h2>

          <div className="mt-4 flex gap-2 overflow-x-auto pb-2">
            {quickDays.map((day) => {
              const { weekday, day: dayLabel } = shortDayLabel(day)
              const isActive = day === date

              return (
                <button
                  key={day}
                  type="button"
                  onClick={() => pickDate(day)}
                  aria-pressed={isActive}
                  className={cn(
                    'flex w-16 shrink-0 flex-col items-center rounded-xl border px-2 py-3 text-sm transition',
                    isActive
                      ? 'border-brand bg-brand text-on-brand'
                      : 'border-line bg-raised text-ink-soft hover:border-brand hover:text-brand',
                  )}
                >
                  <span className="text-xs opacity-80">{weekday}</span>
                  <span className="mt-1 font-semibold">{dayLabel}</span>
                </button>
              )
            })}
          </div>

          <div className="mt-3 flex flex-wrap items-center gap-3">
            <label htmlFor="date-input" className="text-sm text-ink-muted">
              Hoặc chọn ngày khác:
            </label>
            <input
              id="date-input"
              type="date"
              value={date}
              min={today}
              max={addDays(today, 60)}
              onChange={(event) => pickDate(event.target.value)}
              className="h-10 rounded-xl border border-line bg-raised px-3 text-sm text-ink outline-none focus:border-brand"
            />
          </div>
          {errorFor('date') && (
            <p className="mt-2 text-xs text-brand" role="alert">
              {errorFor('date')}
            </p>
          )}
        </section>

        <section>
          <h2 className="text-lg font-semibold text-ink">2. Chọn khung giờ</h2>
          <p className="mt-1 text-sm text-ink-muted">Ngày {formatDate(date)}</p>

          {availability.error ? (
            <ErrorState className="mt-4" description={availability.error.message} onRetry={availability.reload} />
          ) : availability.isLoading ? (
            <div className="mt-4 grid grid-cols-3 gap-2 sm:grid-cols-5">
              {Array.from({ length: 21 }, (_, index) => (
                <Skeleton key={index} className="h-12" />
              ))}
            </div>
          ) : (
            <div className="mt-4 grid grid-cols-3 gap-2 sm:grid-cols-5">
              {availability.data?.slots.map((slot) => {
                const remaining = slot.remainingSeats + ownSeats(slot.time)
                const isAvailable = remaining > 0

                return (
                  <button
                    key={slot.time}
                    type="button"
                    disabled={!isAvailable}
                    onClick={() => setTime(slot.time)}
                    aria-pressed={time === slot.time}
                    className={cn(
                      'flex flex-col items-center rounded-xl border px-2 py-2 text-sm transition',
                      time === slot.time
                        ? 'border-brand bg-brand text-on-brand'
                        : isAvailable
                          ? 'border-line bg-raised text-ink-soft hover:border-brand hover:text-brand'
                          : 'cursor-not-allowed border-line bg-sunken text-ink-muted/50 line-through',
                    )}
                  >
                    <span className="font-medium tabular-nums">{slot.time}</span>
                    <span className="text-[11px] opacity-75">{isAvailable ? `còn ${remaining}` : 'hết chỗ'}</span>
                  </button>
                )
              })}
            </div>
          )}

          {errorFor('time') && (
            <p className="mt-2 text-xs text-brand" role="alert">
              {errorFor('time')}
            </p>
          )}
        </section>

        <section>
          <h2 className="text-lg font-semibold text-ink">3. Thông tin liên hệ</h2>
          <div className="mt-4 grid gap-4 sm:grid-cols-2">
            <TextField
              label="Họ và tên"
              required
              autoComplete="name"
              value={customerName}
              onChange={(event) => setCustomerName(event.target.value)}
              error={errorFor('customerName')}
              placeholder="Nguyễn Văn A"
            />
            <TextField
              label="Số điện thoại"
              required
              type="tel"
              inputMode="tel"
              autoComplete="tel"
              value={phone}
              onChange={(event) => setPhone(event.target.value)}
              error={errorFor('phone')}
              hint="10 chữ số, bắt đầu bằng 0"
              placeholder="0901234567"
            />
            <TextField
              label="Email"
              type="email"
              autoComplete="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              error={errorFor('email')}
              hint="Không bắt buộc"
              placeholder="ban@email.com"
            />
            <SelectField
              label="Số khách"
              required
              value={String(partySize)}
              onChange={(event) => setPartySize(Number(event.target.value))}
              error={errorFor('partySize')}
            >
              {Array.from({ length: MAX_PARTY_SIZE }, (_, index) => index + 1).map((size) => (
                <option key={size} value={size}>
                  {size} người
                </option>
              ))}
            </SelectField>
          </div>

          <TextAreaField
            className="mt-4"
            label="Ghi chú"
            rows={3}
            value={note}
            onChange={(event) => setNote(event.target.value)}
            error={errorFor('note')}
            maxLength={500}
            hint="Ví dụ: ngồi quầy bếp mở, có trẻ nhỏ, dị ứng hải sản"
            placeholder="Cho chúng tôi biết nếu bạn có yêu cầu riêng."
          />
        </section>

        <div className="flex flex-wrap items-center gap-3">
          <Button type="submit" size="lg" className="w-full sm:w-auto" disabled={isSubmitting || !time}>
            {isSubmitting ? <Spinner className="size-4" /> : submitLabel}
          </Button>
          {onCancel && (
            <Button size="lg" variant="ghost" onClick={onCancel} disabled={isSubmitting}>
              Thôi, giữ lịch cũ
            </Button>
          )}
        </div>

        {!time && <p className="text-sm text-ink-muted">Hãy chọn một khung giờ để tiếp tục.</p>}
      </form>

      <aside className="h-fit space-y-6 lg:sticky lg:top-24">
        <div className="rounded-card border border-line bg-raised p-6">
          <h2 className="font-jp text-sm tracking-widest text-brand">ご案内</h2>
          <h3 className="mt-1 font-semibold text-ink">Tóm tắt lượt đặt</h3>

          <dl className="mt-4 space-y-2.5 text-sm">
            <div className="flex justify-between">
              <dt className="text-ink-muted">Ngày</dt>
              <dd className="text-ink">{formatDate(date)}</dd>
            </div>
            <div className="flex justify-between">
              <dt className="text-ink-muted">Giờ</dt>
              <dd className="text-ink">{time || '—'}</dd>
            </div>
            <div className="flex justify-between">
              <dt className="text-ink-muted">Số khách</dt>
              <dd className="text-ink">{partySize} người</dd>
            </div>
          </dl>
        </div>

        <div className="rounded-card border border-line bg-sunken p-6 text-sm text-ink-muted">
          <p className="font-medium text-ink">Lưu ý nhỏ</p>
          <ul className="mt-3 space-y-2">
            <li>Quán giữ bàn trong 15 phút kể từ giờ đặt.</li>
            <li>
              Muốn đổi giờ hoặc huỷ, vào{' '}
              <Link to="/tra-cuu" className="text-brand hover:underline">
                Tra cứu
              </Link>{' '}
              và nhập mã đặt bàn.
            </li>
            <li>
              Nhóm trên {MAX_PARTY_SIZE} người xin gọi {BRAND.hotline} để quán xếp phòng riêng.
            </li>
          </ul>
        </div>
      </aside>
    </div>
  )
}
