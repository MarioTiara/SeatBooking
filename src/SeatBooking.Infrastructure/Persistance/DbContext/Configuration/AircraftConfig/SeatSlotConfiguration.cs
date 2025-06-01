using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        builder.HasMany(s => s.PriceAlternatives)
            .WithOne(pa => pa.SeatSlot)
            .HasForeignKey(pa => pa.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Taxes)
            .WithOne(t => t.SeatSlot)
            .HasForeignKey(t => t.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

     
        builder.HasMany(s => s.SeatSelections)
            .WithOne()
            .HasForeignKey("SeatSlotId")
            .OnDelete(DeleteBehavior.NoAction);



        builder.Navigation(s => s.PriceAlternatives)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_priceAlternatives");

        builder.Navigation(s => s.Taxes)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_taxes");


        builder.Navigation(s => s.SeatSelections)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_seatSelections");

        // Value converter for List<string> <-> string
        var stringListConverter = new ValueConverter<List<string>, string>(
            v => string.Join(";", v ?? new List<string>()),
            v => string.IsNullOrEmpty(v) ? new List<string>() : v.Split(';', StringSplitOptions.None).ToList()
        );

        // Map _seatCharacteristics
        builder.Property(typeof(List<string>), "_seatCharacteristics")
            .HasColumnName("SeatCharacteristics")
            .HasConversion(stringListConverter);

        // Map _rawSeatCharacteristics
        builder.Property(typeof(List<string>), "_rawSeatCharacteristics")
            .HasColumnName("RawSeatCharacteristics")
            .HasConversion(stringListConverter);

        builder.Property(typeof(List<string>), "_slotCharacteristics")
            .HasColumnName("SlotCharacteristics")
            .HasConversion(stringListConverter);

        // Map _designations
        builder.Property(typeof(List<string>), "_designations")
            .HasColumnName("Designations")
            .HasConversion(stringListConverter);

        // Map _limitations
        builder.Property(typeof(List<string>), "_limitations")
            .HasColumnName("Limitations")
            .HasConversion(stringListConverter);
    }
}