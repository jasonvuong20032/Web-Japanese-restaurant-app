import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import { RootLayout } from '@/components/layout/RootLayout'
import { CartProvider } from '@/context/CartContext'
import { ThemeProvider } from '@/context/ThemeContext'
import { AboutPage } from '@/pages/AboutPage'
import { BlogPage } from '@/pages/BlogPage'
import { BlogPostPage } from '@/pages/BlogPostPage'
import { CheckoutPage } from '@/pages/CheckoutPage'
import { DishDetailPage } from '@/pages/DishDetailPage'
import { HomePage } from '@/pages/HomePage'
import { MenuPage } from '@/pages/MenuPage'
import { NotFoundPage } from '@/pages/NotFoundPage'
import { OrderLookupPage } from '@/pages/OrderLookupPage'
import { ReservationPage } from '@/pages/ReservationPage'

const router = createBrowserRouter([
  {
    element: <RootLayout />,
    children: [
      { path: '/', element: <HomePage /> },
      { path: '/thuc-don', element: <MenuPage /> },
      { path: '/mon-an/:slug', element: <DishDetailPage /> },
      { path: '/dat-mon', element: <CheckoutPage /> },
      { path: '/dat-ban', element: <ReservationPage /> },
      { path: '/tra-cuu', element: <OrderLookupPage /> },
      { path: '/tra-cuu/:code', element: <OrderLookupPage /> },
      { path: '/cau-chuyen', element: <BlogPage /> },
      { path: '/cau-chuyen/:slug', element: <BlogPostPage /> },
      { path: '/ve-chung-toi', element: <AboutPage /> },
      { path: '*', element: <NotFoundPage /> },
    ],
  },
])

export function App() {
  return (
    <ThemeProvider>
      <CartProvider>
        <RouterProvider router={router} />
      </CartProvider>
    </ThemeProvider>
  )
}
