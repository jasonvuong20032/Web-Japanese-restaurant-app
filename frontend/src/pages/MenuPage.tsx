import { useCallback, useEffect, useMemo, useState } from 'react'
import type { ReactNode } from 'react'
import { useSearchParams } from 'react-router-dom'
import type { DishSort } from '@/types/api'
import { api } from '@/lib/api'
import { cn } from '@/lib/cn'
import { formatPrice } from '@/lib/format'
import { useAsync } from '@/hooks/useAsync'
import { useDebounced } from '@/hooks/useDebounced'
import { usePageTitle } from '@/hooks/usePageTitle'
import { DishCard } from '@/components/DishCard'
import { Button } from '@/components/ui/Button'
import { DishCardSkeleton, EmptyState, ErrorState } from '@/components/ui/Feedback'
import { PageHeader } from '@/components/ui/PageHeader'
import { Pagination } from '@/components/ui/Pagination'

const PAGE_SIZE = 12

const SORT_OPTIONS: { value: DishSort; label: string }[] = [
  { value: 'default', label: 'Gợi ý của quán' },
  { value: 'popular', label: 'Bán chạy nhất' },
  { value: 'rating', label: 'Điểm đánh giá cao' },
  { value: 'price_asc', label: 'Giá thấp đến cao' },
  { value: 'price_desc', label: 'Giá cao đến thấp' },
]

/** Mốc lọc giá, đơn vị VND. */
const PRICE_STOPS = [100_000, 200_000, 350_000, 500_000]

const SPICY_OPTIONS = [
  { value: '0', label: 'Không cay' },
  { value: '1', label: 'Tối đa cay nhẹ' },
  { value: '2', label: 'Tối đa cay vừa' },
]

