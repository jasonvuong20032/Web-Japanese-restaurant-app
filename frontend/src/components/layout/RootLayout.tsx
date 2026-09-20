import { useEffect } from 'react'
import { Outlet, ScrollRestoration } from 'react-router-dom'
import { Header } from './Header'
import { Footer } from './Footer'
import { CartDrawer } from '@/components/CartDrawer'

export function RootLayout() {
  useEffect(() => {
    document.documentElement.lang = 'vi'
  }, [])

  return (
    <div className="flex min-h-dvh flex-col">
      <Header />
      <main id="main" className="flex-1">
        <Outlet />
      </main>
      <Footer />
      <CartDrawer />
      <ScrollRestoration />
    </div>
  )
}
