using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
    public class ApplicationUserService : IApplicationUserService
    {
        public Task AddUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
