namespace TravelAgencyWebApp.ViewModels.Booking
{
	public class BookingSearchViewModel
    {
        public string? SearchTerm { get; set; }

        public IEnumerable<BookingViewModel> Bookings { get; set; } = null!;

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
