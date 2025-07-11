using SupermarketLoyalty.Core.Entities;

namespace SupermarketLoyalty.Core.DTOs
{
    public class CustomerDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string MembershipNumber { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
        public int TotalPoints { get; set; }
        public int LifetimePoints { get; set; }
        public MembershipTier Tier { get; set; }
        public bool IsActive { get; set; }
        public List<ExpiringPointDto> ExpiringPoints { get; set; } = new();
    }

    public class CreateCustomerDto
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public MembershipTier Tier { get; set; } = MembershipTier.Bronze;
    }

    public class UpdateCustomerDto
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class ExpiringPointDto
    {
        public int Points { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime EarnedDate { get; set; }
    }
}