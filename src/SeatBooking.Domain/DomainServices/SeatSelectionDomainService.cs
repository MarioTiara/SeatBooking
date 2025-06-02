using SeatBooking.Domain.AircraftAggregate;
using SeatBooking.Domain.PassengerAggregate;

namespace SeatBooking.Domain.IDomainServices
{
    public class SeatSelectionDomainService
    {
        public bool TrySelectSeat(Aircraft aircraft, Passenger passenger, string slotCode)
        {
            if (passenger.HasSeatSelection())
            {
                var oldSlot = passenger.SeatSelection!.SeatSlot;
                oldSlot.MarkAvailable();
            }

            var slot = aircraft.Cabins
            .SelectMany(c => c.SeatRows)
            .SelectMany(r => r.SeatSlots)
            .FirstOrDefault(s => s.Code == slotCode);

            if (slot == null || slot.Available == false)
                return false;

            passenger.SetSeatSelection(slot);
            slot.MarkSelected();
            return true;
        }
    }
}