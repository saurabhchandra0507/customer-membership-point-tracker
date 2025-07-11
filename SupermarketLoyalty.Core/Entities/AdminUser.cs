using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SupermarketLoyalty.Core.Entities
{
    public class AdminUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public AdminRole Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public enum AdminRole
    {
        Admin = 0,
        Manager = 1
    }
}