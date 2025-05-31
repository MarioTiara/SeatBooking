using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.SegmentAggregate;
using SeatBooking.Domain.Shared;

namespace SeatBooking.Infrastructure.Persistence.Configurations;

public class SegmentConfiguration : IEntityTypeConfiguration<Segment>
{
    public void Configure(EntityTypeBuilder<Segment> builder)
    {
        builder.ToTable("Segment");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.CabinClass).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Equipment).IsRequired().HasMaxLength(50);
        builder.Property(s => s.BookingClass).IsRequired().HasMaxLength(10);
        builder.Property(s => s.FareBasis).IsRequired().HasMaxLength(50);
        builder.Property(s => s.SegmentRef).IsRequired().HasMaxLength(50);

        builder.HasOne(s => s.Origin)
            .WithMany()
            .HasForeignKey(s => s.OriginAirportCode)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(s => s.Destination)
            .WithMany()
            .HasForeignKey(s => s.DestinationAirportCode)
            .OnDelete(DeleteBehavior.Cascade); // Only one CASCADE allowed

        builder.HasOne(s => s.Flight)
            .WithMany()
            .HasForeignKey("FlightId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<FlightInfo>()
            .WithMany()
            .HasForeignKey("FlightInfoId")
            .OnDelete(DeleteBehavior.NoAction);
    }
}