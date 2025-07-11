using SupermarketLoyalty.Core.DTOs;

namespace SupermarketLoyalty.Core.Interfaces
{
    public interface IRewardService
    {
        Task<IEnumerable<RewardDto>> GetAllRewardsAsync();
        Task<RewardDto?> GetRewardByIdAsync(int id);
        Task<IEnumerable<RewardDto>> GetAvailableRewardsForCustomerAsync(string customerId);
        Task<RewardDto> CreateRewardAsync(CreateRewardDto createRewardDto);
        Task<RewardDto?> UpdateRewardAsync(int id, CreateRewardDto updateRewardDto);
        Task<bool> DeleteRewardAsync(int id);
        Task<RewardRedemptionDto> RedeemRewardAsync(string customerId, RedeemRewardDto redeemRewardDto);
        Task<IEnumerable<RewardRedemptionDto>> GetRedemptionHistoryAsync(string customerId);
        Task<IEnumerable<RewardRedemptionDto>> GetAllRedemptionsAsync();
        Task<bool> UpdateRedemptionStatusAsync(int redemptionId, int status);
    }
}