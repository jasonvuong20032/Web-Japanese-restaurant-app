import { useState } from 'react'
import { cn } from '@/lib/cn'

interface SmartImageProps {
  src: string
  alt: string
  /** Ký tự kanji hiện thay ảnh khi URL lỗi — ảnh món lấy từ Wikimedia Commons nên chuyện này có xảy ra. */
  fallback?: string
  className?: string
  imgClassName?: string
  loading?: 'lazy' | 'eager'
}

export function SmartImage({
  src,
  alt,
  fallback = '和',
  className,
  imgClassName,
  loading = 'lazy',
}: SmartImageProps) {
  const [status, setStatus] = useState<'loading' | 'loaded' | 'error'>('loading')

  return (
    <div className={cn('relative overflow-hidden bg-sunken', className)}>
      {status !== 'error' && (
        <img
          src={src}
          alt={alt}
          loading={loading}
          decoding="async"
          onLoad={() => setStatus('loaded')}
          onError={() => setStatus('error')}
          className={cn(
            'size-full object-cover transition-opacity duration-500',
            status === 'loaded' ? 'opacity-100' : 'opacity-0',
            imgClassName,
          )}
        />
      )}

      {status !== 'loaded' && (
        <div
          className="absolute inset-0 grid place-items-center bg-linear-to-br from-sakura-100 to-sunken dark:from-raised dark:to-sunken"
          aria-hidden="true"
        >
          <span className="font-jp text-4xl text-ink-muted/50">{fallback}</span>
        </div>
      )}
    </div>
  )
}
