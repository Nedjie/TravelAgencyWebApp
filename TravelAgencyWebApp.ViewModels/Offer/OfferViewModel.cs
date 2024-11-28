using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.ViewModels.Offer
{
	public class OfferViewModel
	{
		[Key]
		[Comment("Offer identifier")]
		public int Id { get; set; }
		
		[Required(ErrorMessage = OfferTitleRequiredError)]
		[StringLength(OfferTitleMaxLength,
			   ErrorMessage = OfferTitleMaxLengthError)]
		[Comment("Offer title")]
		public string? Title { get; set; }

		[Required(ErrorMessage = OfferDescriptionRequiredError)]
		[Comment("Offer description")]
		public string? Description { get; set; }

		[Required(ErrorMessage = OfferPriceRequiredError)]
		[Range(OfferPriceRangeMin, OfferPriceRangeMax,
			ErrorMessage = OfferPriceRangeError)]
		[Comment("Offer price")]
		public decimal Price { get; set; }


		[Url(ErrorMessage = OfferImageUrlInvalidError)]
		[Comment("Offer image")]
		public string? ImageUrl { get; set; }

		[Required(ErrorMessage = "Please select a traveling way method.")]
		[Comment("Traveling way method")]
		public string? TravelingWayMethod { get; set; }

		public int UserId { get; set; }

	}
}
