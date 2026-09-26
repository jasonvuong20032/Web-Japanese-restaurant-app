using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm tempura và đồ chiên — 20 món tempura, katsu, karaage và ăn vặt chiên.</summary>
    private static readonly IReadOnlyList<Dish> Tempura =
    [
        Make("tempura", "tempura-tom-su", "Tempura tôm sú", "海老天ぷら", "Ebi tempura", 158000,
            "Bốn con tôm sú duỗi thẳng, nhúng bột lạnh đá và chiên dầu mè 175°C — vỏ bột mỏng như ren.",
            "Tôm sú, Bột tempura, Dầu mè, Nước chấm tentsuyu, Củ cải bào",
            "Món chiên, Hải sản, Bán chạy, Giòn", 12, 380, 4.8, 426, isFeatured: true),

        Make("tempura", "tempura-moriawase", "Tempura thập cẩm", "天ぷら盛り合わせ", "Tempura moriawase", 198000,
            "Hai tôm, cá bạc, mực, bí đỏ, cà tím, nấm và lá tía tô chiên giòn, chấm muối matcha hoặc tentsuyu.",
            "Tôm, Cá kisu, Mực, Bí đỏ, Cà tím, Nấm shiitake, Lá tía tô",
            "Món chiên, Chia sẻ, Kinh điển", 15, 520, 4.8, 312),

        Make("tempura", "tempura-rau-cu-chay", "Tempura rau củ chay", "野菜天ぷら", "Yasai tempura", 118000,
            "Bảy loại rau củ theo mùa chiên giòn, bột pha nước khoáng có ga nên nhẹ và không ngấm dầu.",
            "Bí đỏ, Khoai lang, Cà tím, Ớt chuông, Nấm, Củ sen, Lá tía tô",
            "Món chiên, Thuần chay, Giá tốt", 12, 340, 4.6, 168),

        Make("tempura", "kakiage", "Kakiage rau củ tôm nhỏ", "かき揚げ", "Kakiage", 98000,
            "Hành tây, cà rốt và tôm nhỏ trộn bột chiên thành bánh tròn giòn rụm, ăn với udon hoặc cơm.",
            "Hành tây, Cà rốt, Tôm nhỏ, Bột tempura, Tentsuyu",
            "Món chiên, Giá tốt, Giòn", 10, 420, 4.5, 146),

        Make("tempura", "tonkatsu-than-lung", "Tonkatsu thăn lưng heo", "ロースとんかつ", "Rosu tonkatsu", 168000,
            "Thăn lưng heo Kurobuta dày 2cm, bột panko tươi chiên hai lần nhiệt, ăn cùng bắp cải bào và sốt.",
            "Thăn heo Kurobuta, Bột panko, Bắp cải, Sốt tonkatsu, Mè xay, Cơm",
            "Món chiên, Heo, Bán chạy, Giòn", 18, 860, 4.8, 384, isFeatured: true),

        Make("tempura", "hire-katsu", "Hirekatsu thăn nội heo", "ヒレカツ", "Hire katsu", 178000,
            "Thăn nội heo nạc mềm, ít mỡ, chiên xù vàng ruộm — lựa chọn nhẹ hơn tonkatsu thăn lưng.",
            "Thăn nội heo, Bột panko, Bắp cải, Sốt tonkatsu, Cơm",
            "Món chiên, Heo, Ít mỡ", 18, 720, 4.7, 214),

        Make("tempura", "chicken-katsu", "Gà chiên xù chicken katsu", "チキンカツ", "Chicken katsu", 138000,
            "Đùi gà rút xương chiên panko, giòn ngoài mọng trong, rưới sốt tonkatsu ngọt thơm.",
            "Đùi gà, Bột panko, Sốt tonkatsu, Bắp cải, Cơm",
            "Món chiên, Gà, Trẻ em", 15, 780, 4.6, 196),

        Make("tempura", "karaage-ga", "Gà chiên karaage", "鶏の唐揚げ", "Karaage", 118000,
            "Đùi gà ướp gừng, tỏi, nước tương qua đêm, lăn bột khoai tây chiên hai lần cho giòn lâu.",
            "Đùi gà, Gừng, Tỏi, Nước tương, Bột khoai tây, Sốt mayo, Chanh",
            "Món chiên, Gà, Bán chạy, Trẻ em", 12, 560, 4.8, 452),

        Make("tempura", "chicken-nanban", "Gà nanban Miyazaki", "チキン南蛮", "Chicken nanban", 145000,
            "Gà chiên nhúng giấm ngọt rồi phủ sốt tartare trứng kiểu Miyazaki, chua ngọt béo hài hoà.",
            "Đùi gà, Giấm nanban, Sốt tartare, Bắp cải, Cơm",
            "Món chiên, Gà, Đặc sản", 15, 820, 4.7, 168),

        Make("tempura", "ebi-fry", "Tôm chiên xù ebi fry", "エビフライ", "Ebi fry", 165000,
            "Tôm to tẩm panko chiên thẳng tắp, chấm sốt tartare trứng dưa chuột muối.",
            "Tôm sú, Bột panko, Sốt tartare, Bắp cải, Chanh",
            "Món chiên, Hải sản, Trẻ em", 12, 460, 4.6, 184),

        Make("tempura", "kaki-fry", "Hàu chiên xù kaki fry", "カキフライ", "Kaki fry", 185000,
            "Hàu Hiroshima to tẩm panko chiên nhanh để bên trong vẫn mọng nước biển.",
            "Hàu Hiroshima, Bột panko, Sốt tartare, Chanh",
            "Món chiên, Hải sản, Mùa lạnh", 12, 420, 4.7, 128),

        Make("tempura", "korokke-khoai-tay", "Korokke khoai tây bò", "コロッケ", "Korokke", 68000,
            "Khoai tây nghiền trộn thịt bò xay và hành tây, chiên xù vàng — món ăn vặt tuổi thơ người Nhật.",
            "Khoai tây, Thịt bò xay, Hành tây, Bột panko, Sốt tonkatsu",
            "Món chiên, Giá tốt, Trẻ em", 10, 320, 4.5, 176),

        Make("tempura", "kani-cream-korokke", "Korokke kem cua", "カニクリームコロッケ", "Kani cream korokke", 98000,
            "Vỏ panko giòn, bên trong là sốt béchamel nóng chảy với thịt cua tuyết.",
            "Thịt cua, Sốt béchamel, Bột panko, Sốt cà chua",
            "Món chiên, Hải sản", 12, 360, 4.6, 124),

        Make("tempura", "kushikatsu-set", "Set xiên chiên kushikatsu Osaka", "串カツ盛り合わせ", "Kushikatsu set", 168000,
            "Mười xiên thịt, hải sản và rau chiên xù kiểu Osaka, chấm sốt chung — không chấm hai lần!",
            "Thịt bò, Thịt heo, Tôm, Mực, Hành, Khoai tây, Sốt kushikatsu",
            "Món chiên, Xiên, Chia sẻ, Đặc sản", 15, 680, 4.7, 158),

        Make("tempura", "agedashi-tofu", "Đậu phụ chiên agedashi", "揚げ出し豆腐", "Agedashi tofu", 78000,
            "Đậu phụ non lăn bột khoai chiên nhẹ, ngâm trong nước dashi nóng, thêm củ cải bào và gừng.",
            "Đậu phụ non, Bột khoai tây, Dashi, Củ cải bào, Gừng, Hành lá",
            "Món chiên, Thanh nhẹ", 10, 220, 4.6, 184),

        Make("tempura", "takoyaki", "Bánh bạch tuộc takoyaki", "たこ焼き", "Takoyaki", 78000,
            "Tám viên takoyaki nhân bạch tuộc, rưới sốt, mayo và rắc cá bào nhảy múa trên mặt bánh nóng.",
            "Bạch tuộc, Bột bánh, Gừng hồng, Sốt takoyaki, Mayo, Cá bào katsuobushi",
            "Giá tốt, Bán chạy, Đặc sản", 10, 380, 4.7, 518),

        Make("tempura", "okonomiyaki-osaka", "Bánh xèo Nhật okonomiyaki", "お好み焼き", "Okonomiyaki", 145000,
            "Bánh bắp cải và bột nướng trên bàn sắt với ba chỉ heo, tôm, mực, phủ sốt và cá bào.",
            "Bắp cải, Bột bánh, Ba chỉ heo, Tôm, Mực, Sốt okonomi, Mayo, Rong aonori",
            "Đặc sản, Chia sẻ", 18, 620, 4.7, 256),

        Make("tempura", "ika-geso-karaage", "Râu mực chiên giòn", "ゲソ唐揚げ", "Geso karaage", 88000,
            "Râu mực ướp gừng chiên giòn, dai sần sật, món nhắm khoái khẩu trong izakaya.",
            "Râu mực, Bột khoai tây, Gừng, Chanh, Mayo shichimi",
            "Món chiên, Hải sản", 10, 280, 4.5, 118, spicyLevel: 1),

        Make("tempura", "ten-don", "Cơm tempura tendon", "天丼", "Tendon", 175000,
            "Tôm, cá, rau củ tempura chất trên cơm nóng, rưới sốt tendon đậm ngọt thấm xuống cơm.",
            "Tôm tempura, Cá kisu, Bí đỏ, Cà tím, Sốt tendon, Cơm",
            "Món chiên, Bán chạy", 15, 820, 4.8, 286),

        Make("tempura", "gyoza-hanetsuki", "Gyoza chiên váy giòn", "羽根つき餃子", "Hanetsuki gyoza", 88000,
            "Tám chiếc gyoza nối nhau bằng lớp váy bột giòn mỏng như cánh ve, đáy vàng giòn.",
            "Thịt heo, Bắp cải, Hẹ, Gừng, Vỏ gyoza, Giấm tương ớt",
            "Món chiên, Heo, Giá tốt", 12, 360, 4.6, 214),
    ];
}
