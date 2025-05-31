using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SeatBooking.Infrastructure.Persistance.DbContext;

public class SeatBookingDbContextFactory : IDesignTimeDbContextFactory<SeatBookingDbContext>
{
    public SeatBookingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SeatBookingDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SeatBooking;User Id=sa;Password=Your_password123;TrustServerCertificate=True;");

        return new SeatBookingDbContext(optionsBuilder.Options);
    }
}