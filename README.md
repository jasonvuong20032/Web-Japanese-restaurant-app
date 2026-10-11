# Sakura Tei — Website ẩm thực Nhật Bản

<img src="frontend/public/sakura.svg" alt="Logo Sakura Tei" width="56" align="left" />

Bài tập web full-stack: website nhà hàng Nhật với thực đơn 250 món, đặt món giao tận nơi,
đặt bàn trực tuyến và chuyên mục bài viết. **Sakura Tei** là thương hiệu hư cấu, logo tự thiết kế.

| | |
| --- | --- |
| 🌐 **Website** | <https://web-japanese-restaurant-app.pages.dev> |
| 📄 **Báo cáo đồ án** | [docs/BAO-CAO.md](docs/BAO-CAO.md) · bản Word: [docs/BAO-CAO-Sakura-Tei.docx](docs/BAO-CAO-Sakura-Tei.docx) |
| 🧩 **Hạ tầng** | Cloudflare Pages (frontend) + Render (API .NET + SQLite) |

> Lần đầu mở web có thể phải chờ khoảng 50 giây vì API trên Render gói Free tự ngủ khi không có ai
> truy cập. Chờ một phút rồi tải lại trang là chạy.

- **Backend** — ASP.NET Core 9 Minimal API + Entity Framework Core, dữ liệu lưu trong **SQLite**.
- **Frontend** — React 19 + TypeScript + Vite + Tailwind CSS v4, có giao diện sáng/tối.

## Chạy ở máy

Cần .NET SDK 9 và Node.js 20 trở lên — không cần cài database hay Docker. Mở hai terminal:

```bash
# 1. API — http://localhost:5187 (Swagger ở /swagger)
cd backend/SakuraTei.Api
dotnet run

# 2. Web — http://localhost:5173
cd frontend
npm install
npm run dev
```

Lần chạy đầu, API tự tạo database `backend/SakuraTei.Api/App_Data/sakuratei.db` và nạp 12 nhóm,
250 món, 6 bài viết. Muốn về dữ liệu gốc thì tắt API, xoá file đó rồi chạy lại. Mở file bằng
[DB Browser for SQLite](https://sqlitebrowser.org) nếu muốn xem bảng.

Vite chuyển mọi request `/api` sang cổng 5187 nên môi trường phát triển không phải đụng tới CORS.

## Kiểm tra trước khi nộp

```bash
cd frontend
npm run typecheck          # kiểm tra kiểu TypeScript
npm run build              # dựng bản production vào dist/

# Với API đang chạy ở cổng 5187: thử toàn bộ luồng thêm/sửa/xoá (21 bước)
node scripts/api-e2e.mjs
```

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

frontend/
  src/          Trang (pages/), thành phần (components/), gọi API (lib/api.ts),
                thông tin thương hiệu (lib/brand.ts)
  functions/    Pages Function chuyển /api/* sang API trên Render

scripts/api-e2e.mjs   Kiểm thử đầu–cuối API, chỉ chạy với localhost
Dockerfile            Render dùng file này để build API (không cần cài Docker ở máy)
```

## Tài liệu

| Tài liệu | Nội dung |
| --- | --- |
| [docs/BAO-CAO.md](docs/BAO-CAO.md) | **Báo cáo nộp bài:** thương hiệu, logo, các trang, chức năng, database, file .md, thứ tự prompt, deploy, đối chiếu tiêu chí chấm |
| [docs/BAO-CAO-Sakura-Tei.docx](docs/BAO-CAO-Sakura-Tei.docx) | Cùng báo cáo ở dạng **Word** (35 trang): trang bìa, mục lục, ảnh chụp màn hình website thật, link deploy và GitHub |
| [docs/PRD.md](docs/PRD.md) | Yêu cầu sản phẩm: mục tiêu, người dùng, user story, tiêu chí chấp nhận, lộ trình |
| [docs/DESIGN.md](docs/DESIGN.md) | Nhận diện và thiết kế: logo, màu, chữ, thành phần giao diện, bố cục từng trang |
| [docs/PLANNING.md](docs/PLANNING.md) | Kế hoạch kỹ thuật: kiến trúc, database, hợp đồng API, quy tắc nghiệp vụ, cách kiểm thử |
| [docs/DEPLOY-CLOUDFLARE.md](docs/DEPLOY-CLOUDFLARE.md) | Deploy: frontend lên Cloudflare Pages, API lên Render, nối qua proxy nên không cần CORS |

## Nhánh

| Nhánh | Vai trò |
| --- | --- |
| `main` | Bản ổn định — Cloudflare Pages và Render đều build từ nhánh này |
| `dev` | Nhánh phát triển; tính năng xong và chạy ổn mới gộp sang `main` |
| `feature/*` | Từng tính năng riêng, mở Pull Request vào `dev` |
