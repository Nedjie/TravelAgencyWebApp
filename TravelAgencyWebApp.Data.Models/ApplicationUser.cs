using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.Data.Models
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
		public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
	}
}
