namespace SupermarketLoyalty.Core.DTOs
{
    public class CampaignDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal BonusMultiplier { get; set; }
        public bool IsActive { get; set; }
        public List<int> ApplicableProductIds { get; set; } = new();
    }

    public class CreateCampaignDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal BonusMultiplier { get; set; }
        public List<int> ApplicableProductIds { get; set; } = new();
    }
}