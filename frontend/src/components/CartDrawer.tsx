import { useEffect } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useCart } from '@/context/CartContext'
import { useLockBodyScroll } from '@/hooks/useLockBodyScroll'
import { formatPrice } from '@/lib/format'
import { Button } from '@/components/ui/Button'
import { QuantityStepper } from '@/components/ui/Meters'
import { SmartImage } from '@/components/ui/SmartImage'

export function CartDrawer() {
  const { items, isOpen, closeCart, setQuantity, removeItem, subtotal, deliveryFee, total, amountToFreeDelivery } =
    useCart()
  const navigate = useNavigate()

  useLockBodyScroll(isOpen)

  useEffect(() => {
    if (!isOpen) return

    const onKeyDown = (event: KeyboardEvent) => {
      if (event.key === 'Escape') closeCart()
    }

    window.addEventListener('keydown', onKeyDown)
    return () => window.removeEventListener('keydown', onKeyDown)
  }, [isOpen, closeCart])

  if (!isOpen) return null

  const goToCheckout = () => {
    closeCart()
    navigate('/dat-mon')
  }

  return (
    <div className="fixed inset-0 z-50 flex justify-end" role="dialog" aria-modal="true" aria-label="Giỏ hàng">
      <button
        type="button"
        className="absolute inset-0 bg-black/45 backdrop-blur-sm"
        onClick={closeCart}
        aria-label="Đóng giỏ hàng"
      />

      <aside className="relative flex h-full w-full max-w-md flex-col bg-surface shadow-lift">
        <header className="flex items-center justify-between border-b border-line px-5 py-4">
          <div>
            <p className="font-jp text-xs tracking-widest text-brand">お会計</p>
            <h2 className="text-lg font-semibold text-ink">Giỏ hàng của bạn</h2>
          </div>
          <button
            type="button"
            onClick={closeCart}
            className="grid size-9 place-items-center rounded-full text-xl text-ink-muted transition hover:bg-sunken hover:text-ink"
            aria-label="Đóng"
          >
            ×
          </button>
        </header>

        {items.length === 0 ? (
          <div className="flex flex-1 flex-col items-center justify-center gap-4 px-6 text-center">
            <span className="font-jp text-5xl text-ink-muted/40">空</span>
            <p className="text-ink-muted">Giỏ hàng đang trống.</p>
            <Button variant="outline" onClick={closeCart}>
              Xem thực đơn
            </Button>
          </div>
        ) : (
          <>
            <ul className="flex-1 divide-y divide-line overflow-y-auto px-5">
              {items.map((item) => (
                <li key={item.dishId} className="flex gap-3 py-4">
                  <Link to={`/mon-an/${item.slug}`} onClick={closeCart} className="shrink-0">
                    <SmartImage src={item.imageUrl} alt={item.name} className="size-20 rounded-xl" />
                  </Link>

                  <div className="min-w-0 flex-1">
                    <Link
                      to={`/mon-an/${item.slug}`}
                      onClick={closeCart}
                      className="line-clamp-2 text-sm font-medium text-ink hover:text-brand"
                    >
                      {item.name}
                    </Link>
                    <p className="mt-0.5 text-xs text-ink-muted">{formatPrice(item.price)}</p>

                    <div className="mt-2 flex items-center justify-between gap-2">
                      <QuantityStepper
                        size="sm"
                        value={item.quantity}
                        onChange={(next) => setQuantity(item.dishId, next)}
                      />
                      <button
                        type="button"
                        onClick={() => removeItem(item.dishId)}
                        className="text-xs text-ink-muted underline-offset-2 transition hover:text-brand hover:underline"
                      >
                        Xoá
                      </button>
                    </div>
                  </div>

                  <p className="shrink-0 text-sm font-semibold text-ink tabular-nums">
                    {formatPrice(item.price * item.quantity)}
                  </p>
                </li>
              ))}
            </ul>

            <footer className="space-y-3 border-t border-line bg-raised px-5 py-4">
              {amountToFreeDelivery > 0 ? (
                <p className="rounded-lg bg-matcha-500/12 px-3 py-2 text-xs text-matcha-600 dark:text-matcha-300">
                  Mua thêm <strong>{formatPrice(amountToFreeDelivery)}</strong> để được miễn phí giao hàng.
                </p>
              ) : (
                <p className="rounded-lg bg-matcha-500/12 px-3 py-2 text-xs text-matcha-600 dark:text-matcha-300">
                  Đơn này được <strong>miễn phí giao hàng</strong>.
                </p>
              )}

              <dl className="space-y-1 text-sm">
                <div className="flex justify-between text-ink-muted">
                  <dt>Tạm tính</dt>
                  <dd className="tabular-nums">{formatPrice(subtotal)}</dd>
                </div>
                <div className="flex justify-between text-ink-muted">
                  <dt>Phí giao hàng</dt>
                  <dd className="tabular-nums">{deliveryFee === 0 ? 'Miễn phí' : formatPrice(deliveryFee)}</dd>
                </div>
                <div className="flex justify-between border-t border-line pt-2 text-base font-semibold text-ink">
                  <dt>Tổng cộng</dt>
                  <dd className="tabular-nums text-brand">{formatPrice(total)}</dd>
                </div>
              </dl>

              <Button className="w-full" size="lg" onClick={goToCheckout}>
                Tiến hành đặt món
              </Button>
            </footer>
          </>
        )}
      </aside>
    </div>
  )
}
