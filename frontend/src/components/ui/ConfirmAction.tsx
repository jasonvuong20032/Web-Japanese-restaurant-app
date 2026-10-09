import { useState } from 'react'
import type { ReactNode } from 'react'
import { ApiError } from '@/lib/api'
import { cn } from '@/lib/cn'
import { Button } from '@/components/ui/Button'
import { Spinner } from '@/components/ui/Feedback'

interface ConfirmActionProps {
  /** Nhãn nút ban đầu, ví dụ "Huỷ đơn". */
  label: string
  /** Câu hỏi hiện ra sau khi bấm, nói rõ hậu quả. */
  question: ReactNode
  /** Nhãn nút xác nhận, nên lặp lại hành động: "Huỷ đơn", không phải "OK". */
  confirmLabel: string
  keepLabel?: string
  /** Lỗi ném ra từ đây được hiện ngay dưới câu hỏi, nút vẫn bấm lại được. */
  onConfirm: () => Promise<void>
  className?: string
}

/**
 * Nút cho thao tác không hoàn tác được (huỷ đơn, huỷ đặt bàn): bấm lần đầu chỉ hỏi lại ngay
 * tại chỗ, bấm xác nhận mới gọi API. Dùng thay `window.confirm` để giữ đúng giao diện của trang.
 */
export function ConfirmAction({
  label,
  question,
  confirmLabel,
  keepLabel = 'Không, giữ lại',
  onConfirm,
  className,
}: ConfirmActionProps) {
  const [isAsking, setIsAsking] = useState(false)
  const [isWorking, setIsWorking] = useState(false)
  const [error, setError] = useState<string>()

  if (!isAsking) {
    return (
      <Button variant="outline" onClick={() => setIsAsking(true)} className={className}>
        {label}
      </Button>
    )
  }

  const confirm = async () => {
    setIsWorking(true)
    setError(undefined)

    try {
      await onConfirm()
      setIsAsking(false)
    } catch (cause) {
      setError(cause instanceof ApiError ? cause.message : 'Không thực hiện được. Vui lòng thử lại.')
    } finally {
      setIsWorking(false)
    }
  }

  return (
    <div className={cn('rounded-xl border border-shu-500/30 bg-shu-500/8 p-4', className)} role="group">
      <p className="text-sm text-ink">{question}</p>

      {error && (
        <p className="mt-2 text-sm text-brand" role="alert">
          {error}
        </p>
      )}

      <div className="mt-3 flex flex-wrap gap-2">
        <Button size="sm" onClick={confirm} disabled={isWorking}>
          {isWorking ? <Spinner className="size-4" /> : confirmLabel}
        </Button>
        <Button size="sm" variant="ghost" onClick={() => setIsAsking(false)} disabled={isWorking}>
          {keepLabel}
        </Button>
      </div>
    </div>
  )
}
