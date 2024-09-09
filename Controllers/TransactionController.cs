﻿using financial_manager.Models;
using financial_manager.Models.Enums;
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
                    new OperationResult<Transaction>(
                        true,
                        HttpResponseCode.Ok,
                        null,
                        await _transactionRepository.GetTransactionsAsync(packSize, pageNumber)
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new OperationResult(false, HttpResponseCode.BadRequest, ex.Message)
                );
            }
        }

        [HttpGet("GetUserTransactionQuantity")]
        public async Task<IActionResult> GetUserTransactionQuantityAsync()
        {
            try
            {
                return Ok(
                    new
                    {
                        isSucces = true,
                        httpResponseCode = HttpResponseCode.Ok,
                        message = string.Empty,
                        data = await _transactionRepository.GetUserTransactionQuantityAsync(),
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new OperationResult(false, HttpResponseCode.BadRequest, ex.Message)
                );
            }
        }

        [HttpGet("GetMonthlyTransactions")]
        public async Task<IActionResult> GetMonthlyTransactions([FromQuery] int year)
        {
            try
            {
                return Ok(
                    new
                    {
                        isSuccess = true,
                        httpResponseCode = HttpResponseCode.Ok,
                        message = string.Empty,
                        data = await _transactionRepository.GetMonthlyTransactionsAsync(year),
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new OperationResult(false, HttpResponseCode.BadRequest, ex.Message)
                );
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionAsync([FromQuery] int transactionId)
        {
            try
            {
                await _transactionRepository.DeleteTransactionAsync(transactionId);
                return Ok(new OperationResult(true, HttpResponseCode.NoContent));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                    new OperationResult(false, HttpResponseCode.BadRequest, ex.Message)
                );
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(
                    new OperationResult(false, HttpResponseCode.BadRequest, ex.Message)
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] Transaction transaction)
        {
            try
            {
                await _transactionRepository.CreateTransactionAsync(transaction);
                return Ok(new OperationResult(true, HttpResponseCode.NoContent));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(
                    new OperationResult(false, HttpResponseCode.BadRequest, ex.Message)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new OperationResult(false, HttpResponseCode.InternalServerError, ex.Message)
                );
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTransactionAsync([FromBody] Transaction transaction)
        {
            try
            {
                await _transactionRepository.UpdateTransactionAsync(transaction);
                return Ok(new OperationResult(true, HttpResponseCode.Ok));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(
                    new OperationResult(false, HttpResponseCode.BadRequest, ex.Message)
                );
            }
        }
    }
}
