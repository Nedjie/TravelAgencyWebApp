using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Controllers
{
    public class TravelingWayController(ITravelingWayService travelingWayService,
        ILogger<TravelingWayController> logger) : BaseController(logger)
    {
        private readonly ITravelingWayService _travelingWayService = travelingWayService
            ?? throw new ArgumentNullException(nameof(travelingWayService));


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelingWay>>> GetAllTravelingWays()
        {
            var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
            return Ok(travelingWays);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TravelingWay>> GetTravelingWayById(int id)
        {
            var travelingWay = await _travelingWayService.GetTravelingWayByIdAsync(id);
            if (travelingWay == null)
            {
                //  return HandleNotFound($"Traveling way with ID: {id}");
            }
            return Ok(travelingWay);
        }

        [HttpPost]
        public async Task<ActionResult<TravelingWay>> CreateTravelingWay([FromBody] TravelingWay travelingWay)
        {
            await _travelingWayService.AddTravelingWayAsync(travelingWay);
            return CreatedAtAction(nameof(GetTravelingWayById), new { id = travelingWay.Id }, travelingWay);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravelingWay(int id, [FromBody] TravelingWay travelingWay)
        {
            if (id != travelingWay.Id)
            {
                return BadRequest();
            }

            await _travelingWayService.UpdateTravelingWayAsync(travelingWay);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelingWay(int id)
        {
            await _travelingWayService.DeleteTravelingWayAsync(id);
            return NoContent();
        }
    }
}

