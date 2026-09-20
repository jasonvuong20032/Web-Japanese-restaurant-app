using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm udon — 15 món, gồm cả soba lạnh và udon xào.</summary>
    private static readonly IReadOnlyList<Dish> Udon =
    [
        Make("udon", "kake-udon", "Kake udon truyền thống", "かけうどん", "Kake udon", 92000,
            "Bản udon tối giản nhất: sợi trắng nõn trong bát dashi nóng, chỉ rắc hành lá và bột ớt.",
            "Sợi udon tươi, Dashi kombu-katsuo, Hành lá, Bột ớt shichimi",
            "Kinh điển, Thanh nhẹ, Giá tốt", 8, 386, 4.4, 246),

        Make("udon", "tempura-udon", "Tempura udon", "天ぷらうどん", "Tempura udon", 148000,
            "Tôm tempura chiên riêng đặt lên lúc bưng ra, vỏ còn giòn vài phút trước khi ngấm dashi.",
            "Sợi udon tươi, Tôm sú tempura, Dashi, Rau củ tempura, Hành lá",
            "Bán chạy, Món chiên", 13, 624, 4.8, 512, isFeatured: true),

        Make("udon", "kitsune-udon", "Kitsune udon", "きつねうどん", "Kitsune udon", 108000,
            "Miếng đậu hũ chiên kho ngọt to bản nổi trên mặt, cắn vào là dashi ngọt túa ra.",
            "Sợi udon tươi, Đậu hũ chiên aburaage, Dashi, Hành lá, Mirin",
            "Thuần chay, Kinh điển", 10, 452, 4.5, 288),

        Make("udon", "curry-udon", "Curry udon", "カレーうどん", "Curry udon", 132000,
            "Cà ri Nhật pha loãng bằng dashi rồi làm sánh, bám đều sợi udon dày, cay ấm dịu.",
            "Sợi udon tươi, Cà ri Nhật, Dashi, Thịt bò, Hành tây, Hành lá",
            "Đậm đà, Cay nhẹ, Bán chạy", 12, 596, 4.7, 364, spicyLevel: 1),

        Make("udon", "nabeyaki-udon", "Nabeyaki udon nồi đất", "鍋焼きうどん", "Nabeyaki udon", 178000,
            "Phục vụ trong nồi đất sôi lục bục, có tôm tempura, trứng chần và nấm — ăn tới cuối vẫn nóng.",
            "Sợi udon tươi, Tôm tempura, Trứng gà, Nấm shiitake, Chả cá kamaboko, Dashi",
            "Nồi đất, Mùa lạnh, Đặc sản", 18, 682, 4.8, 232, isFeatured: true),

        Make("udon", "niku-udon", "Niku udon bò", "肉うどん", "Niku udon", 155000,
            "Thịt bò thái mỏng kho sốt sukiyaki ngọt mặn, đặt kín mặt bát udon nước trong.",
            "Sợi udon tươi, Thịt bò thái mỏng, Nước tương, Mirin, Hành tây, Dashi",
            "Đậm đà, Giàu đạm", 12, 648, 4.7, 298),

        Make("udon", "zaru-udon", "Zaru udon lạnh", "ざるうどん", "Zaru udon", 112000,
            "Udon xả nước đá cho sợi săn bật, bày trên vỉ tre, chấm tsuyu lạnh pha gừng và hành.",
            "Sợi udon tươi, Nước chấm tsuyu, Gừng bào, Hành lá, Rong biển nori",
            "Món lạnh, Mùa hè, Ít béo", 9, 368, 4.6, 276),

        Make("udon", "bukkake-udon", "Bukkake udon", "ぶっかけうどん", "Bukkake udon", 122000,
            "Udon lạnh rưới trực tiếp tsuyu đậm đặc, trộn cùng củ cải bào và trứng cút sống.",
            "Sợi udon tươi, Tsuyu đậm, Củ cải bào, Trứng cút, Vỏ tempura, Hành lá",
            "Món lạnh, Mùa hè", 9, 412, 4.5, 184),

        Make("udon", "yaki-udon", "Yaki udon xào", "焼きうどん", "Yaki udon", 138000,
            "Udon xào lửa lớn với nước tương và bơ, sợi hơi cháy cạnh nên dậy mùi rất thơm.",
            "Sợi udon tươi, Thịt heo, Bắp cải, Cà rốt, Nước tương, Bơ nhạt, Cá bào",
            "Món xào, Bán chạy", 11, 578, 4.6, 342),

        Make("udon", "udon-hai-san-lanh", "Udon hải sản nước lạnh", "海鮮冷やしうどん", "Kaisen hiyashi udon", 168000,
            "Udon lạnh với tôm luộc, sò điệp và trứng cá, nước dùng lạnh trong veo vị yuzu.",
            "Sợi udon tươi, Tôm, Sò điệp, Trứng cá tobiko, Dashi lạnh, Yuzu",
            "Món lạnh, Hải sản, Mùa hè", 13, 486, 4.6, 148),

        Make("udon", "udon-chay-nam-rung", "Udon chay nấm rừng", "きのこうどん", "Kinoko udon", 118000,
            "Năm loại nấm xào nhanh rồi thả vào dashi kombu, ngọt thanh và thơm mùi rừng ẩm.",
            "Sợi udon tươi, Nấm shiitake, Nấm maitake, Nấm kim châm, Kombu, Rau bó xôi",
            "Thuần chay, Thanh nhẹ, Ít béo", 11, 392, 4.5, 162),

        Make("udon", "soba-zaru", "Zaru soba lạnh", "ざるそば", "Zaru soba", 118000,
            "Mì kiều mạch tự cán, thơm mùi hạt rang, chấm tsuyu rồi húp nước luộc soba cuối bữa.",
            "Mì soba kiều mạch, Nước chấm tsuyu, Wasabi, Hành lá, Nori",
            "Món lạnh, Ít béo, Kinh điển", 9, 342, 4.6, 268),

        Make("udon", "soba-tempura-nong", "Soba tempura nóng", "天ぷらそば", "Tempura soba", 152000,
            "Soba nóng chan dashi đậm hơn udon một chút, phủ tôm và rau củ tempura vừa vớt.",
            "Mì soba, Tôm tempura, Rau củ tempura, Dashi, Hành lá",
            "Món chiên, Mùa lạnh", 14, 608, 4.7, 214),

        Make("udon", "tanuki-udon", "Tanuki udon", "たぬきうどん", "Tanuki udon", 98000,
            "Rắc đầy vỏ bột tempura giòn, ngấm dashi dần trong lúc ăn nên mỗi gắp một kết cấu khác.",
            "Sợi udon tươi, Vỏ tempura tenkasu, Dashi, Hành lá, Chả cá kamaboko",
            "Giá tốt, Kinh điển", 8, 428, 4.3, 196),

        Make("udon", "udon-bo-wagyu-nong", "Udon bò wagyu", "和牛うどん", "Wagyu udon", 265000,
            "Lát wagyu A4 chần tái ngay trong bát dashi nóng, mỡ tan vào nước làm bát mì béo ngậy.",
            "Bò wagyu A4, Sợi udon tươi, Dashi, Hành tây, Trứng lòng đào, Hành lá",
            "Cao cấp, Đậm đà", 15, 712, 4.8, 124),
    ];
}
