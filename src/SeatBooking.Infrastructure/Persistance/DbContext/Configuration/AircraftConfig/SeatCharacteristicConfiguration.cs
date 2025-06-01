using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class SeatCharacteristicConfiguration : IEntityTypeConfiguration<SeatCharacteristic>
{
    public void Configure(EntityTypeBuilder<SeatCharacteristic> builder)
    {
        builder.ToTable("SeatCharacteristic");
        builder.HasKey(sc => sc.Id);
        builder.Property(sc => sc.Code).IsRequired().HasMaxLength(50);
        builder.Property(sc => sc.IsRaw).IsRequired();
        builder.Property(sc => sc.IsSlot).IsRequired();
        builder.Property(sc => sc.SeatSlotId).IsRequired();
    }
}