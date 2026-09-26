import { Link } from 'react-router-dom'

const COLUMNS = [
  {
    title: 'Khám phá',
    links: [
      { to: '/thuc-don', label: 'Thực đơn 250 món' },
      { to: '/thuc-don?category=sushi', label: 'Sushi & Sashimi' },
      { to: '/thuc-don?category=ramen', label: 'Ramen & Udon' },
      { to: '/thuc-don?category=bbq', label: 'BBQ than hoa' },
    ],
  },
  {
    title: 'Dịch vụ',
    links: [
      { to: '/dat-mon', label: 'Đặt món giao tận nơi' },
      { to: '/dat-ban', label: 'Đặt bàn tại quán' },
      { to: '/tra-cuu', label: 'Tra cứu đơn hàng' },
      { to: '/cau-chuyen', label: 'Câu chuyện ẩm thực' },
    ],
  },
]

export function Footer() {
  return (
    <footer className="mt-24 border-t border-line bg-raised">
      <div className="container-page grid gap-10 py-14 sm:grid-cols-2 lg:grid-cols-4">
        <div className="lg:col-span-2">
          <Link to="/" className="flex items-center gap-2.5">
            <img src="/sakura.svg" alt="" className="size-9" />
            <span className="flex flex-col leading-none">
              <span className="text-xl font-semibold tracking-tight text-ink">Sakura Tei</span>
              <span className="font-jp text-[11px] tracking-[0.25em] text-ink-muted">さくら亭</span>
            </span>
          </Link>

          <p className="mt-4 max-w-sm text-sm text-ink-muted">
            Nhà hàng Nhật với bếp mở, cá về mỗi sáng và nước dùng ninh từ 5 giờ. Chúng tôi nấu theo lối
            washoku: ít gia vị, tôn vị nguyên liệu.
          </p>

          <dl className="mt-6 space-y-2 text-sm">
            <div className="flex gap-2">
              <dt className="text-ink-muted">Địa chỉ:</dt>
              <dd className="text-ink-soft">128 Lê Lợi, Quận 1, TP. Hồ Chí Minh</dd>
            </div>
            <div className="flex gap-2">
              <dt className="text-ink-muted">Hotline:</dt>
              <dd>
                <a href="tel:19006868" className="text-brand hover:underline">
                  1900 6868
                </a>
              </dd>
            </div>
            <div className="flex gap-2">
              <dt className="text-ink-muted">Giờ mở cửa:</dt>
              <dd className="text-ink-soft">11:00 – 22:00 mỗi ngày</dd>
            </div>
          </dl>
        </div>

        {COLUMNS.map((column) => (
          <nav key={column.title}>
            <h3 className="text-sm font-semibold tracking-wide text-ink uppercase">{column.title}</h3>
            <ul className="mt-4 space-y-2.5">
              {column.links.map((link) => (
                <li key={link.label}>
                  <Link to={link.to} className="text-sm text-ink-muted transition hover:text-brand">
                    {link.label}
                  </Link>
                </li>
              ))}
            </ul>
          </nav>
        ))}
      </div>

      <div className="border-t border-line">
        <div className="container-page flex flex-col items-center justify-between gap-2 py-5 text-xs text-ink-muted sm:flex-row">
          <p>© {new Date().getFullYear()} Sakura Tei. Bài tập môn lập trình web.</p>
          <p className="font-jp">おいしい料理と、静かな時間を。</p>
        </div>
      </div>
    </footer>
  )
}
