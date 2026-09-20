import { useCallback, useEffect, useState } from 'react'
import { ApiError } from '@/lib/api'

export interface AsyncState<T> {
  data: T | undefined
  error: ApiError | undefined
  isLoading: boolean
  /** Gọi lại sau khi lỗi, không cần đổi deps. */
  reload: () => void
}

/**
 * Gọi API khi `deps` đổi và huỷ request cũ nếu có request mới chen vào,
 * nhờ vậy kết quả tới muộn không ghi đè kết quả mới hơn.
 *
 * Mọi giá trị mà `fetcher` đọc phải được liệt kê trong `deps`.
 */
export function useAsync<T>(
  fetcher: (signal: AbortSignal) => Promise<T>,
  deps: readonly unknown[],
): AsyncState<T> {
  const [data, setData] = useState<T>()
  const [error, setError] = useState<ApiError>()
  const [isLoading, setIsLoading] = useState(true)
  const [nonce, setNonce] = useState(0)

  const reload = useCallback(() => setNonce((n) => n + 1), [])

  useEffect(() => {
    const controller = new AbortController()
    let active = true

    setIsLoading(true)
    setError(undefined)

    fetcher(controller.signal)
      .then((result) => {
        if (!active) return
        setData(result)
        setIsLoading(false)
      })
      .catch((cause: unknown) => {
        if (!active || controller.signal.aborted) return
        setError(
          cause instanceof ApiError
            ? cause
            : new ApiError('Đã có lỗi không mong muốn xảy ra.', 0),
        )
        setIsLoading(false)
      })

    return () => {
      active = false
      controller.abort()
    }
    // Cố ý chỉ phụ thuộc vào deps do nơi gọi khai báo: fetcher thường là hàm inline,
    // đưa vào đây sẽ khiến effect chạy lại mỗi lần render.
  }, [...deps, nonce])

  return { data, error, isLoading, reload }
}
