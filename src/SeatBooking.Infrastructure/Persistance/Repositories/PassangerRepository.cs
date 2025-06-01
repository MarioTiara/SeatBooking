using SeatBooking.Domain.IRepositories;
using SeatBooking.Domain.PassengerAggregate;
using SeatBooking.Infrastructure.Persistance.DbContext;

namespace SeatBooking.Infrastructure.Persistance.Repositories;

public class PassangerRepository : Repository<Domain.PassengerAggregate.Passenger, int>, IPassengerRepository
{
    public PassangerRepository(SeatBookingDbContext context) : base(context)
    {
    }

    public override async Task AddAsync(Passenger entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync(); // Now passenger.Id is set

        // 3. Set PassengerId on SpecialRequests and SpecialServiceRequestRemarks
        foreach (var req in entity.SpecialPreferences.SpecialRequests)
            req.PassengerId = entity.Id;

        foreach (var remark in entity.SpecialPreferences.SpecialServiceRequestRemarks)
            remark.PassengerId = entity.Id;

        // 4. Save SpecialPreferences (if not cascaded)
        await _context.SaveChangesAsync();
    }

    // You can add any specific methods for the passenger repository here if needed
}