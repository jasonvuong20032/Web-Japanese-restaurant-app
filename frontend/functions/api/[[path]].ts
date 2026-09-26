/**
 * Cloudflare Pages Function: chuyển mọi request /api/* sang API .NET đang chạy ở nơi khác (Render).
 *
 * Nhờ proxy này trình duyệt chỉ nói chuyện với đúng một origin (domain Pages), nên:
 * - không phải cấu hình CORS trên API;
 * - frontend giữ đường dẫn mặc định /api, không phải build lại khi đổi địa chỉ API;
 * - các bản preview của từng nhánh cũng gọi được API.
 *
 * File nằm ngoài src/ nên không đi qua `tsc -b` của Vite — Cloudflare tự biên dịch khi deploy.
 */

interface Env {
  /** Gốc URL của API, ví dụ https://sakura-api.onrender.com — không kèm /api, không có dấu / ở cuối. */
  API_ORIGIN?: string
}

interface PagesContext {
  request: Request
  env: Env
}

/** Trả lỗi đúng dạng ProblemDetails mà frontend (lib/api.ts) đọc được. */
function problem(status: number, title: string, detail: string): Response {
  return new Response(JSON.stringify({ title, detail, status }), {
    status,
    headers: { 'Content-Type': 'application/problem+json; charset=utf-8' },
  })
}

export async function onRequest({ request, env }: PagesContext): Promise<Response> {
  if (!env.API_ORIGIN) {
    return problem(500, 'Chưa cấu hình API', 'Thiếu biến môi trường API_ORIGIN trên Cloudflare Pages.')
  }

  const incoming = new URL(request.url)
  const target = new URL(incoming.pathname + incoming.search, env.API_ORIGIN)

  try {
    // Giữ nguyên method, header và body; Cloudflare tự đặt lại Host theo địa chỉ đích.
    return await fetch(new Request(target, request))
  } catch {
    // Render gói Free ngủ sau 15 phút không có truy cập, lần gọi đầu có thể hỏng trong lúc nó thức dậy.
    return problem(502, 'Không kết nối được tới API', 'Máy chủ API chưa phản hồi. Vui lòng thử lại sau ít phút.')
  }
}
