# Kế hoạch chi tiết — Website ẩm thực Nhật Sakura Tei

Tài liệu này mô tả toàn bộ quyết định thiết kế của dự án: phạm vi, kiến trúc, mô hình dữ liệu,
hợp đồng API, cấu trúc frontend và cách kiểm thử. Ai mới nhận dự án đọc file này là đủ để bắt đầu.

- **Ngày lập:** 20/09/2026
- **Nhóm thực hiện:** Vương Quang Tuấn
- **Trạng thái:** Đã hoàn thành phần bắt buộc, chạy được đầu–cuối
- **Cập nhật 09/10/2026:** dữ liệu chuyển sang SQLite; thêm huỷ đơn, đổi lịch và huỷ đặt bàn;
  nhúng bản đồ Google Maps; tách lại cấu trúc backend (`Data/`, `Common/`) và frontend (form đặt bàn,
  logo, hằng số thương hiệu)

---

## 1. Mục tiêu và phạm vi

### 1.1 Mục tiêu

Xây một website nhà hàng Nhật đầy đủ chức năng của một trang thương mại nhỏ: khách xem thực đơn,
lọc và tìm món, bỏ vào giỏ, đặt giao tận nơi, đặt bàn tại quán và đọc bài viết về ẩm thực.
Yêu cầu kỹ thuật: frontend và backend tách rời, giao tiếp qua REST API.

### 1.2 Trong phạm vi

| Nhóm chức năng | Mô tả |
| --- | --- |
| Thực đơn | 250 món chia mười hai nhóm, lọc theo nhóm/giá/độ cay/thẻ, tìm kiếm không dấu, phân trang |
| Chi tiết món | Mô tả dài, nguyên liệu, dinh dưỡng, đánh giá, gợi ý món cùng nhóm |
| Giỏ hàng | Thêm/sửa/xoá, lưu qua các lần tải trang, tính phí giao và ngưỡng miễn phí |
| Đặt món | Form thông tin người nhận, kiểm tra dữ liệu hai phía, sinh mã đơn, tra cứu lại, tự huỷ khi bếp chưa nhận |
| Đặt bàn | Xem khung giờ còn chỗ theo ngày, giữ chỗ, sinh mã đặt bàn, đổi lịch, huỷ |
| Bài viết | Danh sách có lọc chuyên mục và phân trang, trang đọc chi tiết |
| Giới thiệu | Câu chuyện quán, đội bếp, liên hệ, bản đồ Google Maps nhúng |
| Dữ liệu | SQLite qua Entity Framework Core; tự tạo bảng và nạp dữ liệu mẫu ở lần chạy đầu |
| Giao diện | Sáng/tối, responsive từ điện thoại tới màn hình rộng |

### 1.3 Ngoài phạm vi (có chủ ý)

- **Đăng nhập / tài khoản người dùng.** Không cần cho luồng đặt món của khách vãng lai; mã đơn và
  mã đặt bàn đủ để tra cứu, huỷ, đổi lịch.
- **Thanh toán trực tuyến.** Quán thu tiền khi giao; tích hợp cổng thanh toán là việc của giai đoạn sau.
- **Trang quản trị.** Thực đơn nạp từ dữ liệu mẫu (`Data/Seed`) vào database; muốn sửa thì sửa thẳng
  trong database hoặc sửa dữ liệu mẫu rồi tạo lại database.

---

## 2. Quyết định công nghệ

