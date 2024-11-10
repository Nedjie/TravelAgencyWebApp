using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyWebApp.Common;

namespace TravelAgencyWebApp.ViewModels.Offer
{
    public class ConfirmDeleteOfferViewModel
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
    }
}
