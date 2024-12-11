﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Areas.Identity.Pages.Account
{
	public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null!)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
			if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
			{
				return LocalRedirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home", new { area = "" });
			}
		}
    }
}
