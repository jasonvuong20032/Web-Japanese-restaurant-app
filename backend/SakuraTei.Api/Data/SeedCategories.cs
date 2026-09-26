using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

/// <summary>Mười hai nhóm món của Sakura Tei.</summary>
public static class SeedCategories
{
    public static readonly IReadOnlyList<Category> All =
    [
        new Category
        {
            Slug = "sushi",
            Name = "Sushi",
            NameJp = "寿司",
            Kanji = "寿",
            Description = "Cơm giấm nắm tay cùng hải sản tươi, cắt lát theo thớ để giữ trọn vị ngọt.",
            ImageUrl = "https://images.unsplash.com/photo-1579871494447-9811cf80d66c?auto=format&fit=crop&w=1200&q=80",
            AccentColor = "#D1483C",
            DisplayOrder = 1,
        },
        new Category
        {
            Slug = "sashimi",
            Name = "Sashimi",
            NameJp = "刺身",
            Kanji = "刺",
            Description = "Cá tươi phi lê tại bếp lạnh, thái dày 8mm, dùng cùng wasabi mài tay.",
            ImageUrl = "https://images.unsplash.com/photo-1583623025817-d180a2221d0a?auto=format&fit=crop&w=1200&q=80",
            AccentColor = "#B23A48",
            DisplayOrder = 2,
        },
        new Category
        {
            Slug = "ramen",
            Name = "Ramen",
            NameJp = "ラーメン",
            Kanji = "麺",
            Description = "Nước dùng ninh chậm nhiều giờ, sợi mì tươi làm trong ngày, chan nóng phục vụ ngay.",
            ImageUrl = "https://images.unsplash.com/photo-1569718212165-3a8278d5f624?auto=format&fit=crop&w=1200&q=80",
            AccentColor = "#2C3E63",
            DisplayOrder = 3,
        },
        new Category
        {
            Slug = "udon",
            Name = "Udon",
            NameJp = "うどん",
            Kanji = "饂",
            Description = "Sợi udon dày nhồi tay theo lối Sanuki, dai bật, ăn nóng chan dashi hoặc lạnh chấm tsuyu.",
            ImageUrl = "https://images.unsplash.com/photo-1618841557871-b4664fbf0cb3?auto=format&fit=crop&w=1200&q=80",
            AccentColor = "#8A6D3B",
            DisplayOrder = 4,
        },
        new Category
        {
            Slug = "bbq",
            Name = "BBQ Nhật",
            NameJp = "焼肉",
            Kanji = "焼",
            Description = "Yakiniku & yakitori nướng than hoa binchotan, thịt cắt dày vừa, chấm tare nhà làm.",
            ImageUrl = "https://images.unsplash.com/photo-1600891964092-4316c288032e?auto=format&fit=crop&w=1200&q=80",
            AccentColor = "#A4462F",
            DisplayOrder = 5,
        },
        new Category
        {
            Slug = "wagyu",
            Name = "Bò Wagyu & Kobe",
            NameJp = "和牛",
            Kanji = "牛",
            Description = "Bò Kobe, Matsusaka, Omi hạng A5 — steak, sukiyaki, shabu-shabu và sushi bò khò lửa.",
            ImageUrl = DishPhotos.CategoryCovers["wagyu"],
            AccentColor = "#7A2E2E",
            DisplayOrder = 6,
        },
        new Category
        {
            Slug = "hai-san",
            Name = "Hải sản cao cấp",
            NameJp = "高級海鮮",
            Kanji = "海",
            Description = "Cua hoàng đế, tôm hùm Ise, bào ngư, nhím biển Hokkaido và cá quý từ chợ Toyosu.",
            ImageUrl = DishPhotos.CategoryCovers["hai-san"],
            AccentColor = "#1F5F7A",
            DisplayOrder = 7,
        },
        new Category
        {
            Slug = "tempura",
            Name = "Tempura & đồ chiên",
            NameJp = "天ぷら・揚げ物",
            Kanji = "揚",
            Description = "Tempura vỏ mỏng như ren, tonkatsu, karaage, takoyaki và okonomiyaki nóng giòn.",
            ImageUrl = DishPhotos.CategoryCovers["tempura"],
            AccentColor = "#C08A2B",
            DisplayOrder = 8,
        },
        new Category
        {
            Slug = "donburi",
            Name = "Cơm & Donburi",
            NameJp = "丼・ご飯",
            Kanji = "丼",
            Description = "Katsudon, oyakodon, gyudon, omurice và cơm nắm — no bụng, đậm vị, phục vụ nhanh.",
            ImageUrl = DishPhotos.CategoryCovers["donburi"],
            AccentColor = "#9C5B2E",
            DisplayOrder = 9,
        },
        new Category
        {
            Slug = "mochi",
            Name = "Mochi & Wagashi",
            NameJp = "餅・和菓子",
            Kanji = "餅",
            Description = "Mochi giã tay mỗi sáng, daifuku trái cây, dango, yokan và wagashi dùng cùng matcha.",
            ImageUrl = DishPhotos.CategoryCovers["mochi"],
            AccentColor = "#C46B8A",
            DisplayOrder = 10,
        },
        new Category
        {
            Slug = "trang-mieng",
            Name = "Tráng miệng",
            NameJp = "デザート",
            Kanji = "甘",
            Description = "Purin, bánh ngọt, dorayaki và kem — ngọt dịu kiểu Nhật, không gắt, ăn nhẹ sau bữa chính.",
            ImageUrl = "https://images.unsplash.com/photo-1488477181946-6428a0291777?auto=format&fit=crop&w=1200&q=80",
            AccentColor = "#E8A2AE",
            DisplayOrder = 11,
        },
        new Category
        {
            Slug = "thuc-uong",
            Name = "Thức uống",
            NameJp = "ドリンク",
            Kanji = "茶",
            Description = "Matcha Uji đánh bằng chasen, hojicha rang mộc, soda trái cây và trà lạnh ủ chậm.",
            ImageUrl = "https://images.unsplash.com/photo-1536256263959-770b48d82b0a?auto=format&fit=crop&w=1200&q=80",
            AccentColor = "#6F8F4F",
            DisplayOrder = 12,
        },
    ];
}
