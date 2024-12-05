﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Booking;

namespace TravelAgencyWebApp.Controllers
{
    [Authorize]
	public class BookingController(IBookingService bookingService, IOfferService offerService,
		UserManager<ApplicationUser> userManager, ILogger<BookingController> logger) : BaseController(logger)
	{
		private readonly IBookingService _bookingService = bookingService
			?? throw new ArgumentNullException(nameof(bookingService));
		private readonly IOfferService _offerService = offerService
			?? throw new ArgumentNullException(nameof(offerService));
		private readonly UserManager<ApplicationUser> _userManager = userManager
			?? throw new ArgumentNullException(nameof(userManager));


		public async Task<IActionResult> Index()
		{
			try
			{
				var userIdString = _userManager.GetUserId(User);

				if (!Guid.TryParse(userIdString, out var userId))
				{
					return RedirectToAction("Login", "Account");
				}

				var bookingViewModels = await _bookingService.GetBookingsByUserIdAsync(userId, b => b.Offer!); 

				return View(bookingViewModels);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var booking = await _bookingService.GetBookingByIdAsync(id);

			if (booking == null)
			{
				return NotFound();
			}

			var offer = await _offerService.GetOfferByIdAsync(booking.OfferId);

			if (offer == null)
			{
				return NotFound();
			}

			var model = new BookingViewModel
			{
				Id = booking.Id,
				ReservedByName=booking.ReservedByName,
				OfferId = offer.Id,
				OfferTitle = offer.Title,
				CheckInDate = booking.CheckInDate,
				CheckOutDate = booking.CheckOutDate,
				UserName = booking.UserName,
				OfferImageUrl = offer.ImageUrl
			};

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Create(int id)
		{
			var offer = await _offerService.GetOfferByIdAsync(id);
			if (offer == null)
			{
				return NotFound();
			}
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				Console.WriteLine("User is not authenticated. Claims:");
				foreach (var claim in User.Claims)
				{
					Console.WriteLine($"{claim.Type}: {claim.Value}");
				}
				return Unauthorized();
			}

			var bookingViewModel = new CreateBookingViewModel
			{
				UserId = user.Id,
				OfferId = offer.Id,
				CheckInDate = offer.CheckInDate,
				CheckOutDate = offer.CheckOutDate,
				UserEmail = user.Email,
				UserFullName = user.FullName,
				UserPhoneNumber = user.PhoneNumber

			};
			ViewBag.Offer = offer;
			return View(bookingViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateBookingViewModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Offers = (await _offerService.GetAllOffersAsync())
					.Select(o => new SelectListItem
					{
						Value = o.Id.ToString(),
						Text = o.Title
					});

				return View(model);
			}
			try
			{
				bool isSuccess = await _bookingService.CreateBookingAsync(model);
				if (!isSuccess)
				{
					TempData["ErrorMessage"] = "Error occurred while creating the reservation. Please try again.";
					return View(model);
				}

				TempData["SuccessMessage"] = "Reservation created successfully!";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
				return View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var booking = await _bookingService.GetBookingByIdIncludingUserAndOfferAsync(id);
			if (booking == null)
			{
				return NotFound();
			}

			ViewBag.Offers = (await _offerService.GetAllOffersAsync())
				.Select(o => new SelectListItem
				{
					Value = o.Id.ToString(),
					Text = o.Title
				});

			var model = new EditBookingViewModel
			{
				UserName=booking.User!.FullName,	
				Id = booking.Id,
				UserId = booking.UserId,
				CheckInDate = booking.CheckInDate,
				CheckOutDate = booking.CheckOutDate,
				OfferId = booking.OfferId
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditBookingViewModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Offers = (await _offerService.GetAllOffersAsync())
					.Select(o => new SelectListItem
					{
						Value = o.Id.ToString(),
						Text = o.Title
					});

				return View(model);
			}

			await _bookingService.UpdateBookingAsync(model);

			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var booking = await _bookingService.GetBookingByIdIncludingUserAndOfferAsync(id);

			if (booking == null)
			{
				return NotFound();
			}

			var model = new ConfirmDeleteViewModel
			{
				Id = booking.Id,
				OfferTitle = booking.Offer?.Title ?? "No Offer Title", 
				UserName = booking.User?.FullName ?? "Unknown" 
			};

			return View(model);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(ConfirmDeleteViewModel model)
		{
			try
			{
				await _bookingService.DeleteBookingAsync(model.Id);
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

			return RedirectToAction(nameof(Index));
		}
	}
}

