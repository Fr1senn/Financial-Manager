using System.Security.Claims;

namespace financial_manager.Entities.Extentions
{
    public static class HttpContextExtenstion
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            if (httpContext?.User == null)
            {
                throw new Exception("HTTP context or User is null");
            }

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("User ID not found in the token");
            }

            return Convert.ToInt32(userIdClaim);
        }
    }
}
