# PRD — Website nhà hàng Nhật Sakura Tei

**PRD** (Product Requirements Document) chốt *làm cái gì, cho ai, vì sao, và thế nào thì coi là xong*.
Cách xây được mô tả trong [PLANNING.md](PLANNING.md), còn giao diện trông ra sao thì xem
[DESIGN.md](DESIGN.md).

| Mục | Nội dung |
| --- | --- |
| Sản phẩm | Website thương hiệu và đặt món của nhà hàng Sakura Tei (さくら亭) |
| Phiên bản tài liệu | 1.2 — cập nhật 09/10/2026 |
| Người phụ trách | Vương Quang Tuấn |
| Trạng thái | v1.2 hoàn thành: đủ mọi yêu cầu bắt buộc (mục 6), có database SQLite |
| Hạn nộp | 17/10/2026 |

---

## 1. Bối cảnh

Sakura Tei là nhà hàng Nhật (thương hiệu hư cấu) ở Quận 1, TP. Hồ Chí Minh. Hiện khách đặt món và
đặt bàn qua điện thoại, nên gặp ba vấn đề:

- **Giờ cao điểm gọi không được.** Một số máy, một nhân viên trực, khách phải gọi lại nhiều lần.
- **Khách không xem được thực đơn đầy đủ.** Thực đơn 250 món chỉ có bản giấy tại quán.
- **Đặt bàn dễ trùng.** Nhân viên ghi sổ tay, không biết khung giờ nào đã kín.

## 2. Mục tiêu

| # | Mục tiêu | Cách đo |
| --- | --- | --- |
| G1 | Khách tự xem toàn bộ thực đơn, tìm ra món mình muốn | Tìm được món trong ≤ 3 thao tác (lọc hoặc gõ tên) |
| G2 | Khách tự đặt món giao tận nơi mà không cần gọi điện | Đi từ trang chủ tới mã đơn trong ≤ 2 phút |
| G3 | Khách tự đặt bàn và biết ngay khung giờ còn chỗ | Không bao giờ nhận quá 24 khách trong một khung giờ |
| G4 | Thể hiện được thương hiệu: món chỉn chu, không gian yên tĩnh | Mọi trang dùng đúng logo, hệ màu và giọng văn trong DESIGN.md |

## 3. Ngoài phạm vi

Những thứ **có chủ ý không làm** ở bản này:

| Không làm | Lý do |
| --- | --- |
| Đăng nhập / tài khoản khách | Khách vãng lai đặt món không cần tài khoản; mã đơn đủ để tra cứu |
| Thanh toán trực tuyến | Quán thu tiền khi giao; cổng thanh toán cần pháp nhân và hợp đồng |
| Trang quản trị thực đơn | Thực đơn ít thay đổi; bản đầu sửa thẳng trong database SQLite hoặc trong dữ liệu mẫu là đủ |
| Đa ngôn ngữ | Khách mục tiêu là người Việt; chữ Nhật chỉ dùng để trang trí |
| Ứng dụng di động | Website responsive đã dùng tốt trên điện thoại |

## 4. Người dùng mục tiêu

> Các nhân vật dưới đây là chân dung giả định, dùng để thống nhất cách nghĩ về người dùng.

### P1 — Chị Lan, 29 tuổi, nhân viên văn phòng Quận 1

- **Bối cảnh:** đặt cơm trưa cho mình và 2–3 đồng nghiệp, dùng điện thoại, chỉ có 10 phút.
- **Cần:** lọc nhanh theo giá, biết tổng tiền và phí giao **trước khi** đặt.
- **Khó chịu khi:** form dài, báo lỗi chung chung, không biết sai ở ô nào.

### P2 — Anh Minh, 35 tuổi, đặt bàn cho nhóm 6 người tối thứ Sáu

- **Bối cảnh:** dùng máy tính ở công ty, muốn chốt chỗ trước vài ngày.
- **Cần:** thấy ngay giờ nào còn chỗ, không phải gọi hỏi từng khung giờ.
- **Khó chịu khi:** đặt xong tới nơi lại không có bàn.

### P3 — Bạn Vy, 22 tuổi, sinh viên thích văn hoá Nhật

