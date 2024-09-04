using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using financial_manager.Models;
using financial_manager.Models.Enums;
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
                return Ok(new
                {
                    isSuccess = true,
                    httpResponseCode = HttpResponseCode.Ok,
                    message = string.Empty,
                    data = await _userRepository.GetCurrentUserCredentialsAsync()
                });
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.InternalServerError, ex.Message));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserCredentialsAsync([FromBody] User user)
        {
            try
            {
                await _userRepository.UpdateUserCredentialsAsync(user);
                return Ok(new OperationResult(true, HttpResponseCode.NoContent));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new OperationResult(false, HttpResponseCode.BadRequest, ex.Message));
            }
        }
    }
}