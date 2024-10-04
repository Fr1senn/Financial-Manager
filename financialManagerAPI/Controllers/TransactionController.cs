using System.Net;
using financial_manager.Entities.DTOs;
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

        [HttpPost("all")]
        public async Task<IActionResult> GetTransactions([FromBody] PageRequest request)
        {
            try
            {
                var (transactions, totalCount) = await _transactionRepository.GetTransactionsAsync(request);

                return Ok(ApiResponse<TransactionDTO>.Succeed(transactions, totalCount));
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

        [HttpPost("create")]
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