- **Bối cảnh:** lướt xem món và đọc bài, chưa chắc sẽ đặt.
- **Cần:** ảnh món đẹp, tên tiếng Nhật, nguyên liệu, câu chuyện phía sau món ăn.
- **Khó chịu khi:** ảnh lỗi, trang trống, không biết món đó là gì.

---

## 5. User story và tiêu chí chấp nhận

Trạng thái: ✅ đã có (ghi rõ phiên bản nếu thêm sau v1.1).

### E1 — Khám phá thực đơn

**US-01 · Lọc thực đơn** ✅
*Là chị Lan, tôi muốn lọc món theo nhóm và mức giá, để chọn nhanh món hợp túi tiền.*

- [x] Lọc được theo nhóm, giá tối đa, độ cay, thẻ; kết hợp nhiều bộ lọc cùng lúc.
- [x] Sắp xếp được theo giá tăng/giảm, đánh giá, phổ biến.
- [x] Hiện số món phù hợp; không có kết quả thì hiện thông báo và nút xoá bộ lọc.
- [x] Bộ lọc nằm trên URL: chia sẻ link, bấm Back, tải lại trang đều giữ đúng kết quả.

**US-02 · Tìm món không cần gõ dấu** ✅
*Là khách dùng điện thoại, tôi muốn gõ `ca hoi` mà vẫn ra `cá hồi`.*

- [x] Tìm trong tên tiếng Việt, romaji, mô tả, thẻ, nguyên liệu; không phân biệt hoa thường, có dấu hay không.
- [x] Ô tìm ở Header dẫn sang trang thực đơn kèm từ khoá.
- [x] Gõ liên tục không tạo một mục lịch sử trình duyệt cho mỗi phím (chờ 350ms).

**US-03 · Xem chi tiết món** ✅
*Là bạn Vy, tôi muốn xem ảnh, nguyên liệu và độ cay của món trước khi đặt.*

- [x] Mỗi món có URL riêng, dạng `/mon-an/tonkotsu-ramen`.
- [x] Có 1–3 ảnh riêng, kèm tên tác giả và giấy phép.
- [x] Có tên tiếng Việt, tiếng Nhật, romaji, giá, đánh giá, độ cay, calo, thời gian chế biến, nguyên liệu.
- [x] Gợi ý 4 món cùng nhóm.
- [x] Món không tồn tại thì báo rõ, có đường quay lại thực đơn.

### E2 — Giỏ hàng và đặt món

**US-04 · Quản lý giỏ hàng** ✅
*Là chị Lan, tôi muốn thêm, sửa số lượng, xoá món trong giỏ, và giỏ không mất khi tải lại trang.*

- [x] Thêm từ thẻ món (1 phần) hoặc từ trang chi tiết (chọn số lượng).
- [x] Đổi số lượng trong khoảng 1–50; xoá từng món; xoá hết.
- [x] Giỏ lưu trong trình duyệt; dữ liệu hỏng khi đọc lại thì bỏ qua, trang không vỡ.
- [x] Hiện tạm tính, phí giao, và còn thiếu bao nhiêu để được miễn phí giao.

**US-05 · Đặt món giao tận nơi** ✅
*Là chị Lan, tôi muốn điền địa chỉ và nhận mã đơn để theo dõi.*

- [x] Form gồm họ tên, điện thoại, email (tuỳ chọn), địa chỉ, ghi chú.
- [x] Dữ liệu sai thì báo lỗi **ngay dưới ô** bị sai, cả khi lỗi do server trả về.
- [x] **Giá do server quyết định**, không tin giá từ trình duyệt.
- [x] Thành công thì nhận mã `ST-XXXXXX`, giỏ được làm trống và chuyển thẳng tới trang tra cứu đơn đó.

**US-06 · Tra cứu đơn** ✅
*Là khách vừa đặt, tôi muốn xem lại đơn và trạng thái bằng mã.*

- [x] Nhập mã hoặc mở `/tra-cuu/ST-XXXXXX` là thấy đơn.
- [x] Hiện các bước trạng thái, danh sách món, tổng tiền, giờ dự kiến xong.
- [x] Mã sai thì báo không tìm thấy, không để trang trắng.

