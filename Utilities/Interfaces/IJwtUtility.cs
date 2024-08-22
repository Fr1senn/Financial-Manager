using System.Security.Claims;

namespace financial_manager.Utilities.Interfaces
{
    public interface IJwtUtility
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        string GetJwt();
    }
}
