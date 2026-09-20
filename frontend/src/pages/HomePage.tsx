import { Link } from 'react-router-dom'
import { api } from '@/lib/api'
import { useAsync } from '@/hooks/useAsync'
import { usePageTitle } from '@/hooks/usePageTitle'
import { formatDate } from '@/lib/format'
import { DishCard } from '@/components/DishCard'
import { Button, LinkButton } from '@/components/ui/Button'
import { DishCardSkeleton, ErrorState } from '@/components/ui/Feedback'
import { SectionHeading } from '@/components/ui/SectionHeading'
import { SmartImage } from '@/components/ui/SmartImage'

const HIGHLIGHTS = [
  {
    kanji: '鮮',
    title: 'Cá về mỗi sáng',
    text: 'Hàng bay từ chợ Toyosu ba chuyến một tuần, phi lê tại bếp lạnh 4°C.',
  },
  {
    kanji: '炭',
    title: 'Than trắng binchotan',
    text: 'Cháy không khói, nhiệt bức xạ mạnh nên mặt thịt se nhanh mà lòng vẫn mọng.',
  },
  {
    kanji: '時',
    title: 'Nước dùng 18 tiếng',
    text: 'Nồi tonkotsu bắc từ 5 giờ sáng, hớt bọt liên tục để nước trong mà vẫn sánh.',
  },
  {
    kanji: '茶',
    title: 'Matcha Uji',
    text: 'Mài bằng cối đá, mở hộp chỉ dùng trong 14 ngày để giữ hương cỏ tươi.',
  },
]

const STATS = [
  { value: '120', label: 'món trong thực đơn' },
  { value: '18h', label: 'ninh một nồi tonkotsu' },
  { value: '4.8★', label: 'điểm trung bình' },
]

