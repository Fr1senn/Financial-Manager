using financial_manager.Models;
using financial_manager.Repositories;
using financial_manager.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace financial_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAsync([FromQuery] int packSize = 10, [FromQuery] int pageNumber = 0)
        {
            return Ok(await _transactionRepository.GetTransactionsAsync(packSize, pageNumber));
            //return Ok(10);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransacionAsync([FromQuery] int transactionId)
        {
            try
            {
                await _transactionRepository.DeleteTransactionAsync(transactionId);
                return Ok(new OperationResult(true));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new OperationResult(false, ex.Message));
            }
        }
    }
}
