using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

/// <summary>
/// Toàn bộ 120 món của thực đơn, dựng sẵn trong bộ nhớ khi khởi động.
/// Mỗi nhóm món nằm ở một file <c>SeedDishes.&lt;Nhóm&gt;.cs</c> cho dễ đọc.
/// </summary>
public static partial class SeedDishes
{
    /// <summary>Thực đơn đầy đủ, đã gộp bảy nhóm theo thứ tự hiển thị.</summary>
    public static IReadOnlyList<Dish> All => Combined.Value;

    /// <summary>
    /// Phải gộp qua <see cref="Lazy{T}"/> chứ không gộp thẳng khi khởi tạo trường: bảy nhóm nằm ở
    /// các file partial khác, mà thứ tự chạy của các bộ khởi tạo trường tĩnh giữa các file là do
    /// trình biên dịch quyết định — gộp sớm thì có nhóm còn <c>null</c>.
    /// </summary>
    private static readonly Lazy<IReadOnlyList<Dish>> Combined = new(BuildAll);

    private static IReadOnlyList<Dish> BuildAll() =>
    [
        .. Sushi,
        .. Sashimi,
        .. Ramen,
        .. Udon,
        .. Bbq,
        .. TrangMieng,
        .. ThucUong,
    ];

    /// <summary>
    /// Bảng tra dùng chung cho <see cref="Make"/>. Phải nằm trong lớp lồng riêng: <c>Make</c> được
    /// gọi từ bộ khởi tạo trường của các file partial khác, mà thứ tự chạy giữa các file là do trình
    /// biên dịch quyết định — để ở lớp ngoài thì hai bảng này có thể còn <c>null</c> lúc bị gọi.
    /// Lớp lồng có bộ khởi tạo kiểu riêng, luôn chạy xong trước lần truy cập đầu tiên.
    /// </summary>
    private static class Lookup
    {
        /// <summary>
        /// Câu mô tả tay nghề riêng của từng nhóm, ghép vào phần mô tả dài của mọi món trong nhóm
        /// để trang chi tiết luôn có nội dung đầy đặn mà không phải chép tay 120 lần.
        /// </summary>
        public static readonly Dictionary<string, string> CraftNotes = new()
        {
        ["sushi"] =
            "Cơm sushi của quán dùng gạo Koshihikari trộn giấm akazu ủ hai năm, giữ ở 36°C — đúng thân nhiệt "
            + "bàn tay người nắm. Itamae nắm từng miếng khi có phiếu gọi món, không nắm sẵn.",
        ["sashimi"] =
            "Cá về quán mỗi sáng, phi lê trong phòng lạnh 4°C rồi ủ nghỉ theo đúng số giờ mỗi loại cần để "
            + "thịt chuyển từ giòn sang ngọt. Dao yanagiba kéo một nhịp dứt khoát, mặt cắt phẳng và bóng.",
        ["ramen"] =
            "Nước dùng nấu từ 5 giờ sáng, hớt bọt liên tục và không đun sôi bùng để giữ nước trong. Sợi mì "
            + "cán trong ngày, trụng đúng giây theo độ dày từng loại rồi chan ngay khi tô còn bốc khói.",
        ["udon"] =
            "Bột udon nhồi bằng chân theo lối Sanuki, ủ nghỉ qua đêm cho gluten giãn đều nên sợi có độ bật "
            + "đặc trưng. Dashi rút từ tảo kombu Rishiri và cá ngừ bào katsuobushi, không dùng bột nêm.",
        ["bbq"] =
            "Nướng trên than trắng binchotan — cháy không khói, nhiệt bức xạ mạnh nên mặt thịt se nhanh mà "
            + "lòng vẫn mọng. Sốt tare nhà làm ủ 7 ngày từ nước tương, mirin, táo và tỏi nướng.",
        ["trang-mieng"] =
            "Đồ ngọt ở Sakura Tei giảm 30% lượng đường so với công thức gốc, để vị nguyên liệu chính nổi lên "
            + "trước. Làm mẻ nhỏ mỗi ngày, không dùng chất bảo quản nên chỉ ngon trong ngày.",
        ["thuc-uong"] =
            "Matcha nhập từ Uji, Kyoto, mài bằng cối đá và chỉ mở hộp dùng trong 14 ngày. Nước pha lọc RO "
            + "hạ xuống đúng nhiệt độ của từng loại trà trước khi đánh hoặc ủ.",
    };

