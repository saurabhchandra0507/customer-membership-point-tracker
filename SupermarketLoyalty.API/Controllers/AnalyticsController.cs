using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupermarketLoyalty.Core.DTOs;
using SupermarketLoyalty.Core.Interfaces;

namespace SupermarketLoyalty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public async Task<ActionResult<AnalyticsDto>> GetAnalytics()
        {
            var analytics = await _analyticsService.GetAnalyticsAsync();
            return Ok(analytics);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<AnalyticsDto>> GetAnalyticsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var analytics = await _analyticsService.GetAnalyticsByDateRangeAsync(startDate, endDate);
            return Ok(analytics);
        }

        [HttpGet("top-products")]
        public async Task<ActionResult<IEnumerable<TopProductDto>>> GetTopProducts([FromQuery] int count = 10)
        {
            var topProducts = await _analyticsService.GetTopProductsAsync(count);
            return Ok(topProducts);
        }

        [HttpGet("customers-by-tier")]
        public async Task<ActionResult<Dictionary<string, int>>> GetCustomersByTier()
        {
            var customersByTier = await _analyticsService.GetCustomersByTierAsync();
            return Ok(customersByTier);
        }

        [HttpGet("sales-by-month")]
        public async Task<ActionResult<Dictionary<string, decimal>>> GetSalesByMonth([FromQuery] int months = 12)
        {
            var salesByMonth = await _analyticsService.GetSalesByMonthAsync(months);
            return Ok(salesByMonth);
        }

        [HttpGet("transactions-by-month")]
        public async Task<ActionResult<Dictionary<string, int>>> GetTransactionsByMonth([FromQuery] int months = 12)
        {
            var transactionsByMonth = await _analyticsService.GetTransactionsByMonthAsync(months);
            return Ok(transactionsByMonth);
        }
    }
}