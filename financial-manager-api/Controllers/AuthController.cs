using System.Net;
using financial_manager.Entities;
using financial_manager.Entities.Requests;
using financial_manager.Entities.Shared;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financial_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                return Ok(
                    ApiResponse<TokenResponse>.Succeed(await _authRepository.LoginAsync(request))
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(ModelState.Values.ToString()!, HttpStatusCode.BadRequest));
            }
            try
            {
                await _authRepository.RegisterAsync(request);
                return Ok(ApiResponse.Succeed());
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] Dictionary<string, string> request)
        {
            try
            {
                if (request.TryGetValue("refreshToken", out var refreshToken))
                {
                    var result = await _authRepository.RefreshTokensAsync(refreshToken);
                    return Ok(ApiResponse<TokenResponse>.Succeed(result));
                }
                return BadRequest(
                    ApiResponse.Fail("Refresh token not provided", HttpStatusCode.BadRequest)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] Dictionary<string, string> request)
        {
            try
            {
                if (request.TryGetValue("refreshToken", out var refreshToken))
                {
                    await _authRepository.LogoutAsync(refreshToken);
                    return Ok(ApiResponse.Succeed());
                }
                return BadRequest(
                    ApiResponse.Fail("Refresh token not provided", HttpStatusCode.BadRequest)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }
    }
}
