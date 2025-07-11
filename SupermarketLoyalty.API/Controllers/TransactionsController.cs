using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupermarketLoyalty.Core.DTOs;
using SupermarketLoyalty.Core.Interfaces;
using System.Security.Claims;

namespace SupermarketLoyalty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && transaction.CustomerId != currentUserId)
            {
                return Forbid();
            }

            return Ok(transaction);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionsByCustomer(string customerId)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && currentUserId != customerId)
            {
                return Forbid();
            }

            var transactions = await _transactionService.GetTransactionsByCustomerIdAsync(customerId);
            return Ok(transactions);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(CreateTransactionDto createTransactionDto)
        {
            var transaction = await _transactionService.CreateTransactionAsync(createTransactionDto);
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        [HttpGet("date-range")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var transactions = await _transactionService.GetTransactionsByDateRangeAsync(startDate, endDate);
            return Ok(transactions);
        }
    }
}