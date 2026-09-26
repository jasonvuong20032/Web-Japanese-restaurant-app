using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm mochi và wagashi — 25 món bánh nếp và bánh truyền thống Nhật.</summary>
    private static readonly IReadOnlyList<Dish> Mochi =
    [
        Make("mochi", "mochi-nhan-dau-do-truyen-thong", "Mochi nhân đậu đỏ truyền thống", "こしあん大福", "Koshian daifuku", 45000,
            "Bột nếp giã tay mỗi sáng, bọc nhân đậu đỏ lọc mịn koshian, phủ lớp bột khoai mỏng.",
            "Bột nếp mochigome, Đậu đỏ azuki, Đường mía, Bột katakuri",
            "Mochi, Kinh điển, Không sữa", 3, 176, 4.7, 348),

        Make("mochi", "mochi-xoai-kem", "Mochi xoài kem tươi", "マンゴー大福", "Mango daifuku", 65000,
            "Xoài cát chín cây cắt miếng lớn, bọc kem tươi và vỏ mochi trắng mỏng, ăn lạnh.",
            "Xoài cát, Kem tươi, Bột nếp, Đường",
            "Mochi, Trái cây, Mùa hè", 3, 188, 4.8, 262),

        Make("mochi", "mochi-kem-viet-quat", "Mochi kem việt quất", "ベリークリーム大福", "Berry cream daifuku", 68000,
            "Vỏ mochi hồng nhuộm củ dền, nhân kem tươi đánh cùng mứt việt quất, cắt đôi thấy lõi tím mọng.",
            "Việt quất, Kem tươi, Bột nếp, Củ dền, Đường",
            "Mochi, Trái cây, Mùa hè", 3, 172, 4.8, 186, isFeatured: true),

        Make("mochi", "mochi-kem-dau-tay", "Mochi kem dâu", "いちごアイス大福", "Strawberry ice mochi", 65000,
            "Kem dâu tây Nhật bọc vỏ mochi dẻo, để ra ngoài hai phút cho vỏ mềm lại rồi mới ăn.",
            "Kem dâu tây, Bột nếp, Đường, Bột bắp",
            "Mochi, Trẻ em, Mùa hè", 2, 152, 4.7, 298),

        Make("mochi", "mochi-kem-xoai", "Mochi kem xoài", "マンゴーアイス大福", "Mango ice mochi", 65000,
            "Kem sorbet xoài chua ngọt nằm trong lớp mochi mềm, mát lạnh cho ngày nóng.",
            "Kem xoài, Bột nếp, Đường",
            "Mochi, Mùa hè, Trái cây", 2, 148, 4.6, 176),

        Make("mochi", "yomogi-daifuku", "Mochi ngải cứu yomogi", "よもぎ大福", "Yomogi daifuku", 52000,
            "Vỏ mochi giã cùng lá ngải yomogi non, màu xanh rêu và mùi cỏ tươi, nhân đậu đỏ hạt.",
            "Bột nếp, Lá ngải yomogi, Đậu đỏ tsubuan",
            "Mochi, Thuần chay, Kinh điển", 3, 170, 4.6, 124),

        Make("mochi", "sakura-mochi", "Sakura mochi lá anh đào", "桜餅", "Sakura mochi", 55000,
            "Gạo nếp đồ domyoji nhuộm hồng, bọc đậu đỏ và cuộn trong lá anh đào muối — vị của mùa xuân.",
            "Gạo nếp domyoji, Đậu đỏ, Lá anh đào muối",
            "Mochi, Theo mùa, Thuần chay", 3, 158, 4.8, 214, isFeatured: true),

        Make("mochi", "kusa-mochi", "Kusa mochi", "草餅", "Kusa mochi", 48000,
            "Bánh nếp cỏ truyền thống ngày lễ bé gái Hinamatsuri, rắc bột đậu nành kinako thơm bùi.",
            "Bột nếp, Lá ngải, Đậu đỏ, Bột kinako",
            "Mochi, Thuần chay", 3, 160, 4.5, 86),

        Make("mochi", "kinako-mochi", "Mochi bột đậu nành kinako", "きな粉餅", "Kinako mochi", 45000,
            "Mochi nướng nóng lăn qua bột đậu nành rang kinako trộn đường, rưới mật đường đen.",
            "Bánh mochi, Bột kinako, Đường, Mật đường đen kuromitsu",
            "Mochi, Thuần chay, Giá tốt", 5, 182, 4.6, 142),

        Make("mochi", "isobe-yaki-mochi", "Mochi nướng cuốn rong biển", "磯辺焼き", "Isobeyaki mochi", 48000,
            "Mochi nướng phồng trên than, phết nước tương ngọt rồi cuốn rong biển nori — vị mặn ngọt.",
            "Bánh mochi, Nước tương, Đường, Rong nori",
            "Mochi, Thuần chay, Nướng", 6, 190, 4.6, 118),

        Make("mochi", "zenzai-mochi-nuong", "Chè đậu đỏ zenzai mochi nướng", "ぜんざい", "Zenzai", 65000,
            "Chè đậu đỏ nấu sệt ăn nóng với hai viên mochi nướng xém cạnh, kèm dưa muối chấm vị.",
            "Đậu đỏ, Mochi nướng, Đường, Muối",
            "Mochi, Mùa lạnh, Thuần chay", 8, 280, 4.7, 156),

        Make("mochi", "shiratama-dango-matcha", "Shiratama matcha đậu đỏ", "白玉抹茶あずき", "Shiratama matcha", 72000,
            "Viên nếp shiratama mềm dẻo với kem matcha, đậu đỏ và mật đường đen.",
            "Bột nếp shiratamako, Kem matcha, Đậu đỏ, Mật đường đen",
            "Mochi, Matcha", 5, 238, 4.7, 164),

        Make("mochi", "hanami-dango", "Hanami dango ba màu", "花見団子", "Hanami dango", 48000,
            "Xiên ba viên dango hồng, trắng, xanh tượng trưng cho mùa hoa anh đào, ngọt nhẹ và dai.",
            "Bột gạo, Bột nếp, Bột matcha, Màu củ dền",
            "Mochi, Theo mùa, Thuần chay, Xiên", 3, 150, 4.6, 196),

        Make("mochi", "anko-dango", "Dango phủ đậu đỏ", "あんこ団子", "Anko dango", 48000,
            "Xiên dango nướng nhẹ, phủ dày lớp đậu đỏ mịn — người anh em của mitarashi dango.",
            "Bột gạo, Bột nếp, Đậu đỏ koshian",
            "Mochi, Thuần chay, Xiên", 4, 168, 4.5, 102),

        Make("mochi", "botamochi-ohagi", "Ohagi nếp phủ đậu", "おはぎ", "Ohagi", 52000,
            "Nếp giã dở bọc ngoài bằng đậu đỏ, vừng đen và kinako — món bánh dịp lễ Higan.",
            "Gạo nếp, Đậu đỏ, Vừng đen, Bột kinako",
            "Mochi, Kinh điển, Thuần chay", 3, 210, 4.5, 94),

        Make("mochi", "kashiwa-mochi", "Kashiwa mochi lá sồi", "柏餅", "Kashiwa mochi", 52000,
            "Bánh nếp nhân đậu gói trong lá sồi kashiwa, ăn ngày Tết thiếu nhi 5/5 để cầu may.",
            "Bột gạo joshinko, Đậu đỏ, Lá sồi kashiwa",
            "Mochi, Theo mùa, Thuần chay", 3, 166, 4.5, 72),

        Make("mochi", "yatsuhashi-kyoto", "Bánh yatsuhashi Kyoto", "生八ツ橋", "Nama yatsuhashi", 58000,
            "Lá bánh nếp quế mỏng gấp tam giác bọc đậu đỏ — món quà nổi tiếng nhất của Kyoto.",
            "Bột gạo, Quế, Đậu đỏ, Bột matcha",
            "Mochi, Đặc sản", 2, 142, 4.6, 128),

        Make("mochi", "nerikiri-hoa-mua", "Nerikiri tạo hình hoa theo mùa", "練り切り", "Nerikiri", 68000,
            "Wagashi nặn tay bằng đậu trắng và nếp, tạo hình hoa theo mùa, dùng kèm trà matcha.",
            "Đậu trắng shiroan, Bột nếp gyuhi, Màu tự nhiên",
            "Mochi, Đặc sản, Thuần chay", 3, 128, 4.8, 112),

        Make("mochi", "yokan-matcha", "Thạch đậu yokan matcha", "抹茶羊羹", "Matcha yokan", 52000,
            "Thạch đậu trắng nấu với thạch kanten và matcha, cắt thành thanh dày, đậm và mịn.",
            "Đậu trắng, Thạch kanten, Bột matcha, Đường",
            "Mochi, Matcha, Thuần chay", 2, 158, 4.5, 88),

        Make("mochi", "mizu-yokan", "Mizu yokan mát lạnh", "水羊羹", "Mizu yokan", 48000,
            "Yokan loãng hơn, trong và mát, tan ngay trong miệng — wagashi mùa hè.",
            "Đậu đỏ, Thạch kanten, Đường",
            "Mochi, Mùa hè, Món lạnh, Thuần chay", 2, 112, 4.4, 64),

        Make("mochi", "kuzumochi", "Kuzumochi mật đen", "くずもち", "Kuzumochi", 55000,
            "Bánh bột sắn dây kuzu trong suốt, dai mát, rưới mật đường đen và rắc kinako.",
            "Bột sắn dây kuzu, Mật đường đen, Bột kinako",
            "Mochi, Món lạnh, Thuần chay", 3, 138, 4.5, 76),

        Make("mochi", "mochi-phomai-nuong", "Mochi phô mai nướng", "チーズ餅", "Cheese mochi", 58000,
            "Mochi nướng kẹp phô mai tan chảy và rong biển, món ăn vặt mặn ngọt khi còn nóng.",
            "Bánh mochi, Phô mai, Rong nori, Nước tương",
            "Mochi, Nướng, Trẻ em", 6, 240, 4.5, 98),

        Make("mochi", "raindrop-cake", "Bánh giọt nước mizu shingen", "水信玄餅", "Mizu shingen mochi", 62000,
            "Giọt thạch nước suối trong suốt như pha lê, lắc nhẹ là rung rinh, chấm kinako và mật đen.",
            "Nước suối, Thạch agar, Bột kinako, Mật đường đen",
            "Mochi, Ít calo, Thuần chay", 2, 48, 4.6, 142),

        Make("mochi", "monaka-kem", "Monaka kẹp kem vani đậu đỏ", "アイス最中", "Ice monaka", 58000,
            "Vỏ bánh nếp giòn xốp kẹp kem vani và đậu đỏ, ăn ngay khi vỏ còn giòn.",
            "Vỏ monaka, Kem vani, Đậu đỏ",
            "Mochi, Trẻ em", 2, 216, 4.6, 118),

        Make("mochi", "set-wagashi-tra-dao", "Set wagashi trà đạo", "和菓子と抹茶セット", "Wagashi and matcha set", 145000,
            "Ba viên wagashi theo mùa và một bát matcha usucha đánh bằng chasen — nghi thức trà đạo thu nhỏ.",
            "Nerikiri, Daifuku, Yokan, Matcha Uji",
            "Mochi, Matcha, Set, Đặc sản", 8, 360, 4.9, 176, isFeatured: true),
    ];
}
