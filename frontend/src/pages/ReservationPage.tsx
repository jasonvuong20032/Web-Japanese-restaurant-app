import { useState } from 'react'
import type { Reservation } from '@/types/api'
import { api } from '@/lib/api'
import { BRAND } from '@/lib/brand'
import { formatDate } from '@/lib/format'
import { usePageTitle } from '@/hooks/usePageTitle'
import { ReservationForm } from '@/components/ReservationForm'
import { Button, LinkButton } from '@/components/ui/Button'
import { PageHeader } from '@/components/ui/PageHeader'

export function ReservationPage() {
  usePageTitle('Đặt bàn')

  const [confirmed, setConfirmed] = useState<Reservation>()

  if (confirmed) {
    return <ReservationConfirmation reservation={confirmed} onBook={() => setConfirmed(undefined)} />
  }

  return (
    <>
      <PageHeader
        eyebrow="ご予約"
        title="Đặt bàn tại quán"
        description={`Quán nhận khách từ 11:00 đến ${BRAND.lastSeating}, mỗi khung 30 phút, tối đa 24 chỗ một khung. Nhóm trên 12 người vui lòng gọi hotline ${BRAND.hotline}.`}
      />

      <ReservationForm
        submitLabel="Xác nhận đặt bàn"
        onSubmit={async (payload) => {
          setConfirmed(await api.createReservation(payload))
          window.scrollTo({ top: 0, behavior: 'smooth' })
        }}
      />
    </>
  )
}

function ReservationConfirmation({
  reservation,
  onBook,
}: {
  reservation: Reservation
  onBook: () => void
}) {
  return (
    <div className="container-page py-16 lg:py-24">
      <div className="mx-auto max-w-lg rounded-card border border-line bg-raised p-8 text-center shadow-soft">
        <span className="mx-auto grid size-16 place-items-center rounded-full bg-matcha-500/15 font-jp text-3xl text-matcha-600 dark:text-matcha-300">
          予
        </span>

        <h1 className="mt-6 text-2xl font-semibold text-ink">Đã giữ bàn cho bạn</h1>
        <p className="mt-2 text-ink-muted">
          Quán sẽ gọi xác nhận trong ít phút. Lưu lại mã bên dưới — cần đổi giờ hay huỷ bàn thì dùng mã này.
        </p>

        <p className="mt-6 rounded-xl bg-sunken px-4 py-3 font-mono text-xl tracking-widest text-brand">
          {reservation.code}
        </p>

        <dl className="mt-6 space-y-2.5 text-left text-sm">
          <div className="flex justify-between border-b border-line pb-2">
            <dt className="text-ink-muted">Người đặt</dt>
            <dd className="text-ink">{reservation.customerName}</dd>
          </div>
          <div className="flex justify-between border-b border-line pb-2">
            <dt className="text-ink-muted">Điện thoại</dt>
            <dd className="text-ink">{reservation.phone}</dd>
          </div>
          <div className="flex justify-between border-b border-line pb-2">
            <dt className="text-ink-muted">Thời gian</dt>
            <dd className="text-ink">
              {reservation.time} · {formatDate(reservation.date)}
            </dd>
          </div>
          <div className="flex justify-between">
            <dt className="text-ink-muted">Số khách</dt>
            <dd className="text-ink">{reservation.partySize} người</dd>
          </div>
        </dl>

        <div className="mt-8 flex flex-wrap justify-center gap-3">
          <LinkButton to={`/dat-ban/${reservation.code}`}>Xem, đổi lịch hoặc huỷ</LinkButton>
          <LinkButton to="/thuc-don" variant="secondary">
            Xem trước thực đơn
          </LinkButton>
          <Button variant="ghost" onClick={onBook}>
            Đặt thêm một bàn
          </Button>
        </div>
      </div>
    </div>
  )
}
