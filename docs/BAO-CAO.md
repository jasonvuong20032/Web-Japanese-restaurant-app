# Báo cáo đồ án cuối khoá — Website nhà hàng Nhật Sakura Tei

<p align="center">
  <img src="../frontend/public/sakura.svg" alt="Logo Sakura Tei" width="96" />
  <br />
  <strong>Sakura Tei · さくら亭</strong>
  <br />
  <em>おいしい料理と、静かな時間を。 — Món ngon và một khoảng lặng.</em>
</p>

| Mục | Thông tin |
| --- | --- |
| Sinh viên thực hiện | Vương Quang Tuấn |
| Thương hiệu | **Sakura Tei** (さくら亭) — thương hiệu **hư cấu**, logo tự thiết kế |
| Mã nguồn (Public) | <https://github.com/jasonvuong20032/Web-Japanese-restaurant-app> — nhánh `main` |
| Website đã deploy | <https://web-japanese-restaurant-app.pages.dev> (chính) · <https://sakura-api-dro3.onrender.com> (dự phòng, trên Render) |
| Database | **SQLite** qua Entity Framework Core 9 — 6 bảng |
| Hạ tầng | Cloudflare Pages (frontend) + Render (API .NET) |
| Ngày nộp | 17/10/2026 |

> **Lưu ý khi mở website:** API chạy trên Render gói Free, tự ngủ sau 15 phút không có truy cập.
> Lần mở đầu tiên có thể mất khoảng **50 giây** để API thức dậy; trong lúc đó trang có thể báo
> *"Không kết nối được tới API"*. Chờ một phút rồi tải lại trang là chạy bình thường.

## Mục lục

