using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomAPI.Interfaces;
using MeetingRoomAPI.Models;

namespace MeetingRoomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeetingAttendeesController : ControllerBase
    {
        private readonly IMeetingAttendeeService _attendeeService;

        public MeetingAttendeesController(IMeetingAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MeetingAttendee>>> GetAllAttendees()
        {
            var attendees = await _attendeeService.GetAllAttendeesAsync();
            return Ok(attendees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingAttendee>> GetAttendee(int id)
        {
            var attendee = await _attendeeService.GetAttendeeByIdAsync(id);
            if (attendee == null) return NotFound();
            return Ok(attendee);
        }

        [HttpPost]
        public async Task<ActionResult<MeetingAttendee>> CreateAttendee(MeetingAttendee attendee)
        {
            var createdAttendee = await _attendeeService.CreateAttendeeAsync(attendee);
            return CreatedAtAction(nameof(GetAttendee), new { id = createdAttendee.AttendeeId }, createdAttendee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAttendee(int id, MeetingAttendee attendee)
        {
            if (id != attendee.AttendeeId) return BadRequest();
            await _attendeeService.UpdateAttendeeAsync(attendee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendee(int id)
        {
            await _attendeeService.DeleteAttendeeAsync(id);
            return NoContent();
        }

        [HttpGet("meeting/{meetingId}")]
        public async Task<ActionResult<List<MeetingAttendee>>> GetMeetingAttendees(int meetingId)
        {
            var attendees = await _attendeeService.GetMeetingAttendeesAsync(meetingId);
            return Ok(attendees);
        }
    }
} 