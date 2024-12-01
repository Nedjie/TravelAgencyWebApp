namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IRoleService
    {
        Task<bool> RoleExistsAsync(string roleName);
		Task<IEnumerable<string>> GetAllRoleNamesAsync();
	}
}
