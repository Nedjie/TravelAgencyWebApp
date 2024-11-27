using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Offer;
using static TravelAgencyWebApp.Common.ApplicationConstants;

namespace TravelAgencyWebApp.Areas.Admin.Controllers
{
	[Area(AdminRoleName)]
	[Authorize(Roles = AdminRoleName)]
	public class HomeController : Controller
	{
		private readonly IOfferService _offerService;
		private readonly IApplicationUserService _userService;
		private readonly IReviewService _reviewService;

		public HomeController(IOfferService offerService,
			IApplicationUserService userService,
			IReviewService reviewService)
		{
			_offerService = offerService;
			_userService = userService;
			_reviewService = reviewService;

		}

		public async Task<IActionResult> Index()
		{
			var offers = await _offerService.GetAllOffersAsync();
			var users = await _userService.GetAllUsersAsync();
			var reviews = await _reviewService.GetAllReviewsAsync();
			ViewBag.Users = users;
			ViewBag.Reviews = reviews;
			return View(offers);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRole(string userId, string role)
		{
			Guid userGuid = Guid.Empty;
			if (!this.IsGuidValid(userId, ref userGuid))
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool userExists = await _userService
				.UserExistsByIdAsync(userGuid);
			if (!userExists)
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool assignResult = await _userService
				.AssignUserToRoleAsync(userGuid, role);
			if (!assignResult)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRole(string userId, string role)
		{
			Guid userGuid = Guid.Empty;
			if (!this.IsGuidValid(userId, ref userGuid))
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool userExists = await _userService
				.UserExistsByIdAsync(userGuid);
			if (!userExists)
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool removeResult = await _userService
				.RemoveUserRoleAsync(userGuid, role);
			if (!removeResult)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			Guid userGuid = Guid.Empty;
			if (!this.IsGuidValid(userId, ref userGuid))
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool userExists = await _userService
				.UserExistsByIdAsync(userGuid);
			if (!userExists)
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool removeResult = await _userService
				.DeleteUserAsync(userGuid);
			if (!removeResult)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.RedirectToAction(nameof(Index));
		}

		public bool IsGuidValid(string? id, ref Guid parsedGuid)
		{
			// Non-existing parameter in the URL
			if (String.IsNullOrWhiteSpace(id))
			{
				return false;
			}

			// Invalid parameter in the URL
			bool isGuidValid = Guid.TryParse(id, out parsedGuid);
			if (!isGuidValid)
			{
				return false;
			}

			return true;
		}
	}
}

