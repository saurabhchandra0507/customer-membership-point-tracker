using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupermarketLoyalty.Core.DTOs;
using SupermarketLoyalty.Core.Interfaces;
using System.Security.Claims;

namespace SupermarketLoyalty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardsController : ControllerBase
    {
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RewardDto>>> GetAllRewards()
        {
            var rewards = await _rewardService.GetAllRewardsAsync();
            return Ok(rewards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RewardDto>> GetReward(int id)
        {
            var reward = await _rewardService.GetRewardByIdAsync(id);
            if (reward == null)
            {
                return NotFound();
            }

            return Ok(reward);
        }

        [HttpGet("available/{customerId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RewardDto>>> GetAvailableRewardsForCustomer(string customerId)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && currentUserId != customerId)
            {
                return Forbid();
            }

            var rewards = await _rewardService.GetAvailableRewardsForCustomerAsync(customerId);
            return Ok(rewards);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RewardDto>> CreateReward(CreateRewardDto createRewardDto)
        {
            var reward = await _rewardService.CreateRewardAsync(createRewardDto);
            return CreatedAtAction(nameof(GetReward), new { id = reward.Id }, reward);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RewardDto>> UpdateReward(int id, CreateRewardDto updateRewardDto)
        {
            var reward = await _rewardService.UpdateRewardAsync(id, updateRewardDto);
            if (reward == null)
            {
                return NotFound();
            }

            return Ok(reward);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReward(int id)
        {
            var result = await _rewardService.DeleteRewardAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("redeem")]
        [Authorize]
        public async Task<ActionResult<RewardRedemptionDto>> RedeemReward(RedeemRewardDto redeemRewardDto)
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(customerId))
            {
                return Unauthorized();
            }

            try
            {
                var redemption = await _rewardService.RedeemRewardAsync(customerId, redeemRewardDto);
                return Ok(redemption);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("redemptions/{customerId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RewardRedemptionDto>>> GetRedemptionHistory(string customerId)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && currentUserId != customerId)
            {
                return Forbid();
            }

            var redemptions = await _rewardService.GetRedemptionHistoryAsync(customerId);
            return Ok(redemptions);
        }

        [HttpGet("redemptions")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<RewardRedemptionDto>>> GetAllRedemptions()
        {
            var redemptions = await _rewardService.GetAllRedemptionsAsync();
            return Ok(redemptions);
        }
    }
}