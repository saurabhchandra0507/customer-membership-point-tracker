using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SupermarketLoyalty.Core.Entities
{
    public class Customer : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string MembershipNumber { get; set; } = string.Empty;

        public DateTime JoinDate { get; set; }
        public int TotalPoints { get; set; }
        public int LifetimePoints { get; set; }
        public MembershipTier Tier { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<ExpiringPoint> ExpiringPoints { get; set; } = new List<ExpiringPoint>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public virtual ICollection<RewardRedemption> RewardRedemptions { get; set; } = new List<RewardRedemption>();
    }

    public enum MembershipTier
    {
        Bronze = 0,
        Silver = 1,
        Gold = 2,
        Platinum = 3
    }
}