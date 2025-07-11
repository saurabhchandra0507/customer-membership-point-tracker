using System.ComponentModel.DataAnnotations;

namespace SupermarketLoyalty.Core.Entities
{
    public class ExpiringPoint
    {
        public int Id { get; set; }
        
        [Required]
        public string CustomerId { get; set; } = string.Empty;
        
        public int Points { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime EarnedDate { get; set; }
        public bool IsExpired { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
    }
}