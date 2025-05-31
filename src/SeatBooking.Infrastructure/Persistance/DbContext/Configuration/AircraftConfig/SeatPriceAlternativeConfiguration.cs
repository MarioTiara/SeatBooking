using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class SeatPriceComponentConfiguration : IEntityTypeConfiguration<SeatPriceComponent>
{
    public void Configure(EntityTypeBuilder<SeatPriceComponent> builder)
    {
        builder.ToTable("SeatPriceComponent");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(c => c.Currency).IsRequired().HasMaxLength(10);
        builder.Property(c => c.SeatPriceAlternativeId).IsRequired();
    }
}