export function HomePage() {
  usePageTitle('Ẩm thực Nhật Bản giữa lòng Sài Gòn')

  const categories = useAsync((signal) => api.getCategories(signal), [])
  const featured = useAsync((signal) => api.getFeaturedDishes(8, signal), [])
  const posts = useAsync((signal) => api.getLatestPosts(3, signal), [])

  return (
    <>
      <Hero />

      <section className="container-page py-16 sm:py-20">
        <SectionHeading
          eyebrow="こだわり"
          title="Bốn điều chúng tôi không đổi"
          description="Thực đơn thay theo mùa, nhưng cách làm thì giữ nguyên từ ngày mở quán."
        />

        <div className="mt-10 grid gap-5 sm:grid-cols-2 lg:grid-cols-4">
          {HIGHLIGHTS.map((item) => (
            <div
              key={item.kanji}
              className="rounded-card border border-line bg-raised p-6 transition hover:border-brand/40 hover:shadow-soft"
            >
              <span className="grid size-12 place-items-center rounded-full bg-shu-500/10 font-jp text-2xl text-brand">
                {item.kanji}
              </span>
              <h3 className="mt-4 font-semibold text-ink">{item.title}</h3>
              <p className="mt-2 text-sm text-ink-muted">{item.text}</p>
            </div>
          ))}
        </div>
      </section>

      <section className="border-y border-line bg-sunken py-16 sm:py-20">
        <div className="container-page">
          <SectionHeading
            eyebrow="メニュー"
            title="Bảy nhóm món"
            description="Từ nigiri nắm tay tới nồi ramen bốc khói và ly matcha đánh tay."
            action={
              <LinkButton to="/thuc-don" variant="secondary" size="sm">
                Xem toàn bộ thực đơn →
              </LinkButton>
            }
          />

          {categories.error ? (
            <ErrorState
              className="mt-10"
              description={categories.error.message}
              onRetry={categories.reload}
            />
          ) : (
            <div className="mt-10 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
              {categories.isLoading
                ? Array.from({ length: 7 }, (_, index) => (
                    <div key={index} className="aspect-5/4 animate-pulse rounded-card bg-line-soft" />
                  ))
                : categories.data?.map((category) => (
                    <Link
                      key={category.slug}
                      to={`/thuc-don?category=${category.slug}`}
                      className="group relative overflow-hidden rounded-card border border-line"
                    >
                      <SmartImage
                        src={category.imageUrl}
                        alt={category.name}
                        fallback={category.kanji}
                        className="aspect-5/4"
                        imgClassName="transition-transform duration-500 group-hover:scale-110"
                      />
                      <div className="absolute inset-0 bg-linear-to-t from-black/80 via-black/25 to-transparent" />

                      <div className="absolute inset-x-0 bottom-0 p-5">
                        <p className="font-jp text-xs text-white/70">{category.nameJp}</p>
                        <h3 className="mt-0.5 text-lg font-semibold text-white">{category.name}</h3>
                        <p className="mt-1 line-clamp-2 text-xs text-white/70">{category.description}</p>
                        <span
                          className="mt-3 inline-block rounded-full px-2.5 py-0.5 text-[11px] font-medium text-white"
                          style={{ backgroundColor: category.accentColor }}
                        >
                          {category.dishCount} món
                        </span>
                      </div>
                    </Link>
                  ))}
            </div>
          )}
        </div>
      </section>

      <section className="container-page py-16 sm:py-20">
        <SectionHeading
          eyebrow="おすすめ"
          title="Món quán gợi ý"
          description="Những món khách gọi lại nhiều nhất trong ba tháng gần đây."
          action={
            <LinkButton to="/thuc-don?sort=popular" variant="secondary" size="sm">
              Xem món bán chạy →
            </LinkButton>
          }
        />

        {featured.error ? (
          <ErrorState className="mt-10" description={featured.error.message} onRetry={featured.reload} />
        ) : (
          <div className="mt-10 grid gap-5 sm:grid-cols-2 lg:grid-cols-4">
            {featured.isLoading
              ? Array.from({ length: 8 }, (_, index) => <DishCardSkeleton key={index} />)
              : featured.data?.map((dish) => <DishCard key={dish.id} dish={dish} />)}
          </div>
        )}
      </section>

      <ReservationBanner />

      <section className="container-page py-16 sm:py-20">
        <SectionHeading
          eyebrow="読み物"
          title="Câu chuyện ẩm thực"
          description="Ghi chép từ bếp: vì sao cơm quan trọng hơn cá, và 18 tiếng trong một nồi nước dùng."
          action={
            <LinkButton to="/cau-chuyen" variant="secondary" size="sm">
              Đọc tất cả →
            </LinkButton>
          }
        />

        {posts.error ? (
          <ErrorState className="mt-10" description={posts.error.message} onRetry={posts.reload} />
        ) : (
          <div className="mt-10 grid gap-6 md:grid-cols-3">
            {posts.isLoading
              ? Array.from({ length: 3 }, (_, index) => (
                  <div key={index} className="h-80 animate-pulse rounded-card bg-sunken" />
                ))
              : posts.data?.map((post) => (
                  <article
                    key={post.slug}
                    className="group overflow-hidden rounded-card border border-line bg-raised transition hover:-translate-y-1 hover:shadow-lift"
                  >
                    <Link to={`/cau-chuyen/${post.slug}`}>
                      <SmartImage
                        src={post.imageUrl}
                        alt={post.title}
                        fallback="文"
                        className="aspect-16/10"
                        imgClassName="transition-transform duration-500 group-hover:scale-105"
                      />
                      <div className="p-5">
                        <p className="text-xs text-ink-muted">
                          {post.topic} · {formatDate(post.publishedAt)} · {post.readMinutes} phút đọc
                        </p>
                        <h3 className="mt-2 text-lg leading-snug font-semibold text-ink group-hover:text-brand">
                          {post.title}
                        </h3>
                        <p className="mt-2 line-clamp-3 text-sm text-ink-muted">{post.excerpt}</p>
                      </div>
                    </Link>
                  </article>
                ))}
          </div>
        )}
      </section>
    </>
  )
}

