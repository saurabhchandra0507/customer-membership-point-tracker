using System.ComponentModel.DataAnnotations;

namespace SupermarketLoyalty.Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        
        [Required]
        public string CustomerId { get; set; } = string.Empty;
        
        public DateTime Date { get; set; }
        public decimal Subtotal { get; set; }
        public int PointsEarned { get; set; }
        public int PointsRedeemed { get; set; }
        public int TotalPoints { get; set; }
        public TransactionType Type { get; set; }
        
        [MaxLength(50)]
        public string TransactionNumber { get; set; } = string.Empty;

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<TransactionItem> Items { get; set; } = new List<TransactionItem>();
    }

    public enum TransactionType
    {
        Purchase = 0,
        Redemption = 1
    }
}