using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
	public class OfferService : IOfferService
	{
		private readonly IRepository<Offer, int> _offerRepository;
		private readonly IRepository<TravelingWay, int> _travelingWayRepository;

		public OfferService(
			IRepository<Offer, int> offerRepository,
			IRepository<TravelingWay, int> travelingWayRepository)
		{
			_offerRepository = offerRepository;
			_travelingWayRepository = travelingWayRepository;
		}

		public async Task<IEnumerable<Offer>> GetAllOffersAsync()
		{
			var offers = await _offerRepository.GetAllIncludingAsync(o => o.TravelingWay!);
			return offers.Where(offer => !offer.IsDeleted).ToList();
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
		public async Task<(IEnumerable<Offer>, int)> GetFilteredOffersAsync(string searchItem, string selectedTravelingWay, int pageNumber, int pageSize)
		{
			var offers = await _offerRepository.GetAllIncludingAsync(o => o.TravelingWay) ?? new List<Offer>();

			var filteredOffers = offers.AsQueryable();

			if (!string.IsNullOrEmpty(selectedTravelingWay))
			{
				string trimmedTravelingWay = selectedTravelingWay.Trim();

				filteredOffers = filteredOffers
					.Where(offer => offer.TravelingWay != null &&
									offer.TravelingWay.Method.Trim().Equals(trimmedTravelingWay, StringComparison.OrdinalIgnoreCase));
			}

			if (!string.IsNullOrEmpty(searchItem))
			{
				filteredOffers = filteredOffers.Where(offer =>
					(offer.Title != null && offer.Title.ToLower().Contains(searchItem.ToLower())) ||
					(offer.Description != null && offer.Description.ToLower().Contains(searchItem.ToLower())));
			}

			int totalCount = filteredOffers.Count();

			var pagedOffers = filteredOffers
				.Where(offer => !offer.IsDeleted) 
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList(); 

			return (pagedOffers, totalCount);
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
				TravelingWayId = model.TravelingWayId,
				CheckInDate = model.CheckInDate,
				CheckOutDate = model.CheckOutDate
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
				existingOffer.Description = model.Description ?? "No Description";
				existingOffer.Price = model.Price;
				existingOffer.ImageUrl = model.ImageUrl;
				existingOffer.TravelingWayId = model.TravelingWayId;
				existingOffer.CheckInDate = model.CheckInDate;
				existingOffer.CheckOutDate = model.CheckOutDate;

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
