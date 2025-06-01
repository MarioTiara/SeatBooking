using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeatBooking.Domain.IRepositories;
using SeatBooking.Web.Mappers;
using SeatBooking.Web.Models;

namespace SeatBooking.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeatMapController : ControllerBase
{
    private readonly IPassengerRepository repository;
    public SeatMapController(IPassengerRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    [HttpPost]
    public async Task<IActionResult> PostSeatMap([FromBody] RootDto model)
    {
        if (model == null)
            return BadRequest("Invalid seat map data.");

        // You can process, map, or save the model here as needed.
        // For now, just return success with the received data.
        var passanger = model.SeatsItineraryParts.First().SegmentSeatMaps.First().PassengerSeatMaps.First().Passenger;
        var domainPassenger = passanger.ToDomain();
        await repository.AddAsync(domainPassenger);
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        var json = JsonSerializer.Serialize(passanger, options);
        return Ok(json);
    }
}

