using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.Shared;

namespace SeatBooking.Infrastructure.Persistence.Configurations;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.HasKey(a => a.Code);
        builder.Property(a => a.Code).HasMaxLength(10);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);

        builder.HasData(
            new Airport ( code: "CGK", name: "Soekarno-Hatta International", city: "Jakarta" ),
            new Airport ( code: "KUL", name: "Kuala Lumpur International", city: "Kuala Lumpur" )
            // Add more as needed
        );
    }
}