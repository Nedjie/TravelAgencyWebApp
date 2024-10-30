using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> GetAllOffersAsync();
        Task<Offer?> GetOfferByIdAsync(int id);
        Task AddOfferAsync(Offer offer);
        Task UpdateOfferAsync(Offer offer);
        Task DeleteOfferAsync(int id);
    }
}
