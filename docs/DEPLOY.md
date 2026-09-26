# Triển khai lên server

> Muốn dùng **Vercel** cho frontend? Vercel không chạy được .NET nên phải tách đôi —
> xem [DEPLOY-VERCEL.md](DEPLOY-VERCEL.md). Tài liệu này nói về cách gộp một process.

Dự án deploy theo kiểu **gộp một process**: frontend sau khi build được đặt vào `wwwroot` của
API, nên chỉ có một ứng dụng chạy và web dùng chung origin với API. Cách này bỏ được ba thứ
hay gây lỗi khi tách riêng: cấu hình CORS, cấu hình rewrite cho SPA, và biến `VITE_API_BASE_URL`.

## Cách 1 — Docker Compose kèm HTTPS (khuyến nghị)

Cách này dựng hai container: app và **Caddy** làm reverse proxy. Caddy tự xin và tự gia hạn
chứng chỉ Let's Encrypt, nên không phải chạy `certbot` hay đặt cron gì thêm.

**Điều kiện:** tên miền đã trỏ bản ghi `A` về IP server, và cổng 80 với 443 của server mở ra
Internet. Let's Encrypt cần cả hai để xác thực quyền sở hữu tên miền.

```bash
DOMAIN=sakuratei.example.com docker compose up -d --build
```

Chờ khoảng một phút cho Caddy lấy chứng chỉ, rồi mở `https://sakuratei.example.com`.
Xem tiến trình xin chứng chỉ:

```bash
docker compose logs -f caddy
```

App **không publish cổng ra ngoài** — chỉ Caddy nói chuyện được với nó, nên không ai vào thẳng
bằng HTTP cổng 8080 được. Chứng chỉ lưu trong volume `caddy_data`; đừng xoá volume này vì
Let's Encrypt giới hạn số lần cấp chứng chỉ mỗi tuần.

## Cách 2 — Chỉ Docker, không HTTPS

Dùng khi chạy thử trong mạng nội bộ, hoặc khi đã có sẵn reverse proxy khác phía trước.

```bash
docker build -t sakura-tei .
docker run -d --name sakura-tei -p 80:8080 --restart unless-stopped sakura-tei
```

Mở `http://<địa-chỉ-server>` là ra trang chủ. Kiểm tra nhanh:

```bash
curl http://<địa-chỉ-server>/health
# → {"status":"healthy","dishCount":250,"categoryCount":12,...}
```

`Dockerfile` dựng ba tầng: Node build frontend → SDK .NET publish backend kèm `wwwroot` →
ảnh runtime `aspnet:9.0`. Ảnh cuối chạy bằng người dùng thường, không phải root.

## Cách 3 — Chạy trực tiếp trên server (không Docker)

Cần .NET Runtime 9 trên server. Dựng ở máy phát triển:

```bash
# 1. Build frontend
cd frontend
npm ci
npm run build

# 2. Đưa kết quả vào wwwroot của API
cd ../backend/SakuraTei.Api
rm -rf wwwroot && mkdir wwwroot
cp -r ../../frontend/dist/* wwwroot/

# 3. Publish
dotnet publish -c Release -o ./publish
```

Chép thư mục `publish/` lên server rồi chạy:

```bash
ASPNETCORE_ENVIRONMENT=Production ASPNETCORE_URLS=http://0.0.0.0:8080 \
  dotnet SakuraTei.Api.dll
```

Trên server thật nên cho chạy dưới **systemd** để tự khởi động lại, và đặt **Caddy** hoặc
**Nginx** phía trước để lo TLS — cách 1 đã làm sẵn việc này bằng Docker Compose.

<details>
<summary>Ví dụ unit systemd</summary>

```ini
[Unit]
Description=Sakura Tei API
After=network.target

[Service]
WorkingDirectory=/var/www/sakura-tei
ExecStart=/usr/bin/dotnet /var/www/sakura-tei/SakuraTei.Api.dll
Restart=always
RestartSec=5
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://127.0.0.1:8080

[Install]
WantedBy=multi-user.target
```

</details>

## Biến môi trường

| Biến | Mặc định | Ghi chú |
| --- | --- | --- |
| `ASPNETCORE_ENVIRONMENT` | `Production` trong ảnh Docker | Để `Development` sẽ bật Swagger và cho `/` chuyển sang Swagger |
| `ASPNETCORE_URLS` | `http://+:8080` trong ảnh Docker | Cổng lắng nghe |
| `Cors__AllowedOrigins__0` | `http://localhost:5173` | **Chỉ cần khi tách domain.** Deploy gộp thì không dùng tới |

## Ba cái bẫy cần biết

**1. Đừng bật globalization invariant.** `SakuraTei.Api.csproj` để `InvariantGlobalization=false`
là có chủ ý: hàm bỏ dấu tiếng Việt trong `DishService` dùng `string.Normalize(FormD)`, việc này
cần thư viện ICU. Nếu deploy bằng ảnh **Alpine** hoặc đặt `DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true`,
tìm kiếm không dấu sẽ hỏng **âm thầm** — gõ `ca hoi` không ra `cá hồi` mà không có lỗi nào. Kiểm tra sau
khi deploy:

```bash
curl "http://<server>/api/dishes?q=ca%20hoi" 
# totalItems phải khác 0
```

**2. Dữ liệu nằm trong RAM.** Đơn hàng và lượt đặt bàn mất sạch khi restart. Quan trọng hơn:
**đừng chạy nhiều instance** hay bật autoscale — mỗi instance giữ một bộ dữ liệu riêng, khách
tra mã đơn sẽ lúc thấy lúc không tuỳ request rơi vào instance nào. Muốn chạy nhiều instance thì
phải gắn cơ sở dữ liệu trước (xem mục 10 của [PLANNING.md](PLANNING.md)).

**3. Build lại frontend mỗi khi đổi domain — nếu tách riêng.** `VITE_API_BASE_URL` được nướng vào
bundle **lúc build**, không đọc lúc chạy. Cách deploy gộp trong tài liệu này không đụng tới nó,
nhưng nếu sau này tách web và API ra hai domain thì phải nhớ.

## Kiểm tra sau khi deploy

```bash
S=http://<địa-chỉ-server>

curl -s -o /dev/null -w "%{http_code}\n" $S/                      # 200
curl -s -o /dev/null -w "%{http_code}\n" $S/thuc-don              # 200 (không được 404)
curl -s -o /dev/null -w "%{http_code}\n" $S/mon-an/tonkotsu-ramen # 200
curl -s $S/health                                                 # dishCount 250
curl -s $S/api/khong-ton-tai                                      # 404 kèm JSON, không phải HTML
curl -s "$S/api/dishes?q=ca%20hoi" | head -c 80                   # totalItems phải khác 0
```

Hai dòng `/thuc-don` và `/mon-an/...` là quan trọng nhất: trả 404 nghĩa là SPA fallback chưa
hoạt động, khách F5 giữa trang sẽ gặp lỗi.

Lưu ý `/swagger` ở Production trả **200** chứ không phải 404. Swagger không được đăng ký, nên
đường dẫn này rơi vào fallback SPA và nhận `index.html`, rồi React Router hiện trang 404 của
website. Muốn biết Swagger đã tắt hay chưa thì nhìn nội dung trả về có phải giao diện Swagger
không, đừng nhìn mã HTTP.

