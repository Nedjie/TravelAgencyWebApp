using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
	public interface IHomeService
	{
		Task<IEnumerable<Offer>> GetOffersAsync();
		// Add other methods as required, e.g., GetTestimonialsAsync()
	}
}
