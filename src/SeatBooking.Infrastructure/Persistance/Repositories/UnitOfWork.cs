using Microsoft.EntityFrameworkCore.Storage;
using SeatBooking.Domain.IRepositories;
using SeatBooking.Infrastructure.Persistance.DbContext;

namespace SeatBooking.Infrastructure.Persistance.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly SeatBookingDbContext _context;
    private readonly IAircraftRepository _aircraftRepository;
    private readonly IPassengerRepository _passengerRepository;
    private readonly ISegmentRepository _segmentRepository;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(SeatBookingDbContext context, 
                      IAircraftRepository aircraftRepository,
                      IPassengerRepository passengerRepository,
                      ISegmentRepository segmentRepository)
    {
        _context = context;
        _aircraftRepository = aircraftRepository;
        _passengerRepository = passengerRepository;
        _segmentRepository = segmentRepository;
    }

    public IAircraftRepository AircraftRepository => _aircraftRepository;

    public IPassengerRepository PassengerRepository => _passengerRepository;

    public ISegmentRepository SegmentRepository => _segmentRepository;

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
            _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}