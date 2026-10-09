# DESIGN — Nhận diện thương hiệu và hệ thống thiết kế Sakura Tei

Tài liệu này chốt *website trông như thế nào*: logo, màu, chữ, khoảng cách, thành phần giao diện
và bố cục từng trang. Phần *làm gì* nằm trong [PRD.md](PRD.md), phần *xây thế nào* nằm trong
[PLANNING.md](PLANNING.md).

**Nguồn sự thật trong mã:** token màu, font, bo góc và đổ bóng khai báo ở
[`frontend/src/index.css`](../frontend/src/index.css); thành phần dùng chung ở
[`frontend/src/components/ui/`](../frontend/src/components/ui/). Khi tài liệu và mã khác nhau thì
**mã đúng**; sửa tài liệu cho khớp.

---

## 1. Tính cách thương hiệu

Sakura Tei nấu theo lối **washoku**: ít gia vị, tôn vị nguyên liệu, làm kỹ từng khâu nhỏ. Giao diện
phản chiếu đúng tinh thần đó.

| Từ khoá | Thể hiện trên giao diện |
| --- | --- |
| **Tĩnh** | Nền kem ấm, nhiều khoảng trắng, mỗi khối chỉ một ý chính |
| **Mộc** | Bảng màu lấy từ vật liệu truyền thống: son đỏ, hồng anh đào, xanh matcha, xanh chàm |
| **Chỉn chu** | Lưới căn đều, bo góc thống nhất, mọi trạng thái tải/lỗi/rỗng đều được thiết kế |

### Giọng văn

- **Câu ngắn, cụ thể, có số liệu.** Viết "Nước dùng ninh 18 tiếng", không viết "Nước dùng đậm đà
  thơm ngon khó cưỡng".
- **Xưng "chúng tôi", gọi khách là "bạn".**
- **Chữ Nhật để trang trí, không để truyền đạt.** Mỗi tiêu đề có một dòng nhỏ tiếng Nhật phía trên
  (ví dụ メニュー trên "Mười hai nhóm món"); nội dung chính luôn là tiếng Việt.
- **Thông báo lỗi nói rõ cần làm gì.** "Số điện thoại cần 10 chữ số, bắt đầu bằng 0", không viết
  "Dữ liệu không hợp lệ".

---

## 2. Logo

### 2.1 Thành phần

Logo gồm **biểu tượng** và **chữ** (wordmark), đặt cạnh nhau theo chiều ngang:

```
  ┌──────┐
  │  ✿   │  Sakura Tei          ← Be Vietnam Pro, semibold, ink
  │      │  さ く ら 亭          ← Noto Serif JP, giãn chữ 0.25em, ink-muted
  └──────┘
  biểu tượng   khoảng cách 10px
```

<img src="../frontend/public/sakura.svg" alt="Biểu tượng Sakura Tei" width="96" />

### 2.2 Cấu tạo biểu tượng

File: [`frontend/public/sakura.svg`](../frontend/public/sakura.svg), khung 64×64.

| Phần | Cấu tạo | Màu |
| --- | --- | --- |
| 5 cánh hoa | Một cánh vẽ bằng đường cong Bézier, nhân bản bằng cách xoay 72° quanh tâm | Hồng anh đào `#E2899A` (`sakura-400`) |
| Nhị hoa | Hình tròn bán kính 4,5 ở tâm | Son đỏ `#C6402F` (`shu-500`, màu chủ đạo) |

Ý nghĩa: hoa anh đào (桜, *sakura*) là biểu tượng quen thuộc nhất của Nhật Bản và là nửa đầu của
tên quán. Màu son đỏ ở nhị lấy từ cổng torii và con dấu hanko, đồng thời là màu chủ đạo của toàn bộ
website.

### 2.3 Các phiên bản

| Phiên bản | Dùng khi | Ví dụ |
| --- | --- | --- |
| **Ngang** (biểu tượng + chữ) | Mặc định | Header, Footer |
| **Biểu tượng đơn** | Không đủ chỗ cho chữ | Favicon trên tab trình duyệt |

### 2.4 Kích thước và vùng an toàn

| Quy tắc | Giá trị |
| --- | --- |
| Biểu tượng nhỏ nhất | 16px (favicon). Trong trang không nhỏ hơn 24px |
| Biểu tượng ở Header / Footer | 32px / 36px |
| Khoảng cách biểu tượng ↔ chữ | 10px (`gap-2.5`) |
| Vùng an toàn xung quanh | Tối thiểu bằng ¼ chiều cao biểu tượng, không đặt chữ hay viền khác vào |

