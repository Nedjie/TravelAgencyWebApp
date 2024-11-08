using TravelAgencyWebApp.Common.ErrorMessages;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
    public class OfferService : IOfferService
    {
        private readonly IRepository<Offer, int> _offerRepository;

        public OfferService(IRepository<Offer, int> offerRepository)
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
            var offer = await _offerRepository.GetByIdAsync(id);
            if (offer == null)
            {
                throw new EntityNotFoundException($"Offer with ID {id} not found.");
            }

            await _offerRepository.DeleteAsync(offer);
        }
    }
}
