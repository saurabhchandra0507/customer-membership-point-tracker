using SupermarketLoyalty.Core.Entities;

namespace SupermarketLoyalty.Core.DTOs
{
    public class AnalyticsDto
    {
        public int TotalCustomers { get; set; }
        public int ActiveCustomers { get; set; }
        public int TotalPointsIssued { get; set; }
        public int TotalPointsRedeemed { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AverageTransactionValue { get; set; }
        public List<TopProductDto> TopProducts { get; set; } = new();
        public Dictionary<MembershipTier, int> TierDistribution { get; set; } = new();
    }

    public class TopProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal TotalSales { get; set; }
    }
}