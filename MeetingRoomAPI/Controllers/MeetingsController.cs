using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomAPI.Interfaces;
using MeetingRoomAPI.Models;

namespace MeetingRoomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingsController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Meeting>>> GetAllMeetings()
        {
            var meetings = await _meetingService.GetAllMeetingsAsync();
            return Ok(meetings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meeting>> GetMeeting(int id)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id);
            if (meeting == null) return NotFound();
            return Ok(meeting);
        }

        [HttpPost]
        public async Task<ActionResult<Meeting>> CreateMeeting(Meeting meeting)
        {
            var createdMeeting = await _meetingService.CreateMeetingAsync(meeting);
            return CreatedAtAction(nameof(GetMeeting), new { id = createdMeeting.MeetingId }, createdMeeting);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMeeting(int id, Meeting meeting)
        {
            if (id != meeting.MeetingId) return BadRequest();
            await _meetingService.UpdateMeetingAsync(meeting);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeeting(int id)
        {
            await _meetingService.DeleteMeetingAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Meeting>>> GetUserMeetings(int userId)
        {
            var meetings = await _meetingService.GetUserMeetingsAsync(userId);
            return Ok(meetings);
        }
    }
} 