using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.ViewModels.Admin
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<Data.Models.Offer> Offers { get; set; } = new List<Data.Models.Offer>();
        public IEnumerable<ApplicationUser> Users { get; set; }= new List<ApplicationUser>();
    }
}
