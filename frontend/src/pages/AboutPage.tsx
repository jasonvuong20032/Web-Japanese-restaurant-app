import { usePageTitle } from '@/hooks/usePageTitle'
import { LinkButton } from '@/components/ui/Button'
import { PageHeader } from '@/components/ui/PageHeader'
import { SectionHeading } from '@/components/ui/SectionHeading'
import { SmartImage } from '@/components/ui/SmartImage'

const TIMELINE = [
  {
    year: '2016',
    title: 'Một quầy tám ghế',
    text: 'Bếp trưởng Takumi Sato mở quán nhỏ trong hẻm Lê Thánh Tôn, thực đơn vỏn vẹn chín món.',
  },
  {
    year: '2019',
    title: 'Nồi tonkotsu đầu tiên',
    text: 'Quán bắt đầu tự ninh nước dùng thay vì nhập, mỗi mẻ 18 tiếng và có người trực suốt đêm.',
  },
  {
    year: '2022',
    title: 'Chuyển về Lê Lợi',
    text: 'Không gian 120 chỗ với quầy bếp mở, khách ngồi nhìn thẳng vào thớt của itamae.',
  },
  {
    year: '2026',
    title: '250 món trong thực đơn',
    text: 'Mười hai nhóm món, từ nigiri, bò Kobe và hải sản cao cấp tới mochi giã tay và trà Nhật.',
  },
]

const TEAM = [
  {
    name: 'Takumi Sato',
    role: 'Bếp trưởng · Itamae',
    kanji: '佐',
    text: 'Mười tám năm đứng quầy sushi ở Osaka trước khi sang Việt Nam.',
  },
  {
    name: 'Nguyễn Minh Hà',
    role: 'Bếp phó · Phụ trách ramen',
    kanji: '麺',
    text: 'Người giữ lửa nồi tonkotsu và quyết định độ chín của mỗi mẻ mì.',
  },
  {
    name: 'Yui Nakamura',
    role: 'Phụ trách trà & tráng miệng',
    kanji: '茶',
    text: 'Đánh matcha bằng chasen tre, làm purin và mochi theo mẻ nhỏ mỗi ngày.',
  },
]

