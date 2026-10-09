# Sakura Tei — Website ẩm thực Nhật Bản

<img src="frontend/public/sakura.svg" alt="Logo Sakura Tei" width="56" align="left" />

Bài tập web full-stack: website nhà hàng Nhật với thực đơn 250 món, đặt món giao tận nơi,
đặt bàn trực tuyến và chuyên mục bài viết. **Sakura Tei** là thương hiệu hư cấu, logo tự thiết kế.

| | |
| --- | --- |
| 🌐 **Website** | <https://web-japanese-restaurant-app.pages.dev> |
| 📄 **Báo cáo đồ án** | [docs/BAO-CAO.md](docs/BAO-CAO.md) |
| 🧩 **Hạ tầng** | Cloudflare Pages (frontend) + Render (API .NET, Docker) |

> Lần đầu mở web có thể phải chờ khoảng 50 giây vì API trên Render gói Free tự ngủ khi không có ai
> truy cập. Chờ một phút rồi tải lại trang là chạy.

- **Backend** — ASP.NET Core 9 Minimal API + Entity Framework Core, dữ liệu lưu trong **SQLite**.
- **Frontend** — React 19 + TypeScript + Vite + Tailwind CSS v4, có giao diện sáng/tối.

## Chạy nhanh

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

Lần chạy đầu, API tự tạo database `backend/SakuraTei.Api/App_Data/sakuratei.db` và nạp 250 món.
Kiểm thử toàn bộ luồng thêm/sửa/xoá khi API đang chạy: `node scripts/api-e2e.mjs`.

## Tài liệu

| Tài liệu | Nội dung |
| --- | --- |
| [docs/BAO-CAO.md](docs/BAO-CAO.md) | **Báo cáo nộp bài:** thương hiệu, logo, các trang, chức năng, file .md, thứ tự prompt, deploy, đối chiếu tiêu chí chấm |
| [docs/PRD.md](docs/PRD.md) | Yêu cầu sản phẩm: mục tiêu, người dùng, user story, tiêu chí chấp nhận, lộ trình |
| [docs/DESIGN.md](docs/DESIGN.md) | Nhận diện và thiết kế: logo, màu, chữ, thành phần giao diện, bố cục từng trang |
| [docs/PLANNING.md](docs/PLANNING.md) | Kế hoạch kỹ thuật: kiến trúc, mô hình dữ liệu, hợp đồng API, quy tắc nghiệp vụ |
| [docs/README.md](docs/README.md) | Hướng dẫn chạy đầy đủ, cấu trúc thư mục, bảng endpoint |
| [docs/DEPLOY.md](docs/DEPLOY.md) | Triển khai lên server bằng Docker (kèm HTTPS tự động) hoặc chạy trực tiếp |
| [docs/DEPLOY-VERCEL.md](docs/DEPLOY-VERCEL.md) | Deploy tách đôi: frontend lên Vercel, API .NET lên Render |
| [docs/DEPLOY-CLOUDFLARE.md](docs/DEPLOY-CLOUDFLARE.md) | Frontend lên Cloudflare Pages, API .NET lên Render, nối qua proxy nên không cần CORS — **cách đang dùng** |

## Nhánh

| Nhánh | Vai trò |
| --- | --- |
| `main` | Bản ổn định, dùng để deploy và nộp bài |
| `dev` | Nhánh phát triển; tính năng xong và chạy ổn mới gộp sang `main` |
| `feature/*` | Từng tính năng riêng, mở Pull Request vào `dev` |
