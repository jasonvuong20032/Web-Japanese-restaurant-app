using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm thức uống — 14 món, không phục vụ đồ có cồn.</summary>
    private static readonly IReadOnlyList<Dish> ThucUong =
    [
        Make("thuc-uong", "matcha-usucha", "Matcha usucha truyền thống", "薄茶", "Usucha", 78000,
            "Matcha đánh bằng chasen tre tới khi nổi lớp bọt mịn, uống trong chén raku, đắng thanh.",
            "Bột matcha Uji, Nước lọc 80°C",
            "Matcha, Kinh điển, Không sữa", 5, 12, 4.7, 248, isFeatured: true),

        Make("thuc-uong", "matcha-latte-nong", "Matcha latte nóng", "抹茶ラテ", "Matcha latte", 72000,
            "Matcha đánh đặc rồi rót sữa tươi đánh nóng, vị trà vẫn nổi rõ trên nền sữa béo.",
            "Bột matcha Uji, Sữa tươi, Đường mía",
            "Matcha, Bán chạy", 6, 186, 4.8, 624, isFeatured: true),

        Make("thuc-uong", "matcha-latte-da", "Matcha latte đá", "アイス抹茶ラテ", "Iced matcha latte", 75000,
            "Lớp matcha xanh nằm trên nền sữa trắng, khuấy lên uống lạnh — bản bán chạy nhất mùa hè.",
            "Bột matcha Uji, Sữa tươi, Đường mía, Đá viên",
            "Matcha, Món lạnh, Bán chạy", 5, 198, 4.8, 712),

        Make("thuc-uong", "matcha-float", "Matcha float kem", "抹茶フロート", "Matcha float", 92000,
            "Matcha đá thả một viên kem matcha, uống tới đâu kem tan tới đó làm ly trà béo dần.",
            "Bột matcha Uji, Sữa tươi, Kem matcha, Đá viên",
            "Matcha, Món lạnh", 7, 312, 4.7, 286),

        Make("thuc-uong", "hojicha-latte", "Hojicha latte", "ほうじ茶ラテ", "Hojicha latte", 68000,
            "Trà rang hojicha ít caffeine, mùi khói ngọt như rang hạt, hợp uống buổi tối.",
            "Trà hojicha, Sữa tươi, Đường mía",
            "Trà, Ít caffeine", 6, 172, 4.6, 324),

        Make("thuc-uong", "genmaicha-nong", "Genmaicha gạo rang", "玄米茶", "Genmaicha", 52000,
            "Trà xanh trộn gạo lứt rang, thơm mùi cốm, uống kèm đồ chiên rất hợp vì rửa vị tốt.",
            "Trà xanh sencha, Gạo lứt rang, Nước lọc 85°C",
            "Trà, Không sữa, Giá tốt", 4, 8, 4.5, 196),

        Make("thuc-uong", "sencha-lanh-u-cham", "Sencha ủ lạnh", "水出し煎茶", "Mizudashi sencha", 58000,
            "Sencha ủ nước lạnh 8 tiếng nên không ra chát, chỉ còn vị ngọt umami rất rõ.",
            "Trà sencha, Nước lọc lạnh, Đá viên",
            "Trà, Món lạnh, Không sữa, Ít calo", 4, 6, 4.6, 178),

        Make("thuc-uong", "yuzu-soda", "Yuzu soda", "柚子ソーダ", "Yuzu soda", 68000,
            "Mứt yuzu Kochi pha soda lạnh, chua thơm rõ mùi vỏ cam quýt, giải ngấy sau đồ nướng.",
            "Mứt yuzu, Soda, Chanh vàng, Đá viên",
            "Món lạnh, Trái cây, Giải khát", 4, 128, 4.7, 386),

        Make("thuc-uong", "ume-soda", "Ume soda mơ muối", "梅ソーダ", "Ume soda", 66000,
            "Siro mơ ngâm nhà làm pha soda, chua mặn nhẹ đặc trưng, uống rất tỉnh người.",
            "Siro mơ umeshu không cồn, Soda, Mơ muối, Đá viên",
            "Món lạnh, Trái cây, Giải khát", 4, 132, 4.5, 214),

        Make("thuc-uong", "calpis-dau-tay", "Calpis dâu tây", "いちごカルピス", "Ichigo calpis", 62000,
            "Sữa chua lên men Calpis pha sốt dâu tươi, chua ngọt nhẹ, trẻ em rất thích.",
            "Calpis, Dâu tây, Sữa tươi, Đá viên",
            "Món lạnh, Trẻ em, Trái cây", 4, 168, 4.4, 268),

        Make("thuc-uong", "ramune-nguyen-ban", "Ramune chai bi", "ラムネ", "Ramune", 48000,
            "Soda chai thuỷ tinh nắp bi kiểu Nhật, phải ấn viên bi xuống mới mở được.",
            "Soda hương chanh, Đường mía",
            "Món lạnh, Trẻ em, Giá tốt", 2, 142, 4.3, 342),

        Make("thuc-uong", "sua-dau-nanh-kinako", "Sữa đậu nành kinako", "きな粉豆乳", "Kinako tonyu", 58000,
            "Sữa đậu nành nguyên chất đánh cùng bột đậu nành rang, bùi và ấm, không quá ngọt.",
            "Sữa đậu nành, Bột đậu nành kinako, Mật đường đen",
            "Thuần chay, Ngọt dịu", 5, 186, 4.4, 148),

        Make("thuc-uong", "ca-phe-lanh-u-cham", "Cà phê ủ lạnh Nhật", "水出しコーヒー", "Mizudashi coffee", 68000,
            "Cà phê nhỏ giọt lạnh suốt 10 tiếng, không chua gắt, hậu vị ca cao và đường nâu.",
            "Cà phê arabica, Nước lọc lạnh, Đá viên",
            "Cà phê, Món lạnh, Không sữa", 4, 12, 4.6, 232),

        Make("thuc-uong", "nuoc-loc-uji", "Trà lúa mạch mugicha", "麦茶", "Mugicha", 32000,
            "Trà lúa mạch rang uống lạnh, không caffeine, quán phục vụ miễn phí ly đầu cho mỗi khách.",
            "Lúa mạch rang, Nước lọc, Đá viên",
            "Không caffeine, Món lạnh, Giá tốt", 2, 4, 4.3, 186),
    ];
}