export function AboutPage() {
  usePageTitle('Về chúng tôi')

  return (
    <>
      <PageHeader
        eyebrow="私たちについて"
        title="Về Sakura Tei"
        description="Chúng tôi nấu theo lối washoku: ít gia vị, tôn vị nguyên liệu, và làm tới nơi tới chốn từng khâu nhỏ."
      />

      <section className="container-page grid items-center gap-10 py-14 lg:grid-cols-2 lg:gap-16">
        <div>
          <SectionHeading
            eyebrow="心得"
            title="Nấu ít thứ, nhưng nấu cho đúng"
            description="Một bát mì ngon không đến từ nguyên liệu đắt tiền, mà từ việc lặp lại cùng một thao tác đủ nhiều lần để không còn sai."
          />

          <div className="mt-6 space-y-4 text-ink-soft">
            <p>
              Sakura Tei bắt đầu từ một quầy tám ghế. Khi đó bếp chỉ làm chín món, và mỗi món đều do
              một người từ đầu tới cuối. Thực đơn hôm nay đã dài hơn nhiều, nhưng nguyên tắc thì
              không đổi: cái gì tự làm được thì tự làm.
            </p>
            <p>
              Cá về mỗi sáng và được phi lê tại chỗ. Nước dùng nấu từ 5 giờ, hớt bọt liên tục. Sợi mì
              cán trong ngày. Sốt tare ủ bảy ngày. Không có bước nào trong số đó nhanh hơn được, và
              chúng tôi cũng không tìm cách làm cho nhanh hơn.
            </p>
          </div>

          <div className="mt-8 flex flex-wrap gap-3">
            <LinkButton to="/dat-ban">Đặt bàn</LinkButton>
            <LinkButton to="/thuc-don" variant="secondary">
              Xem thực đơn
            </LinkButton>
          </div>
        </div>

        <div className="grid gap-4 sm:grid-cols-2">
          <SmartImage
            src="https://images.unsplash.com/photo-1579871494447-9811cf80d66c?auto=format&fit=crop&w=900&q=80"
            alt="Quầy sushi"
            fallback="寿"
            className="aspect-3/4 rounded-card border border-line"
          />
          <SmartImage
            src="https://images.unsplash.com/photo-1569718212165-3a8278d5f624?auto=format&fit=crop&w=900&q=80"
            alt="Tô ramen"
            fallback="麺"
            className="aspect-3/4 rounded-card border border-line sm:mt-10"
          />
        </div>
      </section>

      <section className="border-y border-line bg-sunken py-14">
        <div className="container-page">
          <SectionHeading eyebrow="歩み" title="Mười năm, bốn cột mốc" align="center" />

          <ol className="mt-10 grid gap-6 sm:grid-cols-2 lg:grid-cols-4">
            {TIMELINE.map((entry) => (
              <li key={entry.year} className="rounded-card border border-line bg-raised p-6">
                <p className="font-jp text-2xl font-semibold text-brand">{entry.year}</p>
                <h3 className="mt-2 font-semibold text-ink">{entry.title}</h3>
                <p className="mt-2 text-sm text-ink-muted">{entry.text}</p>
              </li>
            ))}
          </ol>
        </div>
      </section>

      <section className="container-page py-14">
        <SectionHeading eyebrow="料理人" title="Người đứng bếp" align="center" />

        <div className="mt-10 grid gap-6 md:grid-cols-3">
          {TEAM.map((member) => (
            <div key={member.name} className="rounded-card border border-line bg-raised p-6 text-center">
              <span className="mx-auto grid size-16 place-items-center rounded-full bg-shu-500/10 font-jp text-3xl text-brand">
                {member.kanji}
              </span>
              <h3 className="mt-4 font-semibold text-ink">{member.name}</h3>
              <p className="mt-1 text-sm text-brand">{member.role}</p>
              <p className="mt-3 text-sm text-ink-muted">{member.text}</p>
            </div>
          ))}
        </div>
      </section>

      <section className="border-t border-line bg-sunken py-14">
        <div className="container-page grid gap-10 lg:grid-cols-2">
          <div>
            <SectionHeading eyebrow="お問い合わせ" title="Ghé quán hoặc gọi cho chúng tôi" />

            <dl className="mt-6 space-y-4 text-sm">
              <div>
                <dt className="font-medium text-ink">Địa chỉ</dt>
                <dd className="mt-1 text-ink-muted">128 Lê Lợi, Phường Bến Thành, Quận 1, TP. Hồ Chí Minh</dd>
              </div>
              <div>
                <dt className="font-medium text-ink">Hotline</dt>
                <dd className="mt-1">
                  <a href="tel:19006868" className="text-brand hover:underline">
                    1900 6868
                  </a>
                </dd>
              </div>
              <div>
                <dt className="font-medium text-ink">Email</dt>
                <dd className="mt-1">
                  <a href="mailto:xinchao@sakuratei.vn" className="text-brand hover:underline">
                    xinchao@sakuratei.vn
                  </a>
                </dd>
              </div>
              <div>
                <dt className="font-medium text-ink">Giờ mở cửa</dt>
                <dd className="mt-1 text-ink-muted">
                  11:00 – 22:00 mỗi ngày · nhận đặt bàn tới 21:00
                </dd>
              </div>
            </dl>
          </div>

          <div className="rounded-card border border-line bg-raised p-8">
            <p className="font-jp text-sm tracking-widest text-brand">ご予約</p>
            <h3 className="mt-1 text-xl font-semibold text-ink">Đặt trước cho chắc chỗ</h3>
            <p className="mt-3 text-sm text-ink-muted">
              Cuối tuần quán thường kín bàn từ 18:00. Đặt trước qua website để chọn đúng khung giờ bạn
              muốn — hệ thống hiện số chỗ còn lại theo thời gian thực.
            </p>
            <LinkButton to="/dat-ban" className="mt-6">
              Đặt bàn trực tuyến
            </LinkButton>
          </div>
        </div>
      </section>
    </>
  )
}
