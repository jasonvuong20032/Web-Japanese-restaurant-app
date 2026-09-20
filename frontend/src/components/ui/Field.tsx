import { useId } from 'react'
import type { ComponentPropsWithoutRef, ReactNode } from 'react'
import { cn } from '@/lib/cn'

const controlClass =
  'w-full rounded-xl border bg-raised px-4 py-3 text-sm text-ink placeholder:text-ink-muted/70 ' +
  'transition outline-none focus:border-brand'

interface BaseProps {
  label: string
  error?: string
  hint?: ReactNode
  required?: boolean
  className?: string
}

function Wrapper({
  label,
  error,
  hint,
  required,
  className,
  id,
  children,
}: BaseProps & { id: string; children: ReactNode }) {
  return (
    <div className={cn('space-y-1.5', className)}>
      <label htmlFor={id} className="block text-sm font-medium text-ink-soft">
        {label}
        {required && <span className="ml-0.5 text-brand">*</span>}
      </label>
      {children}
      {error ? (
        <p id={`${id}-error`} className="text-xs text-brand" role="alert">
          {error}
        </p>
      ) : (
        hint && <p className="text-xs text-ink-muted">{hint}</p>
      )}
    </div>
  )
}

type InputProps = BaseProps & Omit<ComponentPropsWithoutRef<'input'>, 'className' | 'id'>

export function TextField({ label, error, hint, required, className, ...props }: InputProps) {
  const id = useId()

  return (
    <Wrapper label={label} error={error} hint={hint} required={required} className={className} id={id}>
      <input
        id={id}
        aria-invalid={error ? true : undefined}
        aria-describedby={error ? `${id}-error` : undefined}
        className={cn(controlClass, error ? 'border-brand' : 'border-line')}
        {...props}
      />
    </Wrapper>
  )
}

type TextAreaProps = BaseProps & Omit<ComponentPropsWithoutRef<'textarea'>, 'className' | 'id'>

export function TextAreaField({ label, error, hint, required, className, ...props }: TextAreaProps) {
  const id = useId()

  return (
    <Wrapper label={label} error={error} hint={hint} required={required} className={className} id={id}>
      <textarea
        id={id}
        rows={4}
        aria-invalid={error ? true : undefined}
        aria-describedby={error ? `${id}-error` : undefined}
        className={cn(controlClass, 'resize-y', error ? 'border-brand' : 'border-line')}
        {...props}
      />
    </Wrapper>
  )
}

type SelectProps = BaseProps & Omit<ComponentPropsWithoutRef<'select'>, 'className' | 'id'>

export function SelectField({ label, error, hint, required, className, children, ...props }: SelectProps) {
  const id = useId()

  return (
    <Wrapper label={label} error={error} hint={hint} required={required} className={className} id={id}>
      <select
        id={id}
        aria-invalid={error ? true : undefined}
        aria-describedby={error ? `${id}-error` : undefined}
        className={cn(controlClass, 'appearance-none', error ? 'border-brand' : 'border-line')}
        {...props}
      >
        {children}
      </select>
    </Wrapper>
  )
}
