using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Code);

        // Lưu "Cancelled" thay vì số 4 để mở database ra đọc được ngay.
        builder.Property(o => o.Status).HasConversion<string>();

        // Dòng món là một phần của đơn: tạo cùng đơn, đọc cùng đơn, không tồn tại một mình.
        // Không đặt khoá ngoại sang Dishes vì dòng món là bản chụp lúc đặt — sau này món có đổi
        // tên, đổi giá hay bị bỏ khỏi thực đơn thì đơn cũ vẫn phải giữ nguyên.
        builder.OwnsMany(o => o.Lines, line =>
        {
            line.ToTable("OrderLines");
            line.WithOwner().HasForeignKey("OrderCode");
            line.Property<int>("Id");
            line.HasKey("Id");
        });
    }
}

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");
        builder.HasKey(r => r.Code);

        // Tính chỗ trống luôn lọc theo ngày rồi gom theo giờ.
        builder.HasIndex(r => new { r.Date, r.Time });
    }
}
