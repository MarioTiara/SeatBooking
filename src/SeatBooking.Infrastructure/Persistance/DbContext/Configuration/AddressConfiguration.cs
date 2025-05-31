using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.PassengerAggregate;
using SeatBooking.Domain.Shared;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");
        builder.HasKey(a => a.Id);

        builder.Property<int>("Id").ValueGeneratedOnAdd();
        builder.Property<int>("PassengerId").IsRequired();
        builder.HasIndex("PassengerId").IsUnique();

        builder.Property(a => a.Street1).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Street2).HasMaxLength(200);
        builder.Property(a => a.Postcode).IsRequired().HasMaxLength(20);
        builder.Property(a => a.State).HasMaxLength(100);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(100);
        builder.Property(a => a.AddressType).IsRequired().HasMaxLength(50);

        builder.HasOne<Passenger>()
            .WithOne(p => p.Address)
            .HasForeignKey<Address>("PassengerId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}