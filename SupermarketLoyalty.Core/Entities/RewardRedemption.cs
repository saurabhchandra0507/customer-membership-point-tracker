using System.ComponentModel.DataAnnotations;

namespace SupermarketLoyalty.Core.Entities
{
    public class RewardRedemption
    {
        public int Id { get; set; }
        
        [Required]
        public string CustomerId { get; set; } = string.Empty;
        
        public int RewardId { get; set; }
        public int PointsRedeemed { get; set; }
        public DateTime RedemptionDate { get; set; }
        public RedemptionStatus Status { get; set; }
        
        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual Reward Reward { get; set; } = null!;
    }

    public enum RedemptionStatus
    {
        Pending = 0,
        Approved = 1,
        Fulfilled = 2,
        Cancelled = 3
    }
}