import { useState } from 'react'
import { Link, useLocation, useNavigate, useParams } from 'react-router-dom'
import type { FormEvent } from 'react'
import type { Order, OrderStatus } from '@/types/api'
import { api } from '@/lib/api'
import { cn } from '@/lib/cn'
import { formatClock, formatDateTime, formatPrice } from '@/lib/format'
import { useAsync } from '@/hooks/useAsync'
import { usePageTitle } from '@/hooks/usePageTitle'
import { Button, LinkButton } from '@/components/ui/Button'
import { ErrorState, Skeleton } from '@/components/ui/Feedback'
import { PageHeader } from '@/components/ui/PageHeader'
import { SmartImage } from '@/components/ui/SmartImage'

const STATUS_STEPS: { status: OrderStatus; label: string; note: string }[] = [
  { status: 'Received', label: 'Đã nhận đơn', note: 'Bếp đã thấy đơn của bạn.' },
  { status: 'Preparing', label: 'Đang chế biến', note: 'Món được làm theo thứ tự đơn vào.' },
  { status: 'Delivering', label: 'Đang giao', note: 'Tài xế đã rời quán.' },
  { status: 'Completed', label: 'Hoàn tất', note: 'Chúc bạn ngon miệng.' },
]

export function OrderLookupPage() {
  const { code } = useParams()
  const location = useLocation()

  // Đơn vừa tạo được đẩy kèm khi điều hướng, khỏi gọi lại API ngay sau khi đặt.
  const justCreated = (location.state as { order?: Order } | null)?.order

  usePageTitle(code ? `Đơn hàng ${code}` : 'Tra cứu đơn hàng')

  if (!code) return <LookupForm />

  return <OrderResult code={code} preloaded={justCreated?.code === code ? justCreated : undefined} />
}

function LookupForm() {
  const navigate = useNavigate()
  const [code, setCode] = useState('')

  const submit = (event: FormEvent) => {
    event.preventDefault()
    const trimmed = code.trim().toUpperCase()
    if (trimmed) navigate(`/tra-cuu/${encodeURIComponent(trimmed)}`)
  }

  return (
    <>
      <PageHeader
        eyebrow="ご注文の確認"
        title="Tra cứu đơn hàng"
        description="Nhập mã đơn dạng ST-XXXXXX mà quán gửi cho bạn sau khi đặt món."
      />

      <div className="container-page py-14">
        <form onSubmit={submit} className="mx-auto max-w-md rounded-card border border-line bg-raised p-6">
          <label htmlFor="order-code" className="block text-sm font-medium text-ink-soft">
            Mã đơn hàng
          </label>
          <input
            id="order-code"
            value={code}
            onChange={(event) => setCode(event.target.value)}
            placeholder="ST-A1B2C3"
            autoComplete="off"
            className="mt-2 h-12 w-full rounded-xl border border-line bg-surface px-4 text-sm tracking-widest text-ink uppercase placeholder:tracking-normal placeholder:text-ink-muted/70 outline-none focus:border-brand"
          />
          <Button type="submit" size="lg" className="mt-4 w-full">
            Tra cứu
          </Button>

          <p className="mt-4 text-xs text-ink-muted">
            Đơn hàng được lưu trong bộ nhớ máy chủ nên sẽ mất khi API khởi động lại — đây là bản demo
            cho bài tập.
          </p>
        </form>
      </div>
    </>
  )
}

