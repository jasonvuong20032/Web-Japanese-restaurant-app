import type {
  Availability,
  BlogPost,
  Category,
  CreateOrderRequest,
  CreateReservationRequest,
  DishDetail,
  DishFilters,
  DishQuery,
  DishSummary,
  Order,
  PagedResult,
  Reservation,
} from '@/types/api'

/** Dev dùng proxy của Vite (xem vite.config.ts); khi build có thể trỏ thẳng qua biến môi trường. */
const BASE_URL = import.meta.env.VITE_API_BASE_URL ?? '/api'

/** Lỗi API kèm mã HTTP và — với lỗi 400 — danh sách lỗi theo từng trường. */
export class ApiError extends Error {
  readonly status: number

  /** Khoá đã chuẩn hoá về camelCase để khớp tên field ở form. */
  readonly fieldErrors: Record<string, string[]>

  constructor(message: string, status: number, fieldErrors: Record<string, string[]> = {}) {
    super(message)
    this.name = 'ApiError'
    this.status = status
    this.fieldErrors = fieldErrors
  }

  /** Lỗi mạng / server sập thì không có phản hồi để đọc. */
  get isNetworkError(): boolean {
    return this.status === 0
  }
}

interface ProblemBody {
  title?: string
  detail?: string
  status?: number
  errors?: Record<string, string[]>
}

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  let response: Response

  try {
    response = await fetch(`${BASE_URL}${path}`, {
      ...init,
      headers: {
        Accept: 'application/json',
        ...(init?.body ? { 'Content-Type': 'application/json' } : {}),
        ...init?.headers,
      },
    })
  } catch (error) {
    // fetch chỉ ném khi không nối được tới server hoặc request bị huỷ.
    if (error instanceof DOMException && error.name === 'AbortError') throw error
    throw new ApiError(
      'Không kết nối được tới máy chủ. Kiểm tra backend đã chạy ở cổng 5187 chưa.',
      0,
    )
  }

  if (!response.ok) {
    throw await toApiError(response)
  }

  if (response.status === 204) {
    return undefined as T
  }

  return (await response.json()) as T
}

async function toApiError(response: Response): Promise<ApiError> {
  let body: ProblemBody = {}

  try {
    body = (await response.json()) as ProblemBody
  } catch {
    /* Phản hồi không phải JSON — dùng thông điệp mặc định bên dưới. */
  }

  const fieldErrors: Record<string, string[]> = {}
  for (const [field, messages] of Object.entries(body.errors ?? {})) {
    fieldErrors[camelize(field)] = messages
  }

  const firstFieldMessage = Object.values(fieldErrors)[0]?.[0]
  const message =
    body.detail ?? firstFieldMessage ?? body.title ?? `Yêu cầu thất bại (mã ${response.status}).`

  return new ApiError(message, response.status, fieldErrors)
}

/** "CustomerName" -> "customerName"; giữ nguyên phần còn lại như "Items[0].DishId". */
function camelize(field: string): string {
  return field.charAt(0).toLowerCase() + field.slice(1)
}

function toQueryString(params: Record<string, string | number | undefined>): string {
  const search = new URLSearchParams()

  for (const [key, value] of Object.entries(params)) {
    if (value === undefined || value === '') continue
    search.set(key, String(value))
  }

  const query = search.toString()
  return query ? `?${query}` : ''
}

export const api = {
  getCategories: (signal?: AbortSignal) => request<Category[]>('/categories', { signal }),

  searchDishes: (query: DishQuery, signal?: AbortSignal) =>
    request<PagedResult<DishSummary>>(
      `/dishes${toQueryString({
        category: query.category,
        q: query.q,
        minPrice: query.minPrice,
        maxPrice: query.maxPrice,
        maxSpicy: query.maxSpicy,
        tag: query.tag,
        // "default" là quy ước của frontend; backend hiểu mọi giá trị lạ là mặc định nên bỏ hẳn cho gọn URL.
        sort: query.sort === 'default' ? undefined : query.sort,
        page: query.page,
        pageSize: query.pageSize,
      })}`,
      { signal },
    ),

  getFeaturedDishes: (take = 8, signal?: AbortSignal) =>
    request<DishSummary[]>(`/dishes/featured${toQueryString({ take })}`, { signal }),

  getDishFilters: (signal?: AbortSignal) => request<DishFilters>('/dishes/filters', { signal }),

  getDish: (slug: string, signal?: AbortSignal) =>
    request<DishDetail>(`/dishes/${encodeURIComponent(slug)}`, { signal }),

  createOrder: (payload: CreateOrderRequest) =>
    request<Order>('/orders', { method: 'POST', body: JSON.stringify(payload) }),

  getOrder: (code: string, signal?: AbortSignal) =>
    request<Order>(`/orders/${encodeURIComponent(code)}`, { signal }),

  getAvailability: (date: string, signal?: AbortSignal) =>
    request<Availability>(`/reservations/availability${toQueryString({ date })}`, { signal }),

  createReservation: (payload: CreateReservationRequest) =>
    request<Reservation>('/reservations', { method: 'POST', body: JSON.stringify(payload) }),

  getReservation: (code: string, signal?: AbortSignal) =>
    request<Reservation>(`/reservations/${encodeURIComponent(code)}`, { signal }),

  getPosts: (params: { topic?: string; page?: number; pageSize?: number }, signal?: AbortSignal) =>
    request<PagedResult<BlogPost>>(`/posts${toQueryString(params)}`, { signal }),

  getPostTopics: (signal?: AbortSignal) => request<string[]>('/posts/topics', { signal }),

  getLatestPosts: (take = 3, signal?: AbortSignal) =>
    request<BlogPost[]>(`/posts/latest${toQueryString({ take })}`, { signal }),

  getPost: (slug: string, signal?: AbortSignal) =>
    request<BlogPost>(`/posts/${encodeURIComponent(slug)}`, { signal }),
}
