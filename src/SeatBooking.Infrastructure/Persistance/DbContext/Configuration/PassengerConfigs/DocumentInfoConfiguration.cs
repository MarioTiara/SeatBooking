using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatBooking.Domain.PassengerAggregate;

namespace SeatBooking.Infrastructure.Persistence.Configurations;
public class DocumentInfoConfiguration : IEntityTypeConfiguration<DocumentInfo>
{
    public void Configure(EntityTypeBuilder<DocumentInfo> builder)
    {
        builder.ToTable("DocumentInfo");
        builder.HasKey(d => d.Id);

        builder.Property<int>("Id").ValueGeneratedOnAdd();
        builder.Property<int>("PassengerId").IsRequired();
        builder.HasIndex("PassengerId").IsUnique();

        builder.Property(d => d.IssuingCountry).IsRequired().HasMaxLength(100);
        builder.Property(d => d.CountryOfBirth).IsRequired().HasMaxLength(100);
        builder.Property(d => d.DocumentType).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Nationality).IsRequired().HasMaxLength(100);

        builder.HasOne<Passenger>()
            .WithOne(p => p.DocumentInfo)
            .HasForeignKey<DocumentInfo>("PassengerId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}