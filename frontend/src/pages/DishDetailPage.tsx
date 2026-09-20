import { useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import { api } from '@/lib/api'
import { formatPrice } from '@/lib/format'
import { useAsync } from '@/hooks/useAsync'
import { usePageTitle } from '@/hooks/usePageTitle'
import { useCart } from '@/context/CartContext'
import { DishCard } from '@/components/DishCard'
import { Badge } from '@/components/ui/Badge'
import { Button } from '@/components/ui/Button'
import { ErrorState, Skeleton } from '@/components/ui/Feedback'
import { QuantityStepper, Rating, SpicyMeter } from '@/components/ui/Meters'
import { SmartImage } from '@/components/ui/SmartImage'

const SPICY_LABELS = ['Không cay', 'Cay nhẹ', 'Cay vừa', 'Rất cay']

export function DishDetailPage() {
  const { slug = '' } = useParams()
  const { addItem } = useCart()
  const [quantity, setQuantity] = useState(1)

  const dish = useAsync((signal) => api.getDish(slug, signal), [slug])

  usePageTitle(dish.data?.name)

  if (dish.isLoading) {
    return (
      <div className="container-page grid gap-10 py-12 lg:grid-cols-2">
        <Skeleton className="aspect-4/3 rounded-card" />
        <div className="space-y-4">
          <Skeleton className="h-8 w-3/4" />
          <Skeleton className="h-4 w-1/2" />
          <Skeleton className="h-24 w-full" />
          <Skeleton className="h-12 w-40" />
        </div>
      </div>
    )
  }

  if (dish.error || !dish.data) {
    const isMissing = dish.error?.status === 404

    return (
      <div className="container-page py-20">
        <ErrorState
          title={isMissing ? 'Không tìm thấy món này' : 'Không tải được món ăn'}
          description={dish.error?.message}
          onRetry={isMissing ? undefined : dish.reload}
        />
        <div className="mt-6 text-center">
          <Link to="/thuc-don" className="text-sm text-brand hover:underline">
            ← Quay lại thực đơn
          </Link>
        </div>
      </div>
    )
  }

  const item = dish.data
  const discount = item.originalPrice
    ? Math.round(((item.originalPrice - item.price) / item.originalPrice) * 100)
    : 0

  return (
    <>
      <div className="border-b border-line bg-sunken">
        <nav className="container-page flex flex-wrap items-center gap-2 py-4 text-sm text-ink-muted" aria-label="Đường dẫn">
          <Link to="/" className="hover:text-brand">
            Trang chủ
          </Link>
          <span aria-hidden="true">/</span>
          <Link to="/thuc-don" className="hover:text-brand">
            Thực đơn
          </Link>
          <span aria-hidden="true">/</span>
          <Link to={`/thuc-don?category=${item.categorySlug}`} className="hover:text-brand">
            {item.categoryName}
          </Link>
          <span aria-hidden="true">/</span>
          <span className="text-ink">{item.name}</span>
        </nav>
      </div>

      <article className="container-page py-10 lg:py-14">
        <div className="grid gap-10 lg:grid-cols-2 lg:gap-14">
          <div className="relative">
            <SmartImage
              src={item.imageUrl}
              alt={item.name}
              loading="eager"
              className="aspect-4/3 rounded-card border border-line shadow-soft"
            />
            <div className="absolute top-4 left-4 flex flex-col items-start gap-2">
              {item.isFeatured && <Badge tone="gold">Món nổi bật</Badge>}
              {discount > 0 && <Badge tone="brand">Giảm {discount}%</Badge>}
            </div>
          </div>

          <div>
            <p className="font-jp text-lg text-brand">{item.nameJp}</p>
            <h1 className="mt-1 text-3xl font-semibold tracking-tight text-ink sm:text-4xl">{item.name}</h1>
            <p className="mt-1 text-sm text-ink-muted italic">{item.nameRomaji}</p>

            <div className="mt-4 flex flex-wrap items-center gap-4">
              <Rating value={item.rating} reviewCount={item.reviewCount} size="md" />
              <SpicyMeter level={item.spicyLevel} />
              <span className="text-sm text-ink-muted">{SPICY_LABELS[Math.min(item.spicyLevel, 3)]}</span>
            </div>

            <p className="mt-5 text-ink-soft">{item.description}</p>

            <div className="mt-6 flex flex-wrap gap-2">
              {item.tags.map((tag) => (
                <Link key={tag} to={`/thuc-don?tag=${encodeURIComponent(tag)}`}>
                  <Badge tone="sakura">{tag}</Badge>
                </Link>
              ))}
            </div>

            <dl className="mt-7 grid grid-cols-3 gap-3 rounded-card border border-line bg-raised p-4 text-center">
              <div>
                <dt className="text-xs text-ink-muted">Năng lượng</dt>
                <dd className="mt-1 font-semibold text-ink">{item.calories} kcal</dd>
              </div>
              <div className="border-x border-line">
                <dt className="text-xs text-ink-muted">Chế biến</dt>
                <dd className="mt-1 font-semibold text-ink">{item.prepMinutes} phút</dd>
              </div>
              <div>
                <dt className="text-xs text-ink-muted">Nhóm món</dt>
                <dd className="mt-1 font-semibold text-ink">{item.categoryName}</dd>
              </div>
            </dl>

            <div className="mt-7 rounded-card border border-line bg-raised p-5">
              <div className="flex items-end gap-3">
                {item.originalPrice && (
                  <span className="text-base text-ink-muted line-through">
                    {formatPrice(item.originalPrice)}
                  </span>
                )}
                <span className="text-3xl font-semibold text-brand">{formatPrice(item.price)}</span>
              </div>

              {item.isAvailable ? (
                <div className="mt-5 flex flex-wrap items-center gap-3">
                  <QuantityStepper value={quantity} onChange={setQuantity} />
                  <Button
                    size="lg"
                    className="flex-1"
                    onClick={() => {
                      addItem(item, quantity)
                      setQuantity(1)
                    }}
                  >
                    Thêm vào giỏ · {formatPrice(item.price * quantity)}
                  </Button>
                </div>
              ) : (
                <p className="mt-5 rounded-lg bg-sunken px-4 py-3 text-sm text-ink-muted">
                  Món này tạm hết hôm nay. Bếp sẽ chuẩn bị lại trong hôm sau.
                </p>
              )}
            </div>
          </div>
        </div>

        <div className="mt-14 grid gap-10 lg:grid-cols-[1fr_20rem]">
          <section>
            <h2 className="text-xl font-semibold text-ink">Về món này</h2>
            <div className="mt-4 space-y-4 leading-relaxed text-ink-soft">
              {item.longDescription.split('\n\n').map((paragraph, index) => (
                <p key={index}>{paragraph}</p>
              ))}
            </div>
          </section>

          <aside className="h-fit rounded-card border border-line bg-raised p-6">
            <h2 className="font-jp text-sm tracking-widest text-brand">材料</h2>
            <h3 className="mt-1 font-semibold text-ink">Nguyên liệu chính</h3>
            <ul className="mt-4 space-y-2">
              {item.ingredients.map((ingredient) => (
                <li key={ingredient} className="flex items-start gap-2 text-sm text-ink-soft">
                  <span className="mt-1.5 size-1.5 shrink-0 rounded-full bg-brand" aria-hidden="true" />
                  {ingredient}
                </li>
              ))}
            </ul>
          </aside>
        </div>

        {item.related.length > 0 && (
          <section className="mt-16">
            <h2 className="text-xl font-semibold text-ink">
              Món khác trong nhóm {item.categoryName}
            </h2>
            <div className="mt-6 grid gap-5 sm:grid-cols-2 lg:grid-cols-4">
              {item.related.map((related) => (
                <DishCard key={related.id} dish={related} />
              ))}
            </div>
          </section>
        )}
      </article>
    </>
  )
}
