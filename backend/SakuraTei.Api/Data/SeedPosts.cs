using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

/// <summary>Bài viết chuyên mục "Câu chuyện ẩm thực".</summary>
public static class SeedPosts
{
    public static readonly IReadOnlyList<BlogPost> All =
    [
        new BlogPost
        {
            Slug = "vi-sao-com-sushi-quan-trong-hon-ca",
            Title = "Vì sao cơm mới là linh hồn của miếng sushi",
            Excerpt =
                "Khách thường nhớ lát cá, nhưng người trong nghề đánh giá một quán sushi qua nắm cơm. "
                + "Nhiệt độ, độ chua và cách nắm quyết định tất cả.",
            Topic = "Văn hoá",
            Author = "Bếp trưởng Takumi Sato",
            ImageUrl = "https://images.unsplash.com/photo-1607301405390-d831c242f59b?auto=format&fit=crop&w=1400&q=80",
            PublishedAt = new DateOnly(2026, 8, 14),
            ReadMinutes = 6,
            Content =
                """
                Ở Nhật có câu "sushi là cơm, cá chỉ là lớp áo". Nghe hơi quá, nhưng càng làm nghề lâu tôi càng thấy đúng.

                ## Nhiệt độ thân người

                Cơm sushi ngon nhất ở khoảng 36°C — đúng bằng thân nhiệt bàn tay. Ấm hơn thì mỡ cá chảy sớm và miếng sushi bị bở. Lạnh hơn thì hạt cơm co lại, cắn vào thấy cứng và khô. Ở Sakura Tei, thùng cơm hangiri bằng gỗ tuyết tùng giữ nhiệt tự nhiên, và chúng tôi chỉ trộn mỗi lần đủ dùng trong 40 phút.

                ## Giấm quyết định hậu vị

                Chúng tôi dùng akazu — giấm đỏ ủ từ bã rượu sake trong hai năm. So với giấm gạo trắng, akazu ít chua gắt hơn, màu ngả hổ phách và để lại hậu vị ngọt. Đây là lý do cơm sushi của quán hơi sẫm màu chứ không trắng tinh.

                ## Nắm bao nhiêu lần là đủ

                Một itamae giỏi nắm xong một miếng nigiri trong ba tới bốn nhịp tay. Nắm nhiều hơn thì cơm bị nén chặt, mất khoảng trống giữa các hạt. Miếng sushi đúng chuẩn phải giữ được hình khi gắp, nhưng tan ra ngay khi chạm lưỡi.

                ## Thử ở nhà thế nào

                Nếu bạn tự làm sushi ở nhà, chỉ cần sửa hai điều là chất lượng lên rõ rệt: dùng gạo Koshihikari hoặc Akitakomachi thay gạo thường, và trộn giấm khi cơm còn nóng bằng động tác xắn chứ không đảo. Hạt cơm sẽ bóng và tơi hơn hẳn.
                """,
        },
        new BlogPost
        {
            Slug = "18-tieng-ninh-mot-noi-tonkotsu",
            Title = "18 tiếng cho một nồi nước dùng tonkotsu",
            Excerpt =
                "Nước dùng trắng đục như sữa không đến từ sữa hay bột. Nó đến từ lửa lớn, "
                + "thời gian dài và một người trực nồi suốt đêm.",
            Topic = "Hậu trường",
            Author = "Bếp trưởng Takumi Sato",
            ImageUrl = "https://images.unsplash.com/photo-1591814468924-caf88d1232e1?auto=format&fit=crop&w=1400&q=80",
            PublishedAt = new DateOnly(2026, 8, 2),
            ReadMinutes = 7,
            Content =
                """
                Nhiều khách hỏi vì sao tô tonkotsu của quán trắng đục mà không hề có sữa. Câu trả lời nằm ở vật lý, không phải nguyên liệu.

                ## Nhũ tương, không phải sữa

                Khi xương heo bị đun sôi mạnh liên tục, collagen tan thành gelatin và mỡ bị đánh vỡ thành hạt cực nhỏ, phân tán đều trong nước. Đó là nhũ tương — cùng nguyên lý với sốt mayonnaise. Nước dùng vì thế đục trắng và sánh, bám vào sợi mì thay vì trôi tuột.

                ## Vì sao phải là 18 tiếng

                Sáu tiếng đầu chỉ đủ rút vị ngọt. Từ giờ thứ mười, tuỷ xương mới bắt đầu tan. Từ giờ thứ mười lăm, nước dùng chuyển từ "ngọt" sang "béo dày". Chúng tôi bắt nồi lúc 5 giờ sáng hôm trước để kịp phục vụ trưa hôm sau.

                ## Công đoạn khó nhất: hớt bọt

                Trong ba tiếng đầu, cứ 15 phút phải hớt bọt một lần. Bỏ sót thì nước dùng có mùi hôi xương và vị đắng kim loại. Đây là việc không máy nào làm thay được.

                ## Tare mới là chữ ký

                Nước dùng là nền, nhưng thứ quyết định tô mì là tare — hỗn hợp nêm đáy bát. Mỗi quán có công thức riêng và thường không chia sẻ. Của chúng tôi gồm nước tương ủ gỗ, muối biển, sake nấu và một chút nước mắm cá cơm — dấu vết Việt Nam duy nhất trong bát mì Nhật này.
                """,
        },
        new BlogPost
        {
            Slug = "mochi-va-mua-xuan-nhat-ban",
            Title = "Mochi và những mùa trong năm của người Nhật",
            Excerpt =
                "Mỗi loại mochi gắn với một thời điểm trong năm. Ăn mochi đúng mùa là cách "
                + "người Nhật đánh dấu thời gian trôi qua.",
            Topic = "Văn hoá",
            Author = "Nguyễn Hạ Vy",
            ImageUrl = "https://images.unsplash.com/photo-1631206753348-db44968fd440?auto=format&fit=crop&w=1400&q=80",
            PublishedAt = new DateOnly(2026, 7, 21),
            ReadMinutes = 5,
            Content =
                """
                Ở Việt Nam ta có bánh chưng cho Tết, bánh trung thu cho rằm tháng Tám. Người Nhật cũng vậy, và mochi là thứ đánh dấu mùa rõ nhất.

                ## Tháng Giêng: kagami mochi

                Hai tầng mochi tròn xếp chồng, đặt quả cam daidai lên trên, bày ở góc trang trọng trong nhà suốt tuần đầu năm. Đến ngày 11 tháng Giêng thì đập ra nấu chè — không cắt bằng dao, vì "cắt" là điềm xấu.

                ## Tháng Ba: sakura mochi

                Vỏ mochi nhuộm hồng, gói trong lá anh đào muối. Lá vừa thơm vừa mặn, ăn cùng phần nhân ngọt tạo ra thế cân bằng rất Nhật. Đây là món của lễ Hinamatsuri, ngày cầu chúc cho các bé gái.

                ## Tháng Năm: kashiwa mochi

                Gói bằng lá sồi — loại lá không rụng cho tới khi chồi mới mọc, tượng trưng cho sự nối tiếp của các thế hệ. Món của ngày Tết thiếu nhi mùng 5 tháng 5.

                ## Mùa hè: warabi mochi

                Trong suốt như thạch, ăn lạnh, lăn bột đậu nành rang. Không phải mochi nếp thật sự, nhưng người Nhật vẫn gọi là mochi vì độ dẻo.

                Ở Sakura Tei chúng tôi giữ daifuku dâu tây quanh năm vì khách Việt rất thích, nhưng vào tháng Ba vẫn làm thêm sakura mochi trong hai tuần. Nếu ghé đúng dịp, bạn sẽ thấy nó trên bảng món viết tay ở cửa.
                """,
        },
        new BlogPost
        {
            Slug = "matcha-khong-phai-tra-xanh-thong-thuong",
            Title = "Matcha không phải là trà xanh pha đặc",
            Excerpt =
                "Rất nhiều người nghĩ matcha chỉ là trà xanh xay ra. Sự thật nằm ở 20 ngày "
                + "cuối cùng trước khi hái lá.",
            Topic = "Nguyên liệu",
            Author = "Nguyễn Hạ Vy",
            ImageUrl = "https://images.unsplash.com/photo-1536256263959-770b48d82b0a?auto=format&fit=crop&w=1400&q=80",
            PublishedAt = new DateOnly(2026, 7, 5),
            ReadMinutes = 6,
            Content =
                """
                Nếu bạn từng thấy matcha ở quán này đắng dịu và ngọt hậu, còn matcha ở chỗ khác chát gắt, khác biệt bắt đầu từ trước khi lá trà được hái.

                ## Hai mươi ngày trong bóng râm

                Ba tuần trước vụ hái, ruộng trà làm matcha được che lưới, chỉ còn 10% ánh sáng. Cây trà phản ứng bằng cách tăng diệp lục và tăng mạnh L-theanine — hoạt chất tạo vị umami ngọt. Đồng thời lượng catechin gây chát giảm xuống. Đó là lý do matcha ngọt hậu còn trà xanh thường thì chát.

                ## Cối đá, không phải máy xay

                Một cối đá granite quay 30 vòng mỗi phút chỉ cho ra 40 gram bột trong một giờ. Chậm như vậy là cố ý: máy xay công nghiệp sinh nhiệt, mà nhiệt phá huỷ hương matcha ngay lập tức.

                ## Độ đắng là một thang, không phải một điểm

                Matcha có nhiều hạng. Hạng nghi lễ (ceremonial) dùng để uống thuần, hạng ẩm thực (culinary) đắng hơn, dùng làm bánh vì phải đủ mạnh để không bị đường và sữa lấn át. Quán dùng hạng nghi lễ cho usucha và hạng ẩm thực cấp cao cho latte.

                ## Bảo quản mới là chỗ nhiều quán sai

                Matcha oxy hoá rất nhanh. Mở hộp ra là đồng hồ bắt đầu chạy: sau 3 tuần màu ngả vàng và mùi cỏ tươi biến mất. Chúng tôi chỉ mở hộp 40g mỗi lần và dùng hết trong 14 ngày. Ở nhà, bạn nên cất matcha trong hộp kín, để ngăn mát, và đừng mua hộp quá to.
                """,
        },
        new BlogPost
        {
            Slug = "thu-tu-goi-mon-o-quan-yakiniku",
            Title = "Gọi món ở quán yakiniku sao cho đúng thứ tự",
            Excerpt =
                "Cùng một khay thịt, gọi đúng thứ tự thì bữa ăn ngon hơn hẳn. "
                + "Nguyên tắc rất đơn giản: nhạt trước, đậm sau.",
            Topic = "Mẹo ăn uống",
            Author = "Trần Minh Khôi",
            ImageUrl = "https://images.unsplash.com/photo-1529193591184-b1d58069ecdd?auto=format&fit=crop&w=1400&q=80",
            PublishedAt = new DateOnly(2026, 6, 18),
            ReadMinutes = 4,
            Content =
                """
                Người Nhật ăn yakiniku theo một trình tự khá cố định. Không phải nghi thức gì cao siêu, chỉ là kinh nghiệm để vị giác không bị "cháy" giữa chừng.

                ## Bắt đầu bằng lưỡi bò

                Gyutan nêm muối, vị nhạt nhất trong khay. Ăn đầu tiên lúc vị giác còn nhạy nhất, và vắt chanh để mở khẩu vị. Gọi gyutan sau thịt ướp tare thì bạn sẽ chẳng nếm được gì.

                ## Tiếp theo là thịt nêm muối

                Nạc cổ heo, ba chỉ, harami nêm muối tiêu. Vẫn nhẹ, nhưng đã bắt đầu có mỡ.

                ## Rồi mới tới thịt ướp tare

                Karubi và các phần ướp sốt đậm. Từ đây trở đi vị giác đã quen với độ mặn ngọt, nên nếm được các lớp vị trong sốt.

                ## Rau và canh xen giữa

                Cứ hai lượt thịt thì nướng một lượt rau. Nấm shiitake và bí ngòi rửa vị rất tốt. Canh miso hoặc kim chi cũng để dành xen giữa chứ đừng ăn hết từ đầu.

                ## Vài điều nên tránh

                Đừng lật thịt liên tục — mỗi mặt chỉ lật một lần thôi. Đừng xếp kín vỉ, nhiệt sẽ tụt và thịt chuyển sang luộc. Và đừng nướng sẵn cả khay rồi mới ăn: yakiniku ngon nhất trong 30 giây đầu sau khi rời vỉ.
                """,
        },
        new BlogPost
        {
            Slug = "cau-chuyen-sakura-tei",
            Title = "Sakura Tei: từ một quầy ramen 6 ghế",
            Excerpt =
                "Năm 2019 chúng tôi mở một quầy ramen sáu ghế trong con hẻm quận 3. "
                + "Đây là chuyện của bảy năm sau đó.",
            Topic = "Câu chuyện quán",
            Author = "Vương Quang Tuấn",
            ImageUrl = "https://images.unsplash.com/photo-1590846406792-0adc7f938f1d?auto=format&fit=crop&w=1400&q=80",
            PublishedAt = new DateOnly(2026, 5, 30),
            ReadMinutes = 5,
            Content =
                """
                Quầy đầu tiên của Sakura Tei chỉ có sáu ghế và một nồi nước dùng. Menu đúng ba món. Chúng tôi mở cửa lúc 11 giờ và đóng khi hết nước dùng, thường là khoảng 2 giờ chiều.

                ## Năm đầu tiên: bán hết trước 1 giờ

                Nghe thì hay, nhưng thật ra là dấu hiệu chúng tôi nấu quá ít vì sợ lỗ. Nhiều khách đi xe từ quận 7 sang rồi phải quay về. Phải mất gần một năm mới dám tăng nồi lên gấp ba.

                ## Bước ngoặt: mời được bếp trưởng Sato

                Anh Takumi Sato làm ở một quán sushi tại Osaka 14 năm trước khi sang Việt Nam. Anh đặt một điều kiện duy nhất khi nhận lời: cá phải nhập trực tiếp, không qua trung gian. Chi phí đội lên 40% và chúng tôi lỗ suốt bốn tháng đầu. Nhưng từ tháng thứ năm, khách quay lại lần hai chiếm hơn một nửa.

                ## Hôm nay: 120 món, vẫn một nguyên tắc

                Thực đơn giờ có 120 món trải trên bảy nhóm. Nhưng nguyên tắc từ quầy sáu ghế thì không đổi: nấu vừa đủ trong ngày, hết là nghỉ. Bạn sẽ thỉnh thoảng thấy một vài món bị đánh dấu hết hàng trên website vào buổi tối — đó không phải lỗi hệ thống.

                ## Cảm ơn

                Cảm ơn những người đã đứng chờ trước quầy sáu ghế năm 2019. Phần lớn trong số họ giờ vẫn ghé, và chúng tôi vẫn nhớ họ gọi món gì.
                """,
        },
    ];
}
