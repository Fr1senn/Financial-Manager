using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using financial_manager.Models;
using financial_manager.Models.Enums;
using financial_manager.Repositories;
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
    }
}