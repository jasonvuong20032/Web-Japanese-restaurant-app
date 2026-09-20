using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm sashimi — 15 món, phần tiêu chuẩn 5 lát trừ khi ghi khác.</summary>
    private static readonly IReadOnlyList<Dish> Sashimi =
    [
        Make("sashimi", "sashimi-ca-hoi", "Sashimi cá hồi", "サーモン刺身", "Sake sashimi", 165000,
            "Năm lát cá hồi dày 8mm cắt từ thăn lưng, vân mỡ đều như đá cẩm thạch.",
            "Cá hồi Na Uy, Củ cải trắng bào, Tía tô, Wasabi tươi",
            "Bán chạy, Cá béo, Ít carb", 8, 232, 4.8, 476, isFeatured: true),

        Make("sashimi", "sashimi-ca-ngu-akami", "Sashimi cá ngừ akami", "赤身刺身", "Akami sashimi", 195000,
            "Nạc lưng cá ngừ vây xanh màu đỏ ruby, thịt chắc, vị sắt nhẹ rất đặc trưng.",
            "Cá ngừ vây xanh, Củ cải trắng bào, Tía tô, Nước tương",
            "Cổ điển, Ít béo, Giàu đạm", 8, 186, 4.7, 312),

        Make("sashimi", "sashimi-chutoro", "Sashimi chutoro", "中トロ刺身", "Chutoro sashimi", 315000,
            "Phần bụng giữa cân bằng nhất giữa nạc và mỡ — đậm như akami mà vẫn tan như otoro.",
            "Cá ngừ vây xanh chutoro, Củ cải trắng bào, Wasabi tươi",
            "Cao cấp, Cá béo", 9, 276, 4.9, 198, isFeatured: true),

        Make("sashimi", "sashimi-otoro", "Sashimi otoro", "大トロ刺身", "Otoro sashimi", 465000,
            "Lát bụng cá ngừ béo nhất, vân mỡ trắng dày, chỉ cần muối biển là đủ.",
            "Cá ngừ vây xanh otoro, Muối Okinawa, Wasabi tươi",
            "Cao cấp, Đặc sản, Cá béo", 9, 348, 4.9, 142),

        Make("sashimi", "sashimi-ca-cam", "Sashimi cá cam hamachi", "ハマチ刺身", "Hamachi sashimi", 185000,
            "Cá cam nuôi Kagoshima, thịt hồng ngà, giòn lúc đầu rồi béo dần ở hậu vị.",
            "Cá cam hamachi, Củ cải trắng bào, Vỏ yuzu, Tía tô",
            "Cá béo, Thơm yuzu", 8, 218, 4.7, 234),

        Make("sashimi", "sashimi-ca-trap-tai", "Sashimi cá tráp tai", "鯛刺身", "Tai sashimi", 178000,
            "Cá tráp đỏ thịt trắng trong, thái mỏng kiểu usuzukuri, vị thanh và hậu ngọt.",
            "Cá tráp đỏ, Hành lá, Sốt ponzu, Ớt momiji oroshi",
            "Ít béo, Thanh nhẹ", 10, 148, 4.6, 167),

        Make("sashimi", "sashimi-so-diep", "Sashimi sò điệp", "帆立刺身", "Hotate sashimi", 198000,
            "Cồi sò điệp sống Hokkaido, ngọt sắc và giòn sựt, chấm muối yuzu thay nước tương.",
            "Sò điệp Hokkaido, Muối yuzu, Chanh vàng, Tía tô",
            "Hải sản, Ngọt thanh", 8, 132, 4.8, 189),

        Make("sashimi", "sashimi-muc", "Sashimi mực trong", "イカ刺身", "Ika sashimi", 145000,
            "Mực lá khía vảy rồng để dễ cắn, thịt trong như thuỷ tinh, ngọt và dai nhẹ.",
            "Mực lá, Gừng bào, Hành lá, Nước tương",
            "Hải sản, Ít calo", 10, 98, 4.4, 143),

        Make("sashimi", "sashimi-tom-ngot", "Sashimi tôm ngọt amaebi", "甘海老刺身", "Amaebi sashimi", 188000,
            "Sáu con tôm ngọt bóc vỏ giữ đuôi, đầu tôm chiên giòn ăn kèm cho đủ vị.",
            "Tôm ngọt amaebi, Wasabi tươi, Muối biển, Chanh",
            "Hải sản, Ngọt thanh, Đặc sản", 12, 124, 4.7, 156),

        Make("sashimi", "sashimi-bach-tuoc", "Sashimi bạch tuộc", "タコ刺身", "Tako sashimi", 155000,
            "Vòi bạch tuộc luộc vừa tới rồi ủ lạnh, giòn sần sật, chấm ponzu hoặc muối mè.",
            "Bạch tuộc, Sốt ponzu, Muối mè, Tía tô",
            "Hải sản, Giòn, Ít béo", 9, 116, 4.3, 128),

        Make("sashimi", "sashimi-ca-thu-saba", "Sashimi cá thu giấm", "しめ鯖", "Shime saba", 138000,
            "Cá thu ướp muối rồi giấm theo lối shime, chua dịu, cắt lát dày khò lửa nhẹ mặt da.",
            "Cá thu, Giấm gạo, Muối biển, Gừng, Hành lá",
            "Cổ điển, Đậm đà", 10, 208, 4.5, 174),

        Make("sashimi", "sashimi-ca-hoi-ap-chao", "Sashimi cá hồi khò lửa", "サーモン炙り", "Sake aburi", 172000,
            "Mặt cá khò tới khi lớp mỡ chảy thơm, lòng vẫn sống, rưới sốt mayonnaise yuzu.",
            "Cá hồi, Sốt mayonnaise yuzu, Hành lá, Mè rang",
            "Khò lửa, Bán chạy", 10, 286, 4.7, 263),

        Make("sashimi", "sashimi-moriawase-3", "Sashimi thập cẩm 3 loại", "刺身三種盛り", "Sashimi moriawase 3", 325000,
            "Mười hai lát gồm cá hồi, cá ngừ và cá cam, bày trên đá lạnh giữ nhiệt suốt bữa.",
            "Cá hồi, Cá ngừ, Cá cam, Củ cải trắng bào, Tía tô, Wasabi tươi",
            "Set, Chia sẻ, Bán chạy", 15, 596, 4.8, 384, isFeatured: true),

        Make("sashimi", "sashimi-moriawase-5", "Sashimi thập cẩm 5 loại", "刺身五種盛り", "Sashimi moriawase 5", 545000,
            "Hai mươi lát năm loại hải sản theo mùa, bày thuyền gỗ, đủ cho ba tới bốn người.",
            "Cá hồi, Cá ngừ, Cá cam, Sò điệp, Tôm ngọt, Củ cải trắng bào",
            "Set, Chia sẻ, Cao cấp", 20, 986, 4.9, 227, isFeatured: true),

        Make("sashimi", "sashimi-deluxe-thuyen", "Thuyền sashimi deluxe", "舟盛り", "Funamori deluxe", 1250000,
            "Thuyền gỗ lớn bảy loại hải sản cao cấp kèm nhím biển và trứng cá hồi, dành cho tiệc.",
            "Otoro, Cá hồi, Cá cam, Sò điệp, Nhím biển, Trứng cá hồi, Tôm ngọt",
            "Set, Tiệc, Cao cấp, Đặc sản", 30, 1840, 5.0, 64),
    ];
}