**US-07 · Huỷ đơn** ✅ v1.2
*Là khách, tôi muốn huỷ đơn khi bếp chưa bắt đầu làm.*

- [x] Nút "Huỷ đơn" ở trang tra cứu, chỉ hiện khi đơn đang ở trạng thái *Đã nhận*.
- [x] Bấm lần đầu chỉ hỏi lại tại chỗ; bấm xác nhận mới huỷ.
- [x] Huỷ xong, trạng thái đơn đổi thành *Đã huỷ* và được lưu vào database.
- [x] Huỷ một đơn đã huỷ, hoặc đơn bếp đã làm, bị từ chối kèm lý do (409).

### E3 — Đặt bàn

**US-08 · Xem chỗ trống và đặt bàn** ✅
*Là anh Minh, tôi muốn thấy khung giờ nào còn chỗ và giữ chỗ ngay.*

- [x] Chọn ngày từ hôm nay tới 60 ngày sau.
- [x] Hiện 21 khung giờ từ 11:00 tới 21:00, mỗi khung ghi số chỗ còn lại; khung kín thì không chọn được.
- [x] Số khách 1–12; nhóm đông hơn được hướng dẫn gọi hotline.
- [x] Hai người đặt cùng lúc không làm vượt 24 chỗ của một khung.
- [x] Thành công thì nhận mã `RS-XXXXXX`.

**US-09 · Sửa và huỷ đặt bàn** ✅ v1.2
*Là anh Minh, tôi muốn đổi giờ hoặc huỷ bàn khi kế hoạch thay đổi.*

- [x] Tra lượt đặt bằng mã `RS-XXXXXX` — ô tra cứu chung nhận cả mã đơn lẫn mã đặt bàn.
- [x] Đổi ngày, giờ, số khách, thông tin liên hệ trên cùng form đặt bàn, điền sẵn dữ liệu cũ.
- [x] Hệ thống kiểm tra lại chỗ trống, **không tính chỗ của chính lượt đang đổi**.
- [x] Huỷ thì xoá lượt đặt và trả lại số chỗ cho khung giờ đó.
- [x] Lượt đã qua giờ hẹn thì không đổi, không huỷ được.

### E4 — Nội dung thương hiệu

**US-10 · Đọc câu chuyện ẩm thực** ✅
*Là bạn Vy, tôi muốn đọc bài về văn hoá và nguyên liệu Nhật.*

- [x] Danh sách bài lọc theo chuyên mục, có phân trang.
- [x] Trang đọc bài có tiểu mục, ngày đăng, thời gian đọc.

**US-11 · Tìm hiểu về quán** ✅
*Là khách lần đầu, tôi muốn biết quán ở đâu, ai nấu, mở cửa lúc nào.*

- [x] Trang Về Chúng Tôi có câu chuyện, dòng thời gian, đội bếp, địa chỉ, hotline, email, giờ mở cửa.
- [x] Bản đồ Google Maps nhúng ngay trong trang, kèm nút mở ứng dụng Google Maps (v1.2).

### E5 — Trải nghiệm chung

**US-12 · Giao diện sáng/tối** ✅

- [x] Lần đầu vào thì theo cài đặt của hệ điều hành, sau đó nhớ lựa chọn của khách.
- [x] Không nháy trắng khi tải trang ở chế độ tối.

**US-13 · Dùng tốt trên điện thoại** ✅

- [x] Header thu gọn thành nút ☰; bộ lọc thực đơn mở dạng ngăn kéo.
- [x] Lưới món tự đổi 3 → 2 → 1 cột; form đặt món và đặt bàn đưa phần tóm tắt xuống dưới; lưới
      khung giờ đổi từ 5 xuống 3 cột.

---

## 6. Yêu cầu chức năng

