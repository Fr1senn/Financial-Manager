using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Repositories;
using financial_manager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Middlewares
{
    public class AccessTokenValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AccessTokenValidatorMiddleware(
            RequestDelegate next,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/Auth/Refresh"))
            {
                await _next(context);
                return;
            }

            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var accessToken = authHeader.ToString().Replace("Bearer ", string.Empty);

                if (!string.IsNullOrEmpty(accessToken))
                {
                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(accessToken);

                        var expClaim = jwtToken.Claims.FirstOrDefault(c =>
                            c.Type == JwtRegisteredClaimNames.Exp
                        );
                        if (expClaim == null)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsync(
                                "Token does not contain an expiration claim"
                            );
                            return;
                        }

                        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(
                            long.Parse(expClaim.Value)
                        );

                        if (expirationTime <= DateTime.Now)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsync("Token is expired");
                            return;
                        }

                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var tokenRepository =
                                scope.ServiceProvider.GetRequiredService<ITokenRepository>();

                            if (await tokenRepository.IsAccessTokenBlacklistedAsync(accessToken))
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync("Token is blacklisted");
                                return;
                            }
                        }
                    }
                    catch (NullReferenceException)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Token not found");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
