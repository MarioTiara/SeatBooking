using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;

public class SeatRowConfiguration : IEntityTypeConfiguration<SeatRow>
{
    public void Configure(EntityTypeBuilder<SeatRow> builder)
    {
        builder.ToTable("SeatRow");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RowNumber).IsRequired();
        builder.Property(r => r.CabinId).IsRequired();

        // Configure the SeatSlots collection using backing field
        builder.Navigation(r => r.SeatSlots)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_seatSlots");

        // Configure relationship with Cabin
        builder.HasOne(r => r.Cabin)
            .WithMany(c => c.SeatRows)
            .HasForeignKey(r => r.CabinId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure relationship with SeatSlots
        builder.HasMany(r => r.SeatSlots)
            .WithOne(s => s.SeatRow)
            .HasForeignKey(s => s.SeatRowId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}