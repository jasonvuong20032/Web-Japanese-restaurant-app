# Sakura Tei — Website ẩm thực Nhật Bản

Bài tập web full-stack: một website nhà hàng Nhật với thực đơn 250 món, đặt món giao tận nơi,
đặt bàn trực tuyến và chuyên mục bài viết.

- **Backend** — ASP.NET Core 9 Minimal API + Entity Framework Core, dữ liệu lưu trong **SQLite**.
- **Frontend** — React 19 + TypeScript + Vite + Tailwind CSS v4, hỗ trợ giao diện sáng/tối.

## Chạy dự án

Cần .NET SDK 9 và Node.js 20 trở lên. **Không cần cài database**: SQLite chỉ là một file. Mở hai
cửa sổ terminal:

```bash
# 1. API — http://localhost:5187 (Swagger ở /swagger)
cd backend/SakuraTei.Api
dotnet run

# 2. Web — http://localhost:5173
cd frontend
npm install
npm run dev
```

Lần chạy đầu, API tự tạo `backend/SakuraTei.Api/App_Data/sakuratei.db` và nạp 12 nhóm, 250 món,
6 bài viết. Muốn về dữ liệu gốc thì tắt API, xoá file đó rồi chạy lại. Mở file bằng
[DB Browser for SQLite](https://sqlitebrowser.org) nếu muốn xem bảng.

Vite proxy mọi request `/api` sang cổng 5187 nên môi trường phát triển không phải đụng tới CORS.
Muốn trỏ sang backend khác thì đặt biến `VITE_API_TARGET` trước khi chạy `npm run dev`.

## Cấu trúc

```
backend/SakuraTei.Api/
  Models/       Dish, Category, Order, Reservation, BlogPost
  Contracts/    DTO request/response + quy tắc kiểm tra dữ liệu
  Data/         SakuraDbContext, DatabaseSetup (tạo bảng, nạp dữ liệu mẫu)
    Configurations/  Ánh xạ từng bảng: khoá, chỉ mục, khoá ngoại, cột JSON
    Seed/            Dữ liệu mẫu: 250 món kèm ảnh, 12 nhóm, 6 bài viết
  Services/     Thực đơn, tạo/huỷ đơn, đặt/đổi lịch/huỷ bàn
  Endpoints/    Khai báo route theo nhóm chức năng
  Common/       ServiceResult, ApiResults, BookingCode, VietnamTime
  App_Data/     File sakuratei.db — API tự tạo, không đưa lên Git

frontend/src/
  types/        Kiểu TypeScript phản chiếu Contracts của backend
  lib/          Lớp gọi API, thông tin thương hiệu (brand.ts), hàm định dạng tiền/ngày
  context/      Giỏ hàng (lưu localStorage) và chế độ sáng/tối
  hooks/        useAsync, useDebounced, useLockBodyScroll, usePageTitle
  components/   Layout + logo, thẻ món, giỏ hàng, form đặt bàn, bộ UI dùng chung
  pages/        Mười trang và trang 404, định tuyến trong App.tsx

scripts/
  api-e2e.mjs   Kiểm thử đầu–cuối API (thêm/sửa/xoá), chỉ chạy với localhost
```

## API

| Phương thức | Đường dẫn | Công dụng |
| --- | --- | --- |
| GET | `/api/categories` | Mười hai nhóm món kèm số lượng |
| GET | `/api/dishes` | Tìm và lọc thực đơn, có phân trang |
| GET | `/api/dishes/featured` | Món nổi bật cho trang chủ |
| GET | `/api/dishes/filters` | Danh sách thẻ và khoảng giá |
| GET | `/api/dishes/{slug}` | Chi tiết một món kèm món liên quan |
| POST | `/api/orders` | Tạo đơn hàng |
| GET | `/api/orders/{code}` | Tra cứu đơn theo mã `ST-XXXXXX` |
| PATCH | `/api/orders/{code}/cancel` | Huỷ đơn khi bếp chưa nhận làm |
| GET | `/api/reservations/availability` | Khung giờ còn chỗ trong ngày |
| POST | `/api/reservations` | Đặt bàn |
| GET | `/api/reservations/{code}` | Tra cứu lượt đặt `RS-XXXXXX` |
| PUT | `/api/reservations/{code}` | Đổi lịch: ngày, giờ, số khách, thông tin liên hệ |
| DELETE | `/api/reservations/{code}` | Huỷ đặt bàn, trả lại chỗ |
| GET | `/api/posts` | Bài viết, lọc theo chuyên mục |
| GET | `/api/posts/{slug}` | Nội dung đầy đủ một bài |

Vài quy ước đáng nhớ:

- Client chỉ gửi `dishId` và `quantity` khi đặt món — **server tự tra giá** từ thực đơn.
- Đơn từ **500.000đ** được miễn phí giao, dưới mức đó tính **25.000đ**.
- Quán nhận khách **11:00–21:00**, mỗi khung 30 phút, **24 chỗ** một khung.
- Chỉ huỷ được đơn còn ở trạng thái `Received`; chỉ đổi/huỷ được lượt đặt bàn **trước giờ hẹn**.
- Tìm kiếm bỏ dấu: gõ `ca hoi` vẫn ra `cá hồi`.
- Lỗi luôn có một hình dạng: 400 kèm lỗi từng trường, 404/409 kèm `detail` để hiện cho người dùng.

## Kiểm tra trước khi nộp

```bash
cd frontend
npm run typecheck   # kiểm tra kiểu
npm run build       # dựng bản production vào dist/

# Với API đang chạy ở cổng 5187:
node scripts/api-e2e.mjs
```

## Triển khai

Bản đang chạy: **Cloudflare Pages** (frontend) + **Render** (API) — xem
[DEPLOY-CLOUDFLARE.md](DEPLOY-CLOUDFLARE.md). Render build API từ `Dockerfile` trên máy chủ của họ,
nên **không cần cài Docker ở máy**. Cách khác: frontend lên Vercel ([DEPLOY-VERCEL.md](DEPLOY-VERCEL.md)),
hoặc tự dựng trên server riêng ([DEPLOY.md](DEPLOY.md)).

## Giới hạn đã biết

Render gói Free không có ổ đĩa bền: mỗi lần deploy lại hoặc khi dịch vụ ngủ dậy, file SQLite được tạo
lại từ dữ liệu mẫu và đơn hàng, lượt đặt bàn cũ mất. Ở máy thì dữ liệu còn mãi. Muốn bền trên
production thì chuyển sang Supabase/PostgreSQL — chỉ đổi `UseSqlite` trong `Data/DatabaseSetup.cs`.
Chi tiết ở mục 8 của [PLANNING.md](PLANNING.md).
