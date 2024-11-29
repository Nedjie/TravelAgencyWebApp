using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Infrastructure.Extensions;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Booking;

namespace TravelAgencyWebApp.Controllers
{
	public class BookingController(IBookingService bookingService, IOfferService offerService,
        UserManager<ApplicationUser> userManager,ILogger<BookingController> logger) : BaseController(logger)
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
                var bookingViewModels = await _bookingService.GetAllBookingsAsync();
                return View(bookingViewModels);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFoundPage();
            }

            return View(booking);
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
				return Unauthorized(); 
			}

			var bookingViewModel = new CreateBookingViewModel
            {
                UserId =user.Id,
                OfferId = offer.Id,
                CheckInDate = DateTime.Now, 
                CheckOutDate = DateTime.Now.AddDays(1).Date, 
                UserEmail=user.Email,
                UserFullName=user.FullName,
                UserPhoneNumber=user.PhoneNumber
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
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound(); // Return 404 if the booking does not exist
            }

            ViewBag.Offers = (await _offerService.GetAllOffersAsync())
                .Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.Title
                });

            var model = new EditBookingViewModel
            {
                Id = booking.Id,
                UserId = Guid.Parse(booking.UserId!),
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
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound(); // make view special for 404
            }
            var model = new ConfirmDeleteViewModel
            {
                Id = booking.Id,
                UserName = booking.UserName ?? "Unknown",
                OfferTitle = booking.OfferTitle ?? "No Offer"
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

