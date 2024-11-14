using TravelAgencyWebApp.Common.ErrorMessages;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Offer;

namespace TravelAgencyWebApp.Services.Data
{
    public class OfferService(IRepository<Offer, int> offerRepository) : IOfferService
    {
        private readonly IRepository<Offer, int> _offerRepository = offerRepository ?? throw new ArgumentNullException(nameof(offerRepository));

        public async Task<IEnumerable<Offer>> GetAllOffersAsync()
        {
            return await _offerRepository.GetAllAsync();
        }
        public async Task<Offer?> GetOfferByIdAsync(int id)
        {
            return await _offerRepository.GetByIdAsync(id);
        }
        public async Task AddOfferAsync(OfferViewModel model)
        {

            if (model == null)
            {
                ArgumentNullException.ThrowIfNull(model, nameof(model));
            }

            var offer = new Offer
            {
                Title = model.Title! ,
                Description = model.Description ?? "No Description",
                Price = model.Price,
                ImageUrl = model.ImageUrl! 
            };

            await _offerRepository.AddAsync(offer);

        }
        public async Task UpdateOfferAsync(OfferViewModel model)
        {
            if (model == null)
            {
                ArgumentNullException.ThrowIfNull(model, nameof(model));
            }

            var offer = await _offerRepository.GetByIdAsync(model.Id);

            ArgumentNullException.ThrowIfNull(offer);

            offer.Title = model.Title!;
            offer.Description = model.Description ?? "No Description";
            offer.Price = model.Price;
            offer.ImageUrl = model.ImageUrl!;

            await _offerRepository.UpdateAsync(offer);
        }
        public async Task DeleteOfferAsync(int id)
        {
            var offer = await _offerRepository.GetByIdAsync(id);

            ArgumentNullException.ThrowIfNull(offer);

            await _offerRepository.DeleteAsync(offer);
        }
    }
}
