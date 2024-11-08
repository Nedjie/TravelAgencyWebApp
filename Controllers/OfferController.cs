using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Controllers
{
    [Route("api/offers")]
    public class OfferController : BaseController
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService, ILogger<BaseController> logger)
            : base(logger)
        {
            _offerService = offerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offer>>> GetAllOffers()
        {
            var offers = await _offerService.GetAllOffersAsync();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetOfferById(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                //  return HandleNotFound($"Offer with ID: {id}");
            }
            return Ok(offer);
        }

        [HttpPost]
        public async Task<ActionResult<Offer>> CreateOffer([FromBody] Offer offer)
        {
            await _offerService.AddOfferAsync(offer);
            return CreatedAtAction(nameof(GetOfferById), new { id = offer.Id }, offer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer(int id, [FromBody] Offer offer)
        {
            if (id != offer.Id)
            {
                return BadRequest();
            }

            await _offerService.UpdateOfferAsync(offer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            await _offerService.DeleteOfferAsync(id);
            return NoContent();
        }
    }
}

