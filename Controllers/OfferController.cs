using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Offer;
using static TravelAgencyWebApp.Common.ApplicationConstants;

namespace TravelAgencyWebApp.Controllers
{
    public class OfferController(IOfferService offerService, ITravelingWayService travelingWayService
        , ILogger<OfferController> logger)
        : BaseController(logger)
    {
        private readonly IOfferService _offerService = offerService
            ?? throw new ArgumentNullException(nameof(offerService));
        private readonly ITravelingWayService _travelingWayService = travelingWayService
            ?? throw new ArgumentNullException(nameof(travelingWayService));

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var offers = await _offerService.GetAllOffersAsync();
            return View(offers.Select(offer => new OfferViewModel
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                Price = offer.Price,
                ImageUrl = offer.ImageUrl,
                TravelingWayMethod = offer.TravelingWay?.Method,
                CheckInDate=offer.CheckInDate,
                CheckOutDate=offer.CheckOutDate
            }));
        }

        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> Create()
        {
            var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
            ViewBag.TravelingWays = travelingWays.Select(tw => new SelectListItem
            {
                Value = tw.Method,
                Text = tw.Method
            }).ToList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> Create(OfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
                ViewBag.TravelingWays = travelingWays.Select(tw => new SelectListItem
                {
                    Value = tw.Method,
                    Text = tw.Method
                }).ToList();
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.TravelingWayMethod))
            {
                ModelState.AddModelError("TravelingWayMethod", "Please select a traveling way method.");
                return View(model);
            }

            var travelingWay = await _travelingWayService.GetByMethodAsync(model.TravelingWayMethod);
            if (travelingWay == null)
            {
                ModelState.AddModelError("TravelingWayMethod", "The selected traveling way method is invalid.");

                var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
                ViewBag.TravelingWays = travelingWays.Select(tw => new SelectListItem
                {
                    Value = tw.Method,
                    Text = tw.Method
                }).ToList();

                return View(model);
            }
            Console.WriteLine($"Creating offer with title: {model.Title}, Check-In: {model.CheckInDate}, Check-Out: {model.CheckOutDate}");
            var offer = new Data.Models.Offer
            {
                Title = model.Title ?? "No Title",
                Description = model.Description ?? "No Description",
                Price = model.Price,
                ImageUrl = model.ImageUrl ?? "default-image-url.png",
                TravelingWayId = travelingWay.Id,
                CheckInDate=model.CheckInDate,
                CheckOutDate=model.CheckOutDate
            };

            await _offerService.AddOfferAsync(offer);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet("offer/details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            var model = new OfferViewModel
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                Price = offer.Price,
                ImageUrl = offer.ImageUrl,
                TravelingWayMethod = offer.TravelingWay?.Method,
                CheckInDate=offer.CheckInDate,
                CheckOutDate=offer.CheckOutDate
            };

            return View(model);
        }

        [Authorize(Roles = AdminRoleName)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
         
            var model = new OfferViewModel
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                Price = offer.Price,
                ImageUrl = offer.ImageUrl,
                TravelingWayMethod = offer.TravelingWay?.Method,
                CheckInDate=offer.CheckInDate,
                CheckOutDate=offer.CheckOutDate

            };

            var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
            ViewBag.TravelingWays = travelingWays.Select(tw => new SelectListItem
            {
                Value = tw.Method,
                Text = tw.Method,
                Selected = tw.Method.Equals(model.TravelingWayMethod, StringComparison.OrdinalIgnoreCase)
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = AdminRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(OfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
                ViewBag.TravelingWays = travelingWays.Select(tw => new SelectListItem
                {
                    Value = tw.Method,
                    Text = tw.Method
                }).ToList();

                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.TravelingWayMethod))
            {
                ModelState.AddModelError("TravelingWayMethod", "Please select a traveling way method.");
                var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
                ViewBag.TravelingWays = travelingWays.Select(tw => new SelectListItem
                {
                    Value = tw.Method,
                    Text = tw.Method
                }).ToList();
                return View(model);
            }

            var travelingWay = await _travelingWayService.GetByMethodAsync(model.TravelingWayMethod);
            if (travelingWay == null)
            {
                ModelState.AddModelError("TravelingWayMethod", "The selected traveling way method is invalid.");
                var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
                ViewBag.TravelingWays = travelingWays.Select(tw => new SelectListItem
                {
                    Value = tw.Method,
                    Text = tw.Method
                }).ToList();
                return View(model);
            }

            var offerToUpdate = new Data.Models.Offer
            {
                Id = model.Id,
                Title = model.Title ?? "No Title",
                Description = model.Description ?? "No Description",
                Price = model.Price,
                ImageUrl = model.ImageUrl ?? "default-image-url.png",
                TravelingWayId = travelingWay.Id,
                CheckInDate=model.CheckInDate,
                CheckOutDate=model.CheckOutDate
            };

            await _offerService.UpdateOfferAsync(offerToUpdate);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = AdminRoleName)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            var model = new ConfirmDeleteOfferViewModel
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description
            };

            return View(model);
        }

        [Authorize(Roles = AdminRoleName)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(ConfirmDeleteOfferViewModel model)
        {
            try
            {
                await _offerService.DeleteOfferAsync(model.Id);
                TempData["SuccessMessage"] = "Offer has been marked as deleted successfully.";
            }
            catch (KeyNotFoundException)
            {
                TempData["ErrorMessage"] = "The offer could not be found.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }

            return RedirectToAction(nameof(Index)); // Redirect back to your index or relevant page
        }

    }
}

