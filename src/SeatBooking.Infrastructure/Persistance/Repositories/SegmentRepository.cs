using Microsoft.EntityFrameworkCore;
using SeatBooking.Domain.IRepositories;
using SeatBooking.Domain.SegmentAggregate;
using SeatBooking.Infrastructure.Persistance.DbContext;

namespace SeatBooking.Infrastructure.Persistance.Repositories;

public class SegmentRepository : Repository<Segment, int>, ISegmentRepository
{
    public SegmentRepository(SeatBookingDbContext context) : base(context)
    {
    }

    public override async Task<List<Segment>> GetAllAsync()
    {
        return await _dbSet
            .Include(s => s.Flight)         // Include FlightInfo
            .Include(s => s.Origin)         // Include Origin Airport
            .Include(s => s.Destination)    // Include Destination Airport
            .Include(s => s.Aircraft)       // Include related Aircraft if needed
            .ToListAsync();
    }
}