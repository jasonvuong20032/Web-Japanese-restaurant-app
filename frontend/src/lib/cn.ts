import clsx from 'clsx'
import type { ClassValue } from 'clsx'

/** Gộp className có điều kiện. Dự án không dùng tailwind-merge nên tránh ghi đè cùng thuộc tính. */
export function cn(...inputs: ClassValue[]): string {
  return clsx(inputs)
}
