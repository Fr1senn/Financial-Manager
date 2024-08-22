using financial_manager.Models;
using financial_manager.Models.Enums;
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

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            try
            {
                return Ok(new OperationResult<TokenResponse>(true, HttpResponseCode.Ok, null, [await _authRepository.LoginAsync(loginModel)]));
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(new OperationResult(false, HttpResponseCode.Unauthorized, ex.Message));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
        }
    }
}