using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
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

        [HttpGet]
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
            return RedirectToAction("Index","Offer");
        }

        // GET: Offer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound(); // Return 404 if the offer does not exist
            }

            // Map the offer to the view model
            var model = new OfferViewModel
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                Price = offer.Price,
                ImageUrl = offer.ImageUrl // Map image URL if necessary
            };

            return View(model); // Return the view with the model for editing
        }

        // POST: Offer/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(OfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return to the view if the model state is invalid
            }

            // Map updated data to the entity
            var offer = new Offer
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl // If image URL is changed
            };

            await _offerService.UpdateOfferAsync(offer); // Call the service to update the offer
            return RedirectToAction(nameof(Index)); // Redirect to the index after updating
        }

        // GET: Offer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound(); // Return 404 if the offer does not exist
            }

            // Create a view model for confirmation, if needed
            return View(offer); // Optionally, you could return a specific delete view model
        }

        // POST: Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _offerService.DeleteOfferAsync(id); // Call the service to delete the offer
            return RedirectToAction(nameof(Index)); // Redirect to the index after deletion
        }
    }
}

