using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{

    public class HomeService(IRepository<Offer, int> offerRepository,
		IRepository<TravelingWay,int>travelingWayRepository) : IHomeService
    {
        private readonly IRepository<Offer, int> _offerRepository = offerRepository 
            ?? throw new ArgumentNullException(nameof(offerRepository));
		private readonly IRepository<TravelingWay,int>_travelingWayRepository=travelingWayRepository
			   ?? throw new ArgumentNullException(nameof(travelingWayRepository));

		public async Task<IEnumerable<Offer>> GetOffersAsync()
        {
            return await _offerRepository.GetAllAsync();
        }

        public async Task<IDictionary<TravelingWay, IEnumerable<Offer>>> GetOffersGroupedByTravelingWayAsync()
        {
            var offers = await _offerRepository.GetAllAsync();
            var travelingWays = await _travelingWayRepository.GetAllAsync();

            return offers
                .Where(o => o.TravelingWayId != 0)
                .GroupBy(o => travelingWays.FirstOrDefault(t => t.Id == o.TravelingWayId)!)
                .ToDictionary(g => g.Key!, g => g.AsEnumerable());
        }
    }
}
