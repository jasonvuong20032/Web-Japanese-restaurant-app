import { Link } from 'react-router-dom'
import type { DishSummary } from '@/types/api'
import { cn } from '@/lib/cn'
import { formatPrice } from '@/lib/format'
import { useCart } from '@/context/CartContext'
import { Badge } from '@/components/ui/Badge'
import { Rating, SpicyMeter } from '@/components/ui/Meters'
import { SmartImage } from '@/components/ui/SmartImage'

/** Một chữ kanji đại diện cho mỗi nhóm, dùng làm ảnh dự phòng khi URL ảnh lỗi. */
const CATEGORY_KANJI: Record<string, string> = {
  sushi: '寿',
  sashimi: '刺',
  ramen: '麺',
  udon: '饂',
  bbq: '焼',
  wagyu: '牛',
  'hai-san': '海',
  tempura: '揚',
  donburi: '丼',
  mochi: '餅',
  'trang-mieng': '甘',
  'thuc-uong': '茶',
}

export function DishCard({ dish, className }: { dish: DishSummary; className?: string }) {
  const { addItem } = useCart()
  const discount = dish.originalPrice
    ? Math.round(((dish.originalPrice - dish.price) / dish.originalPrice) * 100)
    : 0

  return (
    <article
      className={cn(
        'group relative flex flex-col overflow-hidden rounded-card border border-line bg-raised shadow-soft transition duration-300 hover:-translate-y-1 hover:shadow-lift',
        className,
      )}
    >
      <Link to={`/mon-an/${dish.slug}`} className="block focus-visible:outline-offset-4">
        <SmartImage
          src={dish.imageUrl}
          alt={dish.name}
          fallback={CATEGORY_KANJI[dish.categorySlug] ?? '和'}
          className="aspect-4/3"
          imgClassName="group-hover:scale-105 transition-transform duration-500"
        />
      </Link>

      <div className="pointer-events-none absolute top-3 left-3 flex flex-col items-start gap-1.5">
        {dish.isFeatured && <Badge tone="gold">Nổi bật</Badge>}
        {discount > 0 && <Badge tone="brand">-{discount}%</Badge>}
        {!dish.isAvailable && <Badge tone="neutral">Tạm hết</Badge>}
      </div>

      <div className="flex flex-1 flex-col p-4">
        <p className="font-jp text-xs text-ink-muted">{dish.nameJp}</p>

        <h3 className="mt-0.5 text-base leading-snug font-semibold text-ink">
          <Link to={`/mon-an/${dish.slug}`} className="after:absolute after:inset-0 after:content-['']">
            {dish.name}
          </Link>
        </h3>

        <p className="mt-2 line-clamp-2 text-sm text-ink-muted">{dish.description}</p>

        <div className="mt-3 flex items-center gap-3">
          <Rating value={dish.rating} reviewCount={dish.reviewCount} />
          <SpicyMeter level={dish.spicyLevel} />
        </div>

        <div className="mt-auto flex items-end justify-between gap-3 pt-4">
          <div>
            {dish.originalPrice && (
              <p className="text-xs text-ink-muted line-through">{formatPrice(dish.originalPrice)}</p>
            )}
            <p className="text-lg font-semibold text-brand">{formatPrice(dish.price)}</p>
          </div>

          {/* Nút nằm trên lớp phủ của thẻ nhờ z-10 nên bấm vào đây không mở trang chi tiết. */}
          <button
            type="button"
            onClick={() => addItem(dish)}
            disabled={!dish.isAvailable}
            className="relative z-10 grid size-10 place-items-center rounded-full bg-brand text-lg text-on-brand shadow-soft transition hover:bg-brand-hover disabled:cursor-not-allowed disabled:opacity-40"
            aria-label={`Thêm ${dish.name} vào giỏ`}
          >
            +
          </button>
        </div>
      </div>
    </article>
  )
}