| Lớp | Lựa chọn | Lý do |
| --- | --- | --- |
| Backend | ASP.NET Core 9 **Minimal API** | Ít mã khung hơn Controller, đủ cho ~17 endpoint. Swagger sinh tự động. |
| Dữ liệu | **SQLite + EF Core 9** | Một file, không phải cài máy chủ database, đủ cho một quán. Đổi sang PostgreSQL/Supabase chỉ cần đổi provider trong `DatabaseSetup`. |
| Frontend | **React 19 + TypeScript** | Kiểu tĩnh giúp hợp đồng API không lệch giữa hai phía. |
| Build | **Vite 8** | Khởi động nhanh, có sẵn proxy dev để né CORS. |
| Định tuyến | **React Router 7** (data router) | Cần `ScrollRestoration` và định tuyến lồng theo layout. |
| CSS | **Tailwind CSS v4** | Token khai báo trong `@theme`, không cần file config riêng. |
| Trạng thái | **Context + useReducer** | Chỉ có giỏ hàng và theme là trạng thái toàn cục — chưa đáng dùng Redux/Zustand. |
| Gọi API | `fetch` bọc trong `lib/api.ts` | Tránh thêm phụ thuộc chỉ để gọi mười hai endpoint. |

### Vì sao không dùng thư viện quản lý dữ liệu (React Query…)

Dự án chỉ cần ba thứ: trạng thái tải, lỗi, và huỷ request cũ khi tham số đổi. Hook `useAsync`
tự viết (40 dòng) làm đủ cả ba. Đổi lại là không có cache giữa các trang — chấp nhận được ở quy mô này.

---

## 3. Kiến trúc tổng thể

```
┌──────────────────────┐        /api/*         ┌──────────────────────────┐
│  Trình duyệt         │ ───────────────────▶  │  ASP.NET Core Minimal API │
│  React SPA :5173     │ ◀───────────────────  │  :5187                    │
│                      │       JSON camelCase   │                          │
│  • CartContext       │                        │  Endpoints → Services     │
│    (localStorage)    │                        │    → SakuraDbContext      │
│  • ThemeContext      │                        │    → SQLite (App_Data)    │
└──────────────────────┘                        └──────────────────────────┘
        ▲                                                    │
        │ dev: Vite proxy /api ─────────────────────────────┘
```

Ở môi trường phát triển, Vite proxy `/api` sang cổng 5187 nên trình duyệt thấy mọi thứ cùng
một origin và **không phát sinh CORS**. Backend vẫn bật CORS cho `http://localhost:5173`
để trường hợp chạy trực tiếp không qua proxy cũng hoạt động.

### 3.1 Phân lớp backend

```
Endpoints/        Khai báo route, đọc tham số, đổi ServiceResult sang mã HTTP
   ↓
Services/         Nghiệp vụ: lọc, tính tiền, kiểm tra còn chỗ. Đăng ký singleton.
   ↓
Data/             SakuraDbContext + DatabaseSetup (tạo bảng, nạp dữ liệu mẫu)
Data/Configurations/  Ánh xạ từng bảng: khoá, chỉ mục, khoá ngoại, cột JSON
Data/Seed/        Dữ liệu mẫu: 12 nhóm, 250 món kèm ảnh, 6 bài viết — chỉ dùng lúc tạo database
Models/           Thực thể (Dish, Order, Reservation…)
Contracts/        DTO ra/vào + quy tắc kiểm tra dữ liệu tự thân
Common/           ServiceResult, ApiResults, BookingCode, VietnamTime
```

Nguyên tắc: **Endpoint không chứa nghiệp vụ**, Service không biết gì về HTTP. Nhờ vậy đổi
Minimal API sang Controller hay thêm gRPC sau này chỉ phải sửa một lớp.

Mọi thao tác ghi trả về `ServiceResult<T>` với bốn kết cục `Success`, `NotFound`, `Invalid`,
`Conflict`. Endpoint chỉ gọi `ToHttpResult(...)` để đổi sang 2xx / 404 / 400 / 409, nên mọi lỗi của
API có cùng một hình dạng và frontend chỉ cần một cách đọc lỗi.

Service là singleton còn `DbContext` thì không nên sống lâu, nên service nhận
`IDbContextFactory<SakuraDbContext>` và mỗi thao tác tự mở một context rồi bỏ.

### 3.2 Kiến trúc khi deploy

Ở môi trường chạy thật, frontend **không** được host riêng. Bản build của Vite được đặt vào
`wwwroot` của API, và chính API phục vụ luôn các file tĩnh:

