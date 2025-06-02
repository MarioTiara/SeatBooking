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
            return BadRequest("Invalid seat map data.");

        await _seatMapService.SaveSeatMapAsync(request);
        return Ok();
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
                return BadRequest("Invalid selection request.");

            var seatSlot = await _seatMapService.SelectSeatAsync(request);
            return Ok(seatSlot);
        
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    
    }
}



