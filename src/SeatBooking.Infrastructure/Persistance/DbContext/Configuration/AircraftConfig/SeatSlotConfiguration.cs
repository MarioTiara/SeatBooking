using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class SeatSlotConfiguration : IEntityTypeConfiguration<SeatSlot>
{
    public void Configure(EntityTypeBuilder<SeatSlot> builder)
    {
        builder.ToTable("SeatSlot");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.StorefrontSlotCode).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Code).HasMaxLength(50);
        builder.Property(s => s.Available).IsRequired();
        builder.Property(s => s.Entitled).IsRequired();
        builder.Property(s => s.FreeOfCharge).IsRequired();

        builder.HasMany(s => s.SeatCharacteristics)
            .WithOne(sc => sc.SeatSlot)
            .HasForeignKey(sc => sc.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.PriceAlternatives)
            .WithOne(pa => pa.SeatSlot)
            .HasForeignKey(pa => pa.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}