### 2.5 Trên nền tối

Biểu tượng **giữ nguyên màu**: cánh hồng trên nền `#14110F` có tương phản 7,4:1, rõ hơn cả trên nền
sáng. Phần chữ dùng token `ink` / `ink-muted` nên tự đổi sang màu sáng, không cần file logo riêng
cho chế độ tối.

Trên nền sáng, cánh hồng chỉ đạt 2,4:1 với nền kem. Logo được miễn yêu cầu tương phản của WCAG,
nhưng vì lý do này **luôn đặt biểu tượng cạnh chữ** khi nhỏ hơn 24px trong trang.

### 2.6 Không được

- Đổi màu cánh hoa hoặc nhị hoa, hay tô cả biểu tượng một màu.
- Kéo giãn, bóp méo, xoay biểu tượng.
- Thêm bóng đổ, viền, hiệu ứng phát sáng.
- Đặt trên ảnh nhiều chi tiết mà không có nền đệm.
- Thay biểu tượng bằng emoji 🌸 hay ảnh hoa anh đào khác.
- Đổi font của chữ "Sakura Tei" hoặc viết thành "SakuraTei", "Sakura-Tei".

### 2.7 Nơi sử dụng

Cả ba vị trí đều tham chiếu **cùng một file** `/sakura.svg`, không có bản sao nào khác. Phiên bản
ngang (biểu tượng + chữ) chỉ được vẽ ở **một component**:
[`BrandLogo.tsx`](../frontend/src/components/layout/BrandLogo.tsx). Cần logo ở chỗ mới thì dùng lại
component này, đừng tự ghép `<img>` với chữ.

| Vị trí | File | Ghi chú |
| --- | --- | --- |
| Favicon | [`index.html`](../frontend/index.html) | `<link rel="icon" type="image/svg+xml">` |
| Header | [`Header.tsx`](../frontend/src/components/layout/Header.tsx) → `<BrandLogo />` (cỡ `md`) | Bấm vào về Trang chủ; `aria-label="Sakura Tei — trang chủ"` |
| Footer | [`Footer.tsx`](../frontend/src/components/layout/Footer.tsx) → `<BrandLogo size="lg" />` | Kèm mô tả ngắn và thông tin liên hệ |

Tên quán và tên tiếng Nhật trong logo lấy từ `BRAND` ([`lib/brand.ts`](../frontend/src/lib/brand.ts)),
cùng chỗ với địa chỉ, hotline, giờ mở cửa và URL bản đồ.

Ảnh biểu tượng có `alt=""` vì chữ "Sakura Tei" ngay bên cạnh đã mang nghĩa. Nếu không, trình đọc
màn hình sẽ đọc tên quán hai lần.

---

## 3. Màu sắc

### 3.1 Bảng màu thương hiệu

Đặt tên theo màu truyền thống Nhật. Các giá trị này **cố định**, không đổi theo chế độ sáng/tối.

| Họ màu | Tên gốc | Mã chính | Dùng cho |
| --- | --- | --- | --- |
| `shu` | 朱 — son đỏ | `#C6402F` (500) | Màu chủ đạo: nút chính, liên kết, giá tiền, nhị hoa logo |
| `sakura` | 桜 — hồng anh đào | `#E2899A` (400) | Cánh hoa logo, màu bôi đen chữ, badge thẻ món và chuyên mục bài viết |
| `matcha` | 抹茶 — xanh trà | `#6F8F4F` (500) | Thông báo tích cực: được miễn phí giao, đặt bàn thành công |
| `ai` | 藍 — xanh chàm | `#1C2842` (800) | Dải mời đặt bàn ở Trang chủ |
| `gold` | 金 — vàng | `#B8893B` (500) | Sao đánh giá, badge "Nổi bật" |

Thang đầy đủ `shu-50` … `shu-900` và các họ còn lại nằm ở đầu `index.css`.

### 3.2 Token ngữ nghĩa

Giao diện **không dùng mã màu trực tiếp** mà dùng token theo vai trò. Mỗi token có hai giá trị; khi
`<html>` mang class `.dark` thì giá trị tối được áp dụng.