    /// <summary>
    /// Kho ảnh minh hoạ theo nhóm. Mỗi món lấy một ảnh trong kho theo hàm băm của slug nên
    /// kết quả ổn định giữa các lần chạy. Frontend có sẵn ảnh dự phòng khi URL không tải được.
    /// </summary>
        public static readonly Dictionary<string, string[]> ImagePool = new()
    {
        ["sushi"] =
        [
            "https://images.unsplash.com/photo-1579871494447-9811cf80d66c?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1553621042-f6e147245754?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1617196034796-73dfa7b1fd56?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1611143669185-af224c5e3252?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1607301405390-d831c242f59b?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1564489563601-c53cfc451e93?auto=format&fit=crop&w=900&q=80",
        ],
        ["sashimi"] =
        [
            "https://images.unsplash.com/photo-1583623025817-d180a2221d0a?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1534482421-64566f976cfa?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1615361200141-f45040f367be?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1580822184713-fc5400e7fe10?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1519708227418-c8fd9a32b7a2?auto=format&fit=crop&w=900&q=80",
        ],
        ["ramen"] =
        [
            "https://images.unsplash.com/photo-1569718212165-3a8278d5f624?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1557872943-16a5ac26437e?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1591814468924-caf88d1232e1?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1623341214825-9f4f963727da?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1632709810780-b5a4343cebec?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1614563637806-1d0e645e0940?auto=format&fit=crop&w=900&q=80",
        ],
        ["udon"] =
        [
            "https://images.unsplash.com/photo-1618841557871-b4664fbf0cb3?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1626804475297-41608ea09aeb?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1552611052-33e04de081de?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1607330289024-1535c6b4e1c1?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?auto=format&fit=crop&w=900&q=80",
        ],
        ["bbq"] =
        [
            "https://images.unsplash.com/photo-1600891964092-4316c288032e?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1529193591184-b1d58069ecdd?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1607116176880-2b1b2f4b2c3a?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1544025162-d76694265947?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1558030006-450675393462?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1603360946369-dc9bb6258143?auto=format&fit=crop&w=900&q=80",
        ],
        ["trang-mieng"] =
        [
            "https://images.unsplash.com/photo-1488477181946-6428a0291777?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1631206753348-db44968fd440?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1587314168485-3236d6710814?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1563805042-7684c019e1cb?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1519676867240-f03562e64548?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1606313564200-e75d5e30476c?auto=format&fit=crop&w=900&q=80",
        ],
        ["thuc-uong"] =
        [
            "https://images.unsplash.com/photo-1536256263959-770b48d82b0a?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1515823064-d6e0c04616a7?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1544787219-7f47ccb76574?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1558160074-4d7d8bdf4256?auto=format&fit=crop&w=900&q=80",
            "https://images.unsplash.com/photo-1497515114629-f71d768fd07c?auto=format&fit=crop&w=900&q=80",
        ],
        };
    }

    /// <summary>
    /// Dựng một món. Gom vào hàm để 120 bản ghi trong các file nhóm giữ được độ ngắn và nhất quán.
    /// Hai tham số <c>ingredients</c> và <c>tags</c> nhận chuỗi ngăn cách bằng dấu phẩy;
    /// ảnh và phần mô tả dài được suy ra từ nhóm món nên không cần truyền vào.
    /// </summary>
    private static Dish Make(
        string category,
        string slug,
        string name,
        string nameJp,
        string romaji,
        decimal price,
        string description,
        string ingredients,
        string tags,
        int prepMinutes,
        int calories,
        double rating,
        int reviewCount,
        int spicyLevel = 0,
        bool isFeatured = false,
        decimal? originalPrice = null,
        bool isAvailable = true)
    {
        var ingredientList = Split(ingredients);
        var pool = Lookup.ImagePool[category];

        return new Dish
        {
            Id = slug,
            Slug = slug,
            Name = name,
            NameJp = nameJp,
            NameRomaji = romaji,
            CategorySlug = category,
            Description = description,
            LongDescription = $"{description}\n\n{Lookup.CraftNotes[category]}\n\nNguyên liệu chính: {string.Join(", ", ingredientList).ToLowerInvariant()}.",
            Price = price,
            OriginalPrice = originalPrice,
            ImageUrl = pool[(int)(StableHash(slug) % (uint)pool.Length)],
            Tags = Split(tags),
            Ingredients = ingredientList,
            SpicyLevel = spicyLevel,
            Calories = calories,
            PrepMinutes = prepMinutes,
            Rating = rating,
            ReviewCount = reviewCount,
            IsFeatured = isFeatured,
            IsAvailable = isAvailable,
        };
    }

    private static string[] Split(string csv) =>
        csv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    /// <summary>
    /// Băm FNV-1a. Dùng thay cho <c>string.GetHashCode()</c> vì .NET ngẫu nhiên hoá hash của chuỗi
    /// theo từng tiến trình, sẽ khiến ảnh của mỗi món đổi sau mỗi lần khởi động.
    /// </summary>
    private static uint StableHash(string value)
    {
        const uint offsetBasis = 2166136261;
        const uint prime = 16777619;

        var hash = offsetBasis;
        foreach (var c in value)
        {
            hash ^= c;
            hash *= prime;
        }

        return hash;
    }
}
