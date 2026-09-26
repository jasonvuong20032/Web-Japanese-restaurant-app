using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm cơm và donburi — 15 món cơm bát, cơm cuộn trứng và cơm nắm.</summary>
    private static readonly IReadOnlyList<Dish> Donburi =
    [
        Make("donburi", "katsudon", "Cơm heo chiên xù katsudon", "カツ丼", "Katsudon", 145000,
            "Tonkatsu vừa chiên xong om nhanh với hành tây và trứng trong nước dashi ngọt, đặt trên cơm nóng.",
            "Thăn heo chiên xù, Trứng, Hành tây, Dashi, Mirin, Cơm",
            "Heo, Bán chạy, Kinh điển", 15, 920, 4.8, 398, isFeatured: true),

        Make("donburi", "oyakodon", "Cơm gà trứng oyakodon", "親子丼", "Oyakodon", 125000,
            "\"Cơm mẹ con\" — gà và trứng cùng om trong dashi, trứng chín lòng đào sánh mịn phủ lên cơm.",
            "Đùi gà, Trứng gà, Hành tây, Dashi, Mitsuba, Cơm",
            "Gà, Kinh điển, Giá tốt", 12, 720, 4.7, 312),

        Make("donburi", "gyudon", "Cơm bò gyudon", "牛丼", "Gyudon", 105000,
            "Bò Mỹ thái mỏng om hành tây trong nước tương ngọt, rải gừng hồng — bữa trưa nhanh của người Tokyo.",
            "Bò thái mỏng, Hành tây, Dashi, Nước tương, Gừng hồng, Cơm",
            "Bò, Giá tốt, Ăn nhanh", 8, 740, 4.6, 356),

        Make("donburi", "butadon-obihiro", "Cơm heo nướng butadon Obihiro", "帯広豚丼", "Obihiro butadon", 135000,
            "Thăn heo nướng than phết sốt ngọt kiểu Obihiro, Hokkaido, xếp chồng thành hình hoa trên cơm.",
            "Thăn heo, Sốt butadon, Cơm, Hạt đậu xanh",
            "Heo, Đặc sản, Nướng", 15, 780, 4.7, 164),

        Make("donburi", "tekkadon", "Cơm cá ngừ tekkadon", "鉄火丼", "Tekkadon", 225000,
            "Cá ngừ akami ngâm tương zuke xếp kín bát cơm sushi, rắc rong nori và wasabi.",
            "Cá ngừ akami, Nước tương, Cơm sushi, Rong nori, Wasabi",
            "Hải sản, Ít béo, Giàu đạm", 10, 520, 4.7, 188),

        Make("donburi", "negitoro-don", "Cơm negitoro", "ネギトロ丼", "Negitoro don", 195000,
            "Thịt cá ngừ béo nạo mịn trộn hành lá, phủ trên cơm sushi với lòng đỏ trứng cút.",
            "Cá ngừ nạo, Hành lá, Trứng cút, Cơm sushi, Rong nori",
            "Hải sản, Cá béo", 8, 560, 4.7, 176),

        Make("donburi", "sake-don", "Cơm cá hồi sake don", "サーモン丼", "Salmon don", 185000,
            "Cá hồi Na Uy thái dày xếp hình quạt trên cơm sushi, thêm trứng cá chuồn tobiko.",
            "Cá hồi, Trứng cá chuồn tobiko, Cơm sushi, Tía tô, Wasabi",
            "Hải sản, Cá béo, Bán chạy", 8, 580, 4.8, 324),

        Make("donburi", "teriyaki-chicken-don", "Cơm gà teriyaki", "照り焼きチキン丼", "Teriyaki chicken don", 115000,
            "Đùi gà áp chảo da giòn, rim sốt teriyaki bóng lưỡng, kèm trứng lòng đào và rau.",
            "Đùi gà, Sốt teriyaki, Trứng lòng đào, Cơm, Rau cải",
            "Gà, Trẻ em, Giá tốt", 12, 760, 4.6, 268),

        Make("donburi", "katsu-curry", "Cà ri Nhật heo chiên xù", "カツカレー", "Katsu curry", 155000,
            "Tonkatsu giòn đặt cạnh cà ri Nhật sánh đặc ninh táo mật ong, ăn kèm dưa fukujinzuke.",
            "Tonkatsu, Cà ri Nhật, Khoai tây, Cà rốt, Cơm, Dưa fukujinzuke",
            "Heo, Món chiên, Bán chạy", 15, 1020, 4.7, 296, spicyLevel: 1),

        Make("donburi", "omurice", "Cơm cuộn trứng omurice", "オムライス", "Omurice", 128000,
            "Cơm gà sốt cà chua bọc trong trứng omelette mềm, rạch đôi cho trứng chảy phủ kín, rưới sốt demi.",
            "Cơm gà sốt cà chua, Trứng gà, Sốt demi-glace, Bơ",
            "Trẻ em, Kinh điển", 15, 820, 4.7, 284),

        Make("donburi", "yakimeshi", "Cơm chiên Nhật yakimeshi", "焼き飯", "Yakimeshi", 98000,
            "Cơm chiên chảo gang với trứng, chashu thái hạt lựu và hành lá, hạt cơm tơi bóng dầu.",
            "Cơm, Trứng, Thịt chashu, Hành lá, Nước tương",
            "Heo, Giá tốt", 10, 680, 4.5, 196),

        Make("donburi", "onigiri-set", "Set cơm nắm onigiri 3 vị", "おにぎりセット", "Onigiri set", 88000,
            "Ba nắm cơm: cá hồi nướng, mơ muối umeboshi và cá ngừ mayo, bọc rong nori giòn, kèm súp miso.",
            "Cơm, Cá hồi nướng, Mơ muối, Cá ngừ mayo, Rong nori, Súp miso",
            "Giá tốt, Ăn nhanh, Trẻ em", 8, 540, 4.6, 242),

        Make("donburi", "ochazuke-ca-hoi", "Cơm chan trà ochazuke cá hồi", "鮭茶漬け", "Sake chazuke", 95000,
            "Cơm nóng với cá hồi nướng xé, chan trà dashi nóng, thêm rong nori và wasabi — nhẹ bụng cuối bữa.",
            "Cơm, Cá hồi nướng, Trà dashi, Rong nori, Wasabi, Mè",
            "Hải sản, Thanh nhẹ, Ít béo", 6, 380, 4.5, 118),

        Make("donburi", "takikomi-gohan", "Cơm niêu takikomi gohan", "炊き込みご飯", "Takikomi gohan", 118000,
            "Gạo nấu cùng nấm, gà, ngưu bàng và cà rốt trong nồi đất với dashi, cháy giòn đáy nồi.",
            "Gạo Koshihikari, Nấm shiitake, Thịt gà, Ngưu bàng, Cà rốt, Dashi",
            "Gà, Mùa lạnh, Kinh điển", 25, 520, 4.6, 104),

        Make("donburi", "soboro-don", "Cơm ba màu soboro", "三色そぼろ丼", "Sanshoku soboro don", 105000,
            "Ba dải màu trên cơm: gà xay rim tương, trứng bác tơi và đậu Hà Lan xanh — món cơm hộp gia đình.",
            "Gà xay, Trứng, Đậu Hà Lan, Nước tương, Gừng, Cơm",
            "Gà, Trẻ em, Giá tốt", 10, 620, 4.5, 132),
    ];
}
