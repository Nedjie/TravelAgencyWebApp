using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyWebApp.ViewModels.Admin.UserManagement
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
