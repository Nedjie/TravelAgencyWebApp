using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Controllers
{
	public class TravelingWayController : BaseController
	{
		private readonly ITravelingWayService _travelingWayService;

		public TravelingWayController(ITravelingWayService travelingWayService,
									  ILogger<TravelingWayController> logger)
			: base(logger)
		{
			_travelingWayService = travelingWayService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TravelingWay>>> GetAllTravelingWays()
		{
			var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
			return Ok(travelingWays);
		}

		[HttpGet("travelingway/details/{id:int}")]
		public async Task<ActionResult<TravelingWay>> GetTravelingWayById(int id)
		{
			var travelingWay = await _travelingWayService.GetTravelingWayByIdAsync(id);
			if (travelingWay == null)
			{
				return BadRequest();
			}
			return Ok(travelingWay);
		}

		[HttpPost]
		public async Task<ActionResult<TravelingWay>> CreateTravelingWay([FromBody] TravelingWay travelingWay)
		{
			await _travelingWayService.AddTravelingWayAsync(travelingWay);
			return CreatedAtAction(nameof(GetTravelingWayById), new { id = travelingWay.Id }, travelingWay);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTravelingWay(int id)
		{
			await _travelingWayService.DeleteTravelingWayAsync(id);
			return NoContent();
		}
	}
}

