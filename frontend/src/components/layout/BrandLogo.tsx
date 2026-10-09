import { BRAND } from '@/lib/brand'
import { cn } from '@/lib/cn'

const SIZES = {
  md: { mark: 'size-8', name: 'text-lg', jp: 'text-[10px]' },
  lg: { mark: 'size-9', name: 'text-xl', jp: 'text-[11px]' },
} as const

/**
 * Logo Sakura Tei: biểu tượng hoa anh đào + chữ "Sakura Tei / さくら亭".
 * Mọi chỗ hiện logo đều dùng component này để đúng một kiểu — quy tắc logo xem docs/DESIGN.md mục 2.
 */
export function BrandLogo({ size = 'md', className }: { size?: keyof typeof SIZES; className?: string }) {
  const s = SIZES[size]

  return (
    <span className={cn('flex items-center gap-2.5', className)}>
      {/* alt rỗng: chữ ngay bên cạnh đã mang tên quán, để trống tránh trình đọc màn hình đọc hai lần. */}
      <img src="/sakura.svg" alt="" className={s.mark} />
      <span className="flex flex-col leading-none">
        <span className={cn('font-semibold tracking-tight text-ink', s.name)}>{BRAND.name}</span>
        <span className={cn('font-jp tracking-[0.25em] text-ink-muted', s.jp)}>{BRAND.nameJp}</span>
      </span>
    </span>
  )
}
