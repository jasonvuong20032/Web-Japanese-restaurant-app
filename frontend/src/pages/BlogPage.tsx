import { Link, useSearchParams } from 'react-router-dom'
import { api } from '@/lib/api'
import { cn } from '@/lib/cn'
import { formatDate } from '@/lib/format'
import { useAsync } from '@/hooks/useAsync'
import { usePageTitle } from '@/hooks/usePageTitle'
import { EmptyState, ErrorState, Skeleton } from '@/components/ui/Feedback'
import { PageHeader } from '@/components/ui/PageHeader'
import { Pagination } from '@/components/ui/Pagination'
import { SmartImage } from '@/components/ui/SmartImage'

const PAGE_SIZE = 9

export function BlogPage() {
  usePageTitle('Câu chuyện ẩm thực')

  const [searchParams, setSearchParams] = useSearchParams()
  const topic = searchParams.get('topic') ?? undefined
  const page = Number(searchParams.get('page') ?? '1')

  const topics = useAsync((signal) => api.getPostTopics(signal), [])
  const posts = useAsync(
    (signal) => api.getPosts({ topic, page, pageSize: PAGE_SIZE }, signal),
    [topic, page],
  )

  const selectTopic = (next?: string) => {
    setSearchParams(next ? { topic: next } : {}, { replace: true })
  }

  return (
    <>
      <PageHeader
        eyebrow="読み物"
        title="Câu chuyện ẩm thực"
        description="Ghi chép từ bếp Sakura Tei: nguyên liệu, kỹ thuật và những thói quen nhỏ làm nên một bữa ăn Nhật."
      />

      <div className="container-page py-10 lg:py-14">
        <div className="flex flex-wrap gap-2">
          <TopicChip active={!topic} onClick={() => selectTopic(undefined)}>
            Tất cả
          </TopicChip>
          {topics.data?.map((item) => (
            <TopicChip key={item} active={topic === item} onClick={() => selectTopic(item)}>
              {item}
            </TopicChip>
          ))}
        </div>

        {posts.error ? (
          <ErrorState className="mt-10" description={posts.error.message} onRetry={posts.reload} />
        ) : posts.isLoading ? (
          <div className="mt-10 grid gap-6 md:grid-cols-2 lg:grid-cols-3">
            {Array.from({ length: 6 }, (_, index) => (
              <Skeleton key={index} className="h-80 rounded-card" />
            ))}
          </div>
        ) : posts.data && posts.data.items.length > 0 ? (
          <>
            <div className="mt-10 grid gap-6 md:grid-cols-2 lg:grid-cols-3">
              {posts.data.items.map((post) => (
                <article
                  key={post.slug}
                  className="group flex flex-col overflow-hidden rounded-card border border-line bg-raised transition hover:-translate-y-1 hover:shadow-lift"
                >
                  <Link to={`/cau-chuyen/${post.slug}`} className="flex flex-1 flex-col">
                    <SmartImage
                      src={post.imageUrl}
                      alt={post.title}
                      fallback="文"
                      className="aspect-16/10"
                      imgClassName="transition-transform duration-500 group-hover:scale-105"
                    />

                    <div className="flex flex-1 flex-col p-5">
                      <p className="text-xs tracking-wide text-brand uppercase">{post.topic}</p>
                      <h2 className="mt-2 text-lg leading-snug font-semibold text-ink group-hover:text-brand">
                        {post.title}
                      </h2>
                      <p className="mt-2 line-clamp-3 text-sm text-ink-muted">{post.excerpt}</p>

                      <p className="mt-auto pt-4 text-xs text-ink-muted">
                        {post.author} · {formatDate(post.publishedAt)} · {post.readMinutes} phút đọc
                      </p>
                    </div>
                  </Link>
                </article>
              ))}
            </div>

            <Pagination
              className="mt-12"
              page={posts.data.page}
              totalPages={posts.data.totalPages}
              onChange={(next) => {
                setSearchParams(
                  topic ? { topic, page: String(next) } : { page: String(next) },
                  { replace: true },
                )
                window.scrollTo({ top: 0, behavior: 'smooth' })
              }}
            />
          </>
        ) : (
          <EmptyState
            className="mt-10"
            icon="文"
            title="Chưa có bài viết nào"
            description="Chuyên mục này sẽ sớm có bài mới. Trong lúc chờ, mời bạn đọc các chuyên mục khác."
          />
        )}
      </div>
    </>
  )
}

function TopicChip({
  active,
  onClick,
  children,
}: {
  active: boolean
  onClick: () => void
  children: string
}) {
  return (
    <button
      type="button"
      onClick={onClick}
      aria-pressed={active}
      className={cn(
        'rounded-full border px-4 py-2 text-sm transition',
        active
          ? 'border-brand bg-brand text-on-brand'
          : 'border-line bg-raised text-ink-soft hover:border-brand hover:text-brand',
      )}
    >
      {children}
    </button>
  )
}
