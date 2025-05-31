using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.PassengerAggregate;
using SeatBooking.Domain.Shared.ValueObjects;
using SeatBooking.Domain.Shared;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("Passenger");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PassengerIndex).IsRequired();
        builder.Property(p => p.PassengerNameNumber).IsRequired().HasMaxLength(100);
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.DateOfBirth).IsRequired();
        builder.Property(p => p.Gender).IsRequired().HasMaxLength(20);
        builder.Property(p => p.Type).IsRequired().HasMaxLength(50);

        // SpecialPreferences (flattened)
        builder.OwnsOne(p => p.SpecialPreferences, sp =>
        {
            sp.Property(x => x.MealPreference).IsRequired().HasMaxLength(100).HasColumnName("MealPreference");
            sp.Property(x => x.SeatPreference).IsRequired().HasMaxLength(100).HasColumnName("SeatPreference");

            sp.OwnsMany(s => s.SpecialRequests, r =>
            {
                r.WithOwner().HasForeignKey("PassengerId");
                r.Property<int>("Id"); // Shadow property for PK
                r.HasKey("Id");
                r.Property(x => x.Value).IsRequired().HasMaxLength(255);
                r.ToTable("SpecialRequest");
            });

            sp.OwnsMany(s => s.SpecialServiceRequestRemarks, r =>
            {
                r.WithOwner().HasForeignKey("PassengerId");
                r.Property<int>("Id"); // Shadow property for PK
                r.HasKey("Id");
                r.Property(x => x.Value).IsRequired().HasMaxLength(255);
                r.ToTable("SpecialServiceRequestRemark");
            });
        });

        // Address: one-to-one, separate table
        builder.HasOne(p => p.Address)
            .WithOne()
            .HasForeignKey<Address>("PassengerId")
            .OnDelete(DeleteBehavior.Cascade);

        // DocumentInfo: one-to-one, separate table
        builder.HasOne(p => p.DocumentInfo)
            .WithOne()
            .HasForeignKey<DocumentInfo>("PassengerId")
            .OnDelete(DeleteBehavior.Cascade);

        // Relationships
        builder.HasMany(p => p.FrequentFlyers)
            .WithOne()
            .HasForeignKey("PassengerId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.SeatSelection)
            .WithOne(pss => pss.Passenger)
            .HasForeignKey<PassengerSeatSelection>(pss => pss.PassengerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Emails as owned collection
        builder.OwnsMany(p => p.Emails, e =>
        {
            e.WithOwner().HasForeignKey("PassengerId");
            e.Property<int>("Id"); // Shadow property for PK
            e.HasKey("Id");
            e.Property(x => x.Value).IsRequired().HasMaxLength(255);
            e.ToTable("Email");
        });

        // Phones as owned collection
        builder.OwnsMany(p => p.Phones, p =>
        {
            p.WithOwner().HasForeignKey("PassengerId");
            p.Property<int>("Id"); // Shadow property for PK
            p.HasKey("Id");
            p.Property(x => x.Value).IsRequired().HasMaxLength(50);
            p.ToTable("Phone");
        });
    }
}