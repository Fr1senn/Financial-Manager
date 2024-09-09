using System.Net;
using System.Reflection.Metadata.Ecma335;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace financial_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthRepository authRepository, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            try
            {
                return Ok(
                    ApiResponse<TokenResponse>.Succeed(await _authRepository.LoginAsync(loginModel))
                );
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(ApiResponse.Fail(HttpStatusCode.Unauthorized, ex.Message));
            }
            catch (NullReferenceException ex)
            {
                return Unauthorized(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return Unauthorized(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] User user)
        {
            try
            {
                await _userRepository.CreateUserAsync(user);
                return Ok(ApiResponse.Succeed());
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] Dictionary<string, string> request)
        {
            try
            {
                if (request.TryGetValue("refreshToken", out var refreshToken))
                {
                    var result = await _authRepository.RefreshTokensAsync(refreshToken);
                    return Ok(ApiResponse<TokenResponse>.Succeed(result));
                }
                return BadRequest(
                    ApiResponse.Fail(HttpStatusCode.BadRequest, "Refresh token not provided")
                );
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
                ;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
                ;
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
                ;
            }
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync([FromBody] Dictionary<string, string> request)
        {
            try
            {
                if (request.TryGetValue("refreshToken", out var refreshToken))
                {
                    await _authRepository.LogoutAsync(refreshToken);
                    return Ok(ApiResponse.Succeed());
                }
                return BadRequest(
                    ApiResponse.Fail(HttpStatusCode.BadRequest, "Refresh token not provided")
                );
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
                ;
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
                ;
            }
        }
    }
}
