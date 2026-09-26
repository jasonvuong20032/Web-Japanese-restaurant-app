# Deploy lên Cloudflare Pages

> **Đọc phần này trước.** Cloudflare Pages (và Workers) **không chạy được .NET**, nên Pages chỉ
> host **frontend**. API ASP.NET Core vẫn phải nằm ở nơi khác — hướng dẫn này dùng Render gói
> Free, dựng từ `Dockerfile` có sẵn. Tổng chi phí: 0 đồng.

Khác với bản Vercel, frontend ở đây **không gọi thẳng sang Render**. Một Pages Function
(`frontend/functions/api/[[path]].ts`) đứng giữa và chuyển tiếp mọi request `/api/*`:

```
  Trình duyệt ──▶ Cloudflare Pages (sakura-tei.pages.dev)
                   ├─ /, /thuc-don, /assets/…  → file tĩnh trong dist/
                   └─ /api/*  → Pages Function ──HTTPS──▶ Render (sakura-api.onrender.com)
```

Nhờ vậy trình duyệt chỉ thấy **một origin**:

- **Không phải cấu hình CORS** trên API.
- Frontend giữ đường dẫn mặc định `/api` — **không đặt** `VITE_API_BASE_URL`, không phải build lại
  frontend khi đổi địa chỉ API.
- Các bản **preview** của từng nhánh cũng gọi được API (bản Vercel thì bị CORS chặn).

---

## Phần A — Đưa API lên Render

1. Vào [render.com](https://render.com), đăng nhập bằng GitHub.
2. **New → Web Service**, chọn repo `Web-Japanese-restaurant-app`.
3. Cấu hình:

   | Mục | Giá trị |
   | --- | --- |
   | Branch | nhánh chứa code mới nhất (mặc định là nhánh chính) |
   | Language | `Docker` |
   | Dockerfile Path | `./Dockerfile` |
   | Docker Build Context Directory | `.` |
   | Instance Type | `Free` |
   | Health Check Path | `/health` |

   **Không cần** biến `Cors__AllowedOrigins__0` như bản Vercel — proxy đã lo phần đó.

4. Bấm **Deploy**. Lần đầu mất 5–10 phút. Xong thì kiểm tra:

   ```bash
   curl https://sakura-api.onrender.com/health
   # → {"status":"healthy","dishCount":250,"categoryCount":12,...}
   ```

Ghi lại URL này (không kèm `/api`) — bước sau cần đến.

---

## Phần B — Đưa frontend lên Cloudflare Pages

1. Vào [dash.cloudflare.com](https://dash.cloudflare.com) → **Workers & Pages** → **Create** →
   thẻ **Pages** → **Connect to Git**, chọn repo `Web-Japanese-restaurant-app`.
2. Cấu hình build — **quan trọng nhất là Root directory**:

   | Mục | Giá trị |
   | --- | --- |
   | Production branch | nhánh chứa code mới nhất |
   | Framework preset | `React (Vite)` (hoặc `None`) |
   | Build command | `npm run build` |
   | Build output directory | `dist` |
   | **Root directory** | **`frontend`** |

   Để trống Root directory thì Cloudflare tìm `package.json` ở gốc repo và build hỏng; thư mục
   `functions/` cũng phải nằm trong Root directory thì mới được nhận.

3. **Environment variables** — thêm cho cả **Production** lẫn **Preview**:

   | Tên | Giá trị |
   | --- | --- |
   | `API_ORIGIN` | `https://sakura-api.onrender.com` |

   **Không** có `/api` ở cuối, **không** có dấu `/` thừa. Phiên bản Node đã được chốt là 22 bằng
   file `frontend/.node-version`; nếu build vẫn báo sai phiên bản thì thêm biến `NODE_VERSION` = `22`.

4. **Save and Deploy**. Vài phút sau có địa chỉ dạng `https://sakura-tei.pages.dev`.

Pages tự lo hai việc mà bản Vercel phải khai báo trong `vercel.json`:

- **Chuyển trang kiểu SPA.** `dist/` không có `404.html`, nên Pages coi đây là single-page app và trả
  `index.html` cho mọi đường dẫn — vào thẳng `/thuc-don` hay F5 giữa trang vẫn chạy.
- **Cache.** HTML mặc định không cache ở trình duyệt và được xoá khỏi CDN mỗi lần deploy.
  File `frontend/public/_headers` bổ sung phần còn lại: `assets/` cache một năm vì tên file có mã băm.

---

## Những điều cần biết

**1. Đổi biến môi trường phải deploy lại.** Biến `API_ORIGIN` chỉ áp dụng cho các lần deploy
**sau** khi sửa. Sửa xong vào **Deployments → Retry deployment** (không cần đổi code).

**2. Render gói Free ngủ sau 15 phút không ai truy cập.** Lần gọi đầu mất khoảng 50 giây để
thức dậy; trong lúc đó trang có thể báo *"Không kết nối được tới API"*. Mở trang trước vài phút
nếu cần demo.

**3. Đơn hàng và đặt bàn vẫn nằm trong RAM của API.** Render khởi động lại là mất — giới hạn đã
biết của dự án, xem mục 10 của [PLANNING.md](PLANNING.md).

**4. Hạn mức miễn phí.** Chỉ request `/api/*` chạy qua Function và tính vào hạn mức
100.000 request/ngày của gói Free; file tĩnh không bị tính.

---

## Kiểm tra sau khi deploy

```bash
P=https://sakura-tei.pages.dev

# Mọi đường dẫn của web phải trả 200, không được 404
curl -s -o /dev/null -w "%{http_code}\n" $P/
curl -s -o /dev/null -w "%{http_code}\n" $P/thuc-don
curl -s -o /dev/null -w "%{http_code}\n" $P/mon-an/steak-kobe-a5-than-ngoai

# API qua proxy — totalItems phải là 250
curl -s "$P/api/dishes?pageSize=1" | head -c 120
```

Sau đó mở web và thử một vòng thật: lọc thực đơn, xem chi tiết một món, thêm vào giỏ, đặt đơn,
rồi tra cứu lại bằng mã đơn.

| Triệu chứng | Nguyên nhân thường gặp |
| --- | --- |
| API trả 500 *"Chưa cấu hình API"* | Chưa đặt `API_ORIGIN`, hoặc đặt rồi nhưng chưa **Retry deployment** |
| API trả 502 *"Không kết nối được tới API"* | Render đang ngủ (chờ một phút rồi F5), hoặc `API_ORIGIN` sai địa chỉ |
| Build báo không tìm thấy `package.json` | Root directory chưa đặt là `frontend` |
| Vào `/` được nhưng `/thuc-don` trả 404 | Trong `dist/` lỡ có file `404.html` — Pages tắt chế độ SPA khi thấy file này |
| Bản preview gọi API lỗi, production thì chạy | Chưa thêm `API_ORIGIN` cho môi trường **Preview** |

---

## Chạy thử trên máy trước khi deploy

`wrangler` giả lập Pages kèm Function ngay trên máy, không cần tài khoản Cloudflare:

```bash
# Terminal 1: chạy API
cd backend/SakuraTei.Api && dotnet run

# Terminal 2: build web rồi chạy như trên Pages
cd frontend
npm run build
npx wrangler pages dev dist --binding API_ORIGIN=http://localhost:5187
# → mở http://localhost:8788
```
