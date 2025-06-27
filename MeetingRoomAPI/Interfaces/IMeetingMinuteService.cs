using MeetingRoomAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomAPI.Interfaces
{
    public interface IMeetingMinuteService
    {
        Task<List<MeetingMinute>> GetAllMinutesAsync();
        Task<MeetingMinute> GetMinuteByIdAsync(int id);
        Task<MeetingMinute> CreateMinuteAsync(MeetingMinute minute);
        Task UpdateMinuteAsync(MeetingMinute minute);
        Task DeleteMinuteAsync(int id);
        Task<List<MeetingMinute>> GetMeetingMinutesAsync(int meetingId);
    }
} 