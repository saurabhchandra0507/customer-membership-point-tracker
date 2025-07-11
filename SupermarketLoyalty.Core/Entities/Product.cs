using System.ComponentModel.DataAnnotations;

namespace SupermarketLoyalty.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        public decimal PointsPerDollar { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        
        public bool InStock { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public virtual ICollection<TransactionItem> TransactionItems { get; set; } = new List<TransactionItem>();
        public virtual ICollection<CampaignProduct> CampaignProducts { get; set; } = new List<CampaignProduct>();
    }
}