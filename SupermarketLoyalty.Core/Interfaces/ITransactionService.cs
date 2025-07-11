using SupermarketLoyalty.Core.DTOs;

namespace SupermarketLoyalty.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
        Task<TransactionDto?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<TransactionDto>> GetTransactionsByCustomerIdAsync(string customerId);
        Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto createTransactionDto);
        Task<IEnumerable<TransactionDto>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSalesAsync();
        Task<int> GetTotalPointsIssuedAsync();
        Task<int> GetTotalPointsRedeemedAsync();
    }
}