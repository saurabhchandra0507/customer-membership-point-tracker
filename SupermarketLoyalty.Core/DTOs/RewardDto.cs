using SupermarketLoyalty.Core.Entities;

namespace SupermarketLoyalty.Core.DTOs
{
    public class RewardDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PointsCost { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Available { get; set; }
        public MembershipTier? RequiredTier { get; set; }
    }

    public class CreateRewardDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PointsCost { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Available { get; set; } = true;
        public MembershipTier? RequiredTier { get; set; }
    }

    public class RedeemRewardDto
    {
        public int RewardId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }

    public class RewardRedemptionDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public int RewardId { get; set; }
        public string RewardName { get; set; } = string.Empty;
        public int PointsRedeemed { get; set; }
        public DateTime RedemptionDate { get; set; }
        public RedemptionStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}