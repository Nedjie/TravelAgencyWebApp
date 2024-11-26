using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Common.ErrorMessages;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Offer;

namespace TravelAgencyWebApp.Services.Data
{
	public class OfferService(IRepository<Offer, int> offerRepository,
		IRepository<TravelingWay, int> travelingWayRepository) : IOfferService
	{
		private readonly IRepository<Offer, int> _offerRepository = offerRepository
			?? throw new ArgumentNullException(nameof(offerRepository));
		private readonly IRepository<TravelingWay, int> _travelingWayRepository = travelingWayRepository
			?? throw new ArgumentNullException(nameof(travelingWayRepository));

		public async Task<IEnumerable<Offer>> GetAllOffersAsync()
		{
			return await _offerRepository.GetAllIncludingAsync(o => o.TravelingWay!);
		}
		public async Task<Offer?> GetOfferByIdAsync(int id)
		{
			return await _offerRepository.GetIncludingAsync(id, o => o.TravelingWay!);
		}
		public async Task<IDictionary<TravelingWay, IEnumerable<Offer>>> GetOffersGroupedByTravelingWayAsync()
		{
			var offers = await _offerRepository.GetAllAsync();
			var travelingWays = await _travelingWayRepository.GetAllAsync();

			var groupedOffers = offers
				.GroupBy(o => travelingWays.FirstOrDefault(tw => tw.Id == o.TravelingWayId))
				.Where(g => g.Key != null)
				.ToDictionary(
					g => g.Key!,
					g => g.AsEnumerable()
				);

			return groupedOffers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		}
		public async Task AddOfferAsync(Offer model)
		{

			if (model == null)
			{
				ArgumentNullException.ThrowIfNull(model, nameof(model));
			}

			var offer = new Offer
			{
				Title = model.Title!,
				Description = model.Description ?? "No Description",
				Price = model.Price,
				ImageUrl = model.ImageUrl!,
				TravelingWay = model.TravelingWay,
				TravelingWayId = model.TravelingWayId
			};

			await _offerRepository.AddAsync(offer);

		}
		public async Task UpdateOfferAsync(Offer model)
		{
			if (model == null)
			{
				ArgumentNullException.ThrowIfNull(model, nameof(model));
			}

			var existingOffer = await _offerRepository.GetByIdAsync(model.Id);

			ArgumentNullException.ThrowIfNull(model);
			
			if (existingOffer != null)
			{
				existingOffer.Title = model.Title;
				existingOffer.Description =model.Description ?? "No Description";
				existingOffer.Price = model.Price;
				existingOffer.ImageUrl = model.ImageUrl;
				existingOffer.TravelingWayId = model.TravelingWayId; 

				await _offerRepository.UpdateAsync(existingOffer); 
			}
		}
		public async Task DeleteOfferAsync(int id)
		{
			var offer = await _offerRepository.GetByIdAsync(id);

			if (offer == null)
			{
				throw new KeyNotFoundException($"Offer with ID {id} was not found.");
			}

			await _offerRepository.DeleteAsync(offer);
		}
	}
}
