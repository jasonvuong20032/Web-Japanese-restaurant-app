import { usePageTitle } from '@/hooks/usePageTitle'
import { LinkButton } from '@/components/ui/Button'

export function NotFoundPage() {
  usePageTitle('Không tìm thấy trang')

  return (
    <div className="container-page flex min-h-[60dvh] flex-col items-center justify-center py-20 text-center">
      <span className="font-jp text-7xl text-ink-muted/40">迷</span>

      <h1 className="mt-6 text-3xl font-semibold text-ink">Trang này không tồn tại</h1>
      <p className="mt-3 max-w-md text-ink-muted">
        Có thể đường dẫn đã cũ, hoặc món bạn tìm đã được đổi tên. Thử quay lại thực đơn xem sao.
      </p>

      <div className="mt-8 flex flex-wrap justify-center gap-3">
        <LinkButton to="/">Về trang chủ</LinkButton>
        <LinkButton to="/thuc-don" variant="secondary">
          Xem thực đơn
        </LinkButton>
      </div>
    </div>
  )
}
