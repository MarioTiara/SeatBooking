using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.PassengerAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;

public class SpecialServiceRequestRemarkConfiguration : IEntityTypeConfiguration<SpecialServiceRequestRemark>
{
    public void Configure(EntityTypeBuilder<SpecialServiceRequestRemark> builder)
    {
        builder.ToTable("SpecialServiceRequestRemark");
        builder.HasKey(ssrr => ssrr.Id);
        builder.Property(ssrr => ssrr.Value).IsRequired().HasMaxLength(255);
        builder.Property<int>("PassengerId").IsRequired();

        builder.HasOne<Passenger>()
            .WithMany()
            .HasForeignKey("PassengerId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}