import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import type { FormEvent } from 'react'
import { ApiError, api } from '@/lib/api'
import { formatPrice } from '@/lib/format'
import { usePageTitle } from '@/hooks/usePageTitle'
import { useCart } from '@/context/CartContext'
import { Button, LinkButton } from '@/components/ui/Button'
import { EmptyState, Spinner } from '@/components/ui/Feedback'
import { TextAreaField, TextField } from '@/components/ui/Field'
import { QuantityStepper } from '@/components/ui/Meters'
import { PageHeader } from '@/components/ui/PageHeader'
import { SmartImage } from '@/components/ui/SmartImage'

interface FormState {
  customerName: string
  phone: string
  email: string
  address: string
  note: string
}

const EMPTY_FORM: FormState = { customerName: '', phone: '', email: '', address: '', note: '' }

export function CheckoutPage() {
  usePageTitle('Đặt món giao tận nơi')

  const navigate = useNavigate()
  const { items, setQuantity, removeItem, subtotal, deliveryFee, total, amountToFreeDelivery, clearCart } =
    useCart()

  const [form, setForm] = useState<FormState>(EMPTY_FORM)
  const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({})
  const [formError, setFormError] = useState<string>()
  const [isSubmitting, setIsSubmitting] = useState(false)

  const update = (field: keyof FormState) => (event: { target: { value: string } }) => {
    setForm((current) => ({ ...current, [field]: event.target.value }))
  }

  const errorFor = (field: keyof FormState) => fieldErrors[field]?.[0]

  const submit = async (event: FormEvent) => {
    event.preventDefault()
    setIsSubmitting(true)
    setFieldErrors({})
    setFormError(undefined)

    try {
      const order = await api.createOrder({
        customerName: form.customerName.trim(),
        phone: form.phone.trim(),
        email: form.email.trim() || undefined,
        address: form.address.trim(),
        note: form.note.trim() || undefined,
        items: items.map((item) => ({ dishId: item.dishId, quantity: item.quantity })),
      })

      clearCart()
      // Chuyển sang trang tra cứu kèm sẵn đơn vừa tạo, khách không phải nhập lại mã.
      navigate(`/tra-cuu/${order.code}`, { state: { order }, replace: true })
    } catch (cause) {
      if (cause instanceof ApiError) {
        setFieldErrors(cause.fieldErrors)
        // Lỗi ở dòng món (Items[0].DishId…) không gắn được vào ô nhập nào, nên hiện ở đầu form.
        const itemError = Object.entries(cause.fieldErrors).find(([key]) => key.startsWith('items'))
        setFormError(itemError?.[1][0] ?? cause.message)
      } else {
        setFormError('Không gửi được đơn hàng. Vui lòng thử lại.')
      }
    } finally {
      setIsSubmitting(false)
    }
  }

  if (items.length === 0) {
    return (
      <>
        <PageHeader eyebrow="ご注文" title="Đặt món" description="Giỏ hàng của bạn đang trống." />
        <div className="container-page py-14">
          <EmptyState
            title="Chưa có món nào trong giỏ"
            description="Ghé thực đơn và chọn vài món — đơn từ 500.000đ được miễn phí giao hàng."
            action={<LinkButton to="/thuc-don">Xem thực đơn</LinkButton>}
          />
        </div>
      </>
    )
  }

  return (
    <>
      <PageHeader
        eyebrow="ご注文"
        title="Đặt món giao tận nơi"
        description="Bếp nhận đơn ngay khi bạn gửi. Thời gian giao dự kiến 30–45 phút trong bán kính 5km."
      />

      <div className="container-page grid gap-10 py-10 lg:grid-cols-[1fr_22rem] lg:py-14">
        <form id="checkout-form" onSubmit={submit} noValidate className="space-y-8">
          {formError && (
            <p className="rounded-xl border border-shu-500/30 bg-shu-500/8 px-4 py-3 text-sm text-brand" role="alert">
              {formError}
            </p>
          )}

          <section>
            <h2 className="text-lg font-semibold text-ink">Thông tin người nhận</h2>
            <div className="mt-4 grid gap-4 sm:grid-cols-2">
              <TextField
                label="Họ và tên"
                required
                autoComplete="name"
                value={form.customerName}
                onChange={update('customerName')}
                error={errorFor('customerName')}
                placeholder="Nguyễn Văn A"
              />
              <TextField
                label="Số điện thoại"
                required
                type="tel"
                inputMode="tel"
                autoComplete="tel"
                value={form.phone}
                onChange={update('phone')}
                error={errorFor('phone')}
                hint="10 chữ số, bắt đầu bằng 0"
                placeholder="0901234567"
              />
              <TextField
                label="Email"
                type="email"
                autoComplete="email"
                value={form.email}
                onChange={update('email')}
                error={errorFor('email')}
                hint="Không bắt buộc — dùng để gửi hoá đơn"
                placeholder="ban@email.com"
                className="sm:col-span-2"
              />
              <TextField
                label="Địa chỉ giao hàng"
                required
                autoComplete="street-address"
                value={form.address}
                onChange={update('address')}
                error={errorFor('address')}
                hint="Ghi rõ số nhà, tên đường, phường/quận"
                placeholder="12 Nguyễn Huệ, Phường Bến Nghé, Quận 1"
                className="sm:col-span-2"
              />
            </div>
          </section>

          <section>
            <h2 className="text-lg font-semibold text-ink">Ghi chú cho bếp</h2>
            <TextAreaField
              className="mt-4"
              label="Yêu cầu thêm"
              value={form.note}
              onChange={update('note')}
              error={errorFor('note')}
              maxLength={500}
              hint={`${form.note.length}/500 ký tự — ví dụ: ít wasabi, không hành, giao giờ trưa`}
              placeholder="Ví dụ: bỏ wasabi, thêm gừng hồng."
            />
          </section>

          <section>
            <h2 className="text-lg font-semibold text-ink">Món đã chọn ({items.length})</h2>
            <ul className="mt-4 divide-y divide-line rounded-card border border-line bg-raised">
              {items.map((item) => (
                <li key={item.dishId} className="flex flex-wrap items-center gap-4 p-4">
                  <SmartImage src={item.imageUrl} alt={item.name} className="size-16 shrink-0 rounded-xl" />

                  <div className="min-w-0 flex-1">
                    <Link to={`/mon-an/${item.slug}`} className="font-medium text-ink hover:text-brand">
                      {item.name}
                    </Link>
                    <p className="font-jp text-xs text-ink-muted">{item.nameJp}</p>
                    <p className="mt-1 text-sm text-ink-muted">{formatPrice(item.price)}</p>
                  </div>

                  <QuantityStepper
                    size="sm"
                    value={item.quantity}
                    onChange={(next) => setQuantity(item.dishId, next)}
                  />

                  <p className="w-24 shrink-0 text-right font-semibold text-ink tabular-nums">
                    {formatPrice(item.price * item.quantity)}
                  </p>

                  <button
                    type="button"
                    onClick={() => removeItem(item.dishId)}
                    className="text-sm text-ink-muted transition hover:text-brand"
                    aria-label={`Xoá ${item.name}`}
                  >
                    ×
                  </button>
                </li>
              ))}
            </ul>
          </section>

          <Button type="submit" size="lg" className="w-full lg:hidden" disabled={isSubmitting}>
            {isSubmitting ? <Spinner className="size-4" /> : `Đặt đơn · ${formatPrice(total)}`}
          </Button>
        </form>

        <aside className="h-fit lg:sticky lg:top-24">
          <div className="rounded-card border border-line bg-raised p-6">
            <h2 className="font-jp text-sm tracking-widest text-brand">お会計</h2>
            <h3 className="mt-1 text-lg font-semibold text-ink">Tóm tắt đơn hàng</h3>

            {amountToFreeDelivery > 0 && (
              <p className="mt-4 rounded-lg bg-matcha-500/12 px-3 py-2 text-xs text-matcha-600 dark:text-matcha-300">
                Thêm <strong>{formatPrice(amountToFreeDelivery)}</strong> nữa là được miễn phí giao hàng.
              </p>
            )}

            <dl className="mt-5 space-y-2 text-sm">
              <div className="flex justify-between text-ink-muted">
                <dt>Tạm tính</dt>
                <dd className="tabular-nums">{formatPrice(subtotal)}</dd>
              </div>
              <div className="flex justify-between text-ink-muted">
                <dt>Phí giao hàng</dt>
                <dd className="tabular-nums">
                  {deliveryFee === 0 ? 'Miễn phí' : formatPrice(deliveryFee)}
                </dd>
              </div>
              <div className="flex justify-between border-t border-line pt-3 text-lg font-semibold text-ink">
                <dt>Tổng cộng</dt>
                <dd className="tabular-nums text-brand">{formatPrice(total)}</dd>
              </div>
            </dl>

            {/* Nút nằm ngoài <form> nên phải trỏ về form qua thuộc tính form=. */}
            <Button
              type="submit"
              form="checkout-form"
              size="lg"
              className="mt-6 hidden w-full lg:flex"
              disabled={isSubmitting}
            >
              {isSubmitting ? <Spinner className="size-4" /> : 'Đặt đơn ngay'}
            </Button>

            <p className="mt-4 text-xs text-ink-muted">
              Thanh toán tiền mặt hoặc chuyển khoản khi nhận hàng. Bếp sẽ gọi xác nhận trong vài phút.
            </p>
          </div>
        </aside>
      </div>
    </>
  )
}
