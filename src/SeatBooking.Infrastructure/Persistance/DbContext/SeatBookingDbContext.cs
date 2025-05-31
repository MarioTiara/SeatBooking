using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SeatBooking.Infrastructure.Persistance.DbContext;

public class SeatBookingDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SeatBookingDbContext(DbContextOptions<SeatBookingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    // DbSet<T> properties as needed...
}







