namespace TravelAgencyWebApp.ViewModels.Offer
{
    public class OfferSearchViewModel
    {
        public string? SearchTerm { get; set; }

        public string? SelectedTravelingWay { get; set; }

        public IEnumerable<OfferViewModel> Offers { get; set; } = null!;

        public IEnumerable<string> TravelingWays { get; set; } = null!;

        public int TotalOffers { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

    }
}
