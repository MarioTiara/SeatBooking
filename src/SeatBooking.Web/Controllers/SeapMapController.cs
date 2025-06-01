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
    public async Task<IActionResult> PostSeatMap([FromBody] RootDto model)
    {
        if (model == null)
            return BadRequest("Invalid seat map data.");

        // You can process, map, or save the model here as needed.
        // For now, just return success with the received data.

        await _seatMapService.SaveSeatMapAsync(model);
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GetSeatMap()
    {
        var dtos = await _seatMapService.LoadAllAsRootDtoAsync();

        return Ok(dtos);
    }
}



