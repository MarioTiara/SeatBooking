using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class SeatSlotConfiguration : IEntityTypeConfiguration<SeatSlot>
{
    public void Configure(EntityTypeBuilder<SeatSlot> builder)
    {
        builder.ToTable("SeatSlot");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.StorefrontSlotCode).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Code).HasMaxLength(50);
        builder.Property(s => s.Available).IsRequired();
        builder.Property(s => s.Entitled).IsRequired();
        builder.Property(s => s.FreeOfCharge).IsRequired();
        builder.Property(s => s.FeeWaived).IsRequired();
        builder.Property(s => s.OriginallySelected).IsRequired();
        builder.Property(s => s.EntitledRuleId).HasMaxLength(100);
        builder.Property(s => s.FeeWaivedRuleId).HasMaxLength(100);
        builder.Property(s => s.RefundIndicator).HasMaxLength(10);

        builder.Property(s => s.SeatRowId).IsRequired();

        builder.HasMany(s => s.SeatCharacteristics)
            .WithOne(sc => sc.SeatSlot)
            .HasForeignKey(sc => sc.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.PriceAlternatives)
            .WithOne(pa => pa.SeatSlot)
            .HasForeignKey(pa => pa.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Taxes)
            .WithOne(t => t.SeatSlot)
            .HasForeignKey(t => t.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(s => s.Designations, d =>
        {
            d.WithOwner().HasForeignKey("SeatSlotId");
            d.Property<int>("Id"); // Shadow property for PK
            d.HasKey("Id");
            d.Property(x => x.Code).IsRequired().HasMaxLength(100);
            d.ToTable("SlotDesignation");
        });

        builder.OwnsMany(s => s.Limitations, l =>
        {
            l.WithOwner().HasForeignKey("SeatSlotId");
            l.Property<int>("Id"); // Shadow property for PK
            l.HasKey("Id");
            l.Property(x => x.Code).IsRequired().HasMaxLength(100);
            l.ToTable("SlotLimitation");
        });

        builder.HasMany(s => s.SeatSelections)
            .WithOne()
            .HasForeignKey("SeatSlotId")
            .OnDelete(DeleteBehavior.NoAction);
    }
}