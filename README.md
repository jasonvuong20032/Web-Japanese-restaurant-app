# Sakura Tei — Website ẩm thực Nhật Bản

Bài tập web full-stack: website nhà hàng Nhật với thực đơn 250 món, đặt món giao tận nơi,
đặt bàn trực tuyến và chuyên mục bài viết.

- **Backend** — ASP.NET Core 9 Minimal API, dữ liệu trong bộ nhớ.
- **Frontend** — React 19 + TypeScript + Vite + Tailwind CSS v4, có giao diện sáng/tối.

## Chạy nhanh

Cần .NET SDK 9 và Node.js 20 trở lên. Mở hai terminal:

```bash
# 1. API — http://localhost:5187 (Swagger ở /swagger)
cd backend/SakuraTei.Api
dotnet run

# 2. Web — http://localhost:5173
cd frontend
npm install
npm run dev
```

## Tài liệu

| Tài liệu | Nội dung |
| --- | --- |
| [docs/README.md](docs/README.md) | Hướng dẫn chạy đầy đủ, cấu trúc thư mục, bảng endpoint |
| [docs/PLANNING.md](docs/PLANNING.md) | Kế hoạch chi tiết: phạm vi, kiến trúc, mô hình dữ liệu, quy tắc nghiệp vụ, cách kiểm tra |
| [docs/DEPLOY.md](docs/DEPLOY.md) | Triển khai lên server bằng Docker (kèm HTTPS tự động) hoặc chạy trực tiếp |
| [docs/DEPLOY-VERCEL.md](docs/DEPLOY-VERCEL.md) | Deploy tách đôi: frontend lên Vercel, API .NET lên Render |
| [docs/DEPLOY-CLOUDFLARE.md](docs/DEPLOY-CLOUDFLARE.md) | Frontend lên Cloudflare Pages, API .NET lên Render, nối qua proxy nên không cần CORS |
