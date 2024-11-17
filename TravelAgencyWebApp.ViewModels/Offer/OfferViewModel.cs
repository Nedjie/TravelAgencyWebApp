using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TravelAgencyWebApp.Common;

namespace TravelAgencyWebApp.ViewModels.Offer
{
	public class OfferViewModel
	{
		[Key]
		[Comment("Offer identifier")]
		public int Id { get; set; }
		
		[Required(ErrorMessage = DataConstants.OfferTitleRequiredError)]
		[StringLength(DataConstants.OfferTitleMaxLength,
			   ErrorMessage = DataConstants.OfferTitleMaxLengthError)]
		[Comment("Offer title")]
		public string? Title { get; set; }

		[Required(ErrorMessage = DataConstants.OfferDescriptionRequiredError)]
		[Comment("Offer description")]
		public string? Description { get; set; }

		[Required(ErrorMessage = DataConstants.OfferPriceRequiredError)]
		[Range(DataConstants.OfferPriceRangeMin, DataConstants.OfferPriceRangeMax,
			ErrorMessage = DataConstants.OfferPriceRangeError)]
		[Comment("Offer price")]
		public decimal Price { get; set; }


		[Url(ErrorMessage = DataConstants.OfferImageUrlInvalidError)]
		[Comment("Offer image")]
		public string? ImageUrl { get; set; }

		[Required(ErrorMessage = "Please select a traveling way method.")] // MAKE DATA CONSTANTS
		[Comment("Traveling way method")]
		public string? TravelingWayMethod { get; set; }

		public int UserId { get; set; }

	}
}
