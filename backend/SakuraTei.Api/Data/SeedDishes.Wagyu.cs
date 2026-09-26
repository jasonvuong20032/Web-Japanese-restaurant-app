using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm bò Wagyu và Kobe — 25 món từ steak, sukiyaki, shabu-shabu đến sushi bò.</summary>
    private static readonly IReadOnlyList<Dish> Wagyu =
    [
        Make("wagyu", "steak-kobe-a5-than-ngoai", "Steak bò Kobe A5 thăn ngoại", "神戸牛サーロインステーキ", "Kobe beef sirloin steak", 1890000,
            "Thăn ngoại bò Kobe A5 có chứng nhận Hyogo, áp chảo gang rồi nghỉ 4 phút, vân mỡ tan ngay trên lưỡi.",
            "Bò Kobe A5 thăn ngoại, Muối hồng Himalaya, Wasabi tươi, Tỏi chiên, Nước tương thanh",
            "Cao cấp, Bò, Đặc sản, Bán chạy", 18, 780, 4.9, 412, isFeatured: true),

        Make("wagyu", "steak-kobe-a5-than-noi", "Steak bò Kobe A5 thăn nội", "神戸牛ヒレステーキ", "Kobe beef fillet steak", 2150000,
            "Phần thăn nội hiếm nhất của con bò Kobe, mềm như bơ, ít mỡ hơn thăn ngoại nên vị thịt nổi rõ.",
            "Bò Kobe A5 thăn nội, Muối đá Okinawa, Tiêu Sansho, Bơ lên men, Wasabi tươi",
            "Cao cấp, Bò, Đặc sản", 20, 640, 4.9, 236),

        Make("wagyu", "kobe-teppanyaki-set", "Set teppanyaki bò Kobe", "神戸牛鉄板焼きコース", "Kobe teppanyaki course", 2680000,
            "Đầu bếp nướng trên bàn sắt ngay trước mặt: bò Kobe 150g, tôm hùm nhỏ, rau củ và cơm chiên tỏi.",
            "Bò Kobe A5, Tôm hùm baby, Rau củ theo mùa, Cơm chiên tỏi, Súp miso",
            "Cao cấp, Bò, Set, Đặc sản", 35, 1180, 4.9, 188, isFeatured: true),

        Make("wagyu", "matsusaka-steak", "Steak bò Matsusaka", "松阪牛ステーキ", "Matsusaka beef steak", 1980000,
            "Bò Matsusaka vùng Mie — một trong ba dòng bò danh tiếng nhất Nhật, mỡ có điểm tan thấp và thơm ngọt.",
            "Bò Matsusaka A5, Muối biển, Tỏi chiên, Rau mầm, Nước tương Mie",
            "Cao cấp, Bò, Đặc sản", 18, 760, 4.8, 124),

        Make("wagyu", "omi-beef-steak", "Steak bò Omi", "近江牛ステーキ", "Omi beef steak", 1750000,
            "Dòng bò lâu đời nhất Nhật từ vùng Shiga, thớ thịt mịn, vị ngọt nhẹ và hậu vị dài.",
            "Bò Omi A5, Muối biển, Wasabi, Củ cải bào, Ponzu",
            "Cao cấp, Bò", 18, 740, 4.7, 96),

        Make("wagyu", "wagyu-sukiyaki", "Lẩu sukiyaki wagyu", "和牛すき焼き", "Wagyu sukiyaki", 895000,
            "Wagyu thái mỏng nấu trong nồi gang với nước warishita ngọt mặn, nhúng trứng gà sống trước khi ăn.",
            "Wagyu A5 thái mỏng, Đậu phụ nướng, Nấm shiitake, Hành boa-rô, Trứng gà Nhật, Warishita",
            "Bò, Chia sẻ, Mùa lạnh, Bán chạy", 25, 980, 4.9, 356, isFeatured: true),

        Make("wagyu", "wagyu-shabu-shabu", "Lẩu shabu-shabu wagyu", "和牛しゃぶしゃぶ", "Wagyu shabu shabu", 865000,
            "Nhúng wagyu vào nồi dashi kombu sôi lăn tăn trong ba giây, chấm sốt mè goma và ponzu.",
            "Wagyu A5 thái mỏng, Dashi kombu, Cải thảo, Nấm enoki, Sốt goma, Ponzu",
            "Bò, Chia sẻ, Mùa lạnh", 25, 820, 4.8, 268),

        Make("wagyu", "wagyu-tataki", "Wagyu tataki", "和牛のたたき", "Wagyu tataki", 425000,
            "Thăn wagyu áp lửa bốn mặt, lòng còn hồng, thái mỏng, rưới ponzu và rắc hành lá.",
            "Wagyu thăn, Ponzu, Hành lá, Gừng, Tỏi phi, Củ cải bào",
            "Bò, Món lạnh, Khò lửa", 12, 380, 4.8, 214),

        Make("wagyu", "wagyu-nigiri-aburi", "Nigiri wagyu khò lửa", "炙り和牛握り", "Aburi wagyu nigiri", 168000,
            "Hai miếng nigiri phủ wagyu A5, khò lửa tới khi mỡ vừa chảy, chấm chút muối và wasabi.",
            "Wagyu A5, Cơm sushi, Muối hoa, Wasabi, Hành lá",
            "Bò, Khò lửa, Bán chạy", 8, 240, 4.9, 318),

        Make("wagyu", "wagyu-uni-ikura-gunkan", "Gunkan wagyu, nhím biển và trứng cá", "和牛雲丹いくら軍艦", "Wagyu uni ikura gunkan", 285000,
            "Wagyu thái mỏng cuộn quanh cơm, bên trên là uni Hokkaido và ikura — ba vị béo quyện vào nhau.",
            "Wagyu A5, Nhím biển uni, Trứng cá hồi ikura, Cơm sushi, Rong biển nori",
            "Bò, Hải sản, Cao cấp", 10, 280, 4.9, 142),

        Make("wagyu", "wagyu-don", "Cơm wagyu don", "和牛丼", "Wagyu don", 385000,
            "Wagyu áp chảo thái lát xếp trên cơm nóng, rưới sốt tare và thêm lòng đỏ trứng ngâm tương.",
            "Wagyu A5, Cơm Koshihikari, Sốt tare, Lòng đỏ trứng ngâm tương, Hành lá",
            "Bò, Bán chạy", 15, 860, 4.8, 296),

        Make("wagyu", "gyu-katsu-wagyu", "Gyukatsu wagyu", "和牛牛カツ", "Wagyu gyukatsu", 445000,
            "Wagyu tẩm bột panko chiên ngập dầu 60 giây, bên ngoài giòn, bên trong đỏ hồng; tự nướng thêm trên đá nóng.",
            "Wagyu, Bột panko, Trứng, Đá nướng, Muối wasabi, Cơm, Súp miso",
            "Bò, Món chiên, Giòn", 15, 920, 4.8, 202),

        Make("wagyu", "wagyu-katsu-sando", "Sandwich katsu wagyu", "和牛カツサンド", "Wagyu katsu sando", 495000,
            "Bánh mì sữa Hokkaido kẹp wagyu chiên xù và sốt tonkatsu, cắt bốn miếng vuông vắn.",
            "Wagyu chiên xù, Bánh mì sữa shokupan, Sốt tonkatsu, Mù tạt Nhật",
            "Bò, Ăn nhanh, Món chiên", 15, 780, 4.7, 168),

        Make("wagyu", "wagyu-hamburg", "Hamburg wagyu sốt demi", "和牛ハンバーグ", "Wagyu hamburg steak", 325000,
            "Thịt wagyu xay tay nướng trên đĩa gang, nước thịt tứa ra khi cắt, rưới sốt demi-glace ninh hai ngày.",
            "Wagyu xay, Hành tây caramel, Sốt demi-glace, Trứng ốp la, Rau củ nướng",
            "Bò, Trẻ em, Kinh điển", 20, 820, 4.7, 244),

        Make("wagyu", "wagyu-yakiniku-platter", "Mâm yakiniku wagyu 5 phần", "和牛焼肉五種盛り", "Wagyu yakiniku 5 cuts", 1450000,
            "Năm phần thịt wagyu khác nhau — thăn vai, sườn, ba chỉ, lõi vai và lưỡi — tự nướng trên than.",
            "Wagyu thăn vai, Wagyu sườn, Wagyu ba chỉ, Wagyu lõi vai, Lưỡi bò, Sốt tare",
            "Bò, Chia sẻ, Nướng, Cao cấp", 20, 1380, 4.9, 176),

        Make("wagyu", "wagyu-ishiyaki", "Wagyu nướng đá núi lửa", "和牛石焼き", "Wagyu ishiyaki", 685000,
            "Đĩa đá núi lửa nung 300°C mang ra bàn, khách tự áp từng lát wagyu theo độ chín mình thích.",
            "Wagyu A5, Đá núi lửa, Muối tiêu, Tỏi chiên, Sốt ponzu",
            "Bò, Nướng, Cao cấp", 15, 620, 4.8, 132),

        Make("wagyu", "wagyu-yukhoe", "Wagyu yukke trộn trứng", "和牛ユッケ", "Wagyu yukke", 365000,
            "Thăn wagyu thái sợi trộn dầu mè, nước tương ngọt và lòng đỏ trứng, rắc mè rang.",
            "Wagyu thăn, Lòng đỏ trứng, Dầu mè, Nước tương, Lê Nhật, Mè rang",
            "Bò, Món lạnh", 10, 340, 4.6, 98, spicyLevel: 1),

        Make("wagyu", "wagyu-roast-beef-don", "Cơm roast beef wagyu", "和牛ローストビーフ丼", "Wagyu roast beef don", 345000,
            "Roast beef wagyu nấu sous-vide 58°C, xếp hình bông hoa trên cơm, thêm sốt hành tây ngọt.",
            "Wagyu roast beef, Cơm, Sốt hành tây, Lòng đỏ trứng, Wasabi",
            "Bò, Bán chạy", 12, 780, 4.8, 226),

        Make("wagyu", "wagyu-gyudon-onsen", "Gyudon wagyu trứng onsen", "和牛牛丼", "Wagyu gyudon", 225000,
            "Phiên bản cao cấp của gyudon: wagyu thái mỏng om hành tây trong nước dashi ngọt, thêm trứng onsen.",
            "Wagyu thái mỏng, Hành tây, Dashi, Trứng onsen, Gừng hồng, Cơm",
            "Bò, Giá tốt, Kinh điển", 10, 760, 4.7, 312),

        Make("wagyu", "wagyu-curry", "Cà ri Nhật bò wagyu", "和牛カレー", "Wagyu curry rice", 245000,
            "Cà ri Nhật ninh từ táo, mật ong và 24 loại gia vị với thăn vai wagyu hầm mềm.",
            "Thăn vai wagyu, Cà ri Nhật, Khoai tây, Cà rốt, Cơm, Dưa fukujinzuke",
            "Bò, Trẻ em, Đậm đà", 12, 880, 4.6, 184, spicyLevel: 1),

        Make("wagyu", "wagyu-gyutan-sendai", "Lưỡi bò nướng kiểu Sendai", "仙台牛タン焼き", "Sendai gyutan", 385000,
            "Lưỡi bò thái dày ướp muối qua đêm theo lối Sendai, nướng than giòn mép, ăn với cơm lúa mạch.",
            "Lưỡi bò, Muối, Tiêu, Cơm lúa mạch, Súp đuôi bò, Dưa cải",
            "Bò, Đặc sản, Nướng", 15, 620, 4.7, 146),

        Make("wagyu", "wagyu-sushi-omakase", "Omakase sushi wagyu 6 miếng", "和牛寿司おまかせ", "Wagyu sushi omakase", 525000,
            "Sáu miếng sushi từ sáu phần bò khác nhau, mỗi miếng một cách xử lý: sống, khò, ướp tương, luộc lửa nhỏ.",
            "Wagyu nhiều phần, Cơm sushi, Muối hoa, Wasabi, Trứng cá muối, Nước tương thanh",
            "Bò, Cao cấp, Set", 15, 420, 4.9, 118),

        Make("wagyu", "kobe-beef-burger", "Burger bò Kobe", "神戸牛バーガー", "Kobe beef burger", 485000,
            "Miếng patty 180g xay từ bò Kobe, phô mai cheddar, hành tây caramel và bánh brioche nướng bơ.",
            "Bò Kobe xay, Phô mai cheddar, Hành caramel, Bánh brioche, Khoai tây chiên",
            "Bò, Ăn nhanh", 18, 1080, 4.6, 164),

        Make("wagyu", "wagyu-nikujaga", "Nikujaga bò wagyu hầm", "和牛肉じゃが", "Wagyu nikujaga", 198000,
            "Món nhà quê của Nhật: bò wagyu hầm cùng khoai tây, cà rốt, hành tây trong nước dashi ngọt dịu.",
            "Wagyu, Khoai tây, Cà rốt, Hành tây, Mì shirataki, Dashi",
            "Bò, Kinh điển, Mùa lạnh", 15, 520, 4.5, 88),

        Make("wagyu", "set-kobe-kaiseki", "Set kaiseki bò Kobe", "神戸牛懐石", "Kobe beef kaiseki", 3450000,
            "Bữa kaiseki bảy món xoay quanh bò Kobe: khai vị, sashimi bò, súp, nướng, hấp, cơm và tráng miệng.",
            "Bò Kobe A5, Rau theo mùa, Dashi, Cơm niêu, Tráng miệng theo mùa",
            "Cao cấp, Set, Đặc sản, Cho hai người", 60, 1680, 5.0, 64, originalPrice: 3850000),
    ];
}