```
┌───────────────────────────────────────────┐
│  SakuraTei.Api  (một process, một origin)  │
│                                            │
│   /api/*      → Endpoints → Services       │
│                 → App_Data/sakuratei.db    │
│   /health     → kiểm tra sống              │
│   còn lại     → wwwroot/index.html (SPA)   │
└───────────────────────────────────────────┘
```

Ba thứ được giải quyết cùng lúc nhờ cách này: không cần CORS (cùng origin), không cần cấu hình
rewrite ở tầng web server, và không cần biến `VITE_API_BASE_URL` (đường dẫn `/api` mặc định đã đúng).

Hai chi tiết dễ bỏ sót đã xử lý trong `Program.cs`:

- Route `/` chuyển sang Swagger **chỉ đăng ký ở Development**. Để nguyên như cũ thì trang chủ của
  bản production sẽ redirect sang Swagger thay vì mở SPA.
- `/api` có fallback riêng trả **404 JSON**. Nếu để đường dẫn API sai rơi xuống fallback SPA,
  client sẽ nhận HTML và `response.json()` vỡ bằng một lỗi không liên quan tới nguyên nhân thật.

Chi tiết các bước và bẫy khi triển khai nằm ở [DEPLOY.md](DEPLOY.md).

---

## 4. Mô hình dữ liệu

Toàn bộ dữ liệu nằm trong một file SQLite, mặc định `backend/SakuraTei.Api/App_Data/sakuratei.db`
(đổi bằng chuỗi kết nối `ConnectionStrings:Sakura`). File này **không đưa lên Git**: lần chạy đầu API
tự tạo bảng bằng `EnsureCreated` rồi nạp dữ liệu mẫu từ `Data/Seed`. Muốn về dữ liệu gốc, hoặc sau khi
đổi lược đồ, chỉ cần xoá file rồi chạy lại.

```
Categories (Slug PK) ──1───n── Dishes (Id PK, CategorySlug FK, Slug unique)
                                  └ Tags, Ingredients, Photos: cột JSON

Orders (Code PK) ──1───n── OrderLines (Id PK tự tăng, OrderCode FK)

Reservations (Code PK, chỉ mục Date + Time)        Posts (Slug PK)
```

| Bảng | Số dòng ban đầu | Ghi chú |
| --- | --- | --- |
| `Categories` | 12 | Không xoá được nhóm còn món (`ON DELETE RESTRICT`) |
| `Dishes` | 250 | Cột ẩn `DisplayOrder` giữ thứ tự món như dữ liệu mẫu |
| `Posts` | 6 | |
| `Orders`, `OrderLines` | 0 | Dòng món là bản chụp lúc đặt, cố ý **không** có khoá ngoại sang `Dishes` |
| `Reservations` | 0 | Số chỗ còn lại tính từ chính bảng này, không lưu riêng |

Thực đơn và bài viết được đọc **một lần** khi service khởi tạo rồi giữ trong bộ nhớ: 250 món đủ nhỏ, và
tìm không dấu phải làm phía ứng dụng vì SQLite không tự so khớp "ca hoi" với "cá hồi". Đơn hàng và
đặt bàn thì đọc/ghi database ở mỗi request.

### 4.1 Thực đơn

`Category` — mười hai nhóm: `sushi`, `sashimi`, `ramen`, `udon`, `bbq`, `wagyu`, `hai-san`, `tempura`, `donburi`, `mochi`, `trang-mieng`, `thuc-uong`.
Mỗi nhóm có một chữ **kanji đại diện**, dùng làm ảnh dự phòng ở frontend khi URL ảnh lỗi,
và một mã màu `accentColor` để tô badge.

`Dish` — mỗi món có `slug` (định danh trong URL, cũng là `Id`), tên tiếng Việt, tên tiếng Nhật,
tên romaji, mô tả ngắn và dài, giá, giá gốc (nếu giảm), thẻ, nguyên liệu, độ cay 0–3, calo,
thời gian chế biến, điểm đánh giá và số lượt.

