using System.Net;
using financial_manager.Entities;
using financial_manager.Entities.DTOs;
using financial_manager.Entities.Models;
using financial_manager.Entities.Requests;
using financial_manager.Entities.Shared;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financial_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] PageRequest request)
        {
            try
            {
                return Ok(
                    ApiResponse<IEnumerable<TransactionDTO>>.Succeed(
                        await _transactionRepository.GetTransactionsAsync(request)
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpGet("GetUserTransactionQuantity")]
        public async Task<IActionResult> GetUserTransactionQuantity()
        {
            try
            {
                return Ok(
                    ApiResponse<int>.Succeed(
                        await _transactionRepository.GetUserTransactionQuantityAsync()
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpGet("GetMonthlyTransactions")]
        public async Task<IActionResult> GetMonthlyTransactions([FromQuery] int year)
        {
            try
            {
                return Ok(
                    ApiResponse<Dictionary<string, TransactionSummary>>.Succeed(
                        await _transactionRepository.GetMonthlyTransactionsAsync(year)
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransaction([FromQuery] int transactionId)
        {
            try
            {
                await _transactionRepository.DeleteTransactionAsync(transactionId);
                return Ok(ApiResponse.Succeed());
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            try
            {
                await _transactionRepository.CreateTransactionAsync(request);
                return Ok(ApiResponse.Succeed());
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionRequest request)
        {
            try
            {
                await _transactionRepository.UpdateTransactionAsync(request);
                return Ok(ApiResponse.Succeed());
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message, HttpStatusCode.BadRequest));
            }
        }
    }
}