function Hero() {
  return (
    <section className="relative overflow-hidden border-b border-line">
      <div className="absolute inset-0 bg-linear-to-br from-sakura-100 via-surface to-sunken dark:from-raised dark:via-surface dark:to-sunken" />
      <div className="absolute inset-0 bg-asanoha opacity-40" />

      <div className="container-page relative grid items-center gap-12 py-16 lg:grid-cols-2 lg:py-24">
        <div className="animate-fade-up">
          <p className="font-jp text-sm tracking-[0.35em] text-brand">さくら亭 · SAKURA TEI</p>

          <h1 className="mt-4 text-4xl leading-[1.1] font-semibold tracking-tight text-ink sm:text-5xl lg:text-6xl">
            Ẩm thực Nhật Bản,
            <br />
            nấu chậm và tử tế
          </h1>

          <p className="mt-5 max-w-lg text-lg text-ink-muted">
            120 món sushi, sashimi, ramen, udon, BBQ than hoa, tráng miệng và trà Nhật. Đặt giao tận nơi
            trong 45 phút, hoặc giữ một chỗ bên quầy bếp mở.
          </p>

          <div className="mt-8 flex flex-wrap gap-3">
            <LinkButton to="/thuc-don" size="lg">
              Xem thực đơn
            </LinkButton>
            <LinkButton to="/dat-ban" variant="secondary" size="lg">
              Đặt bàn
            </LinkButton>
          </div>

          <dl className="mt-12 grid max-w-md grid-cols-3 gap-6 border-t border-line pt-6">
            {STATS.map((stat) => (
              <div key={stat.label}>
                <dt className="text-2xl font-semibold text-ink">{stat.value}</dt>
                <dd className="mt-1 text-xs text-ink-muted">{stat.label}</dd>
              </div>
            ))}
          </dl>
        </div>

        <div className="relative">
          <SmartImage
            src="https://images.unsplash.com/photo-1553621042-f6e147245754?auto=format&fit=crop&w=1200&q=80"
            alt="Khay sushi tại Sakura Tei"
            fallback="寿"
            loading="eager"
            className="aspect-4/5 rounded-[2rem] border border-line shadow-lift"
          />

          <div className="absolute -bottom-6 -left-4 hidden w-56 rounded-2xl border border-line bg-raised p-4 shadow-lift sm:block">
            <p className="font-jp text-xs text-brand">本日のおすすめ</p>
            <p className="mt-1 text-sm font-semibold text-ink">Omakase 12 miếng</p>
            <p className="mt-1 text-xs text-ink-muted">Itamae chọn theo cá về trong ngày.</p>
          </div>
        </div>
      </div>
    </section>
  )
}

function ReservationBanner() {
  return (
    <section className="border-y border-line bg-ai-800 text-white">
      <div className="container-page flex flex-col items-start justify-between gap-6 py-14 lg:flex-row lg:items-center">
        <div className="max-w-xl">
          <p className="font-jp text-sm tracking-[0.3em] text-sakura-300">ご予約</p>
          <h2 className="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">
            Giữ một chỗ bên quầy bếp mở
          </h2>
          <p className="mt-3 text-white/70">
            Tám ghế counter nhìn thẳng vào thớt của itamae. Quán nhận khách từ 11:00 đến 21:00, mỗi khung
            30 phút.
          </p>
        </div>

        <div className="flex flex-wrap gap-3">
          <LinkButton to="/dat-ban" size="lg" className="bg-white text-ai-800 hover:bg-sakura-100">
            Đặt bàn ngay
          </LinkButton>
          <a href="tel:19006868">
            <Button variant="ghost" size="lg" className="text-white hover:bg-white/10 hover:text-white">
              Gọi 1900 6868
            </Button>
          </a>
        </div>
      </div>
    </section>
  )
}
