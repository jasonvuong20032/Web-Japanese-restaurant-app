import { useState } from 'react'
import type { DishPhoto } from '@/types/api'
import { cn } from '@/lib/cn'
import { SmartImage } from '@/components/ui/SmartImage'

interface DishGalleryProps {
  photos: DishPhoto[]
  alt: string
  fallback?: string
  /** Badge phủ lên góc ảnh lớn (nổi bật, giảm giá…). */
  overlay?: React.ReactNode
}

/** Ảnh lớn của món kèm dải ảnh nhỏ để xem các góc chụp khác và dòng ghi công tác giả. */
export function DishGallery({ photos, alt, fallback, overlay }: DishGalleryProps) {
  const [active, setActive] = useState(0)
  const current = photos[Math.min(active, photos.length - 1)]

  if (!current) return null

  return (
    <div>
      <div className="relative">
        <SmartImage
          key={current.url}
          src={current.url}
          alt={`${alt} — ảnh ${active + 1}/${photos.length}`}
          fallback={fallback}
          loading="eager"
          className="aspect-4/3 rounded-card border border-line shadow-soft"
        />
        {overlay}
        {photos.length > 1 && (
          <span className="absolute right-4 bottom-4 rounded-full bg-black/60 px-2.5 py-1 text-xs font-medium text-white">
            {active + 1} / {photos.length}
          </span>
        )}
      </div>

      {photos.length > 1 && (
        <div className="mt-3 grid grid-cols-4 gap-3" role="tablist" aria-label="Ảnh chi tiết món">
          {photos.map((photo, index) => (
            <button
              key={photo.url}
              type="button"
              role="tab"
              aria-selected={index === active}
              aria-label={`Xem ảnh ${index + 1}`}
              onClick={() => setActive(index)}
              className={cn(
                'overflow-hidden rounded-xl border-2 transition',
                index === active ? 'border-brand' : 'border-transparent opacity-70 hover:opacity-100',
              )}
            >
              <SmartImage src={photo.thumbUrl} alt="" fallback={fallback} className="aspect-4/3" />
            </button>
          ))}
        </div>
      )}

      {current.credit && (
        <p className="mt-2 text-xs text-ink-muted">
          Ảnh:{' '}
          <a href={current.sourceUrl} target="_blank" rel="noreferrer" className="hover:text-brand hover:underline">
            {current.credit}
          </a>{' '}
          · Wikimedia Commons
        </p>
      )}
    </div>
  )
}
