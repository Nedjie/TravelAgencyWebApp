using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.ViewModels.Booking
{
    public class CreateBookingViewModel
    {
        [Comment("User identifier")]
        public Guid? UserId { get; set; }

        public string? UserEmail { get; set; }

        public string? UserFullName { get; set; }

        public string? UserPhoneNumber { get; set; }

        [Required(ErrorMessage = BookingCheckInDateRequiredError)]
        [Comment("Check in date of booking")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = BookingCheckOutDateRequiredError)]
        [Comment("Check out date of booking")]
        public DateTime CheckOutDate { get; set; }

        [Comment("Offer identifier")]
        public int OfferId { get; set; }

        [Comment("AgentId identifier")]
		public string? AgentId { get; set; }
	}
}
