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

        builder.HasMany(r => r.SeatSlots)
            .WithOne(s => s.SeatRow)
            .HasForeignKey(s => s.SeatRowId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}