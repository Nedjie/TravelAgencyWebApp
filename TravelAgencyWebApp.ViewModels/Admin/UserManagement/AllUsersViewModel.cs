namespace TravelAgencyWebApp.ViewModels.Admin.UserManagement
{
    public class AllUsersViewModel
    {
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
