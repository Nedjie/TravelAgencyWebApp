using TravelAgencyWebApp.Common.ErrorMessages;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Offer;

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

        public async Task AddOfferAsync(OfferViewModel model)
        {

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Title))
            {
                throw new ArgumentException("Заглавието е задължително.", nameof(model.Title));
            }

            if (string.IsNullOrWhiteSpace(model.Description))
            {
                throw new ArgumentException("Описание е задължително.", nameof(model.Description));
            }

            if (model.Price <= 0)
            {
                throw new ArgumentException("Цената трябва да бъде положително число.", nameof(model.Price));
            }

            if (!string.IsNullOrWhiteSpace(model.ImageUrl) && !Uri.IsWellFormedUriString(model.ImageUrl, UriKind.Absolute))
            {
                throw new ArgumentException("Невалидна URL адрес за изображение.", nameof(model.ImageUrl));
            }

            var offer = new Offer
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl // Now validated
            };

            await _offerRepository.AddAsync(offer);

        }
        public async Task UpdateOfferAsync(Offer offer)
        {
            if (offer == null)
            {
                throw new ArgumentNullException(nameof(offer));
            }
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
