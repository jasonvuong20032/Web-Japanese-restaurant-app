using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

public static partial class SeedDishes
{
    /// <summary>Nhóm sushi — 20 món, giá tính theo phần 2 miếng hoặc 1 cuộn.</summary>
    private static readonly IReadOnlyList<Dish> Sushi =
    [
        Make("sushi", "nigiri-ca-hoi", "Nigiri cá hồi", "サーモン握り", "Sake nigiri", 68000,
            "Lát cá hồi Na Uy béo đều vắt trên nắm cơm giấm còn ấm, quét một đường nikiri mỏng.",
            "Cá hồi Na Uy, Cơm Koshihikari, Giấm akazu, Wasabi tươi, Nước tương nikiri",
            "Bán chạy, Cá béo, Cổ điển", 6, 142, 4.8, 512, isFeatured: true),

        Make("sushi", "nigiri-ca-ngu-otoro", "Nigiri otoro", "大トロ握り", "Otoro nigiri", 185000,
            "Phần bụng cá ngừ vân béo nhất, tan ngay khi chạm lưỡi, chỉ nêm một hạt muối biển Okinawa.",
            "Cá ngừ vây xanh otoro, Cơm Koshihikari, Muối Okinawa, Wasabi tươi",
            "Cao cấp, Cá béo, Đặc sản", 8, 178, 4.9, 236, isFeatured: true),

        Make("sushi", "nigiri-ca-ngu-akami", "Nigiri akami", "赤身握り", "Akami nigiri", 82000,
            "Thịt nạc lưng cá ngừ đỏ au, ướp nhanh trong nước tương zuke nên vị đậm mà không gắt.",
            "Cá ngừ vây xanh akami, Cơm Koshihikari, Nước tương zuke, Wasabi tươi",
            "Cổ điển, Ít béo", 6, 118, 4.7, 298),

        Make("sushi", "nigiri-ca-tram-hamachi", "Nigiri cá cam hamachi", "ハマチ握り", "Hamachi nigiri", 92000,
            "Cá cam Kagoshima thịt chắc, hậu vị bơ nhẹ, rắc chút vỏ yuzu bào cho dậy mùi.",
            "Cá cam hamachi, Cơm Koshihikari, Vỏ yuzu, Muối biển",
            "Cá béo, Thơm yuzu", 7, 134, 4.8, 187),

        Make("sushi", "nigiri-tom-ngot-amaebi", "Nigiri tôm ngọt amaebi", "甘海老握り", "Amaebi nigiri", 96000,
            "Tôm ngọt Hokkaido ăn sống, thịt trong veo và dính, ngọt hậu kéo dài rất lâu.",
            "Tôm ngọt amaebi, Cơm Koshihikari, Gừng hồng, Wasabi tươi",
            "Hải sản, Ngọt thanh", 7, 96, 4.6, 154),

        Make("sushi", "nigiri-luon-unagi", "Nigiri lươn nướng unagi", "鰻握り", "Unagi nigiri", 105000,
            "Lươn nướng than phết sốt kabayaki ba lượt, da giòn lớp ngoài mà thịt vẫn mềm tơi.",
            "Lươn nước ngọt, Sốt kabayaki, Cơm Koshihikari, Tiêu sansho",
            "Món nướng, Đậm đà, Bán chạy", 12, 216, 4.8, 341, isFeatured: true),

        Make("sushi", "nigiri-trung-tamago", "Nigiri trứng tamago", "玉子握り", "Tamago nigiri", 45000,
            "Trứng cuộn dashi mười hai lớp, ngọt dịu, mềm như bánh bông lan — món thử tay nghề đầu bếp.",
            "Trứng gà, Dashi, Mirin, Rong biển nori, Cơm Koshihikari",
            "Chay-trứng, Trẻ em, Giá tốt", 5, 128, 4.5, 402),

        Make("sushi", "nigiri-so-diep-hotate", "Nigiri sò điệp hotate", "帆立握り", "Hotate nigiri", 98000,
            "Cồi sò điệp Hokkaido khía nhẹ mặt trên, khò lửa ba giây để dậy mùi thơm caramel.",
            "Sò điệp Hokkaido, Cơm Koshihikari, Muối yuzu, Chanh vàng",
            "Hải sản, Khò lửa", 8, 104, 4.7, 176),

        Make("sushi", "nigiri-trung-ca-hoi-ikura", "Gunkan trứng cá hồi ikura", "いくら軍艦", "Ikura gunkan", 115000,
            "Trứng cá hồi ngâm tương sake, hạt căng mọng nổ trong miệng, quấn vành nori giòn.",
            "Trứng cá hồi ikura, Rong biển nori, Cơm Koshihikari, Sake, Nước tương",
            "Cao cấp, Hải sản", 6, 142, 4.8, 223),

        Make("sushi", "gunkan-trung-ca-chuyen-uni", "Gunkan nhím biển uni", "雲丹軍艦", "Uni gunkan", 215000,
            "Nhím biển Hokkaido loại A, béo như kem tươi, vị biển sâu và ngọt hậu rất dài.",
            "Nhím biển uni, Rong biển nori, Cơm Koshihikari, Wasabi tươi",
            "Cao cấp, Đặc sản, Hải sản", 6, 126, 4.9, 118),

        Make("sushi", "cuon-california", "Cuộn California", "カリフォルニアロール", "California maki", 128000,
            "Cuộn ngược tám miếng với thanh cua, bơ và dưa leo, lăn ngoài lớp trứng tôm tobiko cam.",
            "Thanh cua, Bơ, Dưa leo, Trứng tobiko, Sốt mayonnaise Nhật",
            "Cuộn, Nhóm bạn, Bán chạy", 12, 386, 4.6, 624),

        Make("sushi", "cuon-spicy-tuna", "Cuộn cá ngừ cay", "スパイシーツナロール", "Spicy tuna maki", 138000,
            "Cá ngừ băm trộn sốt ớt Nhật shichimi, cay ấm dần chứ không xộc, ăn kèm hành lá thái nhuyễn.",
            "Cá ngừ, Sốt shichimi, Hành lá, Dưa leo, Rong biển nori",
            "Cuộn, Cay, Bán chạy", 12, 342, 4.7, 388, spicyLevel: 2),

        Make("sushi", "cuon-dragon", "Cuộn rồng lươn", "ドラゴンロール", "Dragon maki", 168000,
            "Cuộn tôm chiên xù phủ lát bơ xếp vảy rồng, rưới sốt unagi và mè rang.",
            "Tôm tempura, Bơ, Lươn nướng, Sốt unagi, Mè rang",
            "Cuộn, Đặc sản, Nhóm bạn", 15, 468, 4.8, 297, isFeatured: true),

        Make("sushi", "cuon-rainbow", "Cuộn cầu vồng", "レインボーロール", "Rainbow maki", 178000,
            "Lõi California phủ năm loại cá khác màu xếp so le, đẹp mắt và mỗi miếng một vị.",
            "Cá hồi, Cá ngừ, Cá tráp, Bơ, Thanh cua, Trứng tobiko",
            "Cuộn, Đặc sản, Nhiều loại cá", 16, 412, 4.7, 205),

        Make("sushi", "cuon-futomaki-chay", "Futomaki chay", "太巻き", "Futomaki", 98000,
            "Cuộn dày truyền thống với bảy loại rau củ ngâm, cắt ra thấy hoa văn nhiều màu.",
            "Cà rốt, Dưa leo, Bí ngòi, Nấm shiitake kho, Củ cải muối, Rau bó xôi",
            "Cuộn, Thuần chay, Ít calo", 14, 268, 4.4, 168),

        Make("sushi", "temaki-ca-hoi-bo", "Temaki cá hồi bơ", "サーモンアボカド手巻き", "Temaki sake-avocado", 72000,
            "Nón nori cuốn tay ăn ngay khi vừa cuốn, vỏ còn giòn rôm rốp, nhân cá hồi và bơ chín.",
            "Cá hồi, Bơ, Rong biển nori, Cơm Koshihikari, Sốt mayonnaise Nhật",
            "Cuốn tay, Ăn nhanh", 6, 246, 4.5, 214),

        Make("sushi", "chirashi-thap-cam", "Chirashi thập cẩm", "ちらし寿司", "Chirashi don", 245000,
            "Tô cơm giấm rải chín loại hải sản theo mùa, ăn một tô đủ no và thử được nhiều vị.",
            "Cá hồi, Cá ngừ, Sò điệp, Trứng cá hồi, Tôm, Trứng tamago, Cơm giấm",
            "Cơm tô, No lâu, Bán chạy", 14, 624, 4.8, 356, isFeatured: true),

        Make("sushi", "set-nigiri-9-mieng", "Set nigiri 9 miếng", "握り九貫盛り", "Nigiri moriawase 9", 385000,
            "Chín miếng nigiri đầu bếp chọn trong ngày, xếp theo thứ tự từ cá nhạt tới cá béo.",
            "Cá hồi, Cá ngừ, Cá cam, Sò điệp, Tôm, Lươn, Trứng tamago, Cá tráp",
            "Set, Cho hai người, Bán chạy", 18, 742, 4.9, 428, isFeatured: true),

        Make("sushi", "set-sushi-gia-dinh", "Set sushi gia đình 24 miếng", "family盛り合わせ", "Family moriawase", 685000,
            "Khay lớn gồm nigiri, cuộn và gunkan, đủ cho bốn người dùng bữa chính.",
            "Cá hồi, Cá ngừ, Thanh cua, Trứng tobiko, Bơ, Lươn nướng, Trứng tamago",
            "Set, Gia đình, Chia sẻ", 25, 1860, 4.8, 192, originalPrice: 760000),

        Make("sushi", "inari-sushi", "Inari sushi", "稲荷寿司", "Inarizushi", 52000,
            "Túi đậu hũ chiên kho ngọt nhồi cơm giấm trộn mè, món dân dã ai cũng ăn được.",
            "Đậu hũ chiên aburaage, Cơm giấm, Mè rang, Đường, Nước tương",
            "Thuần chay, Trẻ em, Giá tốt", 5, 186, 4.3, 241),
    ];
}