function OrderResult({ code, preloaded }: { code: string; preloaded?: Order }) {
  const lookup = useAsync((signal) => api.getOrder(code, signal), [code])
  const order = preloaded ?? lookup.data

  if (!order && lookup.isLoading) {
    return (
      <div className="container-page space-y-4 py-14">
        <Skeleton className="h-10 w-64" />
        <Skeleton className="h-40 w-full" />
        <Skeleton className="h-64 w-full" />
      </div>
    )
  }

  if (!order) {
    return (
      <div className="container-page py-20">
        <ErrorState
          title="Không tìm thấy đơn hàng"
          description={lookup.error?.message}
          onRetry={lookup.error?.status === 404 ? undefined : lookup.reload}
        />
        <div className="mt-6 text-center">
          <Link to="/tra-cuu" className="text-sm text-brand hover:underline">
            ← Nhập mã khác
          </Link>
        </div>
      </div>
    )
  }

  const currentStep = STATUS_STEPS.findIndex((step) => step.status === order.status)
  const isCancelled = order.status === 'Cancelled'

  return (
    <>
      <PageHeader
        eyebrow="ご注文の確認"
        title={`Đơn ${order.code}`}
        description={`Đặt lúc ${formatDateTime(order.createdAt)} · dự kiến xong lúc ${formatClock(order.estimatedReadyAt)}.`}
      />

      <div className="container-page grid gap-10 py-10 lg:grid-cols-[1fr_22rem] lg:py-14">
        <div className="space-y-8">
          <section className="rounded-card border border-line bg-raised p-6">
            <h2 className="text-lg font-semibold text-ink">Tình trạng đơn</h2>

            {isCancelled ? (
              <p className="mt-4 rounded-lg bg-shu-500/10 px-4 py-3 text-sm text-brand">
                Đơn hàng này đã bị huỷ.
              </p>
            ) : (
              <ol className="mt-6 space-y-6">
                {STATUS_STEPS.map((step, index) => {
                  const isDone = index <= currentStep
                  const isCurrent = index === currentStep

                  return (
                    <li key={step.status} className="relative flex gap-4 pb-1">
                      {index < STATUS_STEPS.length - 1 && (
                        <span
                          className={cn(
                            'absolute top-9 left-4 h-full w-px',
                            isDone ? 'bg-brand/40' : 'bg-line',
                          )}
                          aria-hidden="true"
                        />
                      )}

                      <span
                        className={cn(
                          'relative z-10 grid size-8 shrink-0 place-items-center rounded-full border text-xs font-semibold',
                          isDone
                            ? 'border-brand bg-brand text-on-brand'
                            : 'border-line bg-surface text-ink-muted',
                        )}
                      >
                        {isDone ? '✓' : index + 1}
                      </span>

                      <div>
                        <p className={cn('font-medium', isCurrent ? 'text-brand' : 'text-ink')}>
                          {step.label}
                        </p>
                        <p className="mt-0.5 text-sm text-ink-muted">{step.note}</p>
                      </div>
                    </li>
                  )
                })}
              </ol>
            )}
          </section>

          <section className="rounded-card border border-line bg-raised">
            <h2 className="border-b border-line px-6 py-4 text-lg font-semibold text-ink">
              Món trong đơn ({order.items.length})
            </h2>
            <ul className="divide-y divide-line">
              {order.items.map((line) => (
                <li key={line.dishId} className="flex items-center gap-4 px-6 py-4">
                  <SmartImage src={line.imageUrl} alt={line.dishName} className="size-16 shrink-0 rounded-xl" />

                  <div className="min-w-0 flex-1">
                    <Link to={`/mon-an/${line.dishSlug}`} className="font-medium text-ink hover:text-brand">
                      {line.dishName}
                    </Link>
                    <p className="mt-0.5 text-sm text-ink-muted">
                      {formatPrice(line.unitPrice)} × {line.quantity}
                    </p>
                  </div>

                  <p className="shrink-0 font-semibold text-ink tabular-nums">{formatPrice(line.lineTotal)}</p>
                </li>
              ))}
            </ul>
          </section>
        </div>

        <aside className="h-fit space-y-6 lg:sticky lg:top-24">
          <div className="rounded-card border border-line bg-raised p-6">
            <h2 className="font-semibold text-ink">Người nhận</h2>
            <dl className="mt-4 space-y-2.5 text-sm">
              <Row label="Họ tên" value={order.customerName} />
              <Row label="Điện thoại" value={order.phone} />
              {order.email && <Row label="Email" value={order.email} />}
              <Row label="Địa chỉ" value={order.address} />
              {order.note && <Row label="Ghi chú" value={order.note} />}
            </dl>
          </div>

          <div className="rounded-card border border-line bg-raised p-6">
            <h2 className="font-semibold text-ink">Thanh toán</h2>
            <dl className="mt-4 space-y-2 text-sm">
              <div className="flex justify-between text-ink-muted">
                <dt>Tạm tính</dt>
                <dd className="tabular-nums">{formatPrice(order.subtotal)}</dd>
              </div>
              <div className="flex justify-between text-ink-muted">
                <dt>Phí giao hàng</dt>
                <dd className="tabular-nums">
                  {order.deliveryFee === 0 ? 'Miễn phí' : formatPrice(order.deliveryFee)}
                </dd>
              </div>
              <div className="flex justify-between border-t border-line pt-2 text-base font-semibold text-ink">
                <dt>Tổng cộng</dt>
                <dd className="tabular-nums text-brand">{formatPrice(order.total)}</dd>
              </div>
            </dl>
          </div>

          <LinkButton to="/thuc-don" variant="secondary" className="w-full">
            Đặt thêm món
          </LinkButton>
        </aside>
      </div>
    </>
  )
}

function Row({ label, value }: { label: string; value: string }) {
  return (
    <div className="flex gap-3">
      <dt className="w-24 shrink-0 text-ink-muted">{label}</dt>
      <dd className="flex-1 text-ink-soft">{value}</dd>
    </div>
  )
}
