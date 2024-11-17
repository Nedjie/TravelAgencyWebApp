using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Infrastructure.Extensions;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Offer;

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
				TravelingWayMethod = offer.TravelingWay?.Method 
			}));
		}

		[HttpGet]
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

			var offer = new Offer
			{
				Title = model.Title ?? "No Title",
				Description = model.Description ?? "No Description",
				Price = model.Price,
				ImageUrl = model.ImageUrl ?? "default-image-url.png",
				TravelingWayId = travelingWay.Id
			};

			await _offerService.AddOfferAsync(offer);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Details(int id)
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
				ImageUrl = offer.ImageUrl,
				TravelingWayMethod = offer.TravelingWay?.Method 
			};

			return View(model);
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
				ImageUrl = offer.ImageUrl,
				TravelingWayMethod = offer.TravelingWay?.Method 
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

		[HttpPost]
		public async Task<IActionResult> Edit(OfferViewModel model)
		{
			if (!ModelState.IsValid)
			{
				var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
				ViewBag.TravelingWays = travelingWays.Select(tw => new SelectListItem
				{
					Value = tw.Method,
					Text = tw.Method
				}).ToList();
				return View(model); // Return with validation errors
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
		
			var offerToUpdate = new Offer
			{
				Title = model.Title ?? "No Title",
				Description = model.Description ?? "No Description",
				Price = model.Price,
				ImageUrl = model.ImageUrl ?? "default-image-url.png",
				TravelingWayId = travelingWay.Id
			};

			_logger.LogInformation("Updating Offer with ID: {Id}, Title: {Title}", offerToUpdate.Id, offerToUpdate.Title);
			
			await _offerService.UpdateOfferAsync(offerToUpdate);
			return RedirectToAction(nameof(Index));
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
			await _offerService.DeleteOfferAsync(id); 
			return RedirectToAction(nameof(Index)); 
		}
	}
}

