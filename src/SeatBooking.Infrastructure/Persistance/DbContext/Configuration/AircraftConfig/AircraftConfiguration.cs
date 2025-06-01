using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;

public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
{
    public void Configure(EntityTypeBuilder<Aircraft> builder)
    {
        builder.ToTable("Aircraft");

        // Primary key
        builder.HasKey(a => a.Code);
        builder.Property(a => a.Code)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(a => a.Name)
               .IsRequired()
               .HasMaxLength(100);

        // Configure backing field for Cabins collection
        builder.Metadata
               .FindNavigation(nameof(Aircraft.Cabins))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(typeof(Cabin), "_cabins")
               .WithOne()
               .HasForeignKey("AircraftCode")
               .OnDelete(DeleteBehavior.Cascade);

        // Optional: You can configure Cabin table mapping separately in its own config
    }
}