using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.SegmentAggregate;
using SeatBooking.Domain.Shared;

namespace SeatBooking.Infrastructure.Persistence.Configurations;

public class FlightInfoConfiguration : IEntityTypeConfiguration<FlightInfo>
{
    public void Configure(EntityTypeBuilder<FlightInfo> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.FlightNumber).IsRequired();
        builder.Property(f => f.OperatingFlightNumber).IsRequired();
        builder.Property(f => f.AirlineCode).IsRequired().HasMaxLength(10);
        builder.Property(f => f.OperatingAirlineCode).IsRequired().HasMaxLength(10);
        builder.Property(f => f.DepartureTerminal).IsRequired().HasMaxLength(50);
        builder.Property(f => f.ArrivalTerminal).IsRequired().HasMaxLength(50);

        // Many-to-many: FlightInfo <-> Airport (StopAirports)
        builder
            .HasMany<Airport>("StopAirports")
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "FlightInfoStopAirport",
                j => j
                    .HasOne<Airport>()
                    .WithMany()
                    .HasForeignKey("AirportCode")
                    .HasPrincipalKey(a => a.Code)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<FlightInfo>()
                    .WithMany()
                    .HasForeignKey("FlightInfoId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("FlightInfoId", "AirportCode");
                    j.Property<int>("StopOrder");
                    j.ToTable("FlightInfoStopAirport");
                }
            );
    }
}