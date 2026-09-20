using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm tráng miệng — 20 món gồm purin, mochi, dorayaki và kem.</summary>
    private static readonly IReadOnlyList<Dish> TrangMieng =
    [
        Make("trang-mieng", "purin-trung-co-dien", "Purin trứng cổ điển", "カスタードプリン", "Custard purin", 58000,
            "Bánh flan kiểu Nhật hấp cách thuỷ ở 85°C, mềm rung rinh, lớp caramel đắng nhẹ ở đáy.",
            "Trứng gà, Sữa tươi, Đường mía, Vani Madagascar, Caramel",
            "Bán chạy, Kinh điển, Trẻ em", 5, 218, 4.8, 684, isFeatured: true),

        Make("trang-mieng", "purin-matcha", "Purin matcha", "抹茶プリン", "Matcha purin", 68000,
            "Purin pha bột matcha Uji, đắng dịu đầu lưỡi rồi ngọt dần, rắc đậu đỏ nguyên hạt.",
            "Bột matcha Uji, Trứng gà, Sữa tươi, Đậu đỏ, Kem tươi",
            "Matcha, Bán chạy", 5, 236, 4.8, 462),

        Make("trang-mieng", "purin-hojicha", "Purin hojicha", "ほうじ茶プリン", "Hojicha purin", 66000,
            "Trà xanh rang hojicha cho purin màu nâu hổ phách và mùi khói ngọt rất dễ chịu.",
            "Trà hojicha, Trứng gà, Sữa tươi, Đường mía, Kem tươi",
            "Trà, Thanh nhẹ", 5, 222, 4.6, 218),

        Make("trang-mieng", "purin-bi-do-hokkaido", "Purin bí đỏ Hokkaido", "かぼちゃプリン", "Kabocha purin", 72000,
            "Bí đỏ Hokkaido hấp nghiền mịn trộn vào trứng sữa, ngọt tự nhiên, kết cấu đặc hơn purin thường.",
            "Bí đỏ Hokkaido, Trứng gà, Sữa tươi, Quế, Kem tươi",
            "Theo mùa, Ngọt dịu", 6, 256, 4.5, 164),

        Make("trang-mieng", "purin-tra-sua-den", "Purin đường đen", "黒糖プリン", "Kokuto purin", 68000,
            "Dùng đường đen Okinawa thay caramel, vị mật mía sâu và hơi khoáng, hậu vị dài.",
            "Đường đen Okinawa, Trứng gà, Sữa tươi, Kem tươi",
            "Đậm đà, Đặc sản", 5, 248, 4.6, 186),

        Make("trang-mieng", "daifuku-dau-tay", "Daifuku dâu tây", "いちご大福", "Ichigo daifuku", 62000,
            "Quả dâu nguyên trái bọc đậu đỏ trắng và vỏ mochi mỏng, cắn ngang thấy dâu còn nguyên.",
            "Dâu tây, Đậu trắng shiroan, Bột nếp mochi, Đường",
            "Mochi, Bán chạy, Theo mùa", 4, 168, 4.9, 596, isFeatured: true),

        Make("trang-mieng", "daifuku-dau-do", "Daifuku đậu đỏ", "豆大福", "Mame daifuku", 48000,
            "Bản mochi truyền thống nhất: vỏ nếp dẻo điểm hạt đậu đen, nhân đậu đỏ tsubuan nguyên hạt.",
            "Bột nếp mochi, Đậu đỏ tsubuan, Đậu đen, Đường",
            "Mochi, Kinh điển, Giá tốt", 4, 182, 4.6, 324),

        Make("trang-mieng", "mochi-kem-matcha", "Mochi kem matcha", "抹茶アイス大福", "Matcha ice mochi", 65000,
            "Viên kem matcha bọc vỏ mochi, ăn lạnh nên vỏ hơi cứng lúc đầu rồi mềm dần trong miệng.",
            "Kem matcha, Bột nếp mochi, Kem tươi, Bột matcha",
            "Mochi, Matcha, Món lạnh", 3, 196, 4.7, 412),

        Make("trang-mieng", "mochi-kem-vung-den", "Mochi kem vừng đen", "黒ごまアイス大福", "Kurogoma ice mochi", 65000,
            "Kem vừng đen rang cháy cạnh, béo bùi và thơm gắt, bọc trong vỏ mochi trắng.",
            "Kem vừng đen, Bột nếp mochi, Vừng đen rang, Kem tươi",
            "Mochi, Món lạnh, Đậm đà", 3, 214, 4.6, 248),

        Make("trang-mieng", "warabi-mochi", "Warabi mochi", "わらび餅", "Warabi mochi", 58000,
            "Mochi bột dương xỉ trong như thạch, lăn bột đậu nành kinako và rưới mật đường đen.",
            "Bột warabi, Bột đậu nành kinako, Mật đường đen, Đường",
            "Mochi, Thanh nhẹ, Kinh điển", 4, 168, 4.7, 286),

        Make("trang-mieng", "dango-mitarashi", "Dango mitarashi", "みたらし団子", "Mitarashi dango", 52000,
            "Ba xiên bánh nếp nướng sơ cho cháy cạnh, rưới sốt tương ngọt sánh còn nóng.",
            "Bột nếp, Nước tương, Đường, Bột năng",
            "Mochi, Xiên, Giá tốt", 6, 186, 4.5, 274),

        Make("trang-mieng", "dorayaki-dau-do", "Dorayaki đậu đỏ", "どら焼き", "Dorayaki", 55000,
            "Hai lớp bánh rán mật ong kẹp đậu đỏ, ăn ấm thì bánh xốp và thơm mùi mật.",
            "Bột mì, Trứng gà, Mật ong, Đậu đỏ anko",
            "Kinh điển, Trẻ em, Bán chạy", 6, 268, 4.6, 358),

        Make("trang-mieng", "dorayaki-kem-matcha", "Dorayaki kem matcha", "抹茶生どら焼き", "Matcha nama dorayaki", 72000,
            "Phiên bản lạnh: kẹp kem tươi matcha dày cộp giữa hai lớp bánh, ăn ngay khi lấy khỏi tủ.",
            "Bột mì, Trứng gà, Kem tươi matcha, Đậu đỏ, Mật ong",
            "Matcha, Món lạnh", 6, 312, 4.7, 232),

        Make("trang-mieng", "taiyaki-dau-do", "Taiyaki cá chép", "たい焼き", "Taiyaki", 52000,
            "Bánh hình cá nướng khuôn gang, vỏ giòn ở vây và mềm ở thân, nhân đậu đỏ tới tận đuôi.",
            "Bột mì, Đậu đỏ anko, Trứng gà, Sữa tươi",
            "Kinh điển, Trẻ em, Giá tốt", 8, 246, 4.5, 312),

        Make("trang-mieng", "kem-matcha-2-vien", "Kem matcha 2 viên", "抹茶アイス", "Matcha ice cream", 62000,
            "Hai viên kem matcha đậm độ số 5, đắng rõ, rắc bột matcha nguyên chất lên mặt.",
            "Bột matcha Uji, Sữa tươi, Kem tươi, Đường mía",
            "Matcha, Món lạnh, Bán chạy", 3, 248, 4.8, 486, isFeatured: true),

        Make("trang-mieng", "kem-vung-den-2-vien", "Kem vừng đen 2 viên", "黒ごまアイス", "Kurogoma ice cream", 62000,
            "Vừng đen rang tay rồi xay nhuyễn, kem có màu xám tro và vị bùi rất đậm.",
            "Vừng đen, Sữa tươi, Kem tươi, Đường mía",
            "Món lạnh, Đậm đà", 3, 262, 4.6, 218),

        Make("trang-mieng", "parfait-matcha", "Parfait matcha", "抹茶パフェ", "Matcha parfait", 128000,
            "Ly cao xếp lớp kem matcha, đậu đỏ, thạch trà, bánh quy giòn và kem tươi — món để chụp ảnh.",
            "Kem matcha, Đậu đỏ, Thạch hojicha, Kem tươi, Bánh quy, Mochi viên",
            "Matcha, Món lạnh, Chia sẻ, Bán chạy", 10, 486, 4.8, 342, isFeatured: true),

        Make("trang-mieng", "kakigori-dau-tay", "Kakigori dâu tây", "いちごかき氷", "Ichigo kakigori", 98000,
            "Đá bào mịn như tuyết, rưới sốt dâu tươi nấu ít đường và một lớp sữa đặc mỏng.",
            "Đá bào, Dâu tây, Sữa đặc, Đường mía, Kem tươi",
            "Món lạnh, Mùa hè, Chia sẻ", 8, 286, 4.6, 196),

        Make("trang-mieng", "anmitsu", "Anmitsu thạch đậu đỏ", "あんみつ", "Anmitsu", 88000,
            "Thạch kanten trong, đậu đỏ, viên mochi shiratama và trái cây, chan mật đường đen.",
            "Thạch kanten, Đậu đỏ, Mochi shiratama, Trái cây theo mùa, Mật đường đen",
            "Kinh điển, Thanh nhẹ", 7, 268, 4.5, 174),

        Make("trang-mieng", "cheesecake-nhat", "Cheesecake Nhật bông", "スフレチーズケーキ", "Souffle cheesecake", 78000,
            "Cheesecake kiểu soufflé nướng cách thuỷ, nhẹ xốp như bông, lắc nhẹ là rung.",
            "Phô mai kem, Trứng gà, Sữa tươi, Bột mì, Chanh vàng",
            "Bán chạy, Ngọt dịu", 6, 286, 4.7, 428),
    ];
}
