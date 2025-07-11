using System.ComponentModel.DataAnnotations;

namespace SupermarketLoyalty.Core.Entities
{
    public class Reward
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        
        public int PointsCost { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        
        public bool Available { get; set; } = true;
        public MembershipTier? RequiredTier { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public virtual ICollection<RewardRedemption> RewardRedemptions { get; set; } = new List<RewardRedemption>();
    }
}