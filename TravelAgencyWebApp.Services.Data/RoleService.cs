using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
    public class RoleService:IRoleService
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleService(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName); 
        }
		public async Task<IEnumerable<string>> GetAllRoleNamesAsync()
		{
			return await _roleManager.Roles.Select(r => r.Name!).ToListAsync();
		}

	}
}
