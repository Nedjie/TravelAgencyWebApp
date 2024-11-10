using System.Security.Claims;

namespace TravelAgencyWebApp.Infrastructure.Extensions
{
    public static class GetUserId
    {
        public static int GetCurrentUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? Convert.ToInt32(userIdClaim.Value) : 0;
        }
    }
}
