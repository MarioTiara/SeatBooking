
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeatBooking.Domain.IRepositories;
using SeatBooking.Infrastructure.Persistance.DbContext;
using SeatBooking.Infrastructure.Persistance.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
        services.AddDbContext<SeatBookingDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IPassengerRepository, PassangerRepository  >();
        return services;
    }
}