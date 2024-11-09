using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
	public interface IHomeService
	{
		Task<IEnumerable<Offer>> GetOffersAsync();
		
	}
}