| Token | Vai trò | Sáng | Tối |
| --- | --- | --- | --- |
| `surface` | Nền trang | `#FBF7F0` kem | `#14110F` |
| `raised` | Nền thẻ, ô nhập | `#FFFFFF` | `#1E1A17` |
| `sunken` | Nền dải section, khối phụ | `#F3EBDF` | `#0D0B0A` |
| `line` | Viền | `#E5D9C7` | `#342C26` |
| `ink` | Chữ chính | `#241F1C` | `#F2EAE0` |
| `ink-soft` | Chữ phụ | `#52483F` | `#CABDB0` |
| `ink-muted` | Chữ mờ, chú thích | `#7A6E63` | `#9B8F83` |
| `brand` | Màu nhấn | `#C6402F` | `#E4604C` (sáng hơn cho nền tối) |
| `brand-hover` | Màu nhấn khi rê chuột | `#A93424` | `#EE9A89` |
| `on-brand` | Chữ trên nền màu nhấn | `#FFF8F4` | `#1A0F0C` |

### 3.3 Độ tương phản

Tính theo công thức WCAG 2.1. Mức AA cho chữ thường là 4,5:1.

| Cặp màu | Sáng | Tối | Đạt |
| --- | --- | --- | --- |
| `ink` trên `surface` | 15,3 | 15,8 | AAA |
| `ink-soft` trên `surface` | 8,3 | 10,2 | AAA |
| `ink-muted` trên `surface` | 4,6 | 6,0 | AA |
| `brand` trên `surface` | 4,7 | 5,4 | AA |
| `on-brand` trên `brand` (chữ trên nút chính) | 4,8 | 5,4 | AA |

`ink-muted` và `brand` chỉ vừa qua mức AA ở chế độ sáng. **Không làm nhạt thêm** hai màu này, và
không dùng `ink-muted` cho chữ nhỏ hơn 12px.

### 3.4 Màu theo nhóm món

Mỗi nhóm món có một `accentColor` riêng (khai báo trong `Data/SeedCategories.cs`), dùng làm nền cho
nhãn "N món" trên thẻ nhóm ở Trang chủ, chữ trên nhãn màu trắng. Màu nhóm chỉ dùng làm **nền**, không
dùng cho chữ, vì không phải màu nào cũng đủ tương phản trên nền kem.

---

## 4. Chữ

| Font | Vai trò | Lý do chọn |
| --- | --- | --- |
| **Be Vietnam Pro** (300–700) | Toàn bộ chữ giao diện | Thiết kế cho tiếng Việt, dấu đặt đúng vị trí, không chồng lên nhau ở cỡ nhỏ |
| **Noto Serif JP** (400–700) | Dòng tiếng Nhật trang trí, năm trong dòng thời gian, chữ kanji dự phòng | Nét serif có chân gợi cảm giác thư pháp; đủ bộ kanji |

Cả hai tải từ Google Fonts với `display=swap`, nên chữ hiện ngay bằng font hệ thống rồi mới đổi.

### Thang cỡ chữ

| Cấp | Cỡ (di động → máy tính) | Độ đậm | Dùng ở |
| --- | --- | --- | --- |
| Hero | 36 → 48 → 60px | 600 | Tiêu đề Trang chủ |
| Tiêu đề trang (`h1`) | 36 → 48px | 600 | `PageHeader` mọi trang |
| Tiêu đề món (`h1`) | 30 → 36px | 600 | Trang chi tiết món |
| Tiêu đề khối (`h2`) | 30 → 36px | 600 | `SectionHeading` |
| Dòng tiếng Nhật trang trí | 14px, giãn chữ 0.3em | 400 | Phía trên mọi `h1`, `h2` |
| Chữ thường | 16px | 400 | Đoạn văn |
| Chữ nhỏ | 14px | 400–500 | Nút, nhãn, mô tả thẻ |
| Chú thích | 12px | 500 | Badge, ghi công ảnh |

Giá tiền và số lượng dùng `tabular-nums` để các chữ số thẳng cột khi thay đổi.

---

## 5. Bố cục, khoảng cách, hình khối

| Thuộc tính | Giá trị |
| --- | --- |
| Khung nội dung (`container-page`) | Rộng tối đa 1280px, lề trái phải 16px → 24px (≥ 640px) → 32px (≥ 1024px) |
| Mốc màn hình | `sm` 640px · `md` 768px · `lg` 1024px · `xl` 1280px |
| Khoảng cách giữa các khối | 56–80px theo chiều dọc |
| Bo góc thẻ (`rounded-card`) | 16px |
| Bo góc nút, badge, ô tìm kiếm | Tròn hẳn (`rounded-full`) |
| Đổ bóng `soft` | Thẻ ở trạng thái thường, nút chính |
| Đổ bóng `lift` | Thẻ khi rê chuột, ngăn kéo giỏ hàng |
| Hoa văn nền `bg-asanoha` | Lưới chấm nhạt 22px, dùng sau tiêu đề trang |

