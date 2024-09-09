using System.Net;
using financial_manager.Models;
using financial_manager.Repositories;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetTransactionsAsync(
            [FromQuery] int packSize = 10,
            [FromQuery] int pageNumber = 0
        )
        {
            try
            {
                return Ok(
                    ApiResponse<IEnumerable<Transaction>>.Succeed(
                        await _transactionRepository.GetTransactionsAsync(packSize, pageNumber)
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpGet("GetUserTransactionQuantity")]
        public async Task<IActionResult> GetUserTransactionQuantityAsync()
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
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
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
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionAsync([FromQuery] int transactionId)
        {
            try
            {
                await _transactionRepository.DeleteTransactionAsync(transactionId);
                return Ok(ApiResponse.Succeed());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] Transaction transaction)
        {
            try
            {
                await _transactionRepository.CreateTransactionAsync(transaction);
                return Ok(ApiResponse.Succeed());
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
        public async Task<IActionResult> UpdateTransactionAsync([FromBody] Transaction transaction)
        {
            try
            {
                await _transactionRepository.UpdateTransactionAsync(transaction);
                return Ok(ApiResponse.Succeed());
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
