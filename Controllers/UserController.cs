using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using financial_manager.Models;
using financial_manager.Repositories;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financial_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
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
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
