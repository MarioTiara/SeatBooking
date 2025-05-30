using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Domain.PassengerAggregate;

public class PassengerSeatSelection
{
    public PassengerSeatSelection(Passenger passenger, SeatSlot seatSlot)
    {
        Passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
        SeatSlot = seatSlot ?? throw new ArgumentNullException(nameof(seatSlot));
        PassengerId = passenger.Id;
        SeatSlotId = seatSlot.Id;
    }
    protected PassengerSeatSelection()
    {
    }

    public int Id { get; private set; }

    public int PassengerId { get; private set; }
    public Passenger Passenger { get; private set; }

    public int SeatSlotId { get; private set; }
    public SeatSlot SeatSlot { get; private set; }
}

