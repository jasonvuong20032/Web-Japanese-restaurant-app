/**
 * Các kiểu phản chiếu Contracts của backend (SakuraTei.Api).
 * JSON trả về dạng camelCase — xem ConfigureHttpJsonOptions trong Program.cs.
 */

export interface PagedResult<T> {
  items: T[]
  page: number
  pageSize: number
  totalItems: number
  totalPages: number
  hasPrevious: boolean
  hasNext: boolean
}

export interface Category {
  slug: string
  name: string
  nameJp: string
  kanji: string
  description: string
  imageUrl: string
  accentColor: string
  displayOrder: number
  dishCount: number
}

export interface DishSummary {
  id: string
  slug: string
  name: string
  nameJp: string
  categorySlug: string
  description: string
  price: number
  /** Không có trường này nghĩa là món không giảm giá (server bỏ qua null khi ghi JSON). */
  originalPrice?: number
  imageUrl: string
  tags: string[]
  spicyLevel: number
  calories: number
  prepMinutes: number
  rating: number
  reviewCount: number
  isFeatured: boolean
  isAvailable: boolean
}

export interface DishDetail extends DishSummary {
  nameRomaji: string
  categoryName: string
  longDescription: string
  ingredients: string[]
  related: DishSummary[]
}

export type DishSort = 'default' | 'price_asc' | 'price_desc' | 'rating' | 'popular'

export interface DishQuery {
  category?: string
  q?: string
  minPrice?: number
  maxPrice?: number
  maxSpicy?: number
  tag?: string
  sort?: DishSort
  page?: number
  pageSize?: number
}

export interface DishFilters {
  tags: string[]
  minPrice: number
  maxPrice: number
}

export interface CartLineRequest {
  dishId: string
  quantity: number
}

export interface CreateOrderRequest {
  customerName: string
  phone: string
  email?: string
  address: string
  note?: string
  items: CartLineRequest[]
}

export interface OrderLine {
  dishId: string
  dishName: string
  dishSlug: string
  imageUrl: string
  unitPrice: number
  quantity: number
  lineTotal: number
}

export type OrderStatus = 'Received' | 'Preparing' | 'Delivering' | 'Completed' | 'Cancelled'

export interface Order {
  code: string
  customerName: string
  phone: string
  email?: string
  address: string
  note?: string
  items: OrderLine[]
  subtotal: number
  deliveryFee: number
  total: number
  status: OrderStatus
  createdAt: string
  estimatedReadyAt: string
}

export interface CreateReservationRequest {
  customerName: string
  phone: string
  email?: string
  /** Dạng yyyy-MM-dd — backend bind sang DateOnly. */
  date: string
  /** Dạng HH:mm. */
  time: string
  partySize: number
  note?: string
}

export interface Reservation {
  code: string
  customerName: string
  phone: string
  email?: string
  date: string
  time: string
  partySize: number
  note?: string
  createdAt: string
}

export interface TimeSlot {
  time: string
  remainingSeats: number
  isAvailable: boolean
}

export interface Availability {
  date: string
  slots: TimeSlot[]
}

export interface BlogPost {
  slug: string
  title: string
  excerpt: string
  /** Markdown rút gọn: `##` mở tiểu mục, dòng trống ngăn đoạn. */
  content: string
  author: string
  imageUrl: string
  topic: string
  publishedAt: string
  readMinutes: number
}
