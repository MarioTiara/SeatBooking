using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistance.DbContext.Configuration;

public class SeatPriceComponentConfiguration : IEntityTypeConfiguration<SeatPriceComponent>
{
    public void Configure(EntityTypeBuilder<SeatPriceComponent> builder)
    {
        builder.ToTable("SeatPriceComponent");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.Currency)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(c => c.SeatPriceAlternativeId)
            .IsRequired();

        builder.HasOne<SeatPriceAlternative>()
               .WithMany(pa => pa.Components)
               .HasForeignKey(c => c.SeatPriceAlternativeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