---

## 6. Thành phần giao diện

Mọi trang ghép từ các thành phần dưới đây. Tạo trang mới thì **dùng lại**, không tự viết nút hay
thẻ riêng.

| Thành phần | File | Biến thể / ghi chú |
| --- | --- | --- |
| `Button`, `LinkButton`, `AnchorButton` | `ui/Button.tsx` | `primary` (nền son đỏ) · `secondary` (nền trắng, viền) · `outline` · `ghost`; cỡ `sm` 36px · `md` 44px · `lg` 52px. `AnchorButton` cho liên kết `tel:`, `mailto:`, Google Maps — **đừng lồng `<Button>` trong `<a>`** |
| `ConfirmAction` | `ui/ConfirmAction.tsx` | Nút cho thao tác không hoàn tác được (huỷ đơn, huỷ bàn): bấm lần đầu hiện câu hỏi tại chỗ trên nền son nhạt, bấm xác nhận mới gọi API; lỗi hiện ngay dưới câu hỏi |
| `BrandLogo` | `layout/BrandLogo.tsx` | Logo ngang, cỡ `md` (Header) và `lg` (Footer) — xem mục 2 |
| `Badge` | `ui/Badge.tsx` | Tông `neutral` · `brand` · `sakura` · `matcha` · `gold` · `muted` |
| `PageHeader` | `ui/PageHeader.tsx` | Dòng Nhật + `h1` + mô tả, nền `sunken` có hoa văn |
| `SectionHeading` | `ui/SectionHeading.tsx` | Dòng Nhật + `h2` + mô tả + nút hành động bên phải; căn trái hoặc giữa |
| `TextField`, `TextAreaField`, `SelectField` | `ui/Field.tsx` | Nhãn, gợi ý, lỗi; tự gắn `aria-invalid` và `aria-describedby` |
| `QuantityStepper` | `ui/Meters.tsx` | Nút − / số / + với giới hạn 1–50 |
| `Rating`, `SpicyMeter` | `ui/Meters.tsx` | Sao đánh giá, độ cay 0–3 |
| `Skeleton`, `DishCardSkeleton`, `Spinner` | `ui/Feedback.tsx` | Khung xương đúng tỉ lệ thẻ thật |
| `ErrorState`, `EmptyState` | `ui/Feedback.tsx` | Thông báo kèm nút thử lại / nút hành động |
| `Pagination` | `ui/Pagination.tsx` | `aria-current="page"` cho trang hiện tại |
| `SmartImage` | `ui/SmartImage.tsx` | Ảnh lỗi thì hiện chữ kanji của nhóm món |
| `DishCard` | `DishCard.tsx` | Ảnh 500px, tên, giá, badge, nút thêm nhanh vào giỏ |
| `DishGallery` | `DishGallery.tsx` | Ảnh lớn + dải ảnh nhỏ + ghi công tác giả |
| `CartDrawer` | `CartDrawer.tsx` | Ngăn kéo bên phải, khoá cuộn trang nền khi mở |
| `ReservationForm` | `ReservationForm.tsx` | Form chọn ngày → khung giờ → thông tin, dùng chung cho đặt bàn và đổi lịch. Cột form có `min-w-0` để dải 14 ngày cuộn ngang thay vì đẩy phần tóm tắt ra khỏi màn hình |

---

## 7. Bố cục các trang chính

### 7.1 Trang Chủ — `/`

