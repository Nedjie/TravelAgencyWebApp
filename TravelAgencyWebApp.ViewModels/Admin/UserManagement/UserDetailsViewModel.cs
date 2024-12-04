using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.ViewModels.Admin.UserManagement
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = FullNameRequiredError)]
        [StringLength(FullNameMaxLength,
          ErrorMessage = FullNameMaxLengthError)]
        public string FullName { get; set; } = null!;

        [StringLength(AddressMaxLength,
           ErrorMessage = AddressMaxLengthError)]
        public string Address { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
