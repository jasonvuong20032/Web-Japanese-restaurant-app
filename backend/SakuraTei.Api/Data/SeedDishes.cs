using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

/// <summary>
/// Toàn bộ 250 món của thực đơn, dựng sẵn trong bộ nhớ khi khởi động.
/// Mỗi nhóm món nằm ở một file <c>SeedDishes.&lt;Nhóm&gt;.cs</c> cho dễ đọc.
/// </summary>
public static partial class SeedDishes
{
    /// <summary>Thực đơn đầy đủ, đã gộp mười hai nhóm theo thứ tự hiển thị.</summary>
    public static IReadOnlyList<Dish> All => Combined.Value;

    /// <summary>
    /// Phải gộp qua <see cref="Lazy{T}"/> chứ không gộp thẳng khi khởi tạo trường: các nhóm nằm ở
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
        .. Wagyu,
        .. HaiSan,
        .. Tempura,
        .. Donburi,
        .. Mochi,
        .. TrangMieng,
        .. ThucUong,
    ];

    /// <summary>
    /// Bảng tra dùng chung cho <see cref="Make"/>. Phải nằm trong lớp lồng riêng: <c>Make</c> được
    /// gọi từ bộ khởi tạo trường của các file partial khác, mà thứ tự chạy giữa các file là do trình
    /// biên dịch quyết định — để ở lớp ngoài thì bảng này có thể còn <c>null</c> lúc bị gọi.
    /// Lớp lồng có bộ khởi tạo kiểu riêng, luôn chạy xong trước lần truy cập đầu tiên.
    /// </summary>
    private static class Lookup
    {
        /// <summary>
        /// Câu mô tả tay nghề riêng của từng nhóm, ghép vào phần mô tả dài của mọi món trong nhóm
        /// để trang chi tiết luôn có nội dung đầy đặn mà không phải chép tay 250 lần.
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
        ["wagyu"] =
            "Bò Kobe, Matsusaka và Omi của quán đều kèm giấy chứng nhận truy xuất tới từng con bò. Thịt được "
            + "cắt tại quầy theo từng phiếu gọi món và để về nhiệt độ phòng 20 phút trước khi lên lửa.",
        ["hai-san"] =
            "Cua, tôm hùm và bào ngư được nuôi sống trong bể nước biển lạnh ngay tại bếp, chỉ sơ chế khi có "
            + "phiếu gọi món. Cá quý bay thẳng từ chợ Toyosu hai chuyến mỗi tuần.",
        ["tempura"] =
            "Bột tempura pha bằng nước đá và trộn sơ vài đường đũa để không lên gluten, nên vỏ mỏng và giòn "
            + "lâu. Dầu chiên là hỗn hợp dầu mè và dầu hạt cải, lọc sau mỗi ca.",
        ["donburi"] =
            "Cơm nấu từ gạo Koshihikari trong nồi gang, xới ra để nghỉ 10 phút trước khi múc bát. Nước sốt "
            + "tare và dashi nấu riêng cho từng món, không dùng chung một nồi.",
        ["mochi"] =
            "Nếp mochigome ngâm qua đêm, đồ chín rồi giã bằng cối gỗ mỗi sáng. Đậu đỏ Hokkaido nấu lửa riu "
            + "riu sáu tiếng, chỉ làm đủ bán trong ngày nên vỏ mochi luôn mềm.",
        ["trang-mieng"] =
            "Đồ ngọt ở Sakura Tei giảm 30% lượng đường so với công thức gốc, để vị nguyên liệu chính nổi lên "
            + "trước. Làm mẻ nhỏ mỗi ngày, không dùng chất bảo quản nên chỉ ngon trong ngày.",
        ["thuc-uong"] =
            "Matcha nhập từ Uji, Kyoto, mài bằng cối đá và chỉ mở hộp dùng trong 14 ngày. Nước pha lọc RO "
            + "hạ xuống đúng nhiệt độ của từng loại trà trước khi đánh hoặc ủ.",
        };
    }

    /// <summary>
    /// Dựng một món. Gom vào hàm để 250 bản ghi trong các file nhóm giữ được độ ngắn và nhất quán.
    /// Hai tham số <c>ingredients</c> và <c>tags</c> nhận chuỗi ngăn cách bằng dấu phẩy;
    /// ảnh lấy từ <see cref="DishPhotos"/> theo slug, phần mô tả dài suy ra từ nhóm món.
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
        var photos = DishPhotos.BySlug.GetValueOrDefault(slug)
            ?? throw new InvalidOperationException($"Món '{slug}' chưa có ảnh trong DishPhotos.");

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
            ImageUrl = photos[0].ThumbUrl,
            Photos = photos,
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
}
