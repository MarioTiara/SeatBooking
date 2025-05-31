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
        builder.Property(c => c.Deck).IsRequired().HasMaxLength(50);

        builder.Property(c => c.AircraftCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(c => c.Aircraft)
            .WithMany(a => a.Cabins)
            .HasForeignKey(c => c.AircraftCode)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.SeatRows)
            .WithOne(r => r.Cabin)
            .HasForeignKey(r => r.CabinId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.SeatColumns)
            .WithOne(sc => sc.Cabin)
            .HasForeignKey(sc => sc.CabinId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}