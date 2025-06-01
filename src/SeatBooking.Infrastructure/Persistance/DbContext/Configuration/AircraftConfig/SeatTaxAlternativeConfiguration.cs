using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;

public class SeatTaxAlternativeConfiguration : IEntityTypeConfiguration<SeatTaxAlternative>
{
    public void Configure(EntityTypeBuilder<SeatTaxAlternative> builder)
    {
        builder.ToTable("SeatTaxAlternative");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.SeatSlotId).IsRequired();

        builder.HasOne(t => t.SeatSlot)
            .WithMany(s => s.Taxes)
            .HasForeignKey(t => t.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

        // Correct: Map public navigation and tell EF to use backing field
        builder.HasMany(t => t.Components)
            .WithOne()
            .HasForeignKey("SeatTaxAlternativeId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.Metadata
            .FindNavigation(nameof(SeatTaxAlternative.Components))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
     builder.HasMany(pa => pa.Components)
                     .WithOne(c => c.SeatTaxAlternative)
                     .HasForeignKey(c => c.SeatTaxAlternativeId)
                     .OnDelete(DeleteBehavior.Restrict);
    }
}
