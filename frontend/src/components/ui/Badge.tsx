import type { ReactNode } from 'react'
import { cn } from '@/lib/cn'

type Tone = 'neutral' | 'brand' | 'sakura' | 'matcha' | 'gold' | 'muted'

const tones: Record<Tone, string> = {
  neutral: 'bg-sunken text-ink-soft border-line',
  brand: 'bg-shu-500/12 text-brand border-shu-500/25',
  sakura: 'bg-sakura-400/15 text-sakura-500 border-sakura-400/30 dark:text-sakura-300',
  matcha: 'bg-matcha-500/15 text-matcha-600 border-matcha-500/30 dark:text-matcha-300',
  gold: 'bg-gold-500/15 text-gold-500 border-gold-500/30 dark:text-gold-400',
  muted: 'bg-transparent text-ink-muted border-line',
}

interface BadgeProps {
  children: ReactNode
  tone?: Tone
  className?: string
}

export function Badge({ children, tone = 'neutral', className }: BadgeProps) {
  return (
    <span
      className={cn(
        'inline-flex items-center gap-1 rounded-full border px-2.5 py-0.5 text-xs font-medium',
        tones[tone],
        className,
      )}
    >
      {children}
    </span>
  )
}
