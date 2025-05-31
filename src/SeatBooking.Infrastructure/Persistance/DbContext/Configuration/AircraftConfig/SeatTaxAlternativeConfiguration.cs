namespace SeatBooking.Infrastructure.Persistance.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

public class SeatTaxAlternativeConfiguration : IEntityTypeConfiguration<SeatTaxAlternative>
{
    public void Configure(EntityTypeBuilder<SeatTaxAlternative> builder)
    {
        builder.ToTable("SeatTaxAlternative");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.SeatSlotId).IsRequired();

        builder.HasOne(t => t.SeatSlot)
            .WithMany(s => s.Taxes)
            .HasForeignKey(t => t.SeatSlotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(t => t.Components, c =>
        {
            c.WithOwner().HasForeignKey("SeatTaxAlternativeId");
            c.Property<int>("Id"); // Shadow property for PK
            c.HasKey("Id");
            c.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
            c.Property(x => x.Currency).IsRequired().HasMaxLength(10);
            c.ToTable("SeatTaxComponent");
        });
    }
}