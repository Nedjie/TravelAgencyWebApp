using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Controllers
{
    public class BookingController : BaseController
    {
		private readonly IBookingService _bookingService;

		public BookingController(IBookingService bookingService, ILogger<BaseController> logger)
			: base(logger)
		{
			_bookingService = bookingService;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				var bookings = await _bookingService.GetAllBookingsAsync();
				return View(bookings);
			}
			catch (Exception ex)
			{
				return HandleException(ex); // Handle exceptions
			}
		}

		public async Task<IActionResult> Details(int id)
		{
			var booking = await _bookingService.GetBookingByIdAsync(id);
			if (booking == null)
			{
				return NotFoundPage(); // Handle not found
			}

			return View(booking); // Return booking details
		}

		public async Task<IActionResult> Create(Booking booking)
		{
			await _bookingService.AddBookingAsync(booking);
			return RedirectToAction(nameof(Index)); // Redirect after creation
		}

		public async Task<IActionResult> Edit(int id)
		{
			var booking = await _bookingService.GetBookingByIdAsync(id);
			if (booking == null)
			{
				return NotFoundPage(); // Handle not found
			}

			return View(booking); // Return booking for editing
		}

		public async Task<IActionResult> Delete(int id)
		{
			await _bookingService.DeleteBookingAsync(id);
			return RedirectToAction(nameof(Index)); // Redirect after deletion
		}
	}
}