```
┌────────────────────────────────────────────────────────────────┐
│ [logo] Sakura Tei   Trang chủ Thực đơn Đặt bàn …   [Tìm…] ☾ 🛒 │ Header cố định
├────────────────────────────────────────────────────────────────┤
│ さくら亭 · SAKURA TEI                         ┌──────────────┐ │
│ Tiêu đề lớn (hero)                            │   ảnh món    │ │ Hero
│ Mô tả ngắn                                    │ ┌──────────┐ │ │
│ [Xem thực đơn]  [Đặt bàn]                     │ │ Omakase  │ │ │
│ 250 món · 18h · 4.8★                          └─┴──────────┴─┘ │
├────────────────────────────────────────────────────────────────┤
│ こだわり  Bốn điều chúng tôi không đổi    [ ][ ][ ][ ]          │
├────────────────────────────────────────────────────────────────┤
│ メニュー  Mười hai nhóm món        [Xem toàn bộ thực đơn →]    │ nền sunken
│ [nhóm][nhóm][nhóm][nhóm][nhóm][nhóm] …                          │
├────────────────────────────────────────────────────────────────┤
│ おすすめ  Món quán gợi ý                                       │
│ [món][món][món][món]                                            │ 8 DishCard
│ [món][món][món][món]                                            │
├────────────────────────────────────────────────────────────────┤
│ ご予約  Lời mời đặt bàn                         [Đặt bàn]       │ nền xanh chàm
├────────────────────────────────────────────────────────────────┤
│ 読み物  Câu chuyện ẩm thực        [bài][bài][bài]               │
├────────────────────────────────────────────────────────────────┤
│ Footer: logo · mô tả · liên hệ │ Khám phá │ Dịch vụ             │
└────────────────────────────────────────────────────────────────┘
```

### 7.2 Về Chúng Tôi — `/ve-chung-toi`

```
┌────────────────────────────────────────────────────────────────┐
│ 私たちについて  Về Sakura Tei  ·  mô tả                         │ PageHeader
├───────────────────────────────┬────────────────────────────────┤
│ 心得  Nấu ít thứ, nhưng nấu    │  [ảnh quầy sushi] [ảnh ramen]  │
│ cho đúng · 2 đoạn văn          │                                │
│ [Đặt bàn] [Xem thực đơn]       │                                │
├───────────────────────────────┴────────────────────────────────┤
│ 歩み  Mười năm, bốn cột mốc   [2016][2019][2022][2026]           │ nền sunken
├────────────────────────────────────────────────────────────────┤
│ 料理人  Người đứng bếp         [佐 bếp trưởng][麺][茶]           │
├───────────────────────────────┬────────────────────────────────┤
│ お問い合わせ  Liên hệ           │  ご予約  Đặt trước cho chắc chỗ │ nền sunken
│ Địa chỉ · Hotline · Email      │  [Đặt bàn trực tuyến]          │
│ Giờ mở cửa                     │                                │
├───────────────────────────────┴────────────────────────────────┤
│ 地図  Đường tới quán · địa chỉ        [Mở trong Google Maps ↗]  │
│ ┌────────────────────────────────────────────────────────────┐ │
│ │   Bản đồ Google Maps (iframe) — 4:3 → 16:9 → 21:9           │ │
│ └────────────────────────────────────────────────────────────┘ │
└────────────────────────────────────────────────────────────────┘
```

Bản đồ nhúng bằng `<iframe>` với URL dạng `output=embed`, không cần API key. Khung bản đồ đổi tỉ lệ
theo màn hình (4:3 trên điện thoại, 16:9 trên máy tính bảng, 21:9 trên máy tính) để không quá thấp
trên điện thoại, cũng không chiếm hết màn hình trên máy tính. `loading="lazy"` để bản đồ chỉ tải khi
khách cuộn tới, và luôn có `title` mô tả cho trình đọc màn hình.

### 7.2b Quản lý lượt đặt bàn — `/dat-ban/:code`

```
┌────────────────────────────────────────────────────────────────┐
│ ご予約の確認  Lượt đặt RS-XXXXXX  ·  19:30 · 12/10 · 4 người      │ PageHeader
├───────────────────────────────────┬────────────────────────────┤
│ Thông tin đặt bàn                  │ Thay đổi lượt đặt          │
│ Người đặt · Điện thoại · Email     │ [Đổi ngày, giờ, số khách]  │ nút chính
│ Ngày · Giờ · Số khách · Ghi chú    │ [Huỷ đặt bàn]              │ outline → hỏi lại
│ Đặt lúc · Đổi lịch lúc             │ [Xem trước thực đơn]       │
└───────────────────────────────────┴────────────────────────────┘
```

Đổi lịch mở `/dat-ban/:code/doi-lich` — cùng `ReservationForm` của trang đặt bàn, điền sẵn dữ liệu
cũ, thêm nút "Thôi, giữ lịch cũ". Lưu xong quay về trang xem kèm thông báo xanh matcha.

### 7.3 Chi tiết món — `/mon-an/:slug`

