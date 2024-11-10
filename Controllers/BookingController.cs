using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Infrastructure.Extensions;
using TravelAgencyWebApp.Services.Data;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Booking;

namespace TravelAgencyWebApp.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IOfferService _offerService;

        public BookingController(IBookingService bookingService, IOfferService offerService, ILogger<BaseController> logger)
            : base(logger)
        {
            _bookingService = bookingService;
            _offerService = offerService;
        }

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
        public IActionResult Create()
        {
            ViewBag.Offers = _offerService.GetAllOffersAsync().Result
                .Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.Title
                });

            return View();
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

            model.UserId =User.GetCurrentUserId();

            await _bookingService.AddBookingAsync(model); 
            return RedirectToAction(nameof(Index)); 
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
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound(); // make view special for 404
            }
            var model = new ConfirmDeleteViewModel
            {
                Id = booking.Id,
                UserName = booking.UserName != null ? booking.UserName : "Unknown",
                OfferTitle = booking.OfferTitle != null ? booking.OfferTitle : "No Offer"
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

