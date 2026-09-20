const currency = new Intl.NumberFormat('vi-VN', {
  style: 'currency',
  currency: 'VND',
  maximumFractionDigits: 0,
})

const compactNumber = new Intl.NumberFormat('vi-VN', { notation: 'compact' })

const longDate = new Intl.DateTimeFormat('vi-VN', {
  day: '2-digit',
  month: 'long',
  year: 'numeric',
})

const dateTime = new Intl.DateTimeFormat('vi-VN', {
  day: '2-digit',
  month: '2-digit',
  year: 'numeric',
  hour: '2-digit',
  minute: '2-digit',
})

const clock = new Intl.DateTimeFormat('vi-VN', { hour: '2-digit', minute: '2-digit' })

export function formatPrice(value: number): string {
  return currency.format(value)
}

export function formatCount(value: number): string {
  return value >= 1000 ? compactNumber.format(value) : String(value)
}

export function formatDate(value: string | Date): string {
  return longDate.format(toDate(value))
}

export function formatDateTime(value: string | Date): string {
  return dateTime.format(toDate(value))
}

export function formatClock(value: string | Date): string {
  return clock.format(toDate(value))
}

/** Ngày hôm nay dạng yyyy-MM-dd theo giờ máy khách — khớp định dạng DateOnly của backend. */
export function todayIso(): string {
  return toIsoDate(new Date())
}

export function toIsoDate(date: Date): string {
  const month = String(date.getMonth() + 1).padStart(2, '0')
  const day = String(date.getDate()).padStart(2, '0')
  return `${date.getFullYear()}-${month}-${day}`
}

export function addDays(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`)
  date.setDate(date.getDate() + days)
  return toIsoDate(date)
}

/** Nhãn ngày ngắn cho dải chọn ngày đặt bàn, ví dụ "T4 24/09". */
export function shortDayLabel(iso: string): { weekday: string; day: string } {
  const date = new Date(`${iso}T00:00:00`)
  const weekdays = ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7']
  return {
    weekday: weekdays[date.getDay()],
    day: `${String(date.getDate()).padStart(2, '0')}/${String(date.getMonth() + 1).padStart(2, '0')}`,
  }
}

/**
 * DateOnly về dạng "2026-08-14"; ngày giờ có offset thì Date dựng thẳng được.
 * Tách riêng để chuỗi ngày trần không bị trình duyệt hiểu là UTC rồi lùi một ngày.
 */
function toDate(value: string | Date): Date {
  if (value instanceof Date) return value
  return /^\d{4}-\d{2}-\d{2}$/.test(value) ? new Date(`${value}T00:00:00`) : new Date(value)
}
