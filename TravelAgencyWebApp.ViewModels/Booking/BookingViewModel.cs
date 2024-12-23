﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TravelAgencyWebApp.Common.Attributes;
using TravelAgencyWebApp.ViewModels.Offer;
using static TravelAgencyWebApp.Common.DataConstants;


namespace TravelAgencyWebApp.ViewModels.Booking
{
    public class BookingViewModel
    {
        [Key]
        [Comment("Booking identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = BookingUserIdRequiredError)]
        [Comment("User identifier")]
        public string? UserId { get; set; }

        [Comment("User name")]
        public string? UserName { get; set; }

        [Comment("User that is done the booking")]
		public string? ReservedByName { get; set; }

		[Comment("Offer identifier")]
        public int OfferId { get; set; }

        [Comment("Offer title")]
        public string? OfferTitle { get; set; }

        [Comment("Offer image")]
        public string? OfferImageUrl { get; set; }

        [Required(ErrorMessage = BookingCheckInDateRequiredError)]
        [Comment("Check in date of booking")]
        public DateTime CheckInDate { get; set; } 

        [Required(ErrorMessage = BookingCheckOutDateRequiredError)]
        [IsBefore("CheckInDate", ErrorMessage = BookingCheckOutDateIsBeforeCheckInDateError)]
        [Comment("Check out date of booking")]
        public DateTime CheckOutDate { get; set; }

		public string? AgentFullName { get; set; }  

		public string? UserFullName { get; set; }

		public OfferViewModel? Offer { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }
	}
}