```
┌────────────────────────────────────────────────────────────────┐
│ Trang chủ › Thực đơn › Ramen › Tonkotsu Ramen                   │ breadcrumb
├───────────────────────────────┬────────────────────────────────┤
│ ┌───────────────────────────┐ │ ラーメン · Tonkotsu ramen       │
│ │                           │ │ Tên món (h1)                   │
│ │       ảnh lớn 960px       │ │ ★ 4.8 (98)   [thẻ][thẻ]         │
│ │                           │ │ Giá bán  (giá gốc gạch ngang)  │
│ └───────────────────────────┘ │ Nhóm · Độ cay · Calo · Thời gian│
│ [ảnh nhỏ][ảnh nhỏ][ảnh nhỏ]   │ [ − 1 + ] [Thêm vào giỏ · …]   │
│ Ảnh: tác giả · CC BY-SA        │                                │
├───────────────────────────────┴────────────────────────────────┤
│ Về món này (mô tả dài)          │ 材料  Nguyên liệu chính         │
├────────────────────────────────────────────────────────────────┤
│ Món khác trong nhóm Ramen       [món][món][món][món]            │
└────────────────────────────────────────────────────────────────┘
```

Trên điện thoại, các khối hai cột xếp thành một cột: ảnh lên trên, thông tin và nút đặt xuống dưới.

---

## 8. Hình ảnh

| Loại ảnh | Nguồn | Quy tắc |
| --- | --- | --- |
| Ảnh món (250 món) | Wikimedia Commons | Mỗi món 1–3 ảnh riêng, không món nào dùng chung ảnh; **bắt buộc ghi tác giả và giấy phép** dưới bộ ảnh |
| Ảnh nhóm món, bài viết, trang giới thiệu | Unsplash | Cắt theo khung bằng tham số URL |
| Ảnh dự phòng | Chữ kanji của nhóm (寿 sushi, 刺 sashimi, 麺 ramen, 牛 wagyu, 茶 thức uống…) | Hiện khi ảnh không tải được |

Ảnh thẻ dùng bản 500px, ảnh lớn ở trang chi tiết dùng bản 960px. Mọi ảnh nội dung có `alt` mô tả
món; ảnh trang trí để `alt=""`.

## 9. Chuyển động

| Hiệu ứng | Thông số | Dùng ở |
| --- | --- | --- |
| `fade-up` | 0,5s, trượt lên 12px | Phần chữ của Hero khi trang vừa tải |
| Chuyển màu | `transition` mặc định ~150ms | Rê chuột trên nút, liên kết, thẻ |
| Nhấn nút | Dịch xuống 1px | Mọi `Button` |
| Header | Nền mờ (`backdrop-blur`) và viền hiện ra khi cuộn quá 8px | Header |

Khi người dùng bật **giảm chuyển động** trong hệ điều hành, mọi hiệu ứng rút về gần 0ms.

## 10. Khả năng tiếp cận

- [x] Chữ đạt AA ở cả hai chế độ (mục 3.3).
- [x] Viền focus 2px màu `brand`, chỉ hiện khi dùng bàn phím (`:focus-visible`).
- [x] Nút chỉ có biểu tượng (giỏ hàng, sáng/tối, menu, đóng) đều có `aria-label`.
- [x] Nút lọc dùng `aria-pressed`; phân trang dùng `aria-current="page"`.
- [x] Ô nhập lỗi có `aria-invalid`, thông báo lỗi có `role="alert"`.
- [x] Ngăn kéo giỏ hàng khoá cuộn trang nền khi mở.
- [x] `<html lang="vi">`; chữ Nhật trang trí không thay cho nội dung tiếng Việt.

## 11. Chế độ tối

1. Script nhỏ trong `index.html` chạy **trước khi React dựng trang**: đọc lựa chọn đã lưu (hoặc cài
   đặt của hệ điều hành) và gắn class `dark` lên `<html>`. Nhờ vậy không có khoảnh khắc nháy trắng.
2. Khối `.dark` trong `index.css` ghi đè các token ngữ nghĩa (mục 3.2). Class như `bg-surface
   text-ink` tự đổi màu mà không cần viết `dark:` ở từng chỗ.
3. Nút ☾ / ☀ ở Header lưu lựa chọn vào `localStorage` với khoá `sakura-theme`.

Thêm màu mới thì thêm vào **cả** `@theme` lẫn `.dark`. Nếu quên, màu đó sẽ đứng yên khi đổi chế độ.
