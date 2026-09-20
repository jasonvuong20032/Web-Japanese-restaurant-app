using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm BBQ Nhật — 18 món yakiniku, yakitori và set nướng.</summary>
    private static readonly IReadOnlyList<Dish> Bbq =
    [
        Make("bbq", "wagyu-a5-karubi", "Wagyu A5 karubi", "和牛A5カルビ", "Wagyu A5 karubi", 685000,
            "Sườn non wagyu A5 vân mỡ dày như tuyết, nướng mỗi mặt 20 giây là mỡ đã tan ra.",
            "Bò wagyu A5, Sốt tare nhà làm, Muối Okinawa, Tỏi nướng",
            "Cao cấp, Bò, Đặc sản", 10, 642, 4.9, 186, isFeatured: true),

        Make("bbq", "wagyu-a5-loin", "Wagyu A5 thăn lưng", "和牛A5ロース", "Wagyu A5 loin", 745000,
            "Thăn lưng ít mỡ hơn karubi nhưng thớ mịn hơn, chấm muối wasabi để giữ vị nguyên bản.",
            "Bò wagyu A5, Muối wasabi, Chanh vàng, Tỏi nướng",
            "Cao cấp, Bò", 10, 586, 4.9, 142),

        Make("bbq", "bo-my-karubi", "Sườn bò karubi", "カルビ", "Karubi", 285000,
            "Sườn bò Mỹ ướp tare 12 tiếng, cắt dày 5mm, cháy cạnh nhưng lòng vẫn hồng.",
            "Sườn bò Mỹ, Sốt tare, Tỏi, Mè rang, Hành lá",
            "Bò, Bán chạy, Đậm đà", 9, 524, 4.8, 486, isFeatured: true),

        Make("bbq", "bo-harami", "Bò harami thăn chảy", "ハラミ", "Harami", 268000,
            "Phần cơ hoành mềm bất ngờ, thớ dài và mọng nước, dân sành yakiniku hay gọi đầu tiên.",
            "Thăn chảy bò, Sốt tare, Tiêu đen, Chanh",
            "Bò, Ít mỡ", 9, 448, 4.8, 324),

        Make("bbq", "bo-tan-luoi", "Lưỡi bò gyutan", "牛タン", "Gyutan", 295000,
            "Lưỡi bò thái lát mỏng, khía chéo hai mặt, nướng nhanh tới khi rìa cong lại thì vắt chanh.",
            "Lưỡi bò, Muối biển, Chanh vàng, Tiêu đen, Hành lá",
            "Bò, Đặc sản, Bán chạy", 8, 386, 4.8, 398),

        Make("bbq", "heo-ba-chi", "Ba chỉ heo samgyeop kiểu Nhật", "豚バラ", "Butabara", 168000,
            "Ba chỉ heo cắt dày, nướng tới khi mỡ trong và mép giòn, cuốn rau xà lách chấm miso.",
            "Ba chỉ heo, Miso nướng, Xà lách, Tỏi, Ớt xanh",
            "Heo, Giá tốt, Chia sẻ", 11, 612, 4.6, 288),

        Make("bbq", "heo-co-tonteki", "Nạc cổ heo tonteki", "豚トロ", "Tontoro", 148000,
            "Nạc cổ heo giòn sần, mỡ xen kẽ đều, nướng xong rắc muối tiêu chanh là đủ.",
            "Nạc cổ heo, Muối tiêu, Chanh, Hành lá",
            "Heo, Giòn, Giá tốt", 9, 486, 4.5, 214),

        Make("bbq", "ga-yakitori-momo", "Yakitori đùi gà", "もも串", "Momo yakitori", 62000,
            "Hai xiên đùi gà xen hành boaro, quét tare ba lượt trên than cho lớp vỏ bóng như men.",
            "Đùi gà, Hành boaro, Sốt tare, Tiêu shichimi",
            "Gà, Xiên nướng, Bán chạy", 10, 268, 4.7, 542, isFeatured: true),

        Make("bbq", "ga-yakitori-tsukune", "Yakitori tsukune viên gà", "つくね串", "Tsukune", 68000,
            "Viên gà băm trộn sụn cho giòn, chấm lòng đỏ trứng sống đánh tan bên cạnh.",
            "Thịt gà băm, Sụn gà, Lòng đỏ trứng, Sốt tare, Hành lá",
            "Gà, Xiên nướng, Bán chạy", 11, 312, 4.8, 468),

        Make("bbq", "ga-yakitori-kawa", "Yakitori da gà giòn", "皮串", "Kawa yakitori", 48000,
            "Da gà cuộn chặt nướng chậm 15 phút cho mỡ rớt hết, còn lại lớp da giòn rụm.",
            "Da gà, Muối biển, Tiêu shichimi, Chanh",
            "Gà, Xiên nướng, Giòn, Giá tốt", 15, 224, 4.6, 356),

        Make("bbq", "ga-yakitori-negima", "Yakitori negima", "ねぎま串", "Negima", 58000,
            "Xen kẽ ức gà và hành lá to bản, hành ngọt lên khi cháy xém giúp thịt bớt khô.",
            "Ức gà, Hành lá to, Sốt tare, Muối biển",
            "Gà, Xiên nướng, Ít béo", 10, 232, 4.5, 288),

        Make("bbq", "hai-san-tom-nuong", "Tôm sú nướng muối", "海老の塩焼き", "Ebi shioyaki", 185000,
            "Bốn con tôm sú nướng nguyên vỏ giữ trọn nước ngọt, vắt chanh khi vỏ vừa chuyển đỏ cam.",
            "Tôm sú, Muối biển, Chanh vàng, Bơ tỏi",
            "Hải sản, Nướng", 12, 268, 4.7, 234),

        Make("bbq", "hai-san-so-diep-nuong", "Sò điệp nướng bơ tỏi", "帆立バター焼き", "Hotate butter yaki", 198000,
            "Sò điệp nướng nguyên vỏ với bơ tỏi và nước tương, thơm tới bàn bên cạnh.",
            "Sò điệp Hokkaido, Bơ nhạt, Tỏi, Nước tương, Hành lá",
            "Hải sản, Nướng, Bán chạy", 13, 286, 4.8, 196),

        Make("bbq", "hai-san-muc-nuong", "Mực nướng nguyên con", "イカ焼き", "Ika yaki", 165000,
            "Mực ống nguyên con nướng than, quét tare, cắt khoanh ăn nóng chấm mayonnaise ớt.",
            "Mực ống, Sốt tare, Mayonnaise Nhật, Bột ớt shichimi",
            "Hải sản, Nướng", 14, 242, 4.5, 178, spicyLevel: 1),

        Make("bbq", "rau-nuong-thap-cam", "Rau củ nướng thập cẩm", "野菜焼き盛り", "Yasai yaki", 98000,
            "Nấm shiitake, bí ngòi, ớt chuông và bắp non nướng than, quét dầu mè và muối yuzu.",
            "Nấm shiitake, Bí ngòi, Ớt chuông, Bắp non, Dầu mè, Muối yuzu",
            "Thuần chay, Nướng, Ít calo", 12, 186, 4.4, 168),

        Make("bbq", "set-bbq-doi", "Set BBQ cho hai người", "焼肉セット二人前", "Yakiniku set 2", 685000,
            "Khay 600g gồm karubi, harami, ba chỉ heo và gà, kèm cơm, kim chi Nhật và canh miso.",
            "Sườn bò, Thăn chảy bò, Ba chỉ heo, Đùi gà, Kim chi, Cơm trắng",
            "Set, Cho hai người, Chia sẻ, Bán chạy", 20, 1640, 4.8, 312, isFeatured: true),

        Make("bbq", "set-bbq-gia-dinh", "Set BBQ gia đình 4 người", "焼肉セット四人前", "Yakiniku set 4", 1285000,
            "Khay 1.2kg sáu loại thịt kèm hải sản nướng, rau củ và bốn phần cơm — tiệc trọn gói.",
            "Sườn bò, Lưỡi bò, Ba chỉ heo, Nạc cổ heo, Gà, Tôm sú, Rau củ",
            "Set, Gia đình, Tiệc, Chia sẻ", 30, 3280, 4.9, 148),

        Make("bbq", "set-wagyu-premium", "Set wagyu premium", "和牛プレミアムセット", "Wagyu premium set", 1850000,
            "300g wagyu A5 ba phần khác nhau, dùng cùng muối tùng lộ, wasabi tươi và sốt tare 7 ngày.",
            "Wagyu A5 karubi, Wagyu A5 thăn lưng, Wagyu A5 misuji, Muối tùng lộ, Wasabi tươi",
            "Set, Cao cấp, Đặc sản", 25, 1480, 5.0, 72),
    ];
}
