import { useMemo } from 'react'
import { Link, useParams } from 'react-router-dom'
import { api } from '@/lib/api'
import { formatDate } from '@/lib/format'
import { useAsync } from '@/hooks/useAsync'
import { usePageTitle } from '@/hooks/usePageTitle'
import { Badge } from '@/components/ui/Badge'
import { ErrorState, Skeleton } from '@/components/ui/Feedback'
import { SmartImage } from '@/components/ui/SmartImage'

type Block = { kind: 'heading' | 'paragraph'; text: string }

/**
 * Nội dung bài viết dùng Markdown rút gọn: dòng mở đầu bằng `## ` là tiểu mục,
 * dòng trống ngăn đoạn. Không kéo thêm thư viện markdown cho đúng hai quy tắc này.
 */
function parseContent(content: string): Block[] {
  return content
    .split(/\n\s*\n/)
    .map((block) => block.trim())
    .filter(Boolean)
    .map((block) =>
      block.startsWith('## ')
        ? { kind: 'heading' as const, text: block.slice(3).trim() }
        : { kind: 'paragraph' as const, text: block },
    )
}

export function BlogPostPage() {
  const { slug = '' } = useParams()
  const post = useAsync((signal) => api.getPost(slug, signal), [slug])

  usePageTitle(post.data?.title)

  const blocks = useMemo(() => (post.data ? parseContent(post.data.content) : []), [post.data])

  if (post.isLoading) {
    return (
      <div className="container-page max-w-3xl space-y-4 py-14">
        <Skeleton className="h-10 w-3/4" />
        <Skeleton className="aspect-16/9 rounded-card" />
        <Skeleton className="h-4 w-full" />
        <Skeleton className="h-4 w-11/12" />
        <Skeleton className="h-4 w-4/5" />
      </div>
    )
  }

  if (post.error || !post.data) {
    return (
      <div className="container-page py-20">
        <ErrorState
          title={post.error?.status === 404 ? 'Không tìm thấy bài viết' : 'Không tải được bài viết'}
          description={post.error?.message}
          onRetry={post.error?.status === 404 ? undefined : post.reload}
        />
        <div className="mt-6 text-center">
          <Link to="/cau-chuyen" className="text-sm text-brand hover:underline">
            ← Về danh sách bài viết
          </Link>
        </div>
      </div>
    )
  }

  const article = post.data

  return (
    <article className="pb-16">
      <header className="border-b border-line bg-sunken bg-asanoha">
        <div className="container-page max-w-3xl py-12 sm:py-16">
          <Link to="/cau-chuyen" className="text-sm text-brand hover:underline">
            ← Câu chuyện ẩm thực
          </Link>

          <div className="mt-5 flex flex-wrap items-center gap-3">
            <Badge tone="sakura">{article.topic}</Badge>
            <span className="text-sm text-ink-muted">
              {formatDate(article.publishedAt)} · {article.readMinutes} phút đọc
            </span>
          </div>

          <h1 className="mt-4 text-3xl leading-tight font-semibold tracking-tight text-ink sm:text-4xl">
            {article.title}
          </h1>

          <p className="mt-4 text-lg text-ink-muted">{article.excerpt}</p>

          <p className="mt-6 flex items-center gap-2 text-sm text-ink-soft">
            <span className="grid size-9 place-items-center rounded-full bg-shu-500/12 font-jp text-brand">
              人
            </span>
            {article.author}
          </p>
        </div>
      </header>

      <div className="container-page max-w-3xl">
        <SmartImage
          src={article.imageUrl}
          alt={article.title}
          fallback="文"
          loading="eager"
          className="mt-10 aspect-16/9 rounded-card border border-line shadow-soft"
        />

        <div className="mt-10 space-y-5">
          {blocks.map((block, index) =>
            block.kind === 'heading' ? (
              <h2 key={index} className="pt-6 text-2xl font-semibold tracking-tight text-ink">
                {block.text}
              </h2>
            ) : (
              <p key={index} className="text-lg leading-relaxed text-ink-soft">
                {block.text}
              </p>
            ),
          )}
        </div>

        <footer className="mt-12 rounded-card border border-line bg-raised p-6">
          <p className="font-jp text-sm tracking-widest text-brand">お店で</p>
          <h2 className="mt-1 text-lg font-semibold text-ink">Muốn nếm thử ngay tại quán?</h2>
          <p className="mt-2 text-sm text-ink-muted">
            Bếp mở cửa 11:00 – 22:00 mỗi ngày. Đặt bàn trước để ngồi quầy nhìn thẳng vào thớt của itamae.
          </p>
          <div className="mt-4 flex flex-wrap gap-3">
            <Link to="/dat-ban" className="text-sm font-medium text-brand hover:underline">
              Đặt bàn →
            </Link>
            <Link to="/thuc-don" className="text-sm font-medium text-brand hover:underline">
              Xem thực đơn →
            </Link>
          </div>
        </footer>
      </div>
    </article>
  )
}
