using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomAPI.Interfaces;
using MeetingRoomAPI.Models;

namespace MeetingRoomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeetingMinutesController : ControllerBase
    {
        private readonly IMeetingMinuteService _minuteService;

        public MeetingMinutesController(IMeetingMinuteService minuteService)
        {
            _minuteService = minuteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MeetingMinute>>> GetAllMinutes()
        {
            var minutes = await _minuteService.GetAllMinutesAsync();
            return Ok(minutes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingMinute>> GetMinute(int id)
        {
            var minute = await _minuteService.GetMinuteByIdAsync(id);
            if (minute == null) return NotFound();
            return Ok(minute);
        }

        [HttpPost]
        public async Task<ActionResult<MeetingMinute>> CreateMinute(MeetingMinute minute)
        {
            var createdMinute = await _minuteService.CreateMinuteAsync(minute);
            return CreatedAtAction(nameof(GetMinute), new { id = createdMinute.MinuteId }, createdMinute);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMinute(int id, MeetingMinute minute)
        {
            if (id != minute.MinuteId) return BadRequest();
            await _minuteService.UpdateMinuteAsync(minute);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMinute(int id)
        {
            await _minuteService.DeleteMinuteAsync(id);
            return NoContent();
        }

        [HttpGet("meeting/{meetingId}")]
        public async Task<ActionResult<List<MeetingMinute>>> GetMeetingMinutes(int meetingId)
        {
            var minutes = await _minuteService.GetMeetingMinutesAsync(meetingId);
            return Ok(minutes);
        }
    }
} 