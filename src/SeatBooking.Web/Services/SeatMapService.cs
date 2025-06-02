using SeatBooking.Domain.IRepositories;
using SeatBooking.Web.Models;
using SeatBooking.Web.Mappers;
using SeatBooking.Domain.AircraftAggregate;
using SeatBooking.Domain.PassengerAggregate;
using SeatBooking.Domain.SegmentAggregate;
using SeatBooking.Domain.IDomainServices;


namespace SeatBooking.Web.Services;

public class SeatMapService
{
    private readonly IUnitOfWork unitOfWork;

    public SeatMapService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> SaveSeatMapAsync(SeatMapRootDto model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        // Map the DTO to domain entities
        var (aircraftList, passengerList, segmentList) = ExtractDomainLists(model);

        // Save the aircraft to the repository
        foreach (var aircraft in aircraftList)
        {
            await unitOfWork.AircraftRepository.AddAsync(aircraft);
        }

        // Save the passengers to the repository
        foreach (var passenger in passengerList)
        {
            await unitOfWork.PassengerRepository.AddAsync(passenger);
        }

        // Save the segments to the repository
        foreach (var segment in segmentList)
        {
            await unitOfWork.SegmentRepository.AddAsync(segment);
        }

        return true;
    }

    private (List<Aircraft> AircraftList, List<Passenger> PassengerList, List<Segment> SegmentList) ExtractDomainLists(SeatMapRootDto model)
    {
        var aircraftList = new List<Aircraft>();
        var passengerList = new List<Passenger>();
        var segmentList = new List<Segment>();

        if (model?.SeatsItineraryParts != null)
        {
            foreach (var itinerary in model.SeatsItineraryParts)
            {
                // Segments
                if (itinerary.SegmentSeatMaps != null)
                {
                    foreach (var segmentSeatMap in itinerary.SegmentSeatMaps)
                    {
                        // Segment
                        if (segmentSeatMap.Segment != null)
                            segmentList.Add(segmentSeatMap.Segment.ToDomain());

                        // Passengers & Aircraft
                        if (segmentSeatMap.PassengerSeatMaps != null)
                        {
                            foreach (var paxSeatMap in segmentSeatMap.PassengerSeatMaps)
                            {
                                if (paxSeatMap.Passenger != null)
                                    passengerList.Add(paxSeatMap.Passenger.ToDomain());

                                if (paxSeatMap.SeatMap != null)
                                    aircraftList.Add(paxSeatMap.SeatMap.ToDomain());
                            }
                        }
                    }
                }
            }
        }

        return (aircraftList, passengerList, segmentList);
    }

    public async Task<SeatMapRootDto> LoadAllAsRootDtoAsync()
    {
        // 1. Load all data from repositories
        var aircraftList = await unitOfWork.AircraftRepository.GetAllAsync();
        var passengerList = await unitOfWork.PassengerRepository.GetAllAsync();
        var segmentList = await unitOfWork.SegmentRepository.GetAllAsync();

        // 2. Map domain entities to DTOs
        var aircraftDtos = aircraftList.Select(a => a.ToDto()).ToList();
        var passengerDtos = passengerList.Select(p => p.ToDto()).ToList();
        var segmentDtos = segmentList.Select(s => s.ToDto()).ToList();

        // 3. Compose RootDto with correct matching logic
        var seatsItineraryParts = new List<SeatsItineraryPartDto>
        {
            new SeatsItineraryPartDto(
                SegmentSeatMaps: segmentDtos.Select(segDto =>
                {
                    // Try to find the matching aircraft for this segment by Equipment/Code
                    var matchingAircraft = aircraftDtos.FirstOrDefault(a => a.Aircraft == segDto.Equipment);

                    // Build passenger seat maps for this segment
                    var passengerSeatMaps = passengerDtos.Select(paxDto => new PassengerSeatMapDto
                    (
                        SeatSelectionEnabledForPax: true, // or your logic
                        SeatMap: matchingAircraft,
                        Passenger: paxDto
                    )).ToList();

                    return new SegmentSeatMapDto
                    (
                        PassengerSeatMaps: passengerSeatMaps,
                        Segment: segDto
                    );
                }).ToList()
            )
        };

        var rootDto = new SeatMapRootDto
        (
            SeatsItineraryParts: seatsItineraryParts,
            SelectedSeats: passengerList.Select(p=>p.SeatSelection.SeatSlot.ToDto()).ToList()
        );

        return rootDto;
    }

    public async Task<bool> SelectSeatAsync(SelectSeatRequestDto request)
    {
        var seatSelectionService = new SeatSelectionDomainService();
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var passenger = await unitOfWork.PassengerRepository.GetByPassengerNameNumberAsync(request.PassengerNameNumber);
            
            if (passenger == null)
                return false;
            var aircraft = await unitOfWork.AircraftRepository.GetByIdAsync(request.AircraftCode);
            if (aircraft == null)
            {
                return false;
            }

            var success = seatSelectionService.TrySelectSeat(
                aircraft,
                passenger,
                request.SeatSlotCode
            );
            if (!success)
            {
                await unitOfWork.RollbackAsync();
                return false;
            }

            await unitOfWork.PassengerRepository.UpdateAsync(passenger);
            await unitOfWork.AircraftRepository.UpdateAsync(aircraft);
            await unitOfWork.PassengerRepository.SaveChangesAsync();
            await unitOfWork.AircraftRepository.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            return true;

        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }

        
    }
}