Mỗi món có 1–3 ảnh riêng từ Wikimedia Commons (`Data/Seed/DishPhotos.cs`), lưu thành cột JSON
`Photos` kèm tên tác giả và giấy phép; `ImageUrl` là bản nhỏ của ảnh đầu tiên, dùng cho thẻ món.

### 4.2 Đơn hàng

`Order` gồm mã `ST-XXXXXX`, thông tin người nhận và danh sách `OrderLine` đã **chốt giá tại thời
điểm đặt**. Trạng thái đi theo vòng: `Received → Preparing → Delivering → Completed`, thêm nhánh
`Cancelled`. Khách tự huỷ được khi đơn còn ở `Received`; trạng thái lưu dạng chữ (`"Cancelled"`)
để mở database ra đọc được ngay.

### 4.3 Đặt bàn

`Reservation` gồm mã `RS-XXXXXX`, ngày, khung giờ, số khách, `CreatedAt` và `UpdatedAt` (lần đổi
lịch gần nhất). Số chỗ đã đặt của một khung giờ là **tổng `PartySize`** của các lượt cùng ngày, cùng
giờ — nên đổi lịch hay huỷ (xoá dòng) là chỗ trống tự đúng theo, không phải cập nhật bộ đếm nào.

---

## 5. Quy tắc nghiệp vụ

Đây là phần dễ làm lệch giữa hai phía nhất, nên ghi rõ ra đây.

| Quy tắc | Giá trị | Nơi cài đặt |
| --- | --- | --- |
| Phí giao hàng | 25.000đ | `OrderService`, lặp lại ở `CartContext` để hiện trước |
| Miễn phí giao từ | 500.000đ | như trên |
| Số lượng mỗi món | 1–50 | `CreateOrderRequest.Validate` + `CartContext` |
| Số loại món mỗi đơn | tối đa 50 | `CreateOrderRequest.Validate` |
| Giờ nhận khách | 11:00–21:00, mỗi 30 phút | `ReservationService` |
| Sức chứa một khung | 24 chỗ | `ReservationService` |
| Số khách một lượt đặt | 1–12 | `ReservationRequest.Validate` |
| Đặt bàn trước tối đa | 60 ngày | như trên |
| Số điện thoại | 10 chữ số, bắt đầu bằng 0 | `Validation.IsVietnamesePhone` |
| Huỷ đơn | Chỉ khi đơn còn ở `Received` (bếp chưa nhận làm) | `OrderService.Cancel` |
| Đổi lịch / huỷ đặt bàn | Chỉ trước giờ hẹn; đổi lịch kiểm tra lại chỗ trống, **không tính chỗ của chính lượt đó** | `ReservationService.Update`, `Delete` |
| "Hôm nay" | Theo giờ Việt Nam (UTC+7), không theo giờ máy chủ | `VietnamTime` |

> **Vì sao cần `VietnamTime`:** Render và hầu hết máy chủ chạy theo UTC. Dùng `DateTimeOffset.Now`
> thì từ 0:00 tới 7:00 sáng giờ Việt Nam máy chủ vẫn tưởng là hôm qua, và khách đặt được bàn cho
> một ngày đã qua.

> **Lưu ý khi sửa:** phí giao và ngưỡng miễn phí nằm ở *hai* nơi (server tính thật, client hiện
> trước cho khách thấy). Đổi một bên mà quên bên kia thì con số trên giỏ hàng sẽ lệch với hoá đơn.
> Hằng số phía client nằm ở đầu `frontend/src/context/CartContext.tsx`.

### Giá luôn do server quyết

Client gửi lên **chỉ `dishId` và `quantity`**. Server tự tra giá từ thực đơn rồi mới tính tiền.
Người dùng sửa giá trong localStorage cũng không ảnh hưởng tới hoá đơn.

---

## 6. Hợp đồng API

Toàn bộ JSON dùng **camelCase**, tiếng Việt giữ nguyên dấu (không escape `\uXXXX`).
Trường `null` bị lược khỏi phản hồi — nên phía TypeScript khai báo là tuỳ chọn (`originalPrice?`).

### 6.1 Danh sách endpoint

| Phương thức | Đường dẫn | Trả về |
| --- | --- | --- |
| GET | `/api/categories` | `Category[]` kèm `dishCount` |
| GET | `/api/dishes` | `PagedResult<DishSummary>` |
| GET | `/api/dishes/featured?take=` | `DishSummary[]` |
| GET | `/api/dishes/filters` | `{ tags, minPrice, maxPrice }` |
| GET | `/api/dishes/{slug}` | `DishDetail` (kèm 4 món liên quan) · 404 |
| POST | `/api/orders` | 201 `Order` · 400 ValidationProblem |
| GET | `/api/orders/{code}` | `Order` · 404 |
| PATCH | `/api/orders/{code}/cancel` | `Order` (trạng thái `Cancelled`) · 404 · 409 |
| GET | `/api/reservations/availability?date=` | `{ date, slots[] }` |
| POST | `/api/reservations` | 201 `Reservation` · 400 |
| GET | `/api/reservations/{code}` | `Reservation` · 404 |
| PUT | `/api/reservations/{code}` | `Reservation` (có `updatedAt`) · 400 · 404 · 409 |
| DELETE | `/api/reservations/{code}` | 204 · 404 · 409 |
| GET | `/api/posts?topic=&page=&pageSize=` | `PagedResult<BlogPost>` |
| GET | `/api/posts/topics` | `string[]` |
| GET | `/api/posts/latest?take=` | `BlogPost[]` |
| GET | `/api/posts/{slug}` | `BlogPost` · 404 |

Thêm `GET /health` để kiểm tra API sống, đang dùng SQLite và đã nạp đủ 250 món.

`PUT` nhận **toàn bộ** thông tin lượt đặt (cùng hình dạng với `POST`), không nhận từng trường lẻ —
form đổi lịch luôn gửi đủ, và nhờ vậy hai thao tác dùng chung một bộ kiểm tra dữ liệu.

### 6.2 Tham số lọc thực đơn

`category`, `q`, `minPrice`, `maxPrice`, `maxSpicy`, `tag`, `sort`, `page`, `pageSize`.

`sort` nhận `price_asc`, `price_desc`, `rating`, `popular`; giá trị khác được hiểu là mặc định
(món nổi bật lên trước, rồi tới điểm đánh giá). Frontend dùng quy ước `default` và **không gửi
tham số này** khi ở chế độ mặc định, cho URL gọn.

### 6.3 Tìm kiếm không dấu

`DishService` tính sẵn một chuỗi đã bỏ dấu cho mỗi món (tên, romaji, mô tả, thẻ, nguyên liệu)
ngay lúc khởi tạo. Nhờ vậy gõ `ca hoi` vẫn ra `cá hồi`, và không phải bỏ dấu lại 250 lần mỗi request.
Chữ `đ/Đ` phải thay tay vì nó không phải là "d cộng dấu" trong Unicode.

### 6.4 Hình dạng lỗi

Lỗi 400 theo chuẩn RFC 7807:

```json
{
  "title": "Dữ liệu gửi lên không hợp lệ",
  "status": 400,
  "errors": {
    "CustomerName": ["Họ tên phải có ít nhất 2 ký tự."],
    "Items": ["Giỏ hàng đang trống."]
  }
}
```

Khoá lỗi là **PascalCase** (lấy từ `nameof`). Lớp `ApiError` phía client đổi chữ đầu về thường
để khớp tên field trên form, nhờ đó thông báo hiện đúng dưới ô nhập tương ứng.
Lỗi ở dòng món (`Items[0].DishId`) không gắn được vào ô nào nên hiện ở đầu form.

Lỗi 404 và 409 dùng dạng rút gọn `{ "title", "detail", "status" }`; frontend hiện `detail` cho người
dùng. 409 nghĩa là yêu cầu hợp lệ nhưng trạng thái hiện tại không cho phép, ví dụ huỷ một đơn đã huỷ
hoặc đổi lịch một lượt đã qua giờ hẹn.

---

## 7. Thiết kế frontend

### 7.1 Sơ đồ trang

| Đường dẫn | Trang | Ghi chú |
| --- | --- | --- |
| `/` | Trang chủ | Hero, mười hai nhóm, món nổi bật, ba bài mới |
| `/thuc-don` | Thực đơn | Bộ lọc đồng bộ query string |
| `/mon-an/:slug` | Chi tiết món | Kèm món liên quan |
| `/dat-mon` | Đặt món | Form + tóm tắt đơn |
| `/dat-ban` | Đặt bàn | Chọn ngày → khung giờ → thông tin (`ReservationForm`) |
| `/dat-ban/:code` | Quản lý lượt đặt | Xem chi tiết, nút đổi lịch, nút huỷ |
| `/dat-ban/:code/doi-lich` | Đổi lịch | Cùng `ReservationForm`, điền sẵn dữ liệu cũ |
| `/tra-cuu` `/tra-cuu/:code` | Tra cứu đơn | Ô nhập nhận cả mã `RS-` và chuyển sang trang đặt bàn; trang đơn có nút huỷ |
| `/cau-chuyen` `/cau-chuyen/:slug` | Bài viết | Danh sách và trang đọc |
| `/ve-chung-toi` | Giới thiệu | Nội dung tĩnh + bản đồ Google Maps nhúng |
| `*` | 404 | |

Đổi lịch có đường dẫn riêng thay vì một trạng thái ẩn trong trang, để nút Back của trình duyệt quay
về đúng trang xem và có thể gửi thẳng link đổi lịch cho khách.

### 7.1b Thành phần dùng chung đáng chú ý

| Thành phần | Vì sao tách riêng |
| --- | --- |
| `ReservationForm` | Đặt mới và đổi lịch dùng chung một form. Khi đổi lịch, khung giờ cũ được cộng trả lại chỗ của chính lượt đó, nếu không khách sẽ thấy thiếu đúng bằng số người của mình |
| `ConfirmAction` | Thao tác không hoàn tác được (huỷ đơn, huỷ bàn) phải hỏi lại tại chỗ; lỗi từ API hiện ngay dưới câu hỏi |
| `BrandLogo` | Logo chỉ được vẽ ở một nơi, Header và Footer dùng chung — đúng quy tắc "một logo duy nhất" |
| `AnchorButton` | Liên kết `tel:`, `mailto:`, Google Maps trông như nút mà không lồng `<button>` trong `<a>` (HTML sai) |
| `lib/brand.ts` | Tên quán, địa chỉ, hotline, giờ mở cửa, URL bản đồ — đổi một dòng là đổi khắp website |

### 7.2 Hệ màu và chế độ tối

Thay vì rắc `dark:` lên từng class, dự án khai báo **token ngữ nghĩa** trong `@theme`
(`--color-surface`, `--color-ink`, `--color-brand`…) rồi **ghi đè chính các biến đó** trong khối `.dark`:

```css
@theme { --color-surface: #fbf7f0; --color-ink: #241f1c; }
.dark  { --color-surface: #14110f; --color-ink: #f2eae0; }
```

Utility của Tailwind v4 biên dịch thành `var(--color-surface)` nên tự đổi theo cascade.
Kết quả: `bg-surface text-ink` đúng ở cả hai chế độ, chỉ vài chỗ đặc biệt mới cần `dark:`.

Chế độ tối được gắn class lên `<html>` bằng **script inline trong `index.html`**, chạy trước khi
React dựng cây — nếu để React làm thì trang sẽ nháy trắng một nhịp lúc tải.

### 7.3 Quản lý trạng thái

- **`CartContext`** — `useReducer` + ghi `localStorage`. Lúc đọc lại có lọc từng phần tử
  (`isCartItem`), vì dữ liệu cũ hoặc bị sửa tay có thể thiếu trường và làm vỡ trang.
- **`ThemeContext`** — đọc trạng thái ban đầu **từ DOM** (class mà script inline đã gắn),
  không đọc lại localStorage để tránh hai nguồn sự thật.
- Mọi thao tác đọc/ghi `localStorage` đều bọc `try/catch`: trình duyệt ở chế độ riêng tư
  có thể ném lỗi, và khi đó trang vẫn phải chạy.

### 7.4 Bộ lọc gắn với URL

Toàn bộ trạng thái lọc của trang thực đơn nằm trong query string, không nằm trong `useState`.
Đổi lấy được ba thứ: chia sẻ link ra đúng kết quả, nút back của trình duyệt hoạt động đúng,
và tải lại trang không mất bộ lọc. Ô tìm kiếm có **debounce 350ms** trước khi ghi vào URL để
không tạo một mục lịch sử cho mỗi phím gõ (dùng `replace: true`).

### 7.5 Xử lý tải và lỗi

Hook `useAsync` huỷ request cũ qua `AbortController` khi tham số đổi, nhờ vậy kết quả về muộn
không ghi đè kết quả mới hơn. Mỗi khối dữ liệu có ba trạng thái hiển thị: **skeleton** đúng tỉ lệ
thẻ thật (để trang không giật khi dữ liệu về), **ErrorState** kèm nút thử lại, và **EmptyState**
khi không có kết quả.

### 7.6 Khả năng tiếp cận

- Mọi nút chỉ có biểu tượng đều có `aria-label`.
- Nút lọc dùng `aria-pressed`, phân trang dùng `aria-current="page"`.
- Ô nhập lỗi có `aria-invalid` và `aria-describedby` trỏ tới thông báo, thông báo mang `role="alert"`.
- Viền focus thống nhất qua `:focus-visible`, đủ tương phản trên cả nền sáng lẫn tối.
- Tôn trọng `prefers-reduced-motion`: tắt hiệu ứng khi người dùng yêu cầu.

---

## 8. Giới hạn đã biết

**Database trên Render gói Free không bền.** Gói Free không có ổ đĩa lưu lâu dài: mỗi lần deploy lại,
hoặc khi dịch vụ ngủ (15 phút không ai truy cập) rồi thức dậy, file SQLite được tạo lại từ dữ liệu mẫu
và các đơn, lượt đặt cũ mất. Ở máy và khi chạy bằng Docker Compose (có volume) thì dữ liệu còn mãi.
Muốn bền trên production: gắn Render Disk (trả phí) hoặc chuyển sang Supabase/PostgreSQL — chỉ phải
đổi `UseSqlite` trong `DatabaseSetup`, phần còn lại giữ nguyên.

**Chỉ chạy một instance.** Khoá chống đặt trùng chỗ (`ReservationService`) nằm trong process, và
SQLite là một file cục bộ. Chạy hai instance là hai database riêng.

**Ảnh món lấy từ Wikimedia Commons qua URL.** Không có ảnh nào nằm trong repo. Mỗi món có 1–3 ảnh
riêng (`Data/Seed/DishPhotos.cs`), không ảnh nào dùng chung giữa hai món; trang chi tiết hiện bộ ảnh kèm
tên tác giả và giấy phép theo yêu cầu của Creative Commons. Ảnh bìa bảy nhóm cũ và ảnh bài viết vẫn
lấy từ Unsplash. Mất mạng thì ảnh không tải được — nên component `SmartImage` hiển thị chữ kanji
của nhóm món làm ảnh dự phòng.

**Kiểm thử tự động mới ở mức đầu–cuối.** `scripts/api-e2e.mjs` gọi thẳng API để thử toàn bộ luồng
thêm/sửa/xoá; chưa có unit test cho từng hàm.

---

## 9. Cách kiểm tra

### 9.1 Kiểm tra tĩnh

```bash
cd frontend
npm run typecheck    # TypeScript strict, không lỗi
npm run build        # tsc -b && vite build
```

### 9.2 Kiểm tra API

