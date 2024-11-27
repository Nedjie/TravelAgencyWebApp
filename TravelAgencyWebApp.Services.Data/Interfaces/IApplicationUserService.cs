using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.ViewModels.Admin.UserManagement;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
	public interface IApplicationUserService
	{
		Task<List<AllUsersViewModel>> GetAllUsersAsync();
		Task<bool> AssignUserToRoleAsync(Guid userId, string roleName);
		Task<bool> UserExistsByIdAsync(Guid userId);
		Task<bool> RemoveUserRoleAsync(Guid userId, string roleName);
		Task<bool> DeleteUserAsync(Guid userId);



	}
}
