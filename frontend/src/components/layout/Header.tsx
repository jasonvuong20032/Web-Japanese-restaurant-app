import { useEffect, useState } from 'react'
import type { FormEvent } from 'react'
import { Link, NavLink, useLocation, useNavigate } from 'react-router-dom'
import { cn } from '@/lib/cn'
import { useCart } from '@/context/CartContext'
import { useTheme } from '@/context/ThemeContext'

const NAV_LINKS = [
  { to: '/', label: 'Trang chủ' },
  { to: '/thuc-don', label: 'Thực đơn' },
  { to: '/dat-ban', label: 'Đặt bàn' },
  { to: '/cau-chuyen', label: 'Câu chuyện' },
  { to: '/ve-chung-toi', label: 'Về chúng tôi' },
]

export function Header() {
  const { totalQuantity, openCart } = useCart()
  const { theme, toggleTheme } = useTheme()
  const [isScrolled, setIsScrolled] = useState(false)
  const [isMenuOpen, setIsMenuOpen] = useState(false)
  const [search, setSearch] = useState('')
  const location = useLocation()
  const navigate = useNavigate()

  useEffect(() => {
    const onScroll = () => setIsScrolled(window.scrollY > 8)
    onScroll()
    window.addEventListener('scroll', onScroll, { passive: true })
    return () => window.removeEventListener('scroll', onScroll)
  }, [])

  // Đổi trang thì đóng menu di động, nếu không nó che mất nội dung trang mới.
  useEffect(() => setIsMenuOpen(false), [location.pathname])

  const submitSearch = (event: FormEvent) => {
    event.preventDefault()
    const keyword = search.trim()
    navigate(keyword ? `/thuc-don?q=${encodeURIComponent(keyword)}` : '/thuc-don')
    setSearch('')
  }

  return (
    <header
      className={cn(
        'sticky top-0 z-40 border-b transition-colors duration-300',
        isScrolled || isMenuOpen
          ? 'border-line bg-surface/90 backdrop-blur-md'
          : 'border-transparent bg-surface',
      )}
    >
      <div className="container-page flex h-16 items-center gap-4 lg:h-20">
        <Link to="/" className="flex shrink-0 items-center gap-2.5" aria-label="Sakura Tei — trang chủ">
          <img src="/sakura.svg" alt="" className="size-8" />
          <span className="flex flex-col leading-none">
            <span className="text-lg font-semibold tracking-tight text-ink">Sakura Tei</span>
            <span className="font-jp text-[10px] tracking-[0.25em] text-ink-muted">さくら亭</span>
          </span>
        </Link>

        <nav className="ml-4 hidden items-center gap-1 lg:flex">
          {NAV_LINKS.map((link) => (
            <NavLink
              key={link.to}
              to={link.to}
              end={link.to === '/'}
              className={({ isActive }) =>
                cn(
                  'rounded-full px-3.5 py-2 text-sm font-medium transition',
                  isActive ? 'bg-sunken text-brand' : 'text-ink-soft hover:bg-sunken hover:text-ink',
                )
              }
            >
              {link.label}
            </NavLink>
          ))}
        </nav>

        <form onSubmit={submitSearch} className="ml-auto hidden max-w-56 flex-1 md:block" role="search">
          <label className="sr-only" htmlFor="header-search">
            Tìm món
          </label>
          <div className="relative">
            <input
              id="header-search"
              type="search"
              value={search}
              onChange={(event) => setSearch(event.target.value)}
              placeholder="Tìm món…"
              className="h-10 w-full rounded-full border border-line bg-raised pr-4 pl-9 text-sm text-ink placeholder:text-ink-muted/70 outline-none transition focus:border-brand"
            />
            <span className="absolute top-1/2 left-3 -translate-y-1/2 text-sm text-ink-muted">⌕</span>
          </div>
        </form>

        <div className="ml-auto flex items-center gap-1 md:ml-0">
          <button
            type="button"
            onClick={toggleTheme}
            className="grid size-10 place-items-center rounded-full text-ink-soft transition hover:bg-sunken hover:text-ink"
            aria-label={theme === 'dark' ? 'Chuyển sang giao diện sáng' : 'Chuyển sang giao diện tối'}
          >
            {theme === 'dark' ? '☀' : '☾'}
          </button>

          <button
            type="button"
            onClick={openCart}
            className="relative grid size-10 place-items-center rounded-full text-ink-soft transition hover:bg-sunken hover:text-ink"
            aria-label={`Mở giỏ hàng, ${totalQuantity} món`}
          >
            <span className="text-lg">🛒</span>
            {totalQuantity > 0 && (
              <span className="absolute -top-0.5 -right-0.5 grid min-w-5 place-items-center rounded-full bg-brand px-1 text-[11px] font-semibold text-on-brand tabular-nums">
                {totalQuantity > 99 ? '99+' : totalQuantity}
              </span>
            )}
          </button>

          <button
            type="button"
            onClick={() => setIsMenuOpen((open) => !open)}
            className="grid size-10 place-items-center rounded-full text-ink-soft transition hover:bg-sunken hover:text-ink lg:hidden"
            aria-expanded={isMenuOpen}
            aria-label="Menu điều hướng"
          >
            {isMenuOpen ? '✕' : '☰'}
          </button>
        </div>
      </div>

      {isMenuOpen && (
        <div className="border-t border-line bg-surface lg:hidden">
          <nav className="container-page flex flex-col py-2">
            {NAV_LINKS.map((link) => (
              <NavLink
                key={link.to}
                to={link.to}
                end={link.to === '/'}
                className={({ isActive }) =>
                  cn(
                    'rounded-lg px-3 py-3 text-sm font-medium transition',
                    isActive ? 'text-brand' : 'text-ink-soft hover:bg-sunken',
                  )
                }
              >
                {link.label}
              </NavLink>
            ))}
            <form onSubmit={submitSearch} className="px-3 py-3 md:hidden" role="search">
              <input
                type="search"
                value={search}
                onChange={(event) => setSearch(event.target.value)}
                placeholder="Tìm món…"
                aria-label="Tìm món"
                className="h-11 w-full rounded-full border border-line bg-raised px-4 text-sm text-ink placeholder:text-ink-muted/70 outline-none focus:border-brand"
              />
            </form>
          </nav>
        </div>
      )}
    </header>
  )
}
