using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomAPI.Interfaces;
using MeetingRoomAPI.Models;

namespace MeetingRoomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActionItemsController : ControllerBase
    {
        private readonly IActionItemService _actionService;

        public ActionItemsController(IActionItemService actionService)
        {
            _actionService = actionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActionItem>>> GetAllActionItems()
        {
            var actions = await _actionService.GetAllActionItemsAsync();
            return Ok(actions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActionItem>> GetActionItem(int id)
        {
            var action = await _actionService.GetActionItemByIdAsync(id);
            if (action == null) return NotFound();
            return Ok(action);
        }

        [HttpPost]
        public async Task<ActionResult<ActionItem>> CreateActionItem(ActionItem action)
        {
            var createdAction = await _actionService.CreateActionItemAsync(action);
            return CreatedAtAction(nameof(GetActionItem), new { id = createdAction.ActionId }, createdAction);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActionItem(int id, ActionItem action)
        {
            if (id != action.ActionId) return BadRequest();
            await _actionService.UpdateActionItemAsync(action);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActionItem(int id)
        {
            await _actionService.DeleteActionItemAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<ActionItem>>> GetUserActionItems(int userId)
        {
            var actions = await _actionService.GetUserActionItemsAsync(userId);
            return Ok(actions);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<List<ActionItem>>> GetOverdueActionItems()
        {
            var actions = await _actionService.GetOverdueActionItemsAsync();
            return Ok(actions);
        }
    }
} 