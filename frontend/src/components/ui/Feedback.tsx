import type { ReactNode } from 'react'
import { cn } from '@/lib/cn'
import { Button } from './Button'

export function Spinner({ className }: { className?: string }) {
  return (
    <span
      role="status"
      aria-label="Đang tải"
      className={cn(
        'inline-block size-5 animate-spin rounded-full border-2 border-current border-t-transparent',
        className,
      )}
    />
  )
}

export function Skeleton({ className }: { className?: string }) {
  return <div className={cn('animate-pulse rounded-lg bg-sunken', className)} />
}

/** Khung chờ cho lưới món — giữ đúng tỉ lệ thẻ thật để trang không giật khi dữ liệu về. */
export function DishCardSkeleton() {
  return (
    <div className="overflow-hidden rounded-card border border-line bg-raised">
      <Skeleton className="aspect-4/3 rounded-none" />
      <div className="space-y-3 p-4">
        <Skeleton className="h-4 w-2/3" />
        <Skeleton className="h-3 w-full" />
        <Skeleton className="h-3 w-4/5" />
        <Skeleton className="h-8 w-1/3" />
      </div>
    </div>
  )
}

interface StateProps {
  title: string
  description?: ReactNode
  action?: ReactNode
  icon?: ReactNode
  className?: string
}

export function EmptyState({ title, description, action, icon, className }: StateProps) {
  return (
    <div
      className={cn(
        'flex flex-col items-center justify-center rounded-card border border-dashed border-line bg-raised/60 px-6 py-14 text-center',
        className,
      )}
    >
      <div className="mb-4 font-jp text-4xl text-ink-muted/60">{icon ?? '空'}</div>
      <h3 className="text-lg font-semibold text-ink">{title}</h3>
      {description && <p className="mt-2 max-w-md text-sm text-ink-muted">{description}</p>}
      {action && <div className="mt-6">{action}</div>}
    </div>
  )
}

export function ErrorState({
  title = 'Không tải được dữ liệu',
  description,
  onRetry,
  className,
}: {
  title?: string
  description?: ReactNode
  onRetry?: () => void
  className?: string
}) {
  return (
    <div
      className={cn(
        'flex flex-col items-center justify-center rounded-card border border-shu-500/30 bg-shu-500/8 px-6 py-12 text-center',
        className,
      )}
    >
      <div className="mb-3 font-jp text-3xl text-brand">誤</div>
      <h3 className="text-lg font-semibold text-ink">{title}</h3>
      {description && <p className="mt-2 max-w-md text-sm text-ink-muted">{description}</p>}
      {onRetry && (
        <Button variant="outline" size="sm" className="mt-5" onClick={onRetry}>
          Thử lại
        </Button>
      )}
    </div>
  )
}
