using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm ramen — 18 món, gồm cả tsukemen và mì trộn mazesoba.</summary>
    private static readonly IReadOnlyList<Dish> Ramen =
    [
        Make("ramen", "tonkotsu-ramen", "Tonkotsu ramen", "豚骨ラーメン", "Tonkotsu ramen", 148000,
            "Xương heo ninh sôi mạnh 18 tiếng cho nước dùng trắng đục sánh như sữa, béo mà không ngấy.",
            "Xương ống heo, Mì sợi mảnh, Thịt ba chỉ chashu, Trứng lòng đào, Mộc nhĩ, Hành lá",
            "Bán chạy, Béo đậm, Kinh điển", 12, 682, 4.9, 864, isFeatured: true),

        Make("ramen", "shoyu-ramen", "Shoyu ramen", "醤油ラーメン", "Shoyu ramen", 132000,
            "Nước dùng gà và cá bào trong veo nêm tương Nhật ủ gỗ, vị thanh nhưng hậu rất sâu.",
            "Nước dùng gà, Nước tương ủ gỗ, Mì sợi xoăn, Thịt chashu, Măng menma, Nori",
            "Kinh điển, Thanh nhẹ", 11, 524, 4.7, 592),

        Make("ramen", "miso-ramen", "Miso ramen", "味噌ラーメン", "Miso ramen", 145000,
            "Tương miso đỏ Hokkaido xào với tỏi và mỡ hành trước khi chan, thơm nức và ấm bụng.",
            "Miso đỏ, Nước dùng heo, Bắp ngọt, Bơ nhạt, Giá đỗ, Thịt băm",
            "Đậm đà, Mùa lạnh", 12, 648, 4.7, 438),

        Make("ramen", "shio-ramen", "Shio ramen", "塩ラーメン", "Shio ramen", 128000,
            "Bản ramen nhẹ nhất: nước dùng gà nêm muối biển, trong như nước trà, tôn vị sợi mì.",
            "Nước dùng gà, Muối biển Okinawa, Mì sợi thẳng, Ức gà áp chảo, Hành tây",
            "Thanh nhẹ, Ít béo", 10, 462, 4.5, 316),

        Make("ramen", "tantanmen", "Tantanmen cay", "担々麺", "Tantanmen", 155000,
            "Nước dùng mè rang sánh mịn dậy vị ớt Tứ Xuyên và tiêu hoa, cay tê đúng điệu.",
            "Mè rang, Thịt heo băm, Dầu ớt rayu, Tiêu hoa, Cải thìa, Mì sợi mảnh",
            "Cay, Đậm đà, Bán chạy", 13, 712, 4.8, 476, spicyLevel: 3, isFeatured: true),

        Make("ramen", "tsukemen-dac", "Tsukemen nước chấm đặc", "つけ麺", "Tsukemen", 168000,
            "Mì lạnh để riêng, chấm vào bát nước cốt cá heo đặc quánh — mỗi gắp một lần chấm.",
            "Mì sợi dày, Nước cốt tonkotsu-gyokai, Thịt chashu, Trứng lòng đào, Bột cá bào",
            "Đặc sản, Đậm đà, Bán chạy", 14, 796, 4.8, 388, isFeatured: true),

        Make("ramen", "ramen-ca-hoi-mien-bac", "Ramen cá hồi miso bơ", "鮭味噌バターラーメン", "Sake miso butter ramen", 172000,
            "Phiên bản Hokkaido: miso trắng, một viên bơ tan chậm và phi lê cá hồi áp chảo giòn da.",
            "Cá hồi, Miso trắng, Bơ nhạt, Bắp ngọt, Khoai tây, Mì sợi xoăn",
            "Đặc sản, Béo đậm", 15, 738, 4.7, 214),

        Make("ramen", "ramen-ga-paitan", "Ramen gà paitan", "鶏白湯ラーメン", "Tori paitan ramen", 142000,
            "Xương gà ninh tới khi nước dùng trắng sánh, ngọt hậu tự nhiên, phủ ức gà ủ nhiệt thấp.",
            "Xương gà, Ức gà sous-vide, Mì sợi thẳng, Hành lá, Dầu tỏi đen",
            "Béo đậm, Giàu đạm", 12, 596, 4.6, 287),

        Make("ramen", "ramen-bo-wagyu", "Ramen bò wagyu", "和牛ラーメン", "Wagyu ramen", 268000,
            "Lát bò wagyu A4 khò lửa trải trên tô nước dùng bò ninh chậm, thơm mùi hành phi.",
            "Bò wagyu A4, Nước dùng bò, Mì sợi dày, Hành phi, Tỏi đen, Trứng lòng đào",
            "Cao cấp, Đậm đà", 16, 824, 4.8, 156),

        Make("ramen", "ramen-hai-san", "Ramen hải sản", "海鮮ラーメン", "Kaisen ramen", 186000,
            "Tô mì đầy tôm, mực và nghêu, nước dùng ngọt vị biển nhờ nấu cùng đầu tôm rang.",
            "Tôm sú, Mực, Nghêu, Nước dùng hải sản, Mì sợi thẳng, Rong biển wakame",
            "Hải sản, Thanh nhẹ", 15, 568, 4.6, 224),

        Make("ramen", "mazesoba", "Mazesoba mì trộn", "まぜそば", "Mazesoba", 152000,
            "Mì không nước, trộn đều với thịt băm đậm vị, lòng đỏ trứng sống và hẹ thái nhuyễn.",
            "Mì sợi dày, Thịt heo băm, Lòng đỏ trứng, Hẹ, Bột cá bào, Tỏi băm",
            "Mì trộn, Đậm đà, Bán chạy", 11, 728, 4.7, 342),

        Make("ramen", "abura-soba", "Abura soba", "油そば", "Abura soba", 138000,
            "Mì trộn dầu mè và tương ở đáy bát, thêm giấm đen và dầu ớt tuỳ khẩu vị khi ăn.",
            "Mì sợi dày, Dầu mè, Nước tương, Thịt chashu, Măng menma, Giấm đen",
            "Mì trộn, Giá tốt", 9, 664, 4.4, 198),

        Make("ramen", "ramen-chay-nam", "Ramen chay nấm rừng", "きのこベジラーメン", "Kinoko vegan ramen", 126000,
            "Nước dùng ngọt từ bốn loại nấm và kombu, không đạm động vật mà vẫn đậm vị umami.",
            "Nấm shiitake, Nấm maitake, Nấm kim châm, Kombu, Đậu hũ, Mì sợi xoăn",
            "Thuần chay, Thanh nhẹ, Ít béo", 12, 428, 4.5, 176),

        Make("ramen", "ramen-cay-jigoku", "Ramen địa ngục", "地獄ラーメン", "Jigoku ramen", 158000,
            "Cấp cay cao nhất của quán: ớt habanero và rayu tự ủ, ai ăn hết tô được ghi bảng vàng.",
            "Ớt habanero, Dầu rayu, Nước dùng tonkotsu, Thịt chashu, Hành lá, Mì sợi mảnh",
            "Siêu cay, Thử thách", 13, 742, 4.6, 268, spicyLevel: 3),

        Make("ramen", "ramen-tom-hum", "Ramen tôm hùm", "海老味噌ラーメン", "Ebi miso ramen", 295000,
            "Nước dùng nấu từ vỏ tôm hùm rang cùng miso trắng, nửa con tôm hùm hấp đặt trên mặt.",
            "Tôm hùm, Miso trắng, Kem tươi, Mì sợi thẳng, Hành lá",
            "Cao cấp, Hải sản, Đặc sản", 20, 686, 4.8, 98),

        Make("ramen", "hiyashi-chuka", "Hiyashi chuka mì lạnh", "冷やし中華", "Hiyashi chuka", 132000,
            "Mì lạnh mùa hè rưới sốt giấm mè, phủ dưa leo, trứng thái sợi, giăm bông và cà chua.",
            "Mì lạnh, Dưa leo, Trứng thái sợi, Giăm bông, Cà chua, Sốt giấm mè",
            "Món lạnh, Mùa hè, Thanh nhẹ", 10, 486, 4.4, 154),

        Make("ramen", "gyoza-chien", "Gyoza chiên 6 cái", "焼き餃子", "Yaki gyoza", 78000,
            "Sáu chiếc gyoza áp chảo dính nhau bằng màng bột giòn tan, nhân heo và hẹ mọng nước.",
            "Thịt heo, Hẹ, Bắp cải, Vỏ bánh gyoza, Dầu mè, Giấm đen",
            "Khai vị, Ăn kèm, Bán chạy", 10, 386, 4.7, 712, isFeatured: true),

        Make("ramen", "chashu-don", "Cơm chashu", "チャーシュー丼", "Chashu don", 95000,
            "Chén cơm nhỏ phủ chashu khò lửa và sốt tare — gọi thêm cho đủ no khi ăn ramen.",
            "Thịt ba chỉ chashu, Cơm trắng, Sốt tare, Hành lá, Mè rang",
            "Ăn kèm, Cơm, Giá tốt", 7, 462, 4.5, 288),
    ];
}
