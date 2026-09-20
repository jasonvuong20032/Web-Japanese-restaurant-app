import { createContext, useCallback, useContext, useEffect, useMemo, useReducer, useState } from 'react'
import type { ReactNode } from 'react'
import type { DishSummary } from '@/types/api'

const STORAGE_KEY = 'sakura-cart'

/** Miễn phí giao từ 500.000đ, phí 25.000đ — khớp OrderService để tổng hiển thị không lệch với hoá đơn. */
export const DELIVERY_FEE = 25_000
export const FREE_DELIVERY_THRESHOLD = 500_000
const MAX_QUANTITY = 50

export interface CartItem {
  dishId: string
  slug: string
  name: string
  nameJp: string
  price: number
  imageUrl: string
  quantity: number
}

type CartAction =
  | { type: 'add'; dish: DishSummary; quantity: number }
  | { type: 'setQuantity'; dishId: string; quantity: number }
  | { type: 'remove'; dishId: string }
  | { type: 'clear' }

function reducer(state: CartItem[], action: CartAction): CartItem[] {
  switch (action.type) {
    case 'add': {
      const existing = state.find((item) => item.dishId === action.dish.id)

      if (existing) {
        return state.map((item) =>
          item.dishId === action.dish.id
            ? { ...item, quantity: Math.min(MAX_QUANTITY, item.quantity + action.quantity) }
            : item,
        )
      }

      return [
        ...state,
        {
          dishId: action.dish.id,
          slug: action.dish.slug,
          name: action.dish.name,
          nameJp: action.dish.nameJp,
          // Chốt giá lúc thêm vào giỏ; server vẫn tự tra lại giá khi tạo đơn.
          price: action.dish.price,
          imageUrl: action.dish.imageUrl,
          quantity: Math.min(MAX_QUANTITY, action.quantity),
        },
      ]
    }

    case 'setQuantity': {
      if (action.quantity < 1) {
        return state.filter((item) => item.dishId !== action.dishId)
      }

      return state.map((item) =>
        item.dishId === action.dishId
          ? { ...item, quantity: Math.min(MAX_QUANTITY, action.quantity) }
          : item,
      )
    }

    case 'remove':
      return state.filter((item) => item.dishId !== action.dishId)

    case 'clear':
      return []
  }
}

function readStoredCart(): CartItem[] {
  try {
    const raw = localStorage.getItem(STORAGE_KEY)
    if (!raw) return []

    const parsed: unknown = JSON.parse(raw)
    if (!Array.isArray(parsed)) return []

    // Dữ liệu cũ hoặc bị sửa tay có thể thiếu trường; lọc bỏ thay vì để vỡ trang.
    return parsed.filter(isCartItem)
  } catch {
    return []
  }
}

function isCartItem(value: unknown): value is CartItem {
  if (typeof value !== 'object' || value === null) return false
  const item = value as Record<string, unknown>
  return (
    typeof item.dishId === 'string' &&
    typeof item.slug === 'string' &&
    typeof item.name === 'string' &&
    typeof item.price === 'number' &&
    typeof item.quantity === 'number' &&
    item.quantity > 0
  )
}

interface CartValue {
  items: CartItem[]
  totalQuantity: number
  subtotal: number
  deliveryFee: number
  total: number
  /** Còn thiếu bao nhiêu tiền nữa thì được miễn phí giao; 0 nghĩa là đã đạt. */
  amountToFreeDelivery: number
  isOpen: boolean
  openCart: () => void
  closeCart: () => void
  addItem: (dish: DishSummary, quantity?: number) => void
  setQuantity: (dishId: string, quantity: number) => void
  removeItem: (dishId: string) => void
  clearCart: () => void
}

const CartContext = createContext<CartValue | undefined>(undefined)

export function CartProvider({ children }: { children: ReactNode }) {
  const [items, dispatch] = useReducer(reducer, undefined, readStoredCart)
  const [isOpen, setIsOpen] = useState(false)

  useEffect(() => {
    try {
      localStorage.setItem(STORAGE_KEY, JSON.stringify(items))
    } catch {
      /* Hết dung lượng hoặc bị chặn — giỏ vẫn dùng được trong phiên này. */
    }
  }, [items])

  const addItem = useCallback((dish: DishSummary, quantity = 1) => {
    dispatch({ type: 'add', dish, quantity })
    setIsOpen(true)
  }, [])

  const setQuantity = useCallback((dishId: string, quantity: number) => {
    dispatch({ type: 'setQuantity', dishId, quantity })
  }, [])

  const removeItem = useCallback((dishId: string) => {
    dispatch({ type: 'remove', dishId })
  }, [])

  const clearCart = useCallback(() => dispatch({ type: 'clear' }), [])
  const openCart = useCallback(() => setIsOpen(true), [])
  const closeCart = useCallback(() => setIsOpen(false), [])

  const value = useMemo<CartValue>(() => {
    const subtotal = items.reduce((sum, item) => sum + item.price * item.quantity, 0)
    const deliveryFee = subtotal === 0 || subtotal >= FREE_DELIVERY_THRESHOLD ? 0 : DELIVERY_FEE

    return {
      items,
      totalQuantity: items.reduce((sum, item) => sum + item.quantity, 0),
      subtotal,
      deliveryFee,
      total: subtotal + deliveryFee,
      amountToFreeDelivery: Math.max(0, FREE_DELIVERY_THRESHOLD - subtotal),
      isOpen,
      openCart,
      closeCart,
      addItem,
      setQuantity,
      removeItem,
      clearCart,
    }
  }, [items, isOpen, openCart, closeCart, addItem, setQuantity, removeItem, clearCart])

  return <CartContext value={value}>{children}</CartContext>
}

export function useCart(): CartValue {
  const context = useContext(CartContext)
  if (!context) throw new Error('useCart phải nằm trong CartProvider.')
  return context
}
