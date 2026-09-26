# Deploy lên Vercel

> **Đọc phần này trước.** Vercel **không chạy được .NET** — nền tảng này chỉ hỗ trợ Node.js,
> Python, Go, Ruby và web tĩnh. Nghĩa là Vercel chỉ host được **frontend**, còn API ASP.NET Core
> bắt buộc phải nằm ở một nơi khác. Kiến trúc gộp một process trong [DEPLOY.md](DEPLOY.md)
> không dùng được ở đây.
>
> Nếu mục tiêu chỉ là "có một link HTTPS công khai để nộp bài" thì deploy **cả cụm** lên Render
> bằng Dockerfile sẵn có sẽ đơn giản hơn hẳn: một nơi, một URL, không phải cấu hình CORS.
> Chỉ chọn Vercel khi bạn muốn frontend nằm trên CDN của Vercel.

Kết quả cuối sẽ là hai dịch vụ:

```
  Vercel                          Render (hoặc Railway/Fly)
  ┌────────────────────┐          ┌──────────────────────────┐
  │ React SPA (tĩnh)   │ ──────▶  │ SakuraTei.Api (.NET 9)    │
  │ sakura-tei.vercel  │  HTTPS   │ sakura-api.onrender.com   │
  └────────────────────┘  CORS    └──────────────────────────┘
```

---

## Phần A — Đưa API lên Render

Làm phần này **trước**, vì bước build frontend cần biết URL của API.

1. Vào [render.com](https://render.com), đăng nhập bằng GitHub.
2. **New → Web Service**, chọn repo `Vuong-Quang-Tuan`.
3. Cấu hình:

   | Mục | Giá trị |
   | --- | --- |
   | Language | `Docker` |
   | Dockerfile Path | `./Dockerfile` |
   | Docker Build Context Directory | `.` |
   | Instance Type | `Free` |
   | Health Check Path | `/health` |

4. **Environment Variables** — thêm một biến, thay domain Vercel của bạn vào:

   | Key | Value |
   | --- | --- |
   | `Cors__AllowedOrigins__0` | `https://sakura-tei.vercel.app` |

   Dấu gạch dưới **đôi** là quy ước của .NET để ánh xạ `Cors:AllowedOrigins[0]` trong
   `appsettings.json`. Chưa biết domain Vercel thì cứ để tạm, quay lại sửa ở bước cuối.

5. Bấm **Deploy**. Lần đầu mất khoảng 5–10 phút vì phải build cả frontend lẫn backend.

Xong thì kiểm tra:

```bash
curl https://sakura-api.onrender.com/health
# → {"status":"healthy","dishCount":250,"categoryCount":12,...}
```

> Dockerfile dựng cả frontend vào `wwwroot`, nên URL Render này mở ra cũng thấy website đầy đủ.
> Không sao cả — Vercel sẽ là bản chính thức, bản trên Render coi như dự phòng.

---

## Phần B — Đưa frontend lên Vercel

1. Vào [vercel.com](https://vercel.com), đăng nhập bằng GitHub.
2. **Add New → Project**, chọn repo `Vuong-Quang-Tuan`.
3. Cấu hình — **quan trọng nhất là Root Directory**:

   | Mục | Giá trị |
   | --- | --- |
   | Framework Preset | `Vite` |
   | **Root Directory** | **`frontend`** |
   | Build Command | `npm run build` (mặc định) |
   | Output Directory | `dist` (mặc định) |

   Để Root Directory trống là Vercel sẽ tìm `package.json` ở gốc repo và build hỏng.

4. **Environment Variables** — thêm:

   | Key | Value |
   | --- | --- |
   | `VITE_API_BASE_URL` | `https://sakura-api.onrender.com/api` |

   Nhớ **có `/api` ở cuối** và **không có dấu `/` thừa**.

5. **Deploy**.

6. Quay lại Render, sửa `Cors__AllowedOrigins__0` thành đúng domain Vercel vừa được cấp
   (ví dụ `https://sakura-tei.vercel.app`), rồi **Manual Deploy → Restart service**.

File `frontend/vercel.json` đã có sẵn trong repo, lo hai việc:

- **Rewrite mọi đường dẫn về `index.html`.** Không có nó thì vào thẳng `/thuc-don` hay F5 giữa
  trang sẽ nhận 404 của Vercel, vì `dist/` chỉ có đúng một file HTML.
- **Đặt cache**: `assets/` cache vĩnh viễn (tên file có mã băm), `index.html` không cache
  (nếu không người dùng kẹt ở bản cũ sau mỗi lần deploy).

---

## Bốn cái bẫy của cách tách đôi này

**1. `VITE_API_BASE_URL` là biến lúc build, không phải lúc chạy.** Giá trị được nướng thẳng vào
file JavaScript. Đổi biến trên Vercel mà không **Redeploy** thì trang vẫn gọi API cũ. Kiểm chứng
được: sau khi build, `grep` URL đó trong `dist/assets/*.js` sẽ thấy nó nằm trong mã.

**2. Bản xem trước của Vercel sẽ bị CORS chặn.** Mỗi lần push nhánh, Vercel tạo một URL preview
khác nhau dạng `sakura-tei-abc123.vercel.app`. Backend dùng `WithOrigins` khớp **chính xác** nên
các URL này không nằm trong danh sách và mọi request API sẽ hỏng. Ba cách xử lý:

- Chỉ dùng bản production, bỏ qua preview (đơn giản nhất cho bài tập).
- Thêm từng URL preview vào `Cors__AllowedOrigins__1`, `__2`… khi cần.
- Đổi `Program.cs` sang `SetIsOriginAllowed(o => o.EndsWith(".vercel.app"))` — tiện nhưng nới
  lỏng bảo mật, chỉ nên làm với dự án bài tập.

**3. Render gói Free ngủ sau 15 phút không ai truy cập.** Request đầu tiên đánh thức dịch vụ
mất **khoảng 50 giây**, trong lúc đó trang sẽ hiện lỗi "Không kết nối được tới máy chủ".
Nếu đưa link cho giảng viên chấm, hãy mở trước vài phút cho nó thức dậy.

**4. Đơn hàng và đặt bàn vẫn nằm trong RAM.** Render restart dịch vụ (khi ngủ dậy, khi deploy)
là mất sạch. Mã đơn vừa tạo tra lại có thể không còn. Đây là giới hạn đã biết của dự án, xem
mục 10 của [PLANNING.md](PLANNING.md).

---

## Kiểm tra sau khi deploy

```bash
V=https://sakura-tei.vercel.app
A=https://sakura-api.onrender.com

# Frontend: mọi route phải 200, không được 404
curl -s -o /dev/null -w "%{http_code}\n" $V/
curl -s -o /dev/null -w "%{http_code}\n" $V/thuc-don
curl -s -o /dev/null -w "%{http_code}\n" $V/mon-an/tonkotsu-ramen

# API
curl -s $A/health
curl -s "$A/api/dishes?q=ca%20hoi" | head -c 60      # totalItems phải khác 0

# CORS: phải thấy Access-Control-Allow-Origin đúng domain Vercel
curl -s -i -X OPTIONS $A/api/orders \
  -H "Origin: $V" \
  -H "Access-Control-Request-Method: POST" \
  -H "Access-Control-Request-Headers: content-type" | grep -i "access-control"
```

Dòng cuối là dòng hay hỏng nhất. Không thấy `Access-Control-Allow-Origin` nghĩa là biến
`Cors__AllowedOrigins__0` trên Render chưa khớp domain Vercel — kiểm tra lại có thừa dấu `/`
ở cuối hoặc nhầm `http` với `https` không.

Cuối cùng, mở website và thử một vòng thật: lọc thực đơn, thêm món vào giỏ, đặt một đơn, rồi
tra cứu lại bằng mã đơn.

Cách đọc triệu chứng khi hỏng:

| Triệu chứng | Nguyên nhân thường gặp |
| --- | --- |
| **Trang nào cũng** báo "Không kết nối được tới máy chủ" | CORS sai domain, hoặc API đang ngủ / chết |
| Lần đầu vào thì lỗi, chờ một phút rồi F5 lại thì chạy | Render gói Free vừa ngủ dậy — bình thường |
| Vào `/` được nhưng `/thuc-don` trả 404 | Rewrite của Vercel chưa ăn — kiểm tra Root Directory có phải `frontend` không |
| Xem thực đơn được nhưng đặt đơn báo lỗi | **Không phải CORS.** Xem thông báo lỗi dưới từng ô nhập — nhiều khả năng dữ liệu không hợp lệ |

Lưu ý chỗ dễ đoán nhầm: khi CORS sai, máy chủ vẫn trả `200` nhưng thiếu header
`Access-Control-Allow-Origin`, và **trình duyệt chặn cả `GET`**. Nên nếu chỉ riêng chức năng đặt
đơn hỏng còn thực đơn vẫn hiện bình thường thì vấn đề nằm ở chỗ khác, không phải CORS.
