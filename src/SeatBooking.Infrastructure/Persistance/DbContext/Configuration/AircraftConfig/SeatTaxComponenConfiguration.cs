using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Infrastructure.Persistance.DbContext.Configuration;

public class SeatTaxComponentConfiguration : IEntityTypeConfiguration<SeatTaxComponent>
{
    public void Configure(EntityTypeBuilder<SeatTaxComponent> builder)
    {
        builder.ToTable("SeatTaxComponent");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.Currency)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(c => c.SeatTaxAlternativeId)
            .IsRequired();

         builder.HasOne<SeatTaxAlternative>()
               .WithMany(pa => pa.Components)
               .HasForeignKey(c => c.SeatTaxAlternativeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
