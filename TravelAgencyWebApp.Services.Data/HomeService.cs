using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
	public class HomeService : IHomeService
	{
		private readonly IRepository<Offer, int> _offerRepository;

		public HomeService(IRepository<Offer, int> offerRepository)
		{
			_offerRepository = offerRepository;
		}

		public async Task<IEnumerable<Offer>> GetOffersAsync()
		{
			return await _offerRepository.GetAllAsync();
		}
	}
}
