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

        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
            IEnumerable<ApplicationUser> allUsers = await _userManager.Users.ToArrayAsync();

            ICollection<AllUsersViewModel> allUsersViewModel = new List<AllUsersViewModel>();

            foreach (ApplicationUser user in allUsers)
            {
                IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

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

        public async Task AddUserAsync(ApplicationUser user, string password)
        {
            {
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        public async Task<IEnumerable<string>> GetRolesByUserIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }

}