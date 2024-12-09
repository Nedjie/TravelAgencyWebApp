using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.ApplicationUser;
using TravelAgencyWebApp.ViewModels.Booking;

namespace TravelAgencyWebApp.Controllers
{
    [Authorize]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IOfferService _offerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _applicationUserService;

        public BookingController(IBookingService bookingService, IOfferService offerService,
                                 UserManager<ApplicationUser> userManager,
                                 IApplicationUserService applicationUserService,
                                 ILogger<BookingController> logger) : base(logger)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _applicationUserService = applicationUserService ?? throw new ArgumentNullException(nameof(applicationUserService));
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var userIdString = _userManager.GetUserId(User);

                if (!Guid.TryParse(userIdString, out var userId))
                {
                    return RedirectToAction("Login", "Account");
                }
                var user = await _userManager.FindByIdAsync(userIdString);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                var userRoles = await _userManager.GetRolesAsync(user);

                IEnumerable<BookingViewModel> bookingViewModels;

                if (userRoles.Contains("Admin"))
                {
                    bookingViewModels = await _bookingService.GetAllBookingsAsync();
                }
                else if (userRoles.Contains("Agent"))
                {
                    bookingViewModels = await _bookingService.GetBookingsForAgentAsync(userId);
                }
                else
                {
                    bookingViewModels = await _bookingService.GetBookingsByUserIdAsync(userId);
                }

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
                ReservedByName = booking.ReservedByName,
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
                TempData["ErrorMessage"] = "Offer not found.";
                return RedirectToAction("Index");
            }

            await PopulateViewBagsAsync();

            var bookingViewModel = new CreateBookingViewModel
            {
                OfferId = offer.Id,
                CheckInDate = offer.CheckInDate,
                CheckOutDate = offer.CheckOutDate,

            };

            return View(bookingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateViewBagsAsync();
                return View(model);
            }

            try
            {
                var agentIdString = _userManager.GetUserId(User);
                if (!Guid.TryParse(agentIdString, out Guid agentId))
                {
                    ModelState.AddModelError("AgentId", "Invalid Agent ID.");
                    await PopulateViewBagsAsync();
                    return View(model);
                }

                model.AgentId = agentId.ToString();

                // Custom Validation: Ensure UserId or Name and Email
                if (!model.UserId.HasValue)
                {
                    if (string.IsNullOrWhiteSpace(model.UserFullName))
                    {
                        ModelState.AddModelError(nameof(model.UserFullName), "Full Name is required for unregistered users.");
                    }
                    if (string.IsNullOrWhiteSpace(model.UserEmail))
                    {
                        ModelState.AddModelError(nameof(model.UserEmail), "Email is required for unregistered users.");
                    }
                    if (!ModelState.IsValid)
                    {
                        await PopulateViewBagsAsync();
                        return View(model);
                    }
                }

                // Proceed with creating the booking
                bool isSuccess = await _bookingService.CreateBookingAsync(model);
                if (!isSuccess)
                {
                    TempData["ErrorMessage"] = "An error occurred while creating the booking. Please try again.";
                    return View(model);
                }

                TempData["SuccessMessage"] = "Booking created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An unexpected error occurred: {ex.Message}");
                await PopulateViewBagsAsync();
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
                FullName=booking.FullName,
                Email=booking.Email,
                Id = booking.Id,
                AgentId = booking.AgentId,
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


        private async Task PopulateViewBagsAsync()
        {
            try
            {
                var offers = await _offerService.GetAllOffersAsync();
                ViewBag.Offers = offers?.Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = o.Title
                }).ToList() ?? new List<SelectListItem>();

                var registeredUsers = await _applicationUserService.GetAllRegisteredUsersAsync();
                ViewBag.RegisteredUsers = registeredUsers?.Select(u => new RegisteredUserViewModel
                {
                    Id = u.Id,
                    FullName = u.FullName
                }).ToList() ?? new List<RegisteredUserViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error populating ViewBags: {ex.Message}");

                ViewBag.Offers = new List<SelectListItem>();
                ViewBag.RegisteredUsers = new List<RegisteredUserViewModel>();
            }
        }
    }
}