export function MenuPage() {
  const [searchParams, setSearchParams] = useSearchParams()
  const [isFilterOpen, setIsFilterOpen] = useState(false)

  const category = searchParams.get('category') ?? undefined
  const tag = searchParams.get('tag') ?? undefined
  const sort = (searchParams.get('sort') as DishSort | null) ?? 'default'
  const maxSpicy = searchParams.get('maxSpicy') ?? undefined
  const maxPrice = searchParams.get('maxPrice') ?? undefined
  const page = Number(searchParams.get('page') ?? '1')
  const urlKeyword = searchParams.get('q') ?? ''

  // Ô tìm kiếm gõ tới đâu hiện tới đó, nhưng chỉ ghi vào URL sau khi ngừng gõ.
  const [keyword, setKeyword] = useState(urlKeyword)
  const debouncedKeyword = useDebounced(keyword)

  useEffect(() => setKeyword(urlKeyword), [urlKeyword])

  const updateParams = useCallback(
    (changes: Record<string, string | undefined>, options?: { keepPage?: boolean }) => {
      setSearchParams(
        (current) => {
          const next = new URLSearchParams(current)

          for (const [key, value] of Object.entries(changes)) {
            if (value === undefined || value === '') {
              next.delete(key)
            } else {
              next.set(key, value)
            }
          }

          // Đổi bộ lọc thì quay về trang 1, nếu không dễ rơi vào trang trống.
          if (!options?.keepPage) next.delete('page')

          return next
        },
        { replace: true },
      )
    },
    [setSearchParams],
  )

  useEffect(() => {
    if (debouncedKeyword === urlKeyword) return
    updateParams({ q: debouncedKeyword || undefined })
  }, [debouncedKeyword, urlKeyword, updateParams])

  const categories = useAsync((signal) => api.getCategories(signal), [])
  const filters = useAsync((signal) => api.getDishFilters(signal), [])

  const dishes = useAsync(
    (signal) =>
      api.searchDishes(
        {
          category,
          q: urlKeyword || undefined,
          tag,
          sort,
          maxSpicy: maxSpicy === undefined ? undefined : Number(maxSpicy),
          maxPrice: maxPrice === undefined ? undefined : Number(maxPrice),
          page,
          pageSize: PAGE_SIZE,
        },
        signal,
      ),
    [category, urlKeyword, tag, sort, maxSpicy, maxPrice, page],
  )

  const activeCategory = categories.data?.find((item) => item.slug === category)

  usePageTitle(activeCategory ? `Thực đơn · ${activeCategory.name}` : 'Thực đơn')

  const activeFilterCount = [category, tag, maxSpicy, maxPrice, urlKeyword || undefined].filter(
    (value) => value !== undefined,
  ).length

  const priceStops = useMemo(() => {
    const highest = filters.data?.maxPrice
    if (highest === undefined) return []
    // Mốc cố định theo thói quen đọc giá tiền Việt. Không chia đều theo giá cao nhất:
    // thực đơn có vài set lớn tiền triệu, chia đều sẽ ra toàn mốc lọc không bỏ được món nào.
    return PRICE_STOPS.filter((stop) => stop < highest)
  }, [filters.data])

  const totalDishCount = categories.data?.reduce((sum, item) => sum + item.dishCount, 0) ?? 0

  const resetFilters = () => setSearchParams({}, { replace: true })

  const filterPanel = (
    <div className="space-y-7">
      <FilterGroup title="Nhóm món">
        <div className="space-y-1">
          <FilterChip active={!category} onClick={() => updateParams({ category: undefined })}>
            Tất cả
            {totalDishCount > 0 && <span className="text-xs text-ink-muted">{totalDishCount}</span>}
          </FilterChip>

          {categories.data?.map((item) => (
            <FilterChip
              key={item.slug}
              active={category === item.slug}
              onClick={() => updateParams({ category: item.slug })}
            >
              <span className="flex items-center gap-2">
                <span className="font-jp text-ink-muted">{item.kanji}</span>
                {item.name}
              </span>
              <span className="text-xs text-ink-muted">{item.dishCount}</span>
            </FilterChip>
          ))}
        </div>
      </FilterGroup>

      <FilterGroup title="Mức giá tối đa">
        <div className="flex flex-wrap gap-2">
          {priceStops.map((stop) => (
            <PillButton
              key={stop}
              active={maxPrice === String(stop)}
              onClick={() =>
                updateParams({ maxPrice: maxPrice === String(stop) ? undefined : String(stop) })
              }
            >
              ≤ {formatPrice(stop)}
            </PillButton>
          ))}
        </div>
      </FilterGroup>

      <FilterGroup title="Độ cay">
        <div className="flex flex-wrap gap-2">
          {SPICY_OPTIONS.map((option) => (
            <PillButton
              key={option.value}
              active={maxSpicy === option.value}
              onClick={() =>
                updateParams({ maxSpicy: maxSpicy === option.value ? undefined : option.value })
              }
            >
              {option.label}
            </PillButton>
          ))}
        </div>
      </FilterGroup>

      <FilterGroup title="Thẻ món">
        <div className="flex flex-wrap gap-2">
          {filters.data?.tags.map((item) => (
            <PillButton
              key={item}
              active={tag === item}
              onClick={() => updateParams({ tag: tag === item ? undefined : item })}
            >
              {item}
            </PillButton>
          ))}
        </div>
      </FilterGroup>

      {activeFilterCount > 0 && (
        <Button variant="ghost" size="sm" className="w-full" onClick={resetFilters}>
          Xoá tất cả bộ lọc ({activeFilterCount})
        </Button>
      )}
    </div>
  )

  return (
    <>
      <PageHeader
        eyebrow="お品書き"
        title={activeCategory?.name ?? 'Thực đơn'}
        description={
          activeCategory?.description ??
          'Một trăm hai mươi món, chia theo bảy nhóm. Lọc theo nhóm, mức giá, độ cay hoặc gõ thẳng tên món — tìm không dấu cũng ra.'
        }
      >
        <div className="relative max-w-md">
          <input
            type="search"
            value={keyword}
            onChange={(event) => setKeyword(event.target.value)}
            placeholder="Tìm sushi, ramen, matcha…"
            aria-label="Tìm món trong thực đơn"
            className="h-12 w-full rounded-full border border-line bg-raised pr-4 pl-11 text-sm text-ink placeholder:text-ink-muted/70 outline-none transition focus:border-brand"
          />
          <span className="absolute top-1/2 left-4 -translate-y-1/2 text-ink-muted">⌕</span>
        </div>
      </PageHeader>

      <div className="container-page py-10">
        <div className="lg:grid lg:grid-cols-[17rem_1fr] lg:gap-10">
          <aside className="hidden lg:block">
            <div className="sticky top-24 max-h-[calc(100dvh-8rem)] overflow-y-auto pr-2 pb-6">
              {filterPanel}
            </div>
          </aside>

          <div>
            <div className="mb-6 flex flex-wrap items-center justify-between gap-3">
              <p className="text-sm text-ink-muted" aria-live="polite">
                {dishes.isLoading
                  ? 'Đang tải thực đơn…'
                  : `${dishes.data?.totalItems ?? 0} món phù hợp`}
              </p>

              <div className="flex items-center gap-2">
                <Button
                  variant="secondary"
                  size="sm"
                  className="lg:hidden"
                  onClick={() => setIsFilterOpen(true)}
                >
                  Bộ lọc{activeFilterCount > 0 ? ` (${activeFilterCount})` : ''}
                </Button>

                <label className="sr-only" htmlFor="sort-select">
                  Sắp xếp
                </label>
                <select
                  id="sort-select"
                  value={sort}
                  onChange={(event) => updateParams({ sort: event.target.value })}
                  className="h-9 rounded-full border border-line bg-raised px-4 text-sm text-ink outline-none focus:border-brand"
                >
                  {SORT_OPTIONS.map((option) => (
                    <option key={option.value} value={option.value}>
                      {option.label}
                    </option>
                  ))}
                </select>
              </div>
            </div>

            {dishes.error ? (
              <ErrorState description={dishes.error.message} onRetry={dishes.reload} />
            ) : dishes.isLoading ? (
              <div className="grid gap-5 sm:grid-cols-2 xl:grid-cols-3">
                {Array.from({ length: PAGE_SIZE }, (_, index) => (
                  <DishCardSkeleton key={index} />
                ))}
              </div>
            ) : dishes.data && dishes.data.items.length > 0 ? (
              <>
                <div className="grid gap-5 sm:grid-cols-2 xl:grid-cols-3">
                  {dishes.data.items.map((dish) => (
                    <DishCard key={dish.id} dish={dish} />
                  ))}
                </div>

                <Pagination
                  className="mt-12"
                  page={dishes.data.page}
                  totalPages={dishes.data.totalPages}
                  onChange={(next) => {
                    updateParams({ page: String(next) }, { keepPage: true })
                    window.scrollTo({ top: 0, behavior: 'smooth' })
                  }}
                />
              </>
            ) : (
              <EmptyState
                title="Không tìm thấy món nào"
                description="Thử bỏ bớt một vài bộ lọc, hoặc tìm bằng tên romaji như “tonkotsu”, “matcha”."
                action={
                  <Button variant="outline" onClick={resetFilters}>
                    Xoá bộ lọc
                  </Button>
                }
              />
            )}
          </div>
        </div>
      </div>

      {isFilterOpen && (
        <div className="fixed inset-0 z-50 flex lg:hidden" role="dialog" aria-modal="true" aria-label="Bộ lọc">
          <button
            type="button"
            className="absolute inset-0 bg-black/45 backdrop-blur-sm"
            onClick={() => setIsFilterOpen(false)}
            aria-label="Đóng bộ lọc"
          />
          <aside className="relative flex h-full w-full max-w-xs flex-col bg-surface shadow-lift">
            <header className="flex items-center justify-between border-b border-line px-5 py-4">
              <h2 className="font-semibold text-ink">Bộ lọc</h2>
              <button
                type="button"
                onClick={() => setIsFilterOpen(false)}
                className="grid size-9 place-items-center rounded-full text-xl text-ink-muted hover:bg-sunken"
                aria-label="Đóng"
              >
                ×
              </button>
            </header>
            <div className="flex-1 overflow-y-auto px-5 py-5">{filterPanel}</div>
            <footer className="border-t border-line p-4">
              <Button className="w-full" onClick={() => setIsFilterOpen(false)}>
                Xem {dishes.data?.totalItems ?? 0} món
              </Button>
            </footer>
          </aside>
        </div>
      )}
    </>
  )
}

function FilterGroup({ title, children }: { title: string; children: ReactNode }) {
  return (
    <section>
      <h3 className="mb-3 text-xs font-semibold tracking-wider text-ink-muted uppercase">{title}</h3>
      {children}
    </section>
  )
}

function FilterChip({
  active,
  onClick,
  children,
}: {
  active: boolean
  onClick: () => void
  children: ReactNode
}) {
  return (
    <button
      type="button"
      onClick={onClick}
      aria-pressed={active}
      className={cn(
        'flex w-full items-center justify-between rounded-lg px-3 py-2 text-sm transition',
        active ? 'bg-shu-500/12 font-medium text-brand' : 'text-ink-soft hover:bg-sunken',
      )}
    >
      {children}
    </button>
  )
}

function PillButton({
  active,
  onClick,
  children,
}: {
  active: boolean
  onClick: () => void
  children: ReactNode
}) {
  return (
    <button
      type="button"
      onClick={onClick}
      aria-pressed={active}
      className={cn(
        'rounded-full border px-3 py-1.5 text-xs transition',
        active
          ? 'border-brand bg-brand text-on-brand'
          : 'border-line text-ink-soft hover:border-brand hover:text-brand',
      )}
    >
      {children}
    </button>
  )
}
