using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Admin.UserManagement;

namespace TravelAgencyWebApp.Services.Data
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;

		public ApplicationUserService(UserManager<ApplicationUser> userManager,
			 RoleManager<IdentityRole<Guid>> roleManager)
		{
            _userManager = userManager;
			_roleManager = roleManager;
		}

        public async Task<List<AllUsersViewModel>> GetAllUsersAsync()
        {
            var allUsers = await _userManager.Users.ToArrayAsync();

            var allUsersViewModel = new List<AllUsersViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);

                allUsersViewModel.Add(new AllUsersViewModel()
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName!,
                    Email = user.Email!,
                    FullName = user.FullName,
                    Roles = roles
                });
            }
            return allUsersViewModel;

        }

		public async Task<bool> AssignUserToRoleAsync(Guid userId, string roleName)
		{
			ApplicationUser? user = await _userManager
				.FindByIdAsync(userId.ToString());
			bool roleExists = await _roleManager.RoleExistsAsync(roleName);

			if (user == null || !roleExists)
			{
				return false;
			}

			bool alreadyInRole = await _userManager.IsInRoleAsync(user, roleName);
			if (!alreadyInRole)
			{
				IdentityResult? result = await _userManager
					.AddToRoleAsync(user, roleName);

				if (!result.Succeeded)
				{
					return false;
				}
			}

			return true;
		}

		public async Task<bool> UserExistsByIdAsync(Guid userId)
		{
			ApplicationUser? user = await _userManager
				.FindByIdAsync(userId.ToString());

			return user != null;
		}

		public async Task<bool> RemoveUserRoleAsync(Guid userId, string roleName)
		{
			ApplicationUser? user = await _userManager
				.FindByIdAsync(userId.ToString());
			bool roleExists = await _roleManager.RoleExistsAsync(roleName);

			if (user == null || !roleExists)
			{
				return false;
			}

			bool alreadyInRole = await _userManager.IsInRoleAsync(user, roleName);
			if (alreadyInRole)
			{
				IdentityResult? result = await _userManager
					.RemoveFromRoleAsync(user, roleName);

				if (!result.Succeeded)
				{
					return false;
				}
			}

			return true;
		}

		public async Task<bool> DeleteUserAsync(Guid userId)
		{
			ApplicationUser? user = await _userManager
				.FindByIdAsync(userId.ToString());

			if (user == null)
			{
				return false;
			}

			IdentityResult? result = await _userManager
				.DeleteAsync(user);
			if (!result.Succeeded)
			{
				return false;
			}

			return true;
		}
	}
}
        
    
