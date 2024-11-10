using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser?> GetUserByIdAsync(int id);
        Task AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(int id);
    }
}
