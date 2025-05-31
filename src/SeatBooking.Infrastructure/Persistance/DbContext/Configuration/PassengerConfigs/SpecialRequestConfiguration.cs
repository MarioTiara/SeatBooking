using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.PassengerAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class SpecialRequestConfiguration : IEntityTypeConfiguration<SpecialRequest>
{
    public void Configure(EntityTypeBuilder<SpecialRequest> builder)
    {
        builder.ToTable("SpecialRequest");
        builder.HasKey(sr => sr.Id);
        builder.Property(sr => sr.Value).IsRequired().HasMaxLength(255);
        builder.Property<int>("PassengerId").IsRequired();

        builder.HasOne<Passenger>()
            .WithMany()
            .HasForeignKey("PassengerId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}