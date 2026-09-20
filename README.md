# Sakura Tei — Website ẩm thực Nhật Bản

Bài tập web full-stack: một website nhà hàng Nhật với thực đơn 120 món, đặt món giao tận nơi,
đặt bàn trực tuyến và chuyên mục bài viết.

- **Backend** — ASP.NET Core 9 Minimal API, dữ liệu nằm trong bộ nhớ (chưa gắn cơ sở dữ liệu).
- **Frontend** — React 19 + TypeScript + Vite + Tailwind CSS v4, hỗ trợ giao diện sáng/tối.

## Chạy dự án

Cần .NET SDK 9 và Node.js 20 trở lên. Mở hai cửa sổ terminal:

```bash
# 1. API — http://localhost:5187 (Swagger ở /swagger)
cd backend/SakuraTei.Api
dotnet run

# 2. Web — http://localhost:5173
cd frontend
npm install
npm run dev
```

Vite proxy mọi request `/api` sang cổng 5187 nên môi trường phát triển không phải đụng tới CORS.
Muốn trỏ sang backend khác thì đặt biến `VITE_API_TARGET` trước khi chạy `npm run dev`.

## Cấu trúc

```
backend/SakuraTei.Api/
  Models/       Dish, Category, Order, Reservation, BlogPost
  Contracts/    DTO request/response + quy tắc kiểm tra dữ liệu
  Data/         120 món và các bài viết dựng sẵn (Seed*)
  Services/     Tra cứu thực đơn, tạo đơn, giữ chỗ đặt bàn
  Endpoints/    Khai báo route theo nhóm chức năng

frontend/src/
  types/        Kiểu TypeScript phản chiếu Contracts của backend
  lib/          Lớp gọi API, hàm định dạng tiền/ngày
  context/      Giỏ hàng (lưu localStorage) và chế độ sáng/tối
  hooks/        useAsync, useDebounced, useLockBodyScroll, usePageTitle
  components/   Layout, thẻ món, drawer giỏ hàng, bộ UI dùng chung
  pages/        Chín trang, định tuyến trong App.tsx
```

## API

| Phương thức | Đường dẫn | Công dụng |
| --- | --- | --- |
| GET | `/api/categories` | Bảy nhóm món kèm số lượng |
| GET | `/api/dishes` | Tìm và lọc thực đơn, có phân trang |
| GET | `/api/dishes/featured` | Món nổi bật cho trang chủ |
| GET | `/api/dishes/filters` | Danh sách thẻ và khoảng giá |
| GET | `/api/dishes/{slug}` | Chi tiết một món kèm món liên quan |
| POST | `/api/orders` | Tạo đơn hàng |
| GET | `/api/orders/{code}` | Tra cứu đơn theo mã `ST-XXXXXX` |
| GET | `/api/reservations/availability` | Khung giờ còn chỗ trong ngày |
| POST | `/api/reservations` | Đặt bàn |
| GET | `/api/reservations/{code}` | Tra cứu lượt đặt `RS-XXXXXX` |
| GET | `/api/posts` | Bài viết, lọc theo chuyên mục |
| GET | `/api/posts/{slug}` | Nội dung đầy đủ một bài |

Vài quy ước đáng nhớ:

- Client chỉ gửi `dishId` và `quantity` khi đặt món — **server tự tra giá** từ thực đơn.
- Đơn từ **500.000đ** được miễn phí giao, dưới mức đó tính **25.000đ**.
- Quán nhận khách **11:00–21:00**, mỗi khung 30 phút, **24 chỗ** một khung.
- Tìm kiếm bỏ dấu: gõ `ca hoi` vẫn ra `cá hồi`.

## Kiểm tra trước khi nộp

```bash
cd frontend
npm run typecheck   # kiểm tra kiểu
npm run build       # dựng bản production vào dist/
```

## Giới hạn đã biết

Đơn hàng và lượt đặt bàn chỉ nằm trong bộ nhớ tiến trình, nên **mất hết khi khởi động lại API**.
Đây là chủ ý của bài tập; muốn giữ lâu dài thì thay bốn service trong `Services/` bằng
`DbContext` của EF Core, phần còn lại không phải sửa.