| Mã | Yêu cầu | Ưu tiên | Trạng thái | User story |
| --- | --- | --- | --- | --- |
| FR-01 | Trang Chủ giới thiệu thương hiệu, nhóm món, món nổi bật, bài mới | Bắt buộc | ✅ | — |
| FR-02 | Trang Về Chúng Tôi | Bắt buộc | ✅ | US-11 |
| FR-03 | Trang chi tiết cho từng món | Bắt buộc | ✅ | US-03 |
| FR-04 | Lọc, sắp xếp, phân trang thực đơn | Bắt buộc | ✅ | US-01 |
| FR-05 | Tìm kiếm không dấu | Nên có | ✅ | US-02 |
| FR-06 | Giỏ hàng thêm/sửa/xoá, lưu qua các lần tải trang | Bắt buộc | ✅ | US-04 |
| FR-07 | Đặt món, sinh mã đơn | Bắt buộc | ✅ | US-05 |
| FR-08 | Tra cứu đơn | Nên có | ✅ | US-06 |
| FR-09 | Đặt bàn theo khung giờ còn chỗ | Bắt buộc | ✅ | US-08 |
| FR-10 | Chuyên mục bài viết | Nên có | ✅ | US-10 |
| FR-11 | Giao diện sáng/tối | Có thì tốt | ✅ | US-12 |
| FR-12 | **Lưu thực đơn, bài viết, đơn hàng, đặt bàn vào database SQLite** | Bắt buộc | ✅ v1.2 | US-05, US-08 |
| FR-13 | Huỷ đơn | Bắt buộc | ✅ v1.2 | US-07 |
| FR-14 | Sửa và huỷ đặt bàn | Bắt buộc | ✅ v1.2 | US-09 |
| FR-15 | Nhúng Google Maps ở trang Về Chúng Tôi | Bắt buộc | ✅ v1.2 | US-11 |

