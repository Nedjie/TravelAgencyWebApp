using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<IEnumerable<Offer>> GetAllOffersAsync()
        {
            return await _offerRepository.GetAllAsync();
        }

        public async Task<Offer?> GetOfferByIdAsync(int id)
        {
            return await _offerRepository.GetByIdAsync(id);
        }

        public async Task AddOfferAsync(Offer offer)
        {
            await _offerRepository.AddAsync(offer);
        }

        public async Task UpdateOfferAsync(Offer offer)
        {
            await _offerRepository.UpdateAsync(offer);
        }

        public async Task DeleteOfferAsync(int id)
        {
            await _offerRepository.DeleteAsync(id);
        }
    }
}
