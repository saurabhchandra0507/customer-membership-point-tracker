using SupermarketLoyalty.Core.DTOs;

namespace SupermarketLoyalty.Core.Interfaces
{
    public interface ICampaignService
    {
        Task<IEnumerable<CampaignDto>> GetAllCampaignsAsync();
        Task<CampaignDto?> GetCampaignByIdAsync(int id);
        Task<IEnumerable<CampaignDto>> GetActiveCampaignsAsync();
        Task<CampaignDto> CreateCampaignAsync(CreateCampaignDto createCampaignDto);
        Task<CampaignDto?> UpdateCampaignAsync(int id, CreateCampaignDto updateCampaignDto);
        Task<bool> DeleteCampaignAsync(int id);
        Task<bool> ToggleCampaignStatusAsync(int id);
        Task<decimal> GetBonusMultiplierForProductAsync(int productId);
    }
}