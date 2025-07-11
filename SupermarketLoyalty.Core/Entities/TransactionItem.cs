using System.ComponentModel.DataAnnotations;

namespace SupermarketLoyalty.Core.Entities
{
    public class TransactionItem
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;
        
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int PointsEarned { get; set; }

        // Navigation properties
        public virtual Transaction Transaction { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}