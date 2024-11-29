using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> GetAllOffersAsync();
		Task<Offer?> GetOfferByIdAsync(int id);
		Task<IDictionary<TravelingWay, IEnumerable<Offer>>> GetOffersGroupedByTravelingWayAsync();	
		Task AddOfferAsync(Offer model);
        Task UpdateOfferAsync(Offer model);
        Task DeleteOfferAsync(int id);
        Task<bool> SoftDeleteOfferAsync(Offer offer);
    }
}
