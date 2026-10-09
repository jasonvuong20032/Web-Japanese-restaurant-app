import { useState } from 'react'
import { Link, useLocation, useNavigate, useParams } from 'react-router-dom'
import type { Reservation } from '@/types/api'
import { api } from '@/lib/api'
import { formatDate, formatDateTime } from '@/lib/format'
import { useAsync } from '@/hooks/useAsync'
import { usePageTitle } from '@/hooks/usePageTitle'
import { ReservationForm } from '@/components/ReservationForm'
import { LinkButton } from '@/components/ui/Button'
import { ConfirmAction } from '@/components/ui/ConfirmAction'
import { ErrorState, Skeleton } from '@/components/ui/Feedback'
import { PageHeader } from '@/components/ui/PageHeader'

interface NavigationState {
  reservation?: Reservation
  notice?: string
}

/**
 * Quản lý một lượt đặt bàn theo mã `RS-XXXXXX`: xem chi tiết, đổi lịch (`/dat-ban/:code/doi-lich`)
 * và huỷ. Đổi lịch có đường dẫn riêng để nút Back của trình duyệt quay về đúng trang xem.
 */
export function ReservationManagePage({ isEditing = false }: { isEditing?: boolean }) {
  const { code = '' } = useParams()

  usePageTitle(isEditing ? `Đổi lịch ${code}` : `Đặt bàn ${code}`)

  // key: đổi sang mã khác thì dựng lại từ đầu, không mang trạng thái của lượt trước sang.
  return <ReservationManager key={code} code={code} isEditing={isEditing} />
}

function ReservationManager({ code, isEditing }: { code: string; isEditing: boolean }) {
  const navigate = useNavigate()
  const location = useLocation()
  const lookup = useAsync((signal) => api.getReservation(code, signal), [code])
  const [isDeleted, setIsDeleted] = useState(false)

  // Vừa đổi lịch xong thì bản mới được đẩy kèm khi điều hướng, khỏi nháy khung xương.
  const state = location.state as NavigationState | null
  const justSaved = state?.reservation?.code === code ? state.reservation : undefined
  const reservation = lookup.data ?? (lookup.isLoading ? justSaved : undefined)

  if (isDeleted) {
    return <DeletedNotice code={code} />
  }

  if (!reservation && lookup.isLoading) {
    return (
      <div className="container-page space-y-4 py-14">
        <Skeleton className="h-10 w-64" />
        <Skeleton className="h-64 w-full" />
      </div>
    )
  }

  if (!reservation) {
    return (
      <div className="container-page py-20">
        <ErrorState
          title="Không tìm thấy lượt đặt bàn"
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

  const viewPath = `/dat-ban/${reservation.code}`
  const hasPassed = new Date(`${reservation.date}T${reservation.time}:00+07:00`) < new Date()

  if (isEditing && !hasPassed) {
    return (
      <>
        <PageHeader
          eyebrow="予約の変更"
          title={`Đổi lịch ${reservation.code}`}
          description="Chọn lại ngày, giờ hoặc số khách. Chỗ ở khung giờ cũ vẫn được giữ cho tới khi bạn bấm lưu."
        />

        <ReservationForm
          initial={reservation}
          submitLabel="Lưu lịch mới"
          onSubmit={async (payload) => {
            const saved = await api.updateReservation(reservation.code, payload)
            navigate(viewPath, {
              state: { reservation: saved, notice: 'Đã đổi lịch. Quán sẽ gọi xác nhận lại trong ít phút.' },
            })
          }}
          onCancel={() => navigate(viewPath)}
        />
      </>
    )
  }

  return (
    <>
      <PageHeader
        eyebrow="ご予約の確認"
        title={`Lượt đặt ${reservation.code}`}
        description={`${reservation.time} · ${formatDate(reservation.date)} · ${reservation.partySize} người`}
      />

      <div className="container-page grid gap-10 py-10 lg:grid-cols-[1fr_22rem] lg:py-14">
        <section className="h-fit rounded-card border border-line bg-raised p-6">
          {justSaved && state?.notice && (
            <p
              className="mb-6 rounded-lg bg-matcha-500/12 px-4 py-3 text-sm text-matcha-600 dark:text-matcha-300"
              role="status"
            >
              {state.notice}
            </p>
          )}

          <h2 className="text-lg font-semibold text-ink">Thông tin đặt bàn</h2>
          <dl className="mt-4 space-y-2.5 text-sm">
            <Row label="Người đặt" value={reservation.customerName} />
            <Row label="Điện thoại" value={reservation.phone} />
            {reservation.email && <Row label="Email" value={reservation.email} />}
            <Row label="Ngày" value={formatDate(reservation.date)} />
            <Row label="Giờ" value={reservation.time} />
            <Row label="Số khách" value={`${reservation.partySize} người`} />
            {reservation.note && <Row label="Ghi chú" value={reservation.note} />}
            <Row label="Đặt lúc" value={formatDateTime(reservation.createdAt)} />
            {reservation.updatedAt && <Row label="Đổi lịch lúc" value={formatDateTime(reservation.updatedAt)} />}
          </dl>
        </section>

        <aside className="h-fit space-y-4 lg:sticky lg:top-24">
          <div className="rounded-card border border-line bg-raised p-6">
            <h2 className="font-semibold text-ink">Thay đổi lượt đặt</h2>

            {hasPassed ? (
              <p className="mt-3 text-sm text-ink-muted">
                Lượt đặt này đã qua giờ hẹn nên không đổi hay huỷ được nữa.
              </p>
            ) : (
              <div className="mt-4 space-y-3">
                <LinkButton to={`${viewPath}/doi-lich`} className="w-full">
                  Đổi ngày, giờ hoặc số khách
                </LinkButton>
                <ConfirmAction
                  label="Huỷ đặt bàn"
                  className="w-full"
                  question={`Huỷ bàn ${reservation.partySize} người lúc ${reservation.time} ngày ${formatDate(reservation.date)}? Chỗ ngồi sẽ được nhường cho khách khác.`}
                  confirmLabel="Huỷ đặt bàn"
                  onConfirm={async () => {
                    await api.deleteReservation(reservation.code)
                    setIsDeleted(true)
                  }}
                />
              </div>
            )}
          </div>

          <LinkButton to="/thuc-don" variant="secondary" className="w-full">
            Xem trước thực đơn
          </LinkButton>
        </aside>
      </div>
    </>
  )
}

function DeletedNotice({ code }: { code: string }) {
  return (
    <div className="container-page py-16 lg:py-24">
      <div className="mx-auto max-w-lg rounded-card border border-line bg-raised p-8 text-center shadow-soft">
        <span className="mx-auto grid size-16 place-items-center rounded-full bg-sunken font-jp text-3xl text-ink-muted">
          消
        </span>
        <h1 className="mt-6 text-2xl font-semibold text-ink">Đã huỷ lượt đặt {code}</h1>
        <p className="mt-2 text-ink-muted">Chỗ ngồi đã được trả lại cho quán. Hẹn gặp bạn dịp khác!</p>

        <div className="mt-8 flex flex-wrap justify-center gap-3">
          <LinkButton to="/dat-ban">Đặt bàn khác</LinkButton>
          <LinkButton to="/" variant="secondary">
            Về trang chủ
          </LinkButton>
        </div>
      </div>
    </div>
  )
}

function Row({ label, value }: { label: string; value: string }) {
  return (
    <div className="flex gap-3">
      <dt className="w-28 shrink-0 text-ink-muted">{label}</dt>
      <dd className="flex-1 text-ink-soft">{value}</dd>
    </div>
  )
}
