using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupermarketLoyalty.Core.DTOs;
using SupermarketLoyalty.Core.Entities;
using SupermarketLoyalty.Core.Interfaces;
using SupermarketLoyalty.Infrastructure.Data;

namespace SupermarketLoyalty.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;

        public CustomerService(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.ExpiringPoints)
                .ToListAsync();

            return customers.Select(MapToDto);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(string id)
        {
            var customer = await _context.Customers
                .Include(c => c.ExpiringPoints)
                .FirstOrDefaultAsync(c => c.Id == id);

            return customer == null ? null : MapToDto(customer);
        }

        public async Task<CustomerDto?> GetCustomerByEmailAsync(string email)
        {
            var customer = await _context.Customers
                .Include(c => c.ExpiringPoints)
                .FirstOrDefaultAsync(c => c.Email == email);

            return customer == null ? null : MapToDto(customer);
        }

        public async Task<CustomerDto?> GetCustomerByMembershipNumberAsync(string membershipNumber)
        {
            var customer = await _context.Customers
                .Include(c => c.ExpiringPoints)
                .FirstOrDefaultAsync(c => c.MembershipNumber == membershipNumber);

            return customer == null ? null : MapToDto(customer);
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = new Customer
            {
                UserName = createCustomerDto.Email,
                Email = createCustomerDto.Email,
                Name = createCustomerDto.Name,
                PhoneNumber = createCustomerDto.PhoneNumber,
                MembershipNumber = GenerateMembershipNumber(),
                JoinDate = DateTime.UtcNow,
                TotalPoints = 0,
                LifetimePoints = 0,
                Tier = createCustomerDto.Tier,
                IsActive = true,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(customer, createCustomerDto.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create customer: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            await _userManager.AddToRoleAsync(customer, "Customer");
            return MapToDto(customer);
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(string id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            customer.Name = updateCustomerDto.Name;
            customer.PhoneNumber = updateCustomerDto.PhoneNumber;
            customer.IsActive = updateCustomerDto.IsActive;

            await _context.SaveChangesAsync();
            return MapToDto(customer);
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleCustomerStatusAsync(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            customer.IsActive = !customer.IsActive;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateCustomerPointsAsync(string customerId, int pointsToAdd)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return;

            customer.TotalPoints += pointsToAdd;
            customer.LifetimePoints += pointsToAdd;

            // Add expiring points (expire after 1 year)
            if (pointsToAdd > 0)
            {
                var expiringPoint = new ExpiringPoint
                {
                    CustomerId = customerId,
                    Points = pointsToAdd,
                    EarnedDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddYears(1),
                    IsExpired = false
                };
                _context.ExpiringPoints.Add(expiringPoint);
            }

            await UpdateCustomerTierAsync(customerId);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RedeemPointsAsync(string customerId, int pointsToRedeem)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null || customer.TotalPoints < pointsToRedeem) return false;

            customer.TotalPoints -= pointsToRedeem;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateCustomerTierAsync(string customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return;

            var newTier = CalculateTier(customer.LifetimePoints);
            if (customer.Tier != newTier)
            {
                customer.Tier = newTier;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ProcessExpiringPointsAsync()
        {
            var expiredPoints = await _context.ExpiringPoints
                .Where(ep => ep.ExpirationDate <= DateTime.UtcNow && !ep.IsExpired)
                .ToListAsync();

            foreach (var expiredPoint in expiredPoints)
            {
                var customer = await _context.Customers.FindAsync(expiredPoint.CustomerId);
                if (customer != null)
                {
                    customer.TotalPoints = Math.Max(0, customer.TotalPoints - expiredPoint.Points);
                    expiredPoint.IsExpired = true;
                }
            }

            await _context.SaveChangesAsync();
        }

        private static CustomerDto MapToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Email = customer.Email!,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber ?? string.Empty,
                MembershipNumber = customer.MembershipNumber,
                JoinDate = customer.JoinDate,
                TotalPoints = customer.TotalPoints,
                LifetimePoints = customer.LifetimePoints,
                Tier = customer.Tier,
                IsActive = customer.IsActive,
                ExpiringPoints = customer.ExpiringPoints?.Select(ep => new ExpiringPointDto
                {
                    Points = ep.Points,
                    ExpirationDate = ep.ExpirationDate,
                    EarnedDate = ep.EarnedDate
                }).ToList() ?? new List<ExpiringPointDto>()
            };
        }

        private static MembershipTier CalculateTier(int lifetimePoints)
        {
            return lifetimePoints switch
            {
                >= 15000 => MembershipTier.Platinum,
                >= 5000 => MembershipTier.Gold,
                >= 1000 => MembershipTier.Silver,
                _ => MembershipTier.Bronze
            };
        }

        private static string GenerateMembershipNumber()
        {
            var prefix = "SM";
            var number = new Random().Next(100000, 999999);
            return $"{prefix}{number:D6}";
        }
    }
}