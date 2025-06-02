namespace SeatBooking.Domain.IRepositories;

public interface IUnitOfWork
{
    IAircraftRepository AircraftRepository { get; }
    IPassengerRepository PassengerRepository { get; }
    ISegmentRepository SegmentRepository { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}