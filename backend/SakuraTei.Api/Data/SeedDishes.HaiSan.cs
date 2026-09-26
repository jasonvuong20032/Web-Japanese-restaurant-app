using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm hải sản cao cấp — 30 món cua hoàng đế, tôm hùm, bào ngư, nhím biển, cá quý.</summary>
    private static readonly IReadOnlyList<Dish> HaiSan =
    [
        Make("hai-san", "cua-hoang-de-hap", "Cua hoàng đế hấp", "タラバガニ蒸し", "Steamed king crab", 2450000,
            "Chân cua hoàng đế Hokkaido hấp hơi nước biển, thịt trắng ngọt, chấm giấm tosazu và bơ chảy.",
            "Cua hoàng đế taraba, Giấm tosazu, Bơ lên men, Chanh yuzu",
            "Hải sản, Cao cấp, Đặc sản, Chia sẻ", 25, 520, 4.9, 186, isFeatured: true),

        Make("hai-san", "cua-hoang-de-nuong-than", "Chân cua hoàng đế nướng than", "焼きタラバガニ", "Grilled king crab", 2350000,
            "Chân cua bổ dọc nướng trên than binchotan, vỏ xém thơm, thịt săn lại và ngọt đậm hơn khi hấp.",
            "Cua hoàng đế taraba, Muối biển, Chanh, Bơ tỏi",
            "Hải sản, Cao cấp, Nướng", 20, 480, 4.8, 124),

        Make("hai-san", "cua-tuyet-zuwai", "Cua tuyết zuwai luộc", "ズワイガニ", "Zuwai snow crab", 1650000,
            "Cua tuyết biển Nhật luộc nước muối, thịt sợi dài, vị ngọt thanh; gạch cua để riêng ăn với cơm.",
            "Cua tuyết zuwai, Muối biển, Giấm sanbaizu, Cơm",
            "Hải sản, Cao cấp, Theo mùa", 20, 420, 4.8, 142),

        Make("hai-san", "cua-long-kegani", "Cua lông Hokkaido", "毛ガニ", "Kegani hairy crab", 1450000,
            "Cua lông nhỏ con nhưng gạch vàng béo ngậy — đặc sản mùa đông của Hokkaido, ăn kèm giấm cua.",
            "Cua lông kegani, Giấm cua, Muối biển",
            "Hải sản, Đặc sản, Theo mùa", 20, 380, 4.7, 88),

        Make("hai-san", "kani-nabe", "Lẩu cua kani nabe", "かに鍋", "Kani nabe", 1350000,
            "Nồi lẩu dashi với chân cua tuyết, rau cải, nấm, đậu phụ; cuối bữa nấu cháo zosui với nước cua.",
            "Cua tuyết, Dashi kombu, Cải thảo, Nấm shiitake, Đậu phụ, Cơm nấu cháo",
            "Hải sản, Chia sẻ, Mùa lạnh", 30, 680, 4.8, 108),

        Make("hai-san", "tom-hum-ise-ebi-sashimi", "Sashimi tôm hùm Ise", "伊勢海老の刺身", "Ise ebi sashimi", 1850000,
            "Tôm hùm gai Ise sống thái sashimi, thịt trong và giòn sật; phần đầu nấu súp miso phục vụ sau.",
            "Tôm hùm gai Ise, Wasabi, Tía tô, Súp miso đầu tôm",
            "Hải sản, Cao cấp, Món lạnh", 25, 280, 4.9, 96, isFeatured: true),

        Make("hai-san", "tom-hum-nuong-bo-toi", "Tôm hùm nướng bơ tỏi", "伊勢海老のバター焼き", "Lobster butter yaki", 1650000,
            "Tôm hùm bổ đôi nướng với bơ tỏi và miso trắng, mép vỏ cháy cạnh, thịt mọng.",
            "Tôm hùm, Bơ, Tỏi, Miso trắng, Mùi tây",
            "Hải sản, Cao cấp, Nướng", 22, 560, 4.8, 134),

        Make("hai-san", "tom-botan-ebi", "Tôm botan ebi Hokkaido", "ボタン海老", "Botan ebi", 385000,
            "Tôm mẫu đơn cỡ lớn ăn sống, thịt dẻo ngọt như kẹo; đầu tôm chiên giòn ăn kèm.",
            "Tôm botan ebi, Wasabi, Chanh yuzu, Đầu tôm chiên",
            "Hải sản, Món lạnh, Cao cấp", 12, 180, 4.8, 156),

        Make("hai-san", "bao-ngu-hap-ruou-sake", "Bào ngư hấp rượu sake", "鮑の酒蒸し", "Awabi sakamushi", 985000,
            "Bào ngư tươi hấp rượu sake bốn tiếng tới khi mềm như thạch, thái lát, rưới gan bào ngư đánh sốt.",
            "Bào ngư, Rượu sake, Kombu, Sốt gan bào ngư",
            "Hải sản, Cao cấp, Đặc sản", 20, 220, 4.8, 92),

        Make("hai-san", "bao-ngu-nuong-bo", "Bào ngư nướng bơ", "鮑のバター焼き", "Awabi butter yaki", 925000,
            "Bào ngư còn sống đặt trên vỉ nướng, co mình lại trong bơ và nước tương, giòn sần sật.",
            "Bào ngư, Bơ, Nước tương, Rượu sake",
            "Hải sản, Cao cấp, Nướng", 15, 240, 4.7, 78),

        Make("hai-san", "uni-hokkaido-hop-go", "Hộp gỗ nhím biển Hokkaido", "北海道雲丹", "Hokkaido uni box", 1250000,
            "Nguyên hộp gỗ uni bafun Hokkaido màu cam đậm, ăn bằng thìa, béo như kem và ngọt vị biển.",
            "Nhím biển bafun uni, Rong nori nướng, Muối biển, Wasabi",
            "Hải sản, Cao cấp, Đặc sản", 5, 260, 4.9, 118, isFeatured: true),

        Make("hai-san", "uni-don", "Cơm nhím biển uni don", "雲丹丼", "Uni don", 895000,
            "Cơm sushi phủ kín uni tươi, rắc rong nori thái chỉ; không cần nước tương vẫn đủ đậm.",
            "Nhím biển uni, Cơm sushi, Rong nori, Wasabi",
            "Hải sản, Cao cấp", 8, 520, 4.9, 146),

        Make("hai-san", "ikura-don", "Cơm trứng cá hồi ikura", "いくら丼", "Ikura don", 425000,
            "Trứng cá hồi ngâm tương nhà làm phủ tràn bát, hạt trứng nổ lách tách, vị mặn ngọt vừa phải.",
            "Trứng cá hồi ikura, Cơm sushi, Nước tương ngâm, Tía tô",
            "Hải sản, Bán chạy", 8, 480, 4.8, 228),

        Make("hai-san", "kaisen-don-cao-cap", "Kaisen don cao cấp", "特上海鮮丼", "Tokujo kaisen don", 695000,
            "Bát cơm với 10 loại hải sản: otoro, uni, ikura, cua, tôm botan, sò điệp, cá tráp, cá cam và trứng tôm.",
            "Otoro, Nhím biển, Trứng cá hồi, Cua, Tôm botan, Sò điệp, Cá tráp, Cơm sushi",
            "Hải sản, Cao cấp, Bán chạy", 12, 620, 4.9, 264, isFeatured: true),

        Make("hai-san", "fugu-sashimi", "Sashimi cá nóc fugu", "ふぐ刺し", "Fugu sashimi", 1450000,
            "Cá nóc hổ do đầu bếp có giấy phép sơ chế, thái mỏng như giấy xếp hình hoa cúc, chấm ponzu.",
            "Cá nóc hổ torafugu, Ponzu, Hành lá, Củ cải momiji oroshi",
            "Hải sản, Cao cấp, Đặc sản, Ít calo", 15, 120, 4.8, 72),

        Make("hai-san", "fugu-karaage", "Cá nóc chiên karaage", "ふぐの唐揚げ", "Fugu karaage", 685000,
            "Thịt cá nóc sát xương ướp gừng tỏi chiên giòn, thịt dai chắc và rất thơm.",
            "Cá nóc hổ, Bột khoai tây, Gừng, Tỏi, Chanh",
            "Hải sản, Món chiên, Đặc sản", 15, 380, 4.6, 58),

        Make("hai-san", "unagi-kabayaki", "Lươn nướng kabayaki", "鰻の蒲焼", "Unagi kabayaki", 485000,
            "Lươn Nhật xẻ lưng theo lối Kanto, hấp rồi nướng ba lần với sốt tare, rắc tiêu sansho.",
            "Lươn Nhật, Sốt tare lươn, Tiêu sansho",
            "Hải sản, Đặc sản, Nướng", 25, 480, 4.8, 182),

        Make("hai-san", "unaju", "Unaju hộp sơn mài", "うな重", "Unaju", 585000,
            "Lươn kabayaki nguyên con đặt trong hộp sơn mài hai tầng, kèm súp gan lươn kimosui.",
            "Lươn Nhật, Cơm, Sốt tare, Tiêu sansho, Súp gan lươn",
            "Hải sản, Cao cấp, Bán chạy", 25, 820, 4.9, 214),

        Make("hai-san", "hitsumabushi", "Hitsumabushi Nagoya", "ひつまぶし", "Hitsumabushi", 565000,
            "Cơm lươn Nagoya ăn ba cách: nguyên bản, thêm gia vị, rồi chan trà dashi nóng cho bát cuối.",
            "Lươn Nhật, Cơm, Hành lá, Wasabi, Rong nori, Trà dashi",
            "Hải sản, Đặc sản", 25, 780, 4.8, 124),

        Make("hai-san", "hau-hiroshima-song", "Hàu sống Hiroshima", "広島生牡蠣", "Hiroshima raw oyster", 285000,
            "Sáu con hàu Hiroshima sống trên đá, vắt chanh hoặc chấm ponzu với củ cải cay momiji.",
            "Hàu Hiroshima, Ponzu, Củ cải momiji oroshi, Chanh",
            "Hải sản, Món lạnh, Ít calo", 8, 140, 4.7, 196),

        Make("hai-san", "hau-nuong-pho-mai", "Hàu nướng miso phô mai", "牡蠣の味噌チーズ焼き", "Kaki miso cheese yaki", 245000,
            "Hàu nướng với lớp miso trắng và phô mai Hokkaido khò vàng mặt.",
            "Hàu, Miso trắng, Phô mai Hokkaido, Hành lá",
            "Hải sản, Nướng", 12, 280, 4.6, 142),

        Make("hai-san", "so-diep-hokkaido-khong-lo", "Sò điệp Hokkaido nướng vỏ", "帆立の貝焼き", "Hotate kaiyaki", 325000,
            "Sò điệp cỡ đại nướng ngay trong vỏ với bơ và nước tương, nước sò sôi sùng sục.",
            "Sò điệp Hokkaido, Bơ, Nước tương, Rượu sake",
            "Hải sản, Nướng", 12, 220, 4.7, 168),

        Make("hai-san", "kinmedai-nitsuke", "Cá kinmedai kho nitsuke", "金目鯛の煮付け", "Kinmedai nitsuke", 785000,
            "Cá hồng mắt vàng nguyên con kho với nước tương, mirin và gừng, da đỏ óng, thịt trắng mềm.",
            "Cá kinmedai, Nước tương, Mirin, Gừng, Ngưu bàng",
            "Hải sản, Đặc sản, Đậm đà", 25, 380, 4.8, 86),

        Make("hai-san", "nodoguro-shioyaki", "Cá nodoguro nướng muối", "のどぐろ塩焼き", "Nodoguro shioyaki", 895000,
            "Cá hồng họng đen — cá trắng béo nhất biển Nhật — nướng muối giản đơn để mỡ tự tứa ra.",
            "Cá nodoguro, Muối biển, Chanh sudachi, Củ cải bào",
            "Hải sản, Cao cấp, Nướng", 25, 420, 4.9, 64),

        Make("hai-san", "tai-kabuto-ni", "Đầu cá tráp kho", "鯛のかぶと煮", "Tai kabuto ni", 385000,
            "Đầu cá tráp kho ngọt mặn, phần má và mắt cá béo nhất, dành cho người sành ăn.",
            "Đầu cá tráp, Nước tương, Rượu sake, Đường, Ngưu bàng",
            "Hải sản, Đậm đà", 25, 360, 4.6, 72),

        Make("hai-san", "gindara-saikyo-yaki", "Cá tuyết đen nướng miso", "銀鱈西京焼き", "Gindara saikyo yaki", 485000,
            "Cá tuyết đen ướp miso Saikyo ngọt ba ngày rồi nướng, thịt mọng dầu và tan như bơ.",
            "Cá tuyết đen, Miso Saikyo, Mirin, Gừng hồng",
            "Hải sản, Bán chạy, Nướng", 20, 420, 4.8, 206),

        Make("hai-san", "otoro-aburi-sushi-set", "Set sushi otoro khò lửa", "大トロ炙り寿司", "Otoro aburi set", 685000,
            "Năm miếng otoro phần bụng cá ngừ vây xanh khò lửa nhẹ, mỡ chảy óng trên cơm.",
            "Otoro cá ngừ vây xanh, Cơm sushi, Muối hoa, Wasabi",
            "Hải sản, Cá béo, Cao cấp, Khò lửa", 12, 380, 4.9, 152),

        Make("hai-san", "mirugai-sashimi", "Sashimi ốc vòi voi", "みる貝刺身", "Mirugai sashimi", 685000,
            "Ốc vòi voi thái mỏng, giòn sừn sựt và ngọt nước; chần qua nước nóng để thịt co lại đẹp.",
            "Ốc vòi voi, Wasabi, Chanh, Nước tương",
            "Hải sản, Món lạnh, Cao cấp", 12, 120, 4.6, 54),

        Make("hai-san", "kaisen-yaki-platter", "Mâm hải sản nướng than", "海鮮焼き盛り合わせ", "Kaisen yaki platter", 1850000,
            "Tôm hùm, sò điệp, hàu, bào ngư và cua nướng trên than hoa, phục vụ cho 2–3 người.",
            "Tôm hùm, Sò điệp, Hàu, Bào ngư, Chân cua, Bơ tỏi",
            "Hải sản, Chia sẻ, Nướng, Cao cấp", 30, 980, 4.8, 98, originalPrice: 2150000),

        Make("hai-san", "kaisen-nabe-cao-cap", "Lẩu hải sản yosenabe", "寄せ鍋", "Yosenabe", 1250000,
            "Nồi lẩu dashi gom cua, tôm, sò, cá và rau củ mùa đông, nấu ngay trên bếp tại bàn.",
            "Cua, Tôm, Sò điệp, Cá tuyết, Cải thảo, Nấm, Dashi",
            "Hải sản, Chia sẻ, Mùa lạnh", 30, 620, 4.7, 88),
    ];
}
