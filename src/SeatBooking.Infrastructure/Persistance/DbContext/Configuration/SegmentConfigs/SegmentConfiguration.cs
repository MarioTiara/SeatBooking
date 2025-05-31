using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.SegmentAggregate;
using SeatBooking.Domain.Shared;

namespace SeatBooking.Infrastructure.Persistence.Segemnt.Configurations;

public class SegmentConfiguration : IEntityTypeConfiguration<Segment>
{
    public void Configure(EntityTypeBuilder<Segment> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.FlightsMiles).IsRequired();
        builder.Property(s => s.AwardFare).IsRequired();
        builder.Property(s => s.Duration).IsRequired();
        builder.Property(s => s.CabinClass).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Equipment).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Departure).IsRequired();
        builder.Property(s => s.Arrival).IsRequired();
        builder.Property(s => s.BookingClass).IsRequired().HasMaxLength(10);
        builder.Property(s => s.LayoverDuration).IsRequired();
        builder.Property(s => s.FareBasis).IsRequired().HasMaxLength(50);
        builder.Property(s => s.SubjectToGovernmentApproval).IsRequired();
        builder.Property(s => s.SegmentRef).IsRequired().HasMaxLength(50);

        builder.HasOne<FlightInfo>()
            .WithMany()
            .HasForeignKey("FlightInfoId")
            .IsRequired();

        builder.HasOne<Airport>()
            .WithMany()
            .HasForeignKey("OriginAirportCode")
            .HasPrincipalKey(a => a.Code)
            .IsRequired();

        builder.HasOne<Airport>()
            .WithMany()
            .HasForeignKey("DestinationAirportCode")
            .HasPrincipalKey(a => a.Code)
            .IsRequired();
    }
}