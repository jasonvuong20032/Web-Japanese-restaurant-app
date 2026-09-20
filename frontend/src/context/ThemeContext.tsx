import { createContext, useCallback, useContext, useEffect, useMemo, useState } from 'react'
import type { ReactNode } from 'react'

type Theme = 'light' | 'dark'

/** Cùng khoá với script inline trong index.html — đổi ở đây phải đổi cả bên đó. */
const STORAGE_KEY = 'sakura-theme'

interface ThemeValue {
  theme: Theme
  toggleTheme: () => void
}

const ThemeContext = createContext<ThemeValue | undefined>(undefined)

function readInitialTheme(): Theme {
  // Script trong index.html đã gắn class trước khi React dựng cây, nên cứ đọc lại từ DOM.
  return document.documentElement.classList.contains('dark') ? 'dark' : 'light'
}

export function ThemeProvider({ children }: { children: ReactNode }) {
  const [theme, setTheme] = useState<Theme>(readInitialTheme)

  useEffect(() => {
    document.documentElement.classList.toggle('dark', theme === 'dark')

    try {
      localStorage.setItem(STORAGE_KEY, theme)
    } catch {
      /* Trình duyệt chặn localStorage — chỉ mất phần ghi nhớ, giao diện vẫn đúng. */
    }
  }, [theme])

  const toggleTheme = useCallback(() => {
    setTheme((current) => (current === 'dark' ? 'light' : 'dark'))
  }, [])

  const value = useMemo(() => ({ theme, toggleTheme }), [theme, toggleTheme])

  return <ThemeContext value={value}>{children}</ThemeContext>
}

export function useTheme(): ThemeValue {
  const context = useContext(ThemeContext)
  if (!context) throw new Error('useTheme phải nằm trong ThemeProvider.')
  return context
}
