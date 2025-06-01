using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;


public class SeatPriceAlternativeConfiguration : IEntityTypeConfiguration<SeatPriceAlternative>
{
       public void Configure(EntityTypeBuilder<SeatPriceAlternative> builder)
       {
              builder.ToTable("SeatPriceAlternative");

              builder.HasKey(pa => pa.Id);
              builder.Property(pa => pa.Id).ValueGeneratedOnAdd();
              builder.Property(pa => pa.SeatSlotId).IsRequired();

              builder.HasOne(pa => pa.SeatSlot)
                     .WithMany(s => s.PriceAlternatives)
                     .HasForeignKey(pa => pa.SeatSlotId)
                     .OnDelete(DeleteBehavior.Cascade);

              // Use public property for navigation and tell EF to use field
              builder.HasMany(pa => pa.Components)
                     .WithOne()
                     .HasForeignKey("SeatPriceAlternativeId")
                     .OnDelete(DeleteBehavior.Restrict);

              builder.Metadata
                     .FindNavigation(nameof(SeatPriceAlternative.Components))!
                     .SetPropertyAccessMode(PropertyAccessMode.Field);
              builder.HasMany(pa => pa.Components)
                     .WithOne(c => c.SeatPriceAlternative)
                     .HasForeignKey(c => c.SeatPriceAlternativeId)
                     .OnDelete(DeleteBehavior.Restrict);
       }
}
