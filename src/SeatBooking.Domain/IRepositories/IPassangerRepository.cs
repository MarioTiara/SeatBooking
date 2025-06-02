namespace SeatBooking.Domain.IRepositories;

public interface IPassengerRepository : IRepository<PassengerAggregate.Passenger, int>
{
    // Define methods for the passenger repository
    Task<PassengerAggregate.Passenger?> GetByPassengerNameNumberAsync(string passengerNameNumber);
}