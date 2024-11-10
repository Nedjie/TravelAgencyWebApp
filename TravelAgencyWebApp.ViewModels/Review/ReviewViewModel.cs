using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.ViewModels.Review
{
	public class ReviewViewModel
	{
		[Comment("Review identifier")]
		public int Id { get; set; }

		[Required(ErrorMessage = ReviewUserIdRequiredError)]
		[Comment("User identifier of review")]
		public string? UserId { get; set; }

		[Required(ErrorMessage = ReviewOfferIdRequiredError)]
		[Comment("Offer identifier")]
		public int OfferId { get; set; }

		[Required(ErrorMessage = ReviewTextRequiredError)]
		[StringLength(ReviewTextMaxLength,
		ErrorMessage = ReviewTextMaxLengthError)]
		[Comment("Review text")]
		public string? ReviewText { get; set; }

		[Range(ReviewRangeMin, ReviewRangeMax,
		ErrorMessage = ReviewRatingRangeError)]
		[Comment("Review rating")]
		public int Rating { get; set; }

		[Comment("Review date")]
		public DateTime CreatedAt { get; set; }
	}
}