1. [Mô tả dự án](#1-mô-tả-dự-án)
2. [Các trang trong dự án](#2-các-trang-trong-dự-án)
3. [Các chức năng và cơ sở dữ liệu](#3-các-chức-năng-và-cơ-sở-dữ-liệu)
4. [Các file .md và chức năng của từng file](#4-các-file-md-và-chức-năng-của-từng-file)
5. [Thứ tự các prompt và kết quả](#5-thứ-tự-các-prompt-và-kết-quả)
6. [Triển khai (deploy) và kiểm thử](#6-triển-khai-deploy-và-kiểm-thử)
7. [Đối chiếu tiêu chí chấm](#7-đối-chiếu-tiêu-chí-chấm)
8. [Hạn chế và hướng phát triển](#8-hạn-chế-và-hướng-phát-triển)

---

## 1. Mô tả dự án

### 1.1 Lĩnh vực

**Ẩm thực / F&B.** Website cho một nhà hàng Nhật Bản tại TP. Hồ Chí Minh, phục vụ ba nhu cầu chính
của khách:

1. **Xem thực đơn** — 250 món chia 12 nhóm, lọc và tìm kiếm được, mỗi món có trang chi tiết riêng.
2. **Đặt món giao tận nơi** — bỏ món vào giỏ, điền địa chỉ, nhận mã đơn để tra cứu; tự huỷ được khi
   bếp chưa bắt đầu làm.
3. **Đặt bàn tại quán** — chọn ngày, xem khung giờ nào còn chỗ, giữ chỗ và nhận mã đặt bàn; đổi lịch
   hoặc huỷ được trước giờ hẹn.

Ngoài ra có chuyên mục **"Câu chuyện ẩm thực"** (bài viết về văn hoá, nguyên liệu, hậu trường bếp)
và trang giới thiệu có **bản đồ Google Maps** chỉ đường tới quán.

### 1.2 Thương hiệu

**Sakura Tei** là thương hiệu **hư cấu**, được tạo riêng cho đồ án.

| Thuộc tính | Nội dung |
| --- | --- |
| Tên | **Sakura** (桜 — hoa anh đào) + **Tei** (亭 — quán, đình nhỏ) → *"Quán hoa anh đào"* |
| Tên tiếng Nhật | さくら亭 |
| Khẩu hiệu | おいしい料理と、静かな時間を。 — *Món ngon và một khoảng lặng* |
| Phong cách nấu | Washoku: ít gia vị, tôn vị nguyên liệu, làm kỹ từng khâu nhỏ |
| Câu chuyện | 2016 mở quầy tám ghế ở hẻm Lê Thánh Tôn → 2019 tự ninh nước dùng tonkotsu 18 tiếng → 2022 chuyển về 128 Lê Lợi, 120 chỗ, bếp mở → 2026 thực đơn 250 món |
| Thông tin liên hệ (hư cấu) | 128 Lê Lợi, P. Bến Thành, Quận 1 · Hotline 1900 6868 · xinchao@sakuratei.vn |
| Giờ mở cửa | 11:00 – 22:00 mỗi ngày, nhận đặt bàn tới 21:00 |

Toàn bộ thông tin trên được khai báo **một lần** trong
[`frontend/src/lib/brand.ts`](../frontend/src/lib/brand.ts); Header, Footer, trang giới thiệu, trang đặt
bàn đều đọc từ đó nên không có chỗ nào ghi lệch.

### 1.3 Logo

Vì là thương hiệu hư cấu, logo được **thiết kế trước** rồi dùng **đúng một file duy nhất** ở mọi nơi
trên website.

<img src="../frontend/public/sakura.svg" alt="Biểu tượng Sakura Tei" width="64" />

| Thành phần | Mô tả |
| --- | --- |
| Biểu tượng | Bông anh đào năm cánh, cánh màu hồng anh đào `#E2899A`, nhị tròn màu son đỏ `#C6402F` (màu chủ đạo của thương hiệu) |
| Chữ (wordmark) | **Sakura Tei** — font Be Vietnam Pro, đậm vừa; dòng phụ **さくら亭** — font Noto Serif JP, giãn chữ rộng |
| File gốc | [`frontend/public/sakura.svg`](../frontend/public/sakura.svg) — vector SVG 64×64, phóng to không vỡ |
| Ý nghĩa | Hoa anh đào là biểu tượng quen thuộc nhất của Nhật Bản; nhị đỏ son lấy từ màu cổng torii và con dấu hanko |

Logo xuất hiện ở ba vị trí, đều dùng chung file trên:

| Vị trí | Mã nguồn | Kích thước |
| --- | --- | --- |
| Biểu tượng tab trình duyệt (favicon) | [`frontend/index.html`](../frontend/index.html) | 16–32px |
| Header — góc trái mọi trang, bấm vào về Trang chủ | [`BrandLogo.tsx`](../frontend/src/components/layout/BrandLogo.tsx) cỡ `md` | 32px + chữ |
| Footer — cuối mọi trang | [`BrandLogo.tsx`](../frontend/src/components/layout/BrandLogo.tsx) cỡ `lg` | 36px + chữ |

Header và Footer cùng dùng component `BrandLogo`, nên logo chỉ được vẽ ở **một chỗ** trong mã nguồn.
Quy tắc dùng logo (vùng an toàn, kích thước tối thiểu, các cách dùng sai) xem tại
[DESIGN.md — mục 2](DESIGN.md#2-logo).

### 1.4 Đối tượng người dùng

| Nhóm khách | Nhu cầu | Chức năng phục vụ |
| --- | --- | --- |
| Dân văn phòng đặt cơm trưa | Đặt nhanh trên điện thoại, biết trước tổng tiền | Thực đơn có lọc, giỏ hàng, đặt món, huỷ đơn |
| Nhóm bạn / gia đình đi ăn tối | Biết giờ nào còn chỗ, giữ chỗ trước, đổi giờ khi kế hoạch thay đổi | Đặt bàn, đổi lịch, huỷ bàn |
| Người thích văn hoá Nhật | Tìm hiểu món, đọc về ẩm thực | Trang chi tiết món, bài viết |

Chân dung người dùng chi tiết nằm trong [PRD.md — mục 4](PRD.md#4-người-dùng-mục-tiêu).

### 1.5 Công nghệ

| Lớp | Công nghệ |
| --- | --- |
| Frontend | React 19, TypeScript, Vite 8, Tailwind CSS v4, React Router 7 |
| Backend | ASP.NET Core 9 Minimal API, Swagger |
| Database | **SQLite** qua **Entity Framework Core 9**: thực đơn, bài viết, đơn hàng, đặt bàn. Giỏ hàng lưu `localStorage` của trình duyệt |
| Triển khai | Cloudflare Pages + Pages Function (proxy `/api`) · Render |
| Công cụ phát triển | VS Code, **Claude Code** (trợ lý lập trình AI), Git/GitHub |

### 1.6 Quy mô

| Chỉ số | Giá trị |
| --- | --- |
| Số món | 250 món, 12 nhóm |
| Ảnh món | 1–3 ảnh riêng cho mỗi món (Wikimedia Commons, có ghi công tác giả và giấy phép) |
| Bài viết | 6 bài, 5 chuyên mục |
| Database | 6 bảng: `Categories`, `Dishes`, `Posts`, `Orders`, `OrderLines`, `Reservations` |
| Số trang | 11 trang (13 đường dẫn) + ngăn kéo giỏ hàng |
| Số endpoint API | 17 + `/health` — trong đó 5 endpoint ghi dữ liệu |
| Mã nguồn | khoảng 10.000 dòng C# và TypeScript |

### 1.7 Kiến trúc tổng quan

```
                    https://web-japanese-restaurant-app.pages.dev
┌───────────────┐   ┌──────────────────────────────────────────────┐
│ Trình duyệt   │──▶│ Cloudflare Pages                             │
│ React SPA     │   │  ├─ /, /thuc-don, /mon-an/…  → file tĩnh     │
│ • giỏ hàng    │   │  └─ /api/*  → Pages Function (proxy) ───┐    │
│  localStorage │   └─────────────────────────────────────────│────┘
└───────────────┘                                           │ HTTPS
                                     ┌──────────────────────▼─────────┐
                                     │ Render — ASP.NET Core 9 API     │
                                     │  Endpoints → Services           │
                                     │    → EF Core → SQLite           │
                                     │      (App_Data/sakuratei.db)    │
                                     └─────────────────────────────────┘
```

Trình duyệt chỉ nói chuyện với **một domain** (Cloudflare), nên không phải cấu hình CORS. Chi tiết
kiến trúc từng lớp nằm trong [PLANNING.md — mục 3](PLANNING.md#3-kiến-trúc-tổng-thể).

---

## 2. Các trang trong dự án

### 2.1 Danh sách trang

| # | Trang | Đường dẫn | Link trực tiếp | Ghi chú |
| --- | --- | --- | --- | --- |
| 1 | **Trang Chủ** (Home) | `/` | [Mở](https://web-japanese-restaurant-app.pages.dev/) | Trang bắt buộc |
| 2 | **Về Chúng Tôi** (About Us) | `/ve-chung-toi` | [Mở](https://web-japanese-restaurant-app.pages.dev/ve-chung-toi) | Trang bắt buộc — có bản đồ Google Maps |
| 3 | **Chi tiết món ăn** (Detail) | `/mon-an/:slug` | [Ví dụ](https://web-japanese-restaurant-app.pages.dev/mon-an/tonkotsu-ramen) | Trang bắt buộc — 250 trang, mỗi món một URL |
| 4 | Thực đơn | `/thuc-don` | [Mở](https://web-japanese-restaurant-app.pages.dev/thuc-don) | Lọc, tìm, phân trang |
| 5 | Đặt món | `/dat-mon` | [Mở](https://web-japanese-restaurant-app.pages.dev/dat-mon) | Form giao hàng + tóm tắt giỏ |
| 6 | Đặt bàn | `/dat-ban` | [Mở](https://web-japanese-restaurant-app.pages.dev/dat-ban) | Chọn ngày → khung giờ → thông tin |
| 7 | Quản lý lượt đặt bàn | `/dat-ban/:code`, `/dat-ban/:code/doi-lich` | Mở từ mã `RS-XXXXXX` | Xem, **đổi lịch**, **huỷ** |
| 8 | Tra cứu | `/tra-cuu`, `/tra-cuu/:code` | [Mở](https://web-japanese-restaurant-app.pages.dev/tra-cuu) | Nhập mã đơn hoặc mã đặt bàn; trang đơn có nút **huỷ đơn** |
| 9 | Câu chuyện ẩm thực | `/cau-chuyen` | [Mở](https://web-japanese-restaurant-app.pages.dev/cau-chuyen) | Danh sách bài, lọc chuyên mục |
| 10 | Chi tiết bài viết | `/cau-chuyen/:slug` | — | Trang đọc bài |
| 11 | Không tìm thấy (404) | mọi đường dẫn khác | [Ví dụ](https://web-japanese-restaurant-app.pages.dev/khong-ton-tai) | Có nút quay về |
| — | Giỏ hàng | (ngăn kéo bên phải) | Bấm biểu tượng 🛒 | Không phải trang riêng, mở được trên mọi trang |

### 2.2 Trang Chủ — `/`

Mục đích: giới thiệu thương hiệu trong vài giây đầu và dẫn khách tới **thực đơn** hoặc **đặt bàn**.

| Thứ tự | Khối | Nội dung | Dẫn tới |
| --- | --- | --- | --- |
| 1 | Hero | Tên thương hiệu, câu giới thiệu, ba con số (250 món · 18h ninh nước dùng · 4.8★), thẻ "Omakase 12 miếng" | Nút **Xem thực đơn**, **Đặt bàn** |
| 2 | Bốn điều chúng tôi không đổi | Bốn cam kết về cách nấu | — |
| 3 | Mười hai nhóm món | 12 thẻ nhóm kèm số món, lấy từ database | `/thuc-don?category=…` |
| 4 | Món quán gợi ý | 8 món nổi bật, có nút thêm nhanh vào giỏ | `/mon-an/:slug` |
| 5 | Dải đặt bàn | Lời mời đặt bàn trên nền xanh chàm, nút gọi hotline | `/dat-ban` |
| 6 | Câu chuyện ẩm thực | 3 bài viết mới nhất | `/cau-chuyen/:slug` |

Dữ liệu khối 3, 4, 6 lấy từ API (`/api/categories`, `/api/dishes/featured`, `/api/posts/latest`);
trong lúc tải hiện khung xương (skeleton) đúng kích thước để trang không bị giật.

### 2.3 Về Chúng Tôi — `/ve-chung-toi`

Mục đích: kể câu chuyện thương hiệu, cung cấp thông tin liên hệ và chỉ đường tới quán.

| Thứ tự | Khối | Nội dung |
| --- | --- | --- |
| 1 | Tiêu đề trang | "Về Sakura Tei" + triết lý washoku |
| 2 | Triết lý | "Nấu ít thứ, nhưng nấu cho đúng" + 2 ảnh + nút **Đặt bàn**, **Xem thực đơn** |
| 3 | Dòng thời gian | 4 cột mốc 2016 → 2019 → 2022 → 2026 |
| 4 | Người đứng bếp | 3 đầu bếp: bếp trưởng, bếp phó ramen, phụ trách trà & tráng miệng |
| 5 | Liên hệ | Địa chỉ, hotline (bấm để gọi), email (bấm để gửi), giờ mở cửa + thẻ đặt bàn trực tuyến |
| 6 | **Đường tới quán** | **Bản đồ Google Maps nhúng** (`<iframe>`) đúng địa chỉ 128 Lê Lợi + nút "Mở trong Google Maps" |

### 2.4 Chi tiết món ăn — `/mon-an/:slug`

Mục đích: cung cấp đủ thông tin để khách quyết định đặt món. Mỗi món có một URL riêng dễ đọc, ví dụ
`/mon-an/tonkotsu-ramen`, nên có thể chia sẻ link.

| Khối | Nội dung |
| --- | --- |
| Đường dẫn (breadcrumb) | Trang chủ › Thực đơn › Nhóm món › Tên món |
| Bộ ảnh | Ảnh lớn + dải ảnh nhỏ (1–3 ảnh), có ghi công tác giả và giấy phép Creative Commons |
| Thông tin chính | Tên tiếng Việt, tên tiếng Nhật, romaji, điểm đánh giá và số lượt, giá (giá gốc gạch ngang nếu đang giảm), thẻ món |
| Thông số | Độ cay (0–3), lượng calo, thời gian chế biến |
| Đặt món | Bộ chọn số lượng + nút **Thêm vào giỏ · {thành tiền}**; món tạm hết thì thay bằng thông báo, không cho đặt |
| Về món này | Mô tả dài |
| Nguyên liệu chính | Danh sách nguyên liệu |
| Món liên quan | 4 món cùng nhóm, bấm sang trang chi tiết món đó |

Món không tồn tại (ví dụ `/mon-an/khong-co`) hiện thông báo "Không tìm thấy món này" kèm liên kết
quay lại thực đơn, không để trang trắng.

### 2.5 Các trang còn lại

| Trang | Mô tả ngắn |
| --- | --- |
| **Thực đơn** | Thanh bên lọc theo nhóm, mức giá tối đa, độ cay, thẻ; sắp xếp theo giá/đánh giá/phổ biến; ô tìm kiếm không dấu; phân trang 12 món mỗi trang. Toàn bộ bộ lọc nằm trên URL nên chia sẻ link sẽ ra đúng kết quả. Trên điện thoại, bộ lọc mở dạng ngăn kéo. |
| **Đặt món** | Form người nhận (họ tên, điện thoại, email, địa chỉ, ghi chú) + tóm tắt đơn (tạm tính, phí giao, tổng). Giỏ trống thì hiện hướng dẫn quay về thực đơn. |
| **Đặt bàn** | Chọn ngày (tối đa 60 ngày tới) → lưới 21 khung giờ 11:00–21:00 kèm số chỗ còn lại → thông tin khách và số người (1–12). Đặt xong có nút sang trang quản lý lượt đặt. |
| **Quản lý lượt đặt bàn** | Thông tin lượt đặt, thời điểm đặt và lần đổi lịch gần nhất. Nút **Đổi ngày, giờ hoặc số khách** mở lại đúng form đặt bàn, điền sẵn dữ liệu cũ. Nút **Huỷ đặt bàn** hỏi lại một lần trước khi xoá. Lượt đã qua giờ hẹn thì khoá cả hai nút. |
| **Tra cứu** | Một ô nhập cho cả hai loại mã: `ST-` mở trang đơn hàng, `RS-` chuyển sang trang quản lý đặt bàn. Trang đơn hàng hiện các bước trạng thái, danh sách món, tổng tiền, giờ dự kiến, và nút **Huỷ đơn** khi bếp chưa nhận làm. |
| **Câu chuyện ẩm thực** | Danh sách bài viết, lọc theo chuyên mục, phân trang; trang đọc bài có tiểu mục, thời gian đọc. |

### 2.6 Liên kết giữa các trang

Mọi trang đều đi qua chung một khung (`RootLayout`) gồm **Header** và **Footer**, nên từ bất kỳ trang
nào cũng tới được các trang chính:

```
Header (mọi trang)
├─ Logo Sakura Tei ─────────────▶ Trang chủ
├─ Trang chủ · Thực đơn · Đặt bàn · Câu chuyện · Về chúng tôi
├─ Ô tìm món ──────────────────▶ /thuc-don?q=…
└─ Nút giỏ hàng ────▶ Ngăn kéo giỏ ──▶ Đặt món ──▶ Trang đơn hàng (huỷ đơn)

Footer (mọi trang)
├─ Khám phá: Thực đơn 250 món · Sushi & Sashimi · Ramen & Udon · BBQ than hoa
└─ Dịch vụ:  Đặt món · Đặt bàn · Tra cứu đơn hàng · Câu chuyện ẩm thực

Liên kết trong nội dung
Trang chủ ──▶ Thực đơn (theo nhóm) ──▶ Chi tiết món ──▶ Món liên quan ──▶ Chi tiết món khác
Đặt bàn ──▶ Xác nhận ──▶ Quản lý lượt đặt ──▶ Đổi lịch / Huỷ
Tra cứu ──▶ (mã ST-) Trang đơn hàng · (mã RS-) Quản lý lượt đặt
Trang chủ ──▶ Bài viết            Về chúng tôi ──▶ Đặt bàn / Thực đơn / Google Maps
```

Trên điện thoại, năm liên kết của Header gom vào nút menu ☰. Liên kết đang mở được tô màu để người
dùng biết mình đang ở trang nào.

---

## 3. Các chức năng và cơ sở dữ liệu

### 3.1 Danh sách chức năng

| Mã | Chức năng | Mô tả | Nơi cài đặt chính |
| --- | --- | --- | --- |
| F01 | Xem và lọc thực đơn | Lọc theo nhóm, giá, độ cay, thẻ; sắp xếp; phân trang; bộ lọc đồng bộ với URL | `MenuPage.tsx`, `DishService.cs` |
| F02 | Tìm kiếm không dấu | Gõ `ca hoi` vẫn ra `cá hồi`; tìm trong tên, romaji, mô tả, thẻ, nguyên liệu | `DishService.cs` |
| F03 | Chi tiết món | Bộ ảnh, thông số, nguyên liệu, món liên quan | `DishDetailPage.tsx`, `DishGallery.tsx` |
| F04 | Giỏ hàng | Thêm món, đổi số lượng, xoá từng món, xoá hết; tự lưu và khôi phục khi tải lại trang | `CartContext.tsx`, `CartDrawer.tsx` |
| F05 | Đặt món giao tận nơi | Kiểm tra dữ liệu ở cả hai phía, **server tự tra giá**, lưu vào database, sinh mã `ST-XXXXXX` | `CheckoutPage.tsx`, `OrderService.cs` |
| F06 | Tra cứu đơn / lượt đặt | Một ô nhập cho cả mã `ST-` và `RS-` | `OrderLookupPage.tsx` |
| F07 | **Huỷ đơn** | Khi đơn còn ở bước "Đã nhận"; hỏi lại trước khi huỷ | `OrderLookupPage.tsx`, `OrderService.Cancel` |
| F08 | Đặt bàn | Xem chỗ trống theo khung giờ, giữ chỗ, lưu vào database, sinh mã `RS-XXXXXX` | `ReservationPage.tsx`, `ReservationService.cs` |
| F09 | **Đổi lịch đặt bàn** | Đổi ngày, giờ, số khách, thông tin liên hệ; kiểm tra lại chỗ trống | `ReservationManagePage.tsx`, `ReservationForm.tsx` |
| F10 | **Huỷ đặt bàn** | Xoá lượt đặt, trả lại chỗ cho khung giờ đó | `ReservationManagePage.tsx`, `ReservationService.Delete` |
| F11 | **Bản đồ Google Maps** | Nhúng bản đồ đúng địa chỉ quán ở trang Về Chúng Tôi | `AboutPage.tsx`, `lib/brand.ts` |
| F12 | Bài viết | Danh sách có lọc chuyên mục, phân trang, trang đọc chi tiết | `BlogPage.tsx`, `BlogPostPage.tsx` |
| F13 | Giao diện sáng/tối | Nút chuyển ở Header, nhớ lựa chọn, không nháy trắng lúc tải | `ThemeContext.tsx`, `index.html` |
| F14 | Responsive | Dùng tốt từ điện thoại tới màn hình rộng; menu, bộ lọc dạng ngăn kéo trên di động | toàn bộ giao diện |
| F15 | Trạng thái tải / lỗi / rỗng | Khung xương khi tải, thông báo lỗi kèm nút thử lại, thông báo khi không có kết quả | `Feedback.tsx`, `useAsync.ts` |
| F16 | Ảnh dự phòng | Ảnh lỗi thì hiện chữ kanji của nhóm món thay vì ô trống | `SmartImage.tsx` |
| F17 | Hỗ trợ tiếp cận | `aria-label`, `aria-invalid`, viền focus rõ, tôn trọng chế độ giảm chuyển động | toàn bộ giao diện |
| F18 | Kiểm tra sức khoẻ API | `GET /health` trả loại database, số món, số nhóm | `Program.cs` |

### 3.2 Các chức năng chỉnh sửa database

Năm thao tác dưới đây **ghi thật vào SQLite**. Tắt API rồi bật lại, dữ liệu vẫn còn — đã kiểm thử ở
mục 6.3.

| Thao tác | Chức năng | Gọi tới | Thay đổi trong database |
| --- | --- | --- | --- |
| **INSERT** | Đặt món | `POST /api/orders` | Thêm 1 dòng `Orders` + các dòng `OrderLines` (đã chốt giá) |
| **UPDATE** | Huỷ đơn | `PATCH /api/orders/{code}/cancel` | `Orders.Status`: `Received` → `Cancelled` |
| **INSERT** | Đặt bàn | `POST /api/reservations` | Thêm 1 dòng `Reservations` |
| **UPDATE** | Đổi lịch | `PUT /api/reservations/{code}` | Sửa ngày, giờ, số khách, liên hệ, ghi `UpdatedAt` |
| **DELETE** | Huỷ đặt bàn | `DELETE /api/reservations/{code}` | Xoá dòng `Reservations`, chỗ trống của khung giờ tự tăng lại |

Ngoài ra giỏ hàng có thêm / sửa số lượng / xoá phía trình duyệt (`localStorage`).

Cách hệ thống bảo vệ dữ liệu khi ghi:

- Client chỉ gửi `dishId` và `quantity`. **Server tự tra giá** từ thực đơn, nên sửa giá trong
  trình duyệt không làm thay đổi hoá đơn.
- Dữ liệu được kiểm tra ở **cả hai phía**. Lỗi từ server trả theo chuẩn RFC 7807 và hiện **đúng dưới ô
  nhập** bị sai.
- Đặt bàn và đổi lịch dùng khoá ghi: hai người đặt cùng lúc không thể vượt quá 24 chỗ của một khung.
  Đổi lịch không tính chỗ của chính lượt đang đổi.
- Thao tác không hợp lệ theo trạng thái bị từ chối với mã **409**: huỷ đơn đã huỷ, huỷ đơn bếp đã
  làm, đổi hay huỷ lượt đặt đã qua giờ hẹn.
- "Hôm nay" luôn tính theo giờ Việt Nam (UTC+7), không theo giờ máy chủ (Render chạy giờ UTC).

### 3.3 Cơ sở dữ liệu

| Mục | Nội dung |
| --- | --- |
| Loại | **SQLite** — một file, không phải cài máy chủ database |
| Truy cập | Entity Framework Core 9 (`SakuraDbContext`, cấu hình từng bảng trong `Data/Configurations/`) |
| Vị trí file | `backend/SakuraTei.Api/App_Data/sakuratei.db` (không đưa lên Git) |
| Tạo bảng | Lần chạy đầu API tự tạo bảng (`EnsureCreated`) và nạp dữ liệu mẫu từ `Data/Seed/` |
| Xem dữ liệu | Mở file bằng [DB Browser for SQLite](https://sqlitebrowser.org) |

```
Categories (Slug PK) ──1───n── Dishes (Id PK, CategorySlug FK, Slug unique)
                                  └ Tags, Ingredients, Photos: cột JSON

Orders (Code PK) ──1───n── OrderLines (Id PK tự tăng, OrderCode FK)

Reservations (Code PK, chỉ mục Date + Time)        Posts (Slug PK)
```

| Bảng | Số dòng ban đầu | Vai trò |
| --- | --- | --- |
| `Categories` | 12 | Nhóm món. Còn món thì không xoá được nhóm (`ON DELETE RESTRICT`) |
| `Dishes` | 250 | Thực đơn; thẻ, nguyên liệu, bộ ảnh lưu dạng JSON trong cột |
| `Posts` | 6 | Bài viết |
| `Orders` | 0 | Đơn hàng, trạng thái lưu dạng chữ (`Received`, `Cancelled`…) |
| `OrderLines` | 0 | Dòng món của đơn — bản chụp tên và giá lúc đặt |
| `Reservations` | 0 | Lượt đặt bàn; chỗ trống tính bằng tổng `PartySize` theo khung giờ |

### 3.4 Quy tắc nghiệp vụ chính

| Quy tắc | Giá trị |
| --- | --- |
| Phí giao hàng | 25.000đ, **miễn phí** cho đơn từ 500.000đ |
| Số lượng mỗi món | 1–50 |
| Huỷ đơn | Chỉ khi bếp chưa nhận làm (trạng thái `Received`) |
| Giờ nhận khách | 11:00–21:00, mỗi khung 30 phút (21 khung một ngày) |
| Sức chứa | 24 chỗ mỗi khung giờ |
| Số khách mỗi lượt đặt | 1–12 |
| Đặt bàn trước tối đa | 60 ngày |
| Đổi lịch / huỷ bàn | Chỉ trước giờ hẹn |
| Số điện thoại | 10 chữ số, bắt đầu bằng 0 |

Bảng đầy đủ kèm nơi cài đặt từng quy tắc: [PLANNING.md — mục 5](PLANNING.md#5-quy-tắc-nghiệp-vụ).

### 3.5 API

| Phương thức | Đường dẫn | Công dụng |
| --- | --- | --- |
| GET | `/api/categories` | 12 nhóm món kèm số món |
| GET | `/api/dishes` | Tìm và lọc thực đơn, có phân trang |
| GET | `/api/dishes/featured` | Món nổi bật cho Trang chủ |
| GET | `/api/dishes/filters` | Danh sách thẻ và khoảng giá |
| GET | `/api/dishes/{slug}` | Chi tiết một món + món liên quan |
| POST | `/api/orders` | Tạo đơn hàng |
| GET | `/api/orders/{code}` | Tra cứu đơn |
| PATCH | `/api/orders/{code}/cancel` | Huỷ đơn |
| GET | `/api/reservations/availability` | Khung giờ còn chỗ trong ngày |
| POST | `/api/reservations` | Đặt bàn |
| GET | `/api/reservations/{code}` | Tra cứu lượt đặt bàn |
| PUT | `/api/reservations/{code}` | Đổi lịch |
| DELETE | `/api/reservations/{code}` | Huỷ đặt bàn |
| GET | `/api/posts`, `/api/posts/topics`, `/api/posts/latest`, `/api/posts/{slug}` | Bài viết |
| GET | `/health` | Kiểm tra API |

Thử nhanh trên bản đã deploy:
<https://web-japanese-restaurant-app.pages.dev/api/dishes?q=ca%20hoi&pageSize=3>

---

## 4. Các file .md và chức năng của từng file

### 4.1 Danh sách

| File | Loại tài liệu | Trả lời câu hỏi | Người đọc chính |
| --- | --- | --- | --- |
| [`README.md`](../README.md) | Cổng vào repo | Dự án là gì, chạy và kiểm thử thế nào, cấu trúc thư mục, tài liệu nằm đâu, link demo ở đâu? | Bất kỳ ai mở repo |
| [`docs/BAO-CAO.md`](BAO-CAO.md) | Báo cáo nộp bài | Dự án đáp ứng đề bài thế nào? (file này) | Giảng viên |
| [`docs/PRD.md`](PRD.md) | Tài liệu yêu cầu sản phẩm | **Làm cái gì, cho ai, vì sao, thế nào là xong?** | Người làm sản phẩm, lập trình viên, AI |
| [`docs/DESIGN.md`](DESIGN.md) | Tài liệu thiết kế | **Trông như thế nào?** Logo, màu, chữ, bố cục, thành phần giao diện | Người thiết kế, lập trình viên frontend, AI |
| [`docs/PLANNING.md`](PLANNING.md) | Kế hoạch kỹ thuật | **Xây như thế nào?** Kiến trúc, database, hợp đồng API, quy tắc nghiệp vụ, cách kiểm thử | Lập trình viên |
| [`docs/DEPLOY-CLOUDFLARE.md`](DEPLOY-CLOUDFLARE.md) | Hướng dẫn triển khai | **Đưa lên mạng thế nào?** Frontend lên Cloudflare Pages, API lên Render | Người vận hành |

Mỗi file trả lời **một** câu hỏi riêng, không file nào lặp nội dung của file khác. Ở prompt 12, ba
file trùng lặp hoặc không còn dùng đã được bỏ: `docs/README.md` (gộp phần còn thiếu vào `README.md`
ở gốc), `DEPLOY.md` (tự dựng server bằng Docker) và `DEPLOY-VERCEL.md` (không dùng Vercel).

### 4.2 PRD.md và DESIGN.md có tác dụng gì?

**PRD.md (Product Requirements Document)** là bản mô tả **yêu cầu sản phẩm**. File này chốt:

- **Mục tiêu và phạm vi:** làm gì, và quan trọng không kém, *không* làm gì (ví dụ: không làm đăng nhập).
- **Người dùng mục tiêu** và **user story**: "Là khách văn phòng, tôi muốn… để…".
- **Tiêu chí chấp nhận** cho từng chức năng, tức là điều kiện để coi một chức năng là "xong".
- **Yêu cầu phi chức năng:** hiệu năng, bảo mật, khả năng tiếp cận.

**DESIGN.md** là bản mô tả **thiết kế và nhận diện thương hiệu**. File này chốt:

- **Logo:** cấu tạo, màu, kích thước tối thiểu, vùng an toàn, các cách dùng sai.
- **Hệ màu** (kể cả chế độ tối), **font chữ**, khoảng cách, bo góc, đổ bóng.
- **Thành phần giao diện** dùng chung và **khung bố cục** từng trang.

Hai file này có tác dụng lớn khi làm việc **cùng AI**:

1. **AI không nhớ giữa các phiên làm việc.** Mỗi cuộc trò chuyện mới, AI bắt đầu từ con số không.
   PRD và DESIGN đóng vai **bộ nhớ dài hạn** của dự án: chỉ cần bảo AI "đọc docs/PRD.md và
   docs/DESIGN.md trước", nó sẽ nắm phạm vi và phong cách mà không phải giải thích lại.
2. **Giữ nhất quán qua nhiều prompt.** Không có DESIGN.md, prompt sau dễ sinh ra nút màu khác, cỡ
   chữ khác prompt trước. Có token màu và quy tắc logo viết sẵn, mọi trang mới đều theo cùng một kiểu.
3. **Chống làm thừa hoặc làm thiếu.** PRD ghi rõ phạm vi, nên AI không tự thêm tính năng ngoài yêu cầu
   (như đăng nhập, thanh toán online) và không bỏ sót tiêu chí chấp nhận.
4. **Làm thước đo để kiểm tra.** Sau mỗi prompt, đối chiếu kết quả với tiêu chí chấp nhận trong PRD
   là biết đã đạt hay chưa. Ở dự án này, chính PRD đã chỉ ra 4 yêu cầu còn thiếu (FR-12 tới FR-15),
   và prompt 10 làm đúng 4 yêu cầu đó.

Thứ tự đọc khi bắt đầu dự án: **PRD** (làm gì) → **DESIGN** (trông ra sao) → **PLANNING** (xây thế
nào) → mã nguồn → **README / DEPLOY** (chạy và đưa lên mạng).

Viết bằng Markdown vì đây là văn bản thuần: Git theo dõi được từng dòng thay đổi, GitHub hiển thị
đẹp ngay trên web, và AI đọc hiểu tốt.

---

## 5. Thứ tự các prompt và kết quả

Dự án được xây cùng **Claude Code**, trợ lý lập trình AI chạy trong VS Code. Mỗi prompt lớn kết thúc
bằng một commit; các commit do AI viết có dòng `Co-Authored-By: Claude`, nên lịch sử Git ghi lại
khá trọn vẹn tiến trình làm việc. Phần "Yêu cầu" của prompt 1–7 được tóm tắt lại từ lịch sử commit;
prompt 8–12 ghi lại theo lời nhắn gốc (gõ không dấu, thêm dấu cho dễ đọc).

### 5.1 Tổng quan

| # | Ngày | Yêu cầu | Kết quả | Commit |
| --- | --- | --- | --- | --- |
| 1 | 20/09/2026 | Dựng toàn bộ website (API + frontend) | 82 file, ~8.900 dòng, 9 trang, 14 endpoint | `e89bfc8` |
| 2 | 20/09/2026 | Viết tài liệu dự án | `PLANNING.md`, `docs/README.md` | `612e6dd` |
| 3 | 20/09/2026 | Chuẩn bị deploy: một process phục vụ cả web lẫn API | `Program.cs`, `Dockerfile`, `DEPLOY.md` | `cf93137` |
| 4 | 20/09/2026 | Thêm HTTPS tự động | `compose.yaml`, `Caddyfile` | `0b55505` |
| 5 | 20/09/2026 | Hướng dẫn deploy lên Vercel | `vercel.json`, `DEPLOY-VERCEL.md` | `4eed945` |
| 6 | 26/09/2026 | Mở rộng thực đơn lên 250 món, ảnh riêng từng món | 130 món mới, 5 nhóm mới, bộ ảnh | `0203cd5` → PR #1 |
| 7 | 26/09/2026 | Deploy lên Cloudflare Pages + Render | Pages Function proxy, `DEPLOY-CLOUDFLARE.md`, website chạy thật | `2979c09` |
| 8 | 09/10/2026 | Viết tài liệu theo tiêu chí chấm, kiểm tra deploy, tạo nhánh | `BAO-CAO.md`, `PRD.md`, `DESIGN.md`, sửa số liệu cũ, nhánh `main`/`dev` | nhánh `dev` |
| 9 | 09/10/2026 | Đối chiếu mọi thứ với ảnh đề bài | Bảng chấm thử: ước 4,5–5,5/10, chỉ ra 3 tiêu chí còn thiếu | — |
| 10 | 09/10/2026 | Làm phần còn thiếu, refactor, push code | SQLite, huỷ đơn, đổi lịch, huỷ bàn, Google Maps, refactor, kiểm thử tự động | nhánh `dev` → `main` |
| 11 | 09/10/2026 | Đưa bản mới lên mạng (chuỗi câu hỏi về Render, Cloudflare) | Render và Cloudflare build từ `main`; kiểm thử 21/21 trên server thật | `a291718` |
| 12 | 11/10/2026 | Làm báo cáo Word, kiểm tra GitHub Public, bỏ tài liệu dư thừa | File Word có ảnh chụp; repo Public; còn 6 file .md | nhánh `main` |

### 5.2 Chi tiết từng prompt

#### Prompt 1 — Dựng toàn bộ website

**Yêu cầu.** Xây website nhà hàng Nhật "Sakura Tei" theo kiểu full-stack: backend ASP.NET Core
Minimal API, frontend React + TypeScript. Có thực đơn lọc và tìm được, chi tiết món, giỏ hàng, đặt
món, đặt bàn, tra cứu đơn, chuyên mục bài viết, trang giới thiệu, giao diện sáng/tối.

**Kết quả.** Commit `e89bfc8`: 82 file, 8.939 dòng.

- API 14 endpoint, thực đơn 120 món chia 7 nhóm, 6 bài viết. Dữ liệu lúc này nằm trong bộ nhớ.
- 9 trang và trang 404, Header/Footer liên kết đầy đủ.
- Các quyết định kỹ thuật đáng chú ý: server tự tra giá khi tạo đơn; giỏ hàng lưu `localStorage` và
  bỏ qua dữ liệu hỏng khi đọc lại; bộ lọc đồng bộ với URL; lỗi 400 hiện đúng dưới ô nhập; chế độ tối
  dùng token CSS nên không phải viết `dark:` ở khắp nơi.

#### Prompt 2 — Viết tài liệu dự án

**Yêu cầu.** Viết tài liệu kế hoạch chi tiết để người mới đọc là nắm được dự án.

**Kết quả.** Commit `612e6dd`.

- `docs/PLANNING.md`: phạm vi, lý do chọn công nghệ, kiến trúc phân lớp, mô hình dữ liệu, quy tắc
  nghiệp vụ, hợp đồng API, thiết kế frontend, giới hạn và hướng phát triển.
- Chuyển README chi tiết vào `docs/`, giữ ở gốc một bản ngắn trỏ sang.

#### Prompt 3 — Chuẩn bị deploy gộp một process

**Yêu cầu.** Làm cho bản build chạy được ở môi trường thật.

**Kết quả.** Commit `cf93137`.

- **Phát hiện lỗi:** bản build của Vite không có nơi nào phục vụ, vào thẳng `/thuc-don` hay F5 giữa
  trang sẽ ra 404.
- **Sửa:** API phục vụ luôn frontend (`UseStaticFiles` + `MapFallbackToFile`), `/api` sai đường dẫn
  trả 404 JSON thay vì HTML, Swagger chỉ bật ở môi trường phát triển.
- `Dockerfile` ba tầng, chạy bằng người dùng thường (không phải root), dùng ảnh Debian vì tìm kiếm
  không dấu cần thư viện ICU.

#### Prompt 4 — HTTPS tự động

**Yêu cầu.** Bổ sung cấu hình HTTPS thay vì chỉ ghi "đặt Nginx phía trước".

**Kết quả.** Commit `0b55505`: `compose.yaml` + `Caddyfile`, chạy một lệnh là có chứng chỉ Let's
Encrypt, tự gia hạn. Đã build và chạy thử ảnh Docker thật.

#### Prompt 5 — Deploy lên Vercel

**Yêu cầu.** Hướng dẫn đưa website lên Vercel.

**Kết quả.** Commit `4eed945`.

- Chỉ ra Vercel **không chạy được .NET**, nên phải tách: frontend lên Vercel, API lên Render.
- `vercel.json`: chuyển mọi đường dẫn về `index.html`, cache hợp lý.
- **AI tự sửa một nhận định sai:** ban đầu ghi "CORS sai thì chỉ POST hỏng". Sau khi thử thật thì
  thấy trình duyệt chặn cả GET, và tài liệu đã được sửa lại cho đúng.

#### Prompt 6 — Mở rộng thực đơn lên 250 món

**Yêu cầu.** Tăng thực đơn lên 250 món, mỗi món có ảnh riêng, không dùng chung ảnh.

**Kết quả.** Commit `0203cd5` trên nhánh `feature/menu-250-mon`, gộp qua Pull Request #1.

- Thêm 130 món, 5 nhóm mới: Bò Wagyu & Kobe, Hải sản cao cấp, Tempura & đồ chiên, Cơm & Donburi,
  Mochi & Wagashi.
- Mỗi món 1–3 ảnh riêng từ Wikimedia Commons, có tác giả và giấy phép.

#### Prompt 7 — Deploy lên Cloudflare Pages

**Yêu cầu.** Đưa website lên Cloudflare.

**Kết quả.** Commit `2979c09`.

- Cloudflare cũng không chạy được .NET, nên API đặt trên Render. Viết một **Pages Function**
  (`functions/api/[[path]].ts`) chuyển tiếp `/api/*` sang Render. Nhờ vậy trình duyệt chỉ thấy một
  domain, không cần CORS, không phải build lại khi đổi địa chỉ API.
- Website chạy thật tại <https://web-japanese-restaurant-app.pages.dev>.

#### Prompt 8 — Viết tài liệu theo tiêu chí chấm, kiểm tra deploy

**Yêu cầu** (kèm ảnh chụp đề bài):

> Dựa vào ảnh trên là tiêu chí chấm bài, vai trò của bạn là senior developer và có kinh nghiệm 5 năm
> dev. Vì thế dựa vào hình trên hãy viết docs thật chi tiết và check xem có deploy server trên ảnh
> yêu cầu chưa.

Nhắn thêm trong lúc AI đang làm: *"Tạo nhánh main và dev luôn đi nha."*

**Kết quả.**

- Kiểm tra deploy bằng request thật: website trên Cloudflare và API trên Render đều chạy, dữ liệu
  đúng bản mới nhất. Phát hiện link GitHub Pages chỉ hiện file README chứ không phải website, nên
  **không dùng link đó để nộp**.
- Phát hiện mã cấu hình Cloudflare chỉ nằm trên nhánh `feature/menu-250-mon`, chưa có ở nhánh chính
  — đã đưa về.
- Viết `docs/BAO-CAO.md`, `docs/PRD.md`, `docs/DESIGN.md`; cập nhật `README.md`.
- Sửa số liệu cũ còn sót trên giao diện: "Bảy nhóm món", "Một trăm hai mươi món", thẻ mô tả SEO ghi
  120 món → 250 món / 12 nhóm.
- Tạo nhánh `main` (bản ổn định để deploy) và `dev` (nhánh phát triển).

#### Prompt 9 — Đối chiếu với ảnh đề bài

**Yêu cầu:**

> Vậy check xem coi mọi thứ có đúng như ảnh tui gửi chưa.

**Kết quả.** Bảng chấm thử từng tiêu chí trong ảnh:

- Đạt: 3 trang bắt buộc, file .md, GitHub Public, deploy, các trang liên kết nhau, logo thương hiệu hư cấu.
- **Chưa đạt:** database CSV/SQLite/Supabase (3 điểm) — dữ liệu lúc đó chỉ nằm trong bộ nhớ;
  chức năng chỉnh sửa database (2 điểm) — chưa có sửa/xoá phía server; nhúng Google Map/YouTube (0,5 điểm).
- Ước điểm lúc đó: **4,5–5,5/10**, kèm danh sách việc cần làm xếp theo số điểm.

#### Prompt 10 — Làm phần còn thiếu, refactor, push code

**Yêu cầu:**

> Giờ làm lại cho tui đi được không, và push code, sau rồi tạo nhánh dev tại chưa có nhánh dev trên
> GitHub, oke không?

Nhắn thêm trong lúc AI đang làm: *"Refactor nhé được không?"* và *"Bỏ Docker nhé, tại nó dư quá đi á."*

**Kết quả.**

- **Database SQLite** qua EF Core 9: 6 bảng, tự tạo và nạp dữ liệu mẫu lần đầu chạy. Thực đơn, bài
  viết, đơn hàng, đặt bàn đều nằm trong database.
- **Chỉnh sửa database:** thêm `PATCH` huỷ đơn, `PUT` đổi lịch, `DELETE` huỷ bàn, cùng giao diện cho
  từng thao tác (trang quản lý lượt đặt, form đổi lịch, nút huỷ có hỏi lại).
- **Nhúng Google Maps** ở trang Về Chúng Tôi.
- **Refactor:**
  - Backend tách lại `Data/`: DbContext, cấu hình từng bảng, dữ liệu mẫu riêng.
  - `ServiceResult` dùng chung cho mọi thao tác ghi, nên lỗi 400/404/409 có cùng một hình dạng.
  - `ReservationRequest` dùng chung cho đặt mới và đổi lịch.
  - Frontend tách `ReservationForm`, `ConfirmAction`, `BrandLogo`, `AnchorButton` và `lib/brand.ts`.
- **Sửa lỗi phát hiện trong lúc làm:**
  - Phần tóm tắt ở trang đặt bàn bị đẩy ra khỏi màn hình (lỗi có sẵn trên bản đang chạy).
  - Nút `<button>` lồng trong `<a>` ở Trang chủ (HTML sai).
  - Ngày "hôm nay" tính theo giờ máy chủ UTC thay vì giờ Việt Nam.
  - Trên Windows, thư mục `data/` trùng với thư mục mã nguồn `Data/` → đổi sang `App_Data/`.
- **Kiểm thử:**
  - 32 bước đầu–cuối, đều đạt. Dữ liệu thực đơn trả về giống hệt từng byte với bản đang chạy, nghĩa là
    chuyển sang database không làm đổi gì phía người dùng.
  - Tắt bật lại API, dữ liệu vẫn còn.
  - Thêm `scripts/api-e2e.mjs` (21 bước) để ai cũng chạy lại được.
- **Docker:** theo yêu cầu, không dùng Docker ở máy nữa. Vẫn giữ `Dockerfile` vì Render cần nó để
  build API .NET.
- Commit lên `dev`, gộp sang `main`, push cả hai nhánh lên GitHub.

#### Prompt 11 — Đưa bản mới lên mạng

**Yêu cầu** (một chuỗi tin nhắn ngắn, kèm ảnh chụp màn hình GitHub, Render, Cloudflare):

> Server không load được món ăn á. · Thế giờ làm sao nộp nhánh main đk? · Giờ làm trên Render sao? ·
> Vô dash Cloudflare đi. · Can you check for me API_ORIGIN? · Chỉ bằng tiếng Việt đi. · Link Render oke đk?

**Kết quả.**

- **Tìm đúng nguyên nhân "không load được món":** bản xem trước của nhánh `main` thiếu biến
  `API_ORIGIN` ở môi trường Preview của Cloudflare. Website chính vẫn tải đủ 250 món, chỉ là bản cũ.
- Hướng dẫn từng bước bằng tiếng Việt theo đúng màn hình người dùng gửi:
  - Render: đổi service `sakura-api` sang build từ `main`.
  - Cloudflare: đổi Production branch sang `main`, thêm `API_ORIGIN` cho Preview.
  - GitHub: đổi nhánh mặc định sang `main`.
- AI không đọc được cài đặt Cloudflare, nên nhờ người dùng gửi giá trị `API_ORIGIN`
  (`https://sakura-api-dro3.onrender.com`), rồi tự kiểm tra từ đó.
- Kiểm tra trên server thật: API mới trả `"database":"SQLite"`; script kiểm thử chạy **21/21 bước
  đạt**; proxy Cloudflare chuyển đúng `PATCH`/`DELETE`.
- Push một commit lên `main` để Cloudflare build lại production. Sau khoảng 30 giây website chính lên
  bản mới; đặt thử một bàn trên website thật để xem trang quản lý lượt đặt, rồi xoá đi.
- Phát hiện link Render cũng mở ra được toàn bộ website, dùng làm link dự phòng.

#### Prompt 12 — Báo cáo Word, kiểm tra GitHub Public, bỏ tài liệu dư thừa

**Yêu cầu:**

> Làm docs Word cho tui đi nha một cách chi tiết, và xem GitHub có để Public chưa.

Nhắn thêm trong lúc AI đang làm: *"Loại bỏ những docs dư thừa trong thư mục docs đi nha."*

**Kết quả.**

- Kiểm tra GitHub: repo **Public** (`"private": false`, `"visibility": "public"`); người không đăng
  nhập vẫn mở được trang repo và file báo cáo trên `main`.
- Làm báo cáo Word `docs/BAO-CAO-Sakura-Tei.docx` theo đúng các mục của đề: trang bìa, mục lục, các
  chương như báo cáo này, kèm ảnh chụp màn hình website thật.
- Bỏ 3 file .md trùng lặp hoặc không còn dùng (mục 4.1), gộp phần còn thiếu vào `README.md` ở gốc,
  sửa mọi liên kết trỏ tới các file đã bỏ.

### 5.3 Rút kinh nghiệm khi làm việc với AI

- **Mỗi prompt một mục tiêu rõ ràng.** Prompt "thêm HTTPS" cho kết quả gọn và kiểm chứng được hơn
  hẳn kiểu "làm cho xong phần deploy".
- **Đưa tiêu chí chấm cho AI.** Prompt 8–9 gửi kèm ảnh đề bài, nhờ đó AI chỉ ra đúng 3 tiêu chí đang
  mất điểm thay vì làm thêm những thứ không được chấm.
- **Yêu cầu AI tự kiểm chứng.** Các commit tốt nhất đều ghi rõ đã kiểm tra gì (gọi API thật, chụp
  màn hình trang thật). Ở prompt 5 và 10, chính việc thử thật đã phát hiện lỗi mà đọc mã không thấy.
- **Lưu kiến thức vào file .md.** AI không nhớ giữa các phiên làm việc; PLANNING, PRD, DESIGN giúp
  phiên sau tiếp tục đúng hướng mà không phải giải thích lại từ đầu.
- **Người vẫn phải duyệt.** AI viết nhanh nhưng vẫn để sót số liệu cũ (120 → 250 món) ở vài chỗ;
  cần đọc lại giao diện thật trước khi nộp.

---

## 6. Triển khai (deploy) và kiểm thử

### 6.1 Địa chỉ

| Thành phần | Địa chỉ |
| --- | --- |
| Website (chính) | <https://web-japanese-restaurant-app.pages.dev> |
| Website (dự phòng) | <https://sakura-api-dro3.onrender.com> — Render phục vụ cả web lẫn API |
| API (gọi qua cùng domain) | <https://web-japanese-restaurant-app.pages.dev/api/categories> |
| Mã nguồn | <https://github.com/jasonvuong20032/Web-Japanese-restaurant-app> |

### 6.2 Cách triển khai

| Phần | Nền tảng | Cách build |
| --- | --- | --- |
| Frontend | **Cloudflare Pages** | Nhánh `main`, root directory `frontend`, lệnh `npm run build`, thư mục kết quả `dist` |
| Proxy `/api/*` | Cloudflare Pages Function | `frontend/functions/api/[[path]].ts`, đọc biến `API_ORIGIN` |
| API + database | **Render** (gói Free) | Nhánh `main`, Render tự build từ `Dockerfile` ở gốc repo; file SQLite nằm trong `/app/App_Data` |

Từng bước cấu hình, cách kiểm tra và bảng đọc lỗi: [DEPLOY-CLOUDFLARE.md](DEPLOY-CLOUDFLARE.md).

### 6.3 Kết quả kiểm thử

**Bản đang chạy trên mạng** (kiểm tra ngày 09/10/2026):

| Kiểm tra | Kết quả | Ý nghĩa |
| --- | --- | --- |
| `GET /`, `/thuc-don`, `/mon-an/tonkotsu-ramen`, `/ve-chung-toi`, `/dat-ban` | 200 | Vào thẳng hoặc F5 giữa trang không bị 404 |
| `GET /api/dishes?pageSize=1` | `totalItems: 250` | Đủ 250 món |
| `GET /api/categories` | 12 nhóm | Đủ 12 nhóm |
| `GET /api/dishes?q=ca%20hoi` | 19 kết quả | Tìm kiếm không dấu hoạt động trên server thật |
| `GET /api/reservations/availability?date=2026-10-10` | 21 khung giờ, mỗi khung 24 chỗ | Đặt bàn hoạt động |
| `POST /api/orders` với dữ liệu sai | 400, kèm lỗi theo từng trường | Proxy chuyển POST đúng, kiểm tra dữ liệu hoạt động |
| Header phản hồi `/api` | `server: cloudflare` + `x-render-origin-server: Kestrel` | Xác nhận đường đi Cloudflare → Render → ASP.NET Core |

**Bản có database trên server thật** (Render build từ nhánh `main`, kiểm tra ngày 09/10/2026):

| Kiểm tra | Kết quả |
| --- | --- |
| `GET /health` trên Render | `"database":"SQLite"`, 250 món, 12 nhóm, giờ máy chủ báo theo `+07:00` |
| `PATCH` và `DELETE` gửi qua proxy Cloudflare | Tới đúng API mới (trả lỗi 404 tiếng Việt của API mới) |
| `node scripts/api-e2e.mjs <địa chỉ Render> --allow-remote` | **21/21 đạt** — thêm, đọc, huỷ đơn; thêm, đọc, sửa, xoá đặt bàn trên database thật |

**Bản có database chạy ở máy** (chế độ Production giống trên Render, ngày 09/10/2026):

| Bộ kiểm thử | Kết quả |
| --- | --- |
| 32 bước đầu–cuối: thêm/đọc/huỷ đơn, thêm/đọc/sửa/xoá đặt bàn, kiểm tra chỗ trống được trả lại, lỗi 400/404/409 | **32/32 đạt** |
| So sánh dữ liệu thực đơn, nhóm món, bài viết, chi tiết món với bản đang chạy | **Giống hệt từng byte** |
| Tắt API rồi bật lại: đơn đã huỷ, lượt đã đổi lịch, chỗ trống còn đúng | **3/3 đạt** |
| `node scripts/api-e2e.mjs` | **21/21 đạt** |
| Chụp màn hình bằng Chrome headless: trang đơn hàng (nút huỷ), quản lý lượt đặt, đổi lịch, Về Chúng Tôi (bản đồ) | Hiển thị đúng |

---

## 7. Đối chiếu tiêu chí chấm

### 7.1 Tiêu chí cộng điểm

| # | Tiêu chí | Điểm | Đáp ứng trong dự án | Trạng thái |
| --- | --- | --- | --- | --- |
| 1 | Ít nhất 3 trang: Trang Chủ, Về Chúng Tôi, Chi tiết | 3 | `/`, `/ve-chung-toi`, `/mon-an/:slug` và 8 trang khác (mục 2) | ✅ Đạt |
| 2 | Có dùng database CSV / SQLite / Supabase | 3 | **SQLite** qua EF Core, 6 bảng; thực đơn 250 món đọc từ database (mục 3.3) | ✅ Đạt |
| 3 | Ít nhất 2 chức năng chỉnh sửa database | 2 | 5 thao tác ghi vào SQLite: đặt món (INSERT), huỷ đơn (UPDATE), đặt bàn (INSERT), đổi lịch (UPDATE), huỷ bàn (DELETE) — mục 3.2 | ✅ Đạt |
| 4 | Nhúng Google Map hoặc video YouTube | 0,5 | Google Maps ở trang Về Chúng Tôi, mục "Đường tới quán" | ✅ Đạt |
| 5 | Sử dụng các file .md | 0,5 | 6 file, mỗi file một vai trò (mục 4) | ✅ Đạt |
| 6 | Push code lên GitHub, để Public | 0,5 | Repo Public, nhánh `main` và `dev` — kiểm tra lại ngày 11/10/2026: người không đăng nhập vẫn xem được | ✅ Đạt |
| 7 | Deploy | 0,5 | Cloudflare Pages + Render (mục 6) | ✅ Đạt |

### 7.2 Tiêu chí trừ điểm

| Tiêu chí | Trạng thái | Bằng chứng |
| --- | --- | --- |
| Các trang phải liên kết với nhau | ✅ Không bị trừ | Header 5 liên kết + logo về Trang chủ, Footer 8 liên kết, nút dẫn trang trong nội dung (mục 2.6) |
| Thương hiệu thật phải có hình và logo thật | Không áp dụng | Sakura Tei là thương hiệu hư cấu |
| Thương hiệu hư cấu phải tạo logo trước và dùng đúng logo đó | ✅ Không bị trừ | Một file logo duy nhất `sakura.svg`, một component `BrandLogo` dùng ở Header và Footer, cùng file làm favicon (mục 1.3, DESIGN.md mục 2) |

---

## 8. Hạn chế và hướng phát triển

| Hạn chế | Ảnh hưởng | Hướng xử lý |
| --- | --- | --- |
| Render gói Free không có ổ đĩa bền | Mỗi lần deploy lại hoặc dịch vụ ngủ dậy, database được tạo lại từ dữ liệu mẫu, đơn và lượt đặt cũ mất | Chuyển sang Supabase/PostgreSQL (chỉ đổi `UseSqlite` trong `DatabaseSetup`) hoặc gắn Render Disk |
| Render gói Free tự ngủ | Lần mở đầu chờ khoảng 50 giây | Mở trước khi demo, hoặc nâng gói trả phí |
| Chỉ chạy được một instance | Không mở rộng ngang được | Chuyển sang PostgreSQL trước khi chạy nhiều instance |
| Chưa có trang quản trị | Bếp chưa tự chuyển trạng thái đơn trên web | Thêm trang quản trị có đăng nhập |
| Kiểm thử mới ở mức đầu–cuối | Chưa có unit test cho từng hàm | Thêm unit test cho `DishService.Search` và phần kiểm tra dữ liệu |

Lộ trình chi tiết từng phiên bản nằm trong [PRD.md — mục 10](PRD.md#10-lộ-trình-phát-hành).
