import { useEffect } from 'react'

/** Khoá cuộn nền khi mở drawer/modal, trả lại nguyên trạng khi đóng. */
export function useLockBodyScroll(locked: boolean): void {
  useEffect(() => {
    if (!locked) return

    const previous = document.body.style.overflow
    document.body.style.overflow = 'hidden'

    return () => {
      document.body.style.overflow = previous
    }
  }, [locked])
}
