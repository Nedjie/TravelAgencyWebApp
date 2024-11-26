using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.ViewModels.Admin.UserManagement;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IApplicationUserService
    {
        Task<List<AllUsersViewModel>> GetAllUsersAsync();
        Task<ApplicationUser?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<string>> GetRolesByUserIdAsync(Guid id);
        Task AddUserAsync(ApplicationUser user, string password);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(Guid id);
    }
}
