using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
	public interface IHomeService
	{
        /// <summary>
        /// Asynchronously retrieves a list of offers.
        /// </summary>
        Task<IEnumerable<Offer>> GetOffersAsync();

    }
}
