namespace SupermarketLoyalty.Core.Entities
{
    public class CampaignProduct
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}