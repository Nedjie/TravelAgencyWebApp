using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Offer;

namespace TravelAgencyWebApp.Controllers
{
    public class OfferController : BaseController
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService, ILogger<BaseController> logger)
                : base(logger)
        {
            _offerService = offerService;
        }

       
        public async Task<IActionResult> Index()
        {
            var offers = await _offerService.GetAllOffersAsync();
            return View(offers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(model);
            }

            await _offerService.AddOfferAsync(model);
            return RedirectToAction(nameof(Index));// dont work redirect !!!!!
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound(); // Return 404 if the offer does not exist
            }

            return View(offer);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound(); // Return 404 if the offer does not exist
            }

            var model = new OfferViewModel
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                Price = offer.Price,
                ImageUrl = offer.ImageUrl 
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Log ModelState issues for debugging
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return View(model); 
            }

            await _offerService.UpdateOfferAsync(model); 
            return RedirectToAction(nameof(Index)); // dont redirect me to INDEX!!!!
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound(); // Return 404 if the offer does not exist
            }

            var model = new ConfirmDeleteOfferViewModel
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _offerService.DeleteOfferAsync(id); // Call the service to delete the offer
            return RedirectToAction(nameof(Index)); 
        }
    }
}

