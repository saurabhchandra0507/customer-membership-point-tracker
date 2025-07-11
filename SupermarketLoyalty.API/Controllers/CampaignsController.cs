using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupermarketLoyalty.Core.DTOs;
using SupermarketLoyalty.Core.Interfaces;

namespace SupermarketLoyalty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetAllCampaigns()
        {
            var campaigns = await _campaignService.GetAllCampaignsAsync();
            return Ok(campaigns);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetActiveCampaigns()
        {
            var campaigns = await _campaignService.GetActiveCampaignsAsync();
            return Ok(campaigns);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDto>> GetCampaign(int id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CampaignDto>> CreateCampaign(CreateCampaignDto createCampaignDto)
        {
            var campaign = await _campaignService.CreateCampaignAsync(createCampaignDto);
            return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, campaign);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CampaignDto>> UpdateCampaign(int id, CreateCampaignDto updateCampaignDto)
        {
            var campaign = await _campaignService.UpdateCampaignAsync(id, updateCampaignDto);
            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var result = await _campaignService.DeleteCampaignAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{id}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleCampaignStatus(int id)
        {
            var result = await _campaignService.ToggleCampaignStatusAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}