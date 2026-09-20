import type { ReactNode } from 'react'
import { cn } from '@/lib/cn'

interface SectionHeadingProps {
  /** Nhãn nhỏ phía trên, thường là tiếng Nhật. */
  eyebrow?: string
  title: string
  description?: ReactNode
  align?: 'left' | 'center'
  className?: string
  action?: ReactNode
}

export function SectionHeading({
  eyebrow,
  title,
  description,
  align = 'left',
  className,
  action,
}: SectionHeadingProps) {
  return (
    <div
      className={cn(
        'flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between',
        align === 'center' && 'sm:flex-col sm:items-center',
        className,
      )}
    >
      <div className={cn('max-w-2xl', align === 'center' && 'text-center')}>
        {eyebrow && (
          <p className="font-jp text-sm tracking-[0.3em] text-brand uppercase">{eyebrow}</p>
        )}
        <h2 className="mt-2 text-3xl font-semibold tracking-tight text-ink sm:text-4xl">{title}</h2>
        {description && <p className="mt-3 text-ink-muted">{description}</p>}
      </div>
      {action && <div className="shrink-0">{action}</div>}
    </div>
  )
}
