using MeetingRoomAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomAPI.Interfaces
{
    public interface IMeetingAttendeeService
    {
        Task<List<MeetingAttendee>> GetAllAttendeesAsync();
        Task<MeetingAttendee> GetAttendeeByIdAsync(int id);
        Task<MeetingAttendee> CreateAttendeeAsync(MeetingAttendee attendee);
        Task UpdateAttendeeAsync(MeetingAttendee attendee);
        Task DeleteAttendeeAsync(int id);
        Task<List<MeetingAttendee>> GetMeetingAttendeesAsync(int meetingId);
    }
} 