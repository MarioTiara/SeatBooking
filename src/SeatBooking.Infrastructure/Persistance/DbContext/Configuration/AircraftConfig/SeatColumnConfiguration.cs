using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class SeatColumnConfiguration : IEntityTypeConfiguration<SeatColumn>
{
    public void Configure(EntityTypeBuilder<SeatColumn> builder)
    {
        builder.ToTable("SeatColumn");
        builder.HasKey(sc => sc.Id);
        builder.Property(sc => sc.Code).IsRequired().HasMaxLength(50);
        builder.Property(sc => sc.CabinId).IsRequired();
    }
}