using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.PassengerAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class PassengerSeatSelectionConfiguration : IEntityTypeConfiguration<PassengerSeatSelection>
{
    public void Configure(EntityTypeBuilder<PassengerSeatSelection> builder)
    {
        builder.ToTable("PassengerSeatSelection");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PassengerId).IsRequired();
        builder.Property(p => p.SeatSlotId).IsRequired();

        builder.HasOne<Passenger>()
            .WithMany()
            .HasForeignKey(p => p.PassengerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.SeatSlot)
            .WithMany()
            .HasForeignKey(p => p.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}