FR-12 tới FR-15 được xếp "Bắt buộc" vì đề bài cuối khoá yêu cầu database, chức năng chỉnh sửa
database và nội dung nhúng (xem [BAO-CAO.md — mục 7](BAO-CAO.md#7-đối-chiếu-tiêu-chí-chấm)).

## 7. Yêu cầu phi chức năng

| Mã | Nhóm | Yêu cầu |
| --- | --- | --- |
| NFR-01 | Hiệu năng | Khi tải dữ liệu, hiện khung xương đúng kích thước để trang không giật; huỷ request cũ khi bộ lọc đổi |
| NFR-02 | Hiệu năng | Ảnh thẻ món dùng bản 500px; file tĩnh có mã băm được cache một năm |
| NFR-03 | Bảo mật | Giá, phí giao và tổng tiền luôn tính ở server |
| NFR-04 | Bảo mật | Dữ liệu đầu vào được kiểm tra ở server; lỗi 500 không lộ stack trace ra ngoài |
| NFR-05 | Bảo mật | Container chạy bằng người dùng thường, không chạy bằng root |
| NFR-06 | Độ tin cậy | Mọi thao tác `localStorage` bọc `try/catch` — trình duyệt chặn lưu trữ thì trang vẫn chạy |
| NFR-07 | Độ tin cậy | Ảnh lỗi thì hiện chữ kanji của nhóm món thay cho ô trống |
| NFR-08 | Tiếp cận | Đủ tương phản WCAG AA cho chữ; nút chỉ có biểu tượng phải có `aria-label`; viền focus rõ ở cả hai chế độ |
| NFR-09 | Tiếp cận | Tôn trọng `prefers-reduced-motion` |
| NFR-10 | Tương thích | Chạy trên bản mới của Chrome, Edge, Firefox, Safari (máy tính và điện thoại) |
| NFR-11 | Triển khai | Vào thẳng hoặc F5 ở mọi đường dẫn đều trả 200, không 404 |
| NFR-12 | Chi phí | Toàn bộ hạ tầng dùng gói miễn phí |
| NFR-13 | Dữ liệu | Mọi thao tác ghi lưu vào SQLite; tắt bật lại API dữ liệu vẫn còn (trên Render gói Free thì chỉ trong một phiên chạy — xem mục 9) |
| NFR-14 | Đúng giờ | "Hôm nay", "đã qua giờ hẹn" tính theo giờ Việt Nam (UTC+7) dù máy chủ chạy giờ UTC |
| NFR-15 | Nhất quán lỗi | Mọi lỗi API có cùng hình dạng: 400 kèm lỗi từng trường, 404/409 kèm thông điệp cho người dùng |

## 8. Quy tắc nghiệp vụ

| Quy tắc | Giá trị |
| --- | --- |
| Phí giao | 25.000đ; miễn phí cho đơn từ 500.000đ |
| Số lượng mỗi món | 1–50 |
| Số loại món mỗi đơn | Tối đa 50 |
| Giờ nhận khách | 11:00–21:00, mỗi khung 30 phút |
| Sức chứa | 24 chỗ mỗi khung |
| Số khách mỗi lượt | 1–12 |
| Đặt trước tối đa | 60 ngày |
| Số điện thoại | 10 chữ số, bắt đầu bằng 0 |
| Mã đơn / mã đặt bàn | `ST-XXXXXX` / `RS-XXXXXX`, bỏ các ký tự dễ nhầm `0 O 1 I` |
| Huỷ đơn | Chỉ khi đơn còn ở trạng thái *Đã nhận* |
| Đổi lịch / huỷ đặt bàn | Chỉ trước giờ hẹn |

Nơi cài đặt từng quy tắc: [PLANNING.md — mục 5](PLANNING.md#5-quy-tắc-nghiệp-vụ).

## 9. Ràng buộc và giả định

- **Đề bài:** tối thiểu 3 trang (Trang Chủ, Về Chúng Tôi, Chi tiết), có database CSV/SQLite/Supabase,
  ít nhất 2 chức năng chỉnh sửa database, nhúng Google Map hoặc YouTube, dùng file .md, repo GitHub
  Public, deploy lên một nền tảng công khai.
- **Ngân sách:** 0 đồng; chỉ dùng gói miễn phí (Cloudflare Pages, Render Free).
- **Hạ tầng:** Render Free tự ngủ sau 15 phút và không giữ file khi khởi động lại. Database SQLite
  đặt trên Render sẽ **trở về dữ liệu gốc** sau mỗi lần deploy hoặc khởi động lại. Chấp nhận được với
  bài tập; nếu cần giữ dữ liệu lâu dài thì dùng Supabase.
- **Thời gian:** một người làm, nộp ngày 17/10/2026.

## 10. Lộ trình phát hành

| Phiên bản | Ngày | Nội dung |
| --- | --- | --- |
| v1.0 | 20/09/2026 | 120 món / 7 nhóm, 9 trang, đặt món, đặt bàn, bài viết; Dockerfile, hướng dẫn Docker và Vercel |
| v1.1 | 26/09/2026 | 250 món / 12 nhóm, ảnh riêng từng món; deploy lên Cloudflare Pages + Render |
| v1.1.1 | 09/10/2026 | Tài liệu PRD, DESIGN, báo cáo; sửa số liệu cũ trên giao diện; tách nhánh `main` / `dev` |
| **v1.2** | 09/10/2026 | FR-12 SQLite · FR-13 huỷ đơn · FR-14 đổi lịch / huỷ đặt bàn · FR-15 Google Maps; refactor backend và frontend; script kiểm thử đầu–cuối |
| v1.3 *(đề xuất)* | sau khi nộp | Supabase/PostgreSQL để dữ liệu bền trên production · trang quản trị đổi trạng thái đơn · unit test |

## 11. Rủi ro

| Rủi ro | Khả năng | Ảnh hưởng | Cách giảm |
| --- | --- | --- | --- |
| Render đang ngủ đúng lúc giảng viên mở web | Cao | Lần đầu chờ ~50 giây | Mở trước khi chấm; ghi chú ở đầu báo cáo |
| Dữ liệu đơn mất khi Render khởi động lại | Cao | Mã đơn cũ tra không ra | Ghi rõ giới hạn; dùng Supabase nếu cần giữ lâu |
| Cloudflare đã build giao diện mới nhưng Render còn chạy API cũ | Trung bình | Nút huỷ đơn / đổi lịch báo lỗi | Cả hai cùng build từ nhánh `main`; kiểm tra `/health` có `"database":"SQLite"` |
| Ảnh Wikimedia/Unsplash không tải được | Thấp | Thiếu ảnh | Ảnh dự phòng kanji |
| Sửa phí giao ở một phía mà quên phía kia | Trung bình | Số tiền ở giỏ lệch với hoá đơn | Ghi chú trong PLANNING mục 5; server luôn là nguồn đúng |
