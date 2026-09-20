import { Link } from 'react-router-dom'
import type { ComponentPropsWithoutRef, ReactNode } from 'react'
import { cn } from '@/lib/cn'

type Variant = 'primary' | 'secondary' | 'ghost' | 'outline'
type Size = 'sm' | 'md' | 'lg'

const base =
  'inline-flex items-center justify-center gap-2 rounded-full font-medium transition ' +
  'disabled:cursor-not-allowed disabled:opacity-50 active:translate-y-px'

const variants: Record<Variant, string> = {
  primary: 'bg-brand text-on-brand hover:bg-brand-hover shadow-soft',
  secondary: 'bg-raised text-ink border border-line hover:border-brand hover:text-brand',
  outline: 'border border-brand text-brand hover:bg-brand hover:text-on-brand',
  ghost: 'text-ink-soft hover:bg-sunken hover:text-ink',
}

const sizes: Record<Size, string> = {
  sm: 'h-9 px-4 text-sm',
  md: 'h-11 px-6 text-sm',
  lg: 'h-13 px-8 text-base',
}

interface CommonProps {
  variant?: Variant
  size?: Size
  className?: string
  children: ReactNode
}

type ButtonProps = CommonProps & ComponentPropsWithoutRef<'button'>

export function Button({ variant = 'primary', size = 'md', className, children, ...props }: ButtonProps) {
  return (
    <button type="button" className={cn(base, variants[variant], sizes[size], className)} {...props}>
      {children}
    </button>
  )
}

type LinkButtonProps = CommonProps & { to: string } & Omit<ComponentPropsWithoutRef<typeof Link>, 'to' | 'className'>

export function LinkButton({ variant = 'primary', size = 'md', className, children, to, ...props }: LinkButtonProps) {
  return (
    <Link to={to} className={cn(base, variants[variant], sizes[size], className)} {...props}>
      {children}
    </Link>
  )
}
