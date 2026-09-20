import { cn } from '@/lib/cn'
import { formatCount } from '@/lib/format'

/** Sao đánh giá: tô đầy theo phần trăm nên nửa sao cũng hiển thị đúng. */
export function Rating({
  value,
  reviewCount,
  size = 'sm',
  className,
}: {
  value: number
  reviewCount?: number
  size?: 'sm' | 'md'
  className?: string
}) {
  const percent = Math.max(0, Math.min(100, (value / 5) * 100))
  const starSize = size === 'md' ? 'text-base' : 'text-xs'

  return (
    <span className={cn('inline-flex items-center gap-1.5', className)}>
      <span
        className={cn('relative inline-block leading-none', starSize)}
        role="img"
        aria-label={`${value.toFixed(1)} trên 5 sao`}
      >
        <span className="text-line select-none">★★★★★</span>
        <span
          className="absolute inset-0 overflow-hidden text-gold-500 select-none"
          style={{ width: `${percent}%` }}
          aria-hidden="true"
        >
          ★★★★★
        </span>
      </span>
      <span className={cn('font-medium text-ink-soft', size === 'md' ? 'text-sm' : 'text-xs')}>
        {value.toFixed(1)}
      </span>
      {reviewCount !== undefined && (
        <span className="text-xs text-ink-muted">({formatCount(reviewCount)})</span>
      )}
    </span>
  )
}

const SPICY_LABELS = ['Không cay', 'Cay nhẹ', 'Cay vừa', 'Rất cay']

/** Ba mức ớt; mức 0 không vẽ gì để thẻ món không bị rối. */
export function SpicyMeter({ level, className }: { level: number; className?: string }) {
  if (level <= 0) return null

  return (
    <span
      className={cn('inline-flex items-center gap-0.5 text-xs', className)}
      title={SPICY_LABELS[Math.min(level, 3)]}
      aria-label={SPICY_LABELS[Math.min(level, 3)]}
    >
      {[1, 2, 3].map((step) => (
        <span key={step} className={step <= level ? 'opacity-100' : 'opacity-25 grayscale'}>
          🌶️
        </span>
      ))}
    </span>
  )
}

export function QuantityStepper({
  value,
  onChange,
  min = 1,
  max = 50,
  size = 'md',
  className,
}: {
  value: number
  onChange: (next: number) => void
  min?: number
  max?: number
  size?: 'sm' | 'md'
  className?: string
}) {
  const buttonSize = size === 'sm' ? 'size-8 text-base' : 'size-10 text-lg'

  return (
    <div className={cn('inline-flex items-center rounded-full border border-line bg-raised', className)}>
      <button
        type="button"
        className={cn(
          'grid place-items-center rounded-full text-ink-soft transition hover:text-brand disabled:opacity-40',
          buttonSize,
        )}
        onClick={() => onChange(value - 1)}
        disabled={value <= min}
        aria-label="Giảm số lượng"
      >
        −
      </button>
      <span
        className={cn('min-w-8 text-center font-semibold tabular-nums', size === 'sm' ? 'text-sm' : 'text-base')}
        aria-live="polite"
      >
        {value}
      </span>
      <button
        type="button"
        className={cn(
          'grid place-items-center rounded-full text-ink-soft transition hover:text-brand disabled:opacity-40',
          buttonSize,
        )}
        onClick={() => onChange(value + 1)}
        disabled={value >= max}
        aria-label="Tăng số lượng"
      >
        +
      </button>
    </div>
  )
}
