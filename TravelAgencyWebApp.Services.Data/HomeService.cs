using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{

    public class HomeService(IRepository<Offer, int> offerRepository) : IHomeService
    {
        private readonly IRepository<Offer, int> _offerRepository = offerRepository 
            ?? throw new ArgumentNullException(nameof(offerRepository));


        public async Task<IEnumerable<Offer>> GetOffersAsync()
        {
            return await _offerRepository.GetAllAsync();
        }
    }
}
