using SupermarketLoyalty.Core.DTOs;

namespace SupermarketLoyalty.Core.Interfaces
{
    public interface IAnalyticsService
    {
        Task<AnalyticsDto> GetAnalyticsAsync();
        Task<AnalyticsDto> GetAnalyticsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TopProductDto>> GetTopProductsAsync(int count = 10);
        Task<Dictionary<string, int>> GetCustomersByTierAsync();
        Task<Dictionary<string, decimal>> GetSalesByMonthAsync(int months = 12);
        Task<Dictionary<string, int>> GetTransactionsByMonthAsync(int months = 12);
    }
}