using SeatBooking.Domain.IRepositories;
using SeatBooking.Web.Models;
using SeatBooking.Web.Mappers;
using SeatBooking.Domain.AircraftAggregate;
using SeatBooking.Domain.PassengerAggregate;
using SeatBooking.Domain.SegmentAggregate;


namespace SeatBooking.Web.Services;

public class SeatMapService
{
    private readonly IAircraftRepository _repositoryAircraft;
    private readonly IPassengerRepository   _repositoryPassenger;
    private readonly ISegmentRepository     _repositorySegment;

    public SeatMapService(IAircraftRepository repositoryAircraft, 
                          IPassengerRepository repositoryPassenger, 
                          ISegmentRepository repositorySegment)
    {
        _repositoryAircraft = repositoryAircraft ?? throw new ArgumentNullException(nameof(repositoryAircraft));
        _repositoryPassenger = repositoryPassenger ?? throw new ArgumentNullException(nameof(repositoryPassenger));
        _repositorySegment = repositorySegment ?? throw new ArgumentNullException(nameof(repositorySegment));
    }

    public async Task<bool> SaveSeatMapAsync(RootDto model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        // Map the DTO to domain entities
        var (aircraftList, passengerList, segmentList) = ExtractDomainLists(model);

        // Save the aircraft to the repository
        foreach (var aircraft in aircraftList)
        {
            await _repositoryAircraft.AddAsync(aircraft);
        }

        // Save the passengers to the repository
        foreach (var passenger in passengerList)
        {
            await _repositoryPassenger.AddAsync(passenger);
        }

        // Save the segments to the repository
        foreach (var segment in segmentList)
        {
            await _repositorySegment.AddAsync(segment);
        }

        return true;
    }

    public (List<Aircraft> AircraftList, List<Passenger> PassengerList, List<Segment> SegmentList) ExtractDomainLists(RootDto model)
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

    public async Task<RootDto> LoadAllAsRootDtoAsync()
    {
        // 1. Load all data from repositories
        var aircraftList = await _repositoryAircraft.GetAllAsync();
        var passengerList = await _repositoryPassenger.GetAllAsync();
        var segmentList = await _repositorySegment.GetAllAsync();

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

        var rootDto = new RootDto
        (
            SeatsItineraryParts: seatsItineraryParts,
            SelectedSeats: new List<object>() // or your logic
        );

        return rootDto;
    }
}