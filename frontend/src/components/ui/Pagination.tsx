import { cn } from '@/lib/cn'

interface PaginationProps {
  page: number
  totalPages: number
  onChange: (page: number) => void
  className?: string
}

/**
 * Dải số trang rút gọn: luôn hiện trang đầu, trang cuối và các trang quanh trang hiện tại,
 * phần bị lược thay bằng dấu ba chấm.
 */
function buildPages(page: number, totalPages: number): (number | 'gap')[] {
  if (totalPages <= 7) {
    return Array.from({ length: totalPages }, (_, index) => index + 1)
  }

  const pages: (number | 'gap')[] = [1]
  const start = Math.max(2, page - 1)
  const end = Math.min(totalPages - 1, page + 1)

  if (start > 2) pages.push('gap')
  for (let i = start; i <= end; i += 1) pages.push(i)
  if (end < totalPages - 1) pages.push('gap')

  pages.push(totalPages)
  return pages
}

export function Pagination({ page, totalPages, onChange, className }: PaginationProps) {
  if (totalPages <= 1) return null

  return (
    <nav className={cn('flex items-center justify-center gap-1', className)} aria-label="Phân trang">
      <button
        type="button"
        className="grid size-10 place-items-center rounded-full text-ink-soft transition hover:bg-sunken disabled:opacity-35"
        onClick={() => onChange(page - 1)}
        disabled={page <= 1}
        aria-label="Trang trước"
      >
        ‹
      </button>

      {buildPages(page, totalPages).map((entry, index) =>
        entry === 'gap' ? (
          <span key={`gap-${index}`} className="px-1 text-ink-muted">
            …
          </span>
        ) : (
          <button
            key={entry}
            type="button"
            className={cn(
              'grid size-10 place-items-center rounded-full text-sm font-medium transition',
              entry === page
                ? 'bg-brand text-on-brand'
                : 'text-ink-soft hover:bg-sunken hover:text-ink',
            )}
            onClick={() => onChange(entry)}
            aria-current={entry === page ? 'page' : undefined}
          >
            {entry}
          </button>
        ),
      )}

      <button
        type="button"
        className="grid size-10 place-items-center rounded-full text-ink-soft transition hover:bg-sunken disabled:opacity-35"
        onClick={() => onChange(page + 1)}
        disabled={page >= totalPages}
        aria-label="Trang sau"
      >
        ›
      </button>
    </nav>
  )
}
