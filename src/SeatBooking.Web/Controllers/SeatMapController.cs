using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeatBooking.Domain.IRepositories;
using SeatBooking.Web.Mappers;
using SeatBooking.Web.Models;
using SeatBooking.Web.Services;

namespace SeatBooking.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeatMapController : ControllerBase
{
    private readonly SeatMapService _seatMapService;

    public SeatMapController(SeatMapService seatMapService)
    {
        _seatMapService = seatMapService ?? throw new ArgumentNullException(nameof(seatMapService));
    }

    [HttpPost]
    public async Task<IActionResult> PostSeatMap([FromBody] SeatMapRootDto request)
    {
        if (request == null)
            return BadRequest(new { message = "Invalid seat map data." });

        await _seatMapService.SaveSeatMapAsync(request);
        return Ok(new { message = "Seat map saved successfully." });
    }

    [HttpGet]
    public async Task<IActionResult> GetSeatMap()
    {
        var dtos = await _seatMapService.LoadAllAsRootDtoAsync();
        return Ok(dtos);
    }

    [HttpPost("select-seat")]
    public async Task<IActionResult> SelectSeat([FromBody] SelectSeatRequestDto request)
    {
        try
        {
            if (request == null)
                return BadRequest(new { message = "Invalid selection request." });

            var success = await _seatMapService.SelectSeatAsync(request);
            if (!success)
                return BadRequest(new { message = "Failed to select seat. Please check the seat slot code and aircraft code." });

            return Ok(new { message = "Seat selected successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error.", error = ex.Message });
        }
    }
}



