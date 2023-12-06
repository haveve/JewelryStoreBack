using System.Security.Claims;

namespace CourseWorkDB.Helpers
{
    public static class ClaimPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Convert.ToInt32(claimsPrincipal.Claims.First(el => el.Type == "UserId").Value);
        }
    }
}
