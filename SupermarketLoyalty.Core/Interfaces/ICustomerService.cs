using SupermarketLoyalty.Core.DTOs;

namespace SupermarketLoyalty.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(string id);
        Task<CustomerDto?> GetCustomerByEmailAsync(string email);
        Task<CustomerDto?> GetCustomerByMembershipNumberAsync(string membershipNumber);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<CustomerDto?> UpdateCustomerAsync(string id, UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomerAsync(string id);
        Task<bool> ToggleCustomerStatusAsync(string id);
        Task UpdateCustomerPointsAsync(string customerId, int pointsToAdd);
        Task<bool> RedeemPointsAsync(string customerId, int pointsToRedeem);
        Task UpdateCustomerTierAsync(string customerId);
        Task ProcessExpiringPointsAsync();
    }
}