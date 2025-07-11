using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SupermarketLoyalty.Core.DTOs;
using SupermarketLoyalty.Core.Entities;
using SupermarketLoyalty.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SupermarketLoyalty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;

        public AuthController(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            IConfiguration configuration,
            ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _customerService = customerService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = await GenerateJwtToken(user);
            var customerDto = await _customerService.GetCustomerByIdAsync(user.Id);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
                Customer = customerDto,
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest("User with this email already exists");
            }

            var customer = new Customer
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                MembershipNumber = GenerateMembershipNumber(),
                JoinDate = DateTime.UtcNow,
                TotalPoints = 0,
                LifetimePoints = 0,
                Tier = MembershipTier.Bronze,
                IsActive = true,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(customer, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(customer, "Customer");

            var token = await GenerateJwtToken(customer);
            var customerDto = await _customerService.GetCustomerByIdAsync(customer.Id);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
                Customer = customerDto,
                IsAdmin = false
            });
        }

        private async Task<string> GenerateJwtToken(Customer user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, user.Name),
                new("MembershipNumber", user.MembershipNumber)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateMembershipNumber()
        {
            var prefix = "SM";
            var number = new Random().Next(100000, 999999);
            return $"{prefix}{number:D6}";
        }
    }
}