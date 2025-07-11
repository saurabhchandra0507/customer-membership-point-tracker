using SupermarketLoyalty.Core.Entities;

namespace SupermarketLoyalty.Core.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Subtotal { get; set; }
        public int PointsEarned { get; set; }
        public int PointsRedeemed { get; set; }
        public int TotalPoints { get; set; }
        public TransactionType Type { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;
        public List<TransactionItemDto> Items { get; set; } = new();
    }

    public class CreateTransactionDto
    {
        public string CustomerId { get; set; } = string.Empty;
        public List<CreateTransactionItemDto> Items { get; set; } = new();
        public int PointsRedeemed { get; set; } = 0;
    }

    public class TransactionItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int PointsEarned { get; set; }
    }

    public class CreateTransactionItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}