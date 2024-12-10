namespace TravelAgencyWebApp.ViewModels.Booking
{
	public class BookingSearchViewModel
    {
        public string? SearchTerm { get; set; }

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

		public string? SelectedReservationHolder { get; set; } 

		public IEnumerable<string> ReservationHolders { get; set; } = Enumerable.Empty<string>();

		public IEnumerable<BookingViewModel> Bookings { get; set; } = null!;
	}
}