Cách nhanh nhất là chạy script kiểm thử đầu–cuối (21 bước: thêm/đọc/huỷ đơn, thêm/đọc/sửa/xoá đặt
bàn, kiểm tra chỗ trống được trả lại). Script chỉ chạy với `localhost` vì nó tạo dữ liệu thử:

```bash
node scripts/api-e2e.mjs            # mặc định http://localhost:5187
```

Hoặc thử tay từng lệnh:

```bash
curl http://localhost:5187/health
# → database "SQLite", dishCount 250, categoryCount 12

curl "http://localhost:5187/api/dishes?q=ca%20hoi"     # tìm không dấu
curl -X POST http://localhost:5187/api/orders \
  -H "Content-Type: application/json" \
  -d '{"customerName":"A","phone":"123","address":"x","items":[]}'
# → 400 kèm lỗi theo từng trường
```

Swagger UI ở `http://localhost:5187/swagger` liệt kê đủ endpoint để thử tay.

### 9.3 Kiểm tra giao diện

Đã dựng và kiểm tra nội dung thật của từng trang bằng Chrome headless. Các điểm cần xác nhận
khi sửa về sau:

| Trang | Cần thấy |
| --- | --- |
| `/` | Hero, 12 nhóm món, 8 món nổi bật, 3 bài viết |
| `/thuc-don` | 12 thẻ món, sidebar lọc, phân trang, số món phù hợp |
| `/mon-an/tonkotsu-ramen` | Tên món, nguyên liệu, món liên quan, nút thêm giỏ |
| `/dat-ban` | 21 khung giờ từ 11:00 tới 21:00, số chỗ còn lại; ở 1280px phần tóm tắt bên phải không bị đẩy khỏi màn hình |
| `/dat-ban/RS-…` | Thông tin lượt đặt, nút "Đổi ngày, giờ hoặc số khách", nút "Huỷ đặt bàn" hỏi lại trước khi xoá |
| `/dat-ban/RS-…/doi-lich` | Form điền sẵn dữ liệu cũ, khung giờ cũ được chọn và vẫn đủ chỗ |
| `/dat-mon` (giỏ trống) | Trạng thái rỗng kèm nút về thực đơn |
| `/tra-cuu/ST-…` | Các bước trạng thái; nút "Huỷ đơn" khi đơn còn ở bước đầu |
| `/tra-cuu/ST-SAI` | Thông báo không tìm thấy, không phải trang trắng |
| `/ve-chung-toi` | Mục "Đường tới quán" với bản đồ Google Maps và nút mở Google Maps |
| `/cau-chuyen/:slug` | Tiêu đề, các tiểu mục `##` đã thành `<h2>` |

Lưu ý: Chrome headless không vẽ được ô bản đồ của Google Maps (cần WebGL), nên bản đồ phải kiểm tra
bằng trình duyệt thật. Headless cũng không thu nhỏ cửa sổ dưới khoảng 500px, nên kiểm tra giao diện
điện thoại bằng chế độ thiết bị của DevTools thay vì `--window-size`.

---

## 10. Hướng phát triển tiếp

Xếp theo thứ tự nên làm:

1. **Dữ liệu bền trên production.** Chuyển sang Supabase/PostgreSQL (đổi `UseSqlite` thành
   `UseNpgsql`) hoặc gắn Render Disk. Kèm theo nên dùng migration thay cho `EnsureCreated`.
2. **Unit test.** Ưu tiên `DishService.Search` (nhiều nhánh lọc nhất), phần `Validate` của hai
   request và `ReservationService.CheckCapacity` — đều dễ test khi dùng SQLite trong bộ nhớ.
3. **Trang quản trị** để bếp chuyển trạng thái đơn (`Preparing`, `Delivering`…) và sửa thực đơn
   ngay trên web.
4. **Tài khoản khách hàng** để xem lại lịch sử đơn, thay cho việc phải nhớ mã đơn.
5. **Tối ưu tải:** tách bundle theo route bằng `React.lazy`, và tự host ảnh có kích thước phù hợp.
