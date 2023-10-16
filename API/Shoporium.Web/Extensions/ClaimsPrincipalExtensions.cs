using Shoporium.Entities.Enums;
using System.Security.Claims;

namespace Shoporium.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return Convert.ToInt64(claim?.Value ?? "0");
        }

        public static UserType GetUserType(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Role);
            return (UserType)Enum.Parse(typeof(UserType), claim!.Value);
        }
    }
}
