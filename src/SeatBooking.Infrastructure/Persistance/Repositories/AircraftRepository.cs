using Microsoft.EntityFrameworkCore;
using SeatBooking.Domain.AircraftAggregate;
using SeatBooking.Domain.IRepositories;
using SeatBooking.Infrastructure.Persistance.DbContext;
using SeatBooking.Infrastructure.Persistance.Repositories;

namespace SeatBooking.Infrastructure.Persistance;

public class AircraftRepository : Repository<Domain.AircraftAggregate.Aircraft, string>, IAircraftRepository
{
    public AircraftRepository(SeatBookingDbContext context) : base(context)
    {
    }

    public async override Task<List<Aircraft>> GetAllAsync()
    {
        return await _dbSet
        .Include(a => a.Cabins)
            .ThenInclude(c => c.SeatRows)
                .ThenInclude(r => r.SeatSlots)
                    .ThenInclude(s => s.PriceAlternatives)
                        .ThenInclude(pa => pa.Components)
        .Include(a => a.Cabins)
            .ThenInclude(c => c.SeatRows)
                .ThenInclude(r => r.SeatSlots)
                    .ThenInclude(s => s.Taxes)
                        .ThenInclude(t => t.Components)
        .Include(a => a.Cabins)
            .ThenInclude(c => c.SeatRows)
                .ThenInclude(r => r.SeatSlots)
                    .ThenInclude(s => s.SeatCharacteristics)
        .Include(a => a.Cabins)
            .ThenInclude(c => c.SeatRows)
                .ThenInclude(r => r.SeatSlots)
                    .ThenInclude(s => s.Designations)
        .Include(a => a.Cabins)
            .ThenInclude(c => c.SeatRows)
                .ThenInclude(r => r.SeatSlots)
                    .ThenInclude(s => s.Limitations)
        .Include(a => a.Cabins)
            .ThenInclude(c => c.SeatRows)
                .ThenInclude(r => r.SeatSlots)
                    .ThenInclude(s => s.SeatSelections)
        .Include(a => a.Cabins)
            .ThenInclude(c => c.SeatColumns)
        .ToListAsync();
    }


}