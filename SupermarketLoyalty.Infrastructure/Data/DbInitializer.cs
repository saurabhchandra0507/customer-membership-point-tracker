using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupermarketLoyalty.Core.Entities;

namespace SupermarketLoyalty.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Create roles
            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Seed data if database is empty
            if (!context.Customers.Any())
            {
                await SeedCustomersAsync(userManager);
            }

            if (!context.Products.Any())
            {
                await SeedProductsAsync(context);
            }

            if (!context.Rewards.Any())
            {
                await SeedRewardsAsync(context);
            }

            if (!context.Campaigns.Any())
            {
                await SeedCampaignsAsync(context);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedCustomersAsync(UserManager<Customer> userManager)
        {
            var customers = new[]
            {
                new Customer
                {
                    UserName = "john.doe@email.com",
                    Email = "john.doe@email.com",
                    Name = "John Doe",
                    PhoneNumber = "(555) 123-4567",
                    MembershipNumber = "SM001234",
                    JoinDate = DateTime.Now.AddYears(-1),
                    TotalPoints = 2450,
                    LifetimePoints = 8750,
                    Tier = MembershipTier.Gold,
                    IsActive = true,
                    EmailConfirmed = true
                },
                new Customer
                {
                    UserName = "jane.smith@email.com",
                    Email = "jane.smith@email.com",
                    Name = "Jane Smith",
                    PhoneNumber = "(555) 234-5678",
                    MembershipNumber = "SM002345",
                    JoinDate = DateTime.Now.AddMonths(-6),
                    TotalPoints = 1280,
                    LifetimePoints = 4500,
                    Tier = MembershipTier.Silver,
                    IsActive = true,
                    EmailConfirmed = true
                },
                new Customer
                {
                    UserName = "bob.johnson@email.com",
                    Email = "bob.johnson@email.com",
                    Name = "Bob Johnson",
                    PhoneNumber = "(555) 345-6789",
                    MembershipNumber = "SM003456",
                    JoinDate = DateTime.Now.AddMonths(-1),
                    TotalPoints = 580,
                    LifetimePoints = 580,
                    Tier = MembershipTier.Bronze,
                    IsActive = true,
                    EmailConfirmed = true
                }
            };

            foreach (var customer in customers)
            {
                await userManager.CreateAsync(customer, "Password123!");
                await userManager.AddToRoleAsync(customer, "Customer");
            }
        }

        private static async Task SeedProductsAsync(ApplicationDbContext context)
        {
            var products = new[]
            {
                new Product
                {
                    Name = "Organic Bananas",
                    Category = "Produce",
                    Price = 2.99m,
                    PointsPerDollar = 1.0m,
                    Description = "Fresh organic bananas",
                    ImageUrl = "https://images.pexels.com/photos/2872755/pexels-photo-2872755.jpeg?auto=compress&cs=tinysrgb&w=300",
                    InStock = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Whole Milk",
                    Category = "Dairy",
                    Price = 3.49m,
                    PointsPerDollar = 2.0m,
                    Description = "Fresh whole milk - 1 gallon",
                    ImageUrl = "https://images.pexels.com/photos/236010/pexels-photo-236010.jpeg?auto=compress&cs=tinysrgb&w=300",
                    InStock = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Artisan Bread",
                    Category = "Bakery",
                    Price = 4.99m,
                    PointsPerDollar = 1.5m,
                    Description = "Freshly baked artisan bread",
                    ImageUrl = "https://images.pexels.com/photos/1586942/pexels-photo-1586942.jpeg?auto=compress&cs=tinysrgb&w=300",
                    InStock = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Premium Coffee",
                    Category = "Beverages",
                    Price = 12.99m,
                    PointsPerDollar = 3.0m,
                    Description = "Premium roasted coffee beans",
                    ImageUrl = "https://images.pexels.com/photos/894695/pexels-photo-894695.jpeg?auto=compress&cs=tinysrgb&w=300",
                    InStock = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Organic Chicken",
                    Category = "Meat",
                    Price = 8.99m,
                    PointsPerDollar = 2.0m,
                    Description = "Fresh organic chicken breast",
                    ImageUrl = "https://images.pexels.com/photos/616354/pexels-photo-616354.jpeg?auto=compress&cs=tinysrgb&w=300",
                    InStock = true,
                    CreatedDate = DateTime.Now
                }
            };

            context.Products.AddRange(products);
        }

        private static async Task SeedRewardsAsync(ApplicationDbContext context)
        {
            var rewards = new[]
            {
                new Reward
                {
                    Name = "$5 Off Next Purchase",
                    Description = "Get $5 off your next purchase of $25 or more",
                    PointsCost = 500,
                    Category = "Discount",
                    ImageUrl = "https://images.pexels.com/photos/4386321/pexels-photo-4386321.jpeg?auto=compress&cs=tinysrgb&w=300",
                    Available = true,
                    CreatedDate = DateTime.Now
                },
                new Reward
                {
                    Name = "Free Coffee",
                    Description = "Get a free premium coffee bag",
                    PointsCost = 1000,
                    Category = "Free Product",
                    ImageUrl = "https://images.pexels.com/photos/894695/pexels-photo-894695.jpeg?auto=compress&cs=tinysrgb&w=300",
                    Available = true,
                    CreatedDate = DateTime.Now
                },
                new Reward
                {
                    Name = "VIP Shopping Experience",
                    Description = "Skip the line and get personal shopping assistance",
                    PointsCost = 2000,
                    Category = "Experience",
                    ImageUrl = "https://images.pexels.com/photos/1005644/pexels-photo-1005644.jpeg?auto=compress&cs=tinysrgb&w=300",
                    Available = true,
                    RequiredTier = MembershipTier.Gold,
                    CreatedDate = DateTime.Now
                }
            };

            context.Rewards.AddRange(rewards);
        }

        private static async Task SeedCampaignsAsync(ApplicationDbContext context)
        {
            var campaigns = new[]
            {
                new Campaign
                {
                    Name = "Holiday Bonus Points",
                    Description = "Earn double points on all purchases during the holiday season",
                    StartDate = DateTime.Now.AddDays(-30),
                    EndDate = DateTime.Now.AddDays(30),
                    BonusMultiplier = 2.0m,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Campaign
                {
                    Name = "Organic Produce Week",
                    Description = "Triple points on all organic produce items",
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now.AddDays(7),
                    BonusMultiplier = 3.0m,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            };

            context.Campaigns.AddRange(campaigns);
        }
    }
}