using System.Security.Claims;

namespace financial_manager.Utilities.Interfaces
{
    public interface IJWTProvider
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        string GetJWT();
    }
}
