using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyWebApp.ViewModels.Booking
{
    public class ConfirmDeleteViewModel
    {

        [Key]
        [Comment("Booking identifier")]
        public int Id { get; set; }

        [Comment("User name")]
        public string? UserName { get; set; }

        [Comment("Offer title")]
        public string? OfferTitle { get; set; }
    }
}
