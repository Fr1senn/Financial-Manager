using System.Net;
using financial_manager.Entities.Models;
using financial_manager.Entities.Shared;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace financial_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserCredentialsAsync()
        {
            try
            {
                return Ok(
                    ApiResponse<User>.Succeed(
                        await _userRepository.GetCurrentUserCredentialsAsync()
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserCredentialsAsync([FromBody] User user)
        {
            try
            {
                await _userRepository.UpdateUserCredentialsAsync(user);
                return Ok(ApiResponse.Succeed());
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }
    }
}
