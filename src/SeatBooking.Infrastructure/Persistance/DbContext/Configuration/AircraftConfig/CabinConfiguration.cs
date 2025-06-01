using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;

public class CabinConfiguration : IEntityTypeConfiguration<Cabin>
{
    public void Configure(EntityTypeBuilder<Cabin> builder)
    {
        builder.ToTable("Cabin");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Deck)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(c => c.AircraftCode)
               .IsRequired()
               .HasMaxLength(50);

        // Relationship: Cabin to Aircraft
        builder.HasOne(c => c.Aircraft)
               .WithMany(a => a.Cabins)
               .HasForeignKey(c => c.AircraftCode)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship: Cabin to SeatRows using backing field
        var seatRowsNav = builder.Metadata.FindNavigation(nameof(Cabin.SeatRows))!;
        seatRowsNav.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(c => c.SeatRows)
               .WithOne()
               .HasForeignKey("CabinId")
               .OnDelete(DeleteBehavior.Restrict);

        // Relationship: Cabin to SeatColumns using backing field
        var seatColumnsNav = builder.Metadata.FindNavigation(nameof(Cabin.SeatColumns))!;
        seatColumnsNav.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(c => c.SeatColumns)
               .WithOne()
               .HasForeignKey("CabinId")
               .OnDelete(DeleteBehavior.Restrict);
    }
}

