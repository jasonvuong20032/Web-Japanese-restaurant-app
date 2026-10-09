import { useEffect } from 'react'
import { BRAND } from '@/lib/brand'

const SUFFIX = BRAND.name

/** Đặt tiêu đề tab theo trang; truyền undefined khi dữ liệu chưa về để giữ tiêu đề cũ. */
export function usePageTitle(title: string | undefined): void {
  useEffect(() => {
    if (!title) return
    document.title = `${title} · ${SUFFIX}`
  }, [title])
}
