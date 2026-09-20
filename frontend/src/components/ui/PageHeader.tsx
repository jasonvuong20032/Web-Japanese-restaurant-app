import type { ReactNode } from 'react'

interface PageHeaderProps {
  eyebrow: string
  title: string
  description?: ReactNode
  children?: ReactNode
}

/** Dải tiêu đề dùng chung cho các trang phụ, giữ nhịp trên/dưới giống nhau toàn site. */
export function PageHeader({ eyebrow, title, description, children }: PageHeaderProps) {
  return (
    <section className="border-b border-line bg-sunken bg-asanoha">
      <div className="container-page py-12 sm:py-16">
        <p className="font-jp text-sm tracking-[0.3em] text-brand uppercase">{eyebrow}</p>
        <h1 className="mt-2 text-4xl font-semibold tracking-tight text-ink sm:text-5xl">{title}</h1>
        {description && <p className="mt-4 max-w-2xl text-ink-muted">{description}</p>}
        {children && <div className="mt-6">{children}</div>}
      </div>
    </section>
  )
}
