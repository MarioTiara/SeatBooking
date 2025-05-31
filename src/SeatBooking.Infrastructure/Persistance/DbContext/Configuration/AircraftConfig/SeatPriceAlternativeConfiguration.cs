namespace SeatBooking.Infrastructure.Persistance.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

public class SeatPriceAlternativeConfiguration : IEntityTypeConfiguration<SeatPriceAlternative>
{
    public void Configure(EntityTypeBuilder<SeatPriceAlternative> builder)
    {
        builder.ToTable("SeatPriceAlternative");
        builder.HasKey(pa => pa.Id);
        builder.Property(pa => pa.SeatSlotId).IsRequired();

        builder.HasOne(pa => pa.SeatSlot)
            .WithMany(s => s.PriceAlternatives)
            .HasForeignKey(pa => pa.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

        // Owned collection for components
        builder.OwnsMany(pa => pa.Components, c =>
        {
            c.WithOwner().HasForeignKey("SeatPriceAlternativeId");
            c.Property<int>("Id"); // Shadow property for PK
            c.HasKey("Id");
            c.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
            c.Property(x => x.Currency).IsRequired().HasMaxLength(10);
            c.ToTable("SeatPriceComponent");
        });
    }
}