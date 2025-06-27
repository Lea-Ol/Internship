using MeetingRoomAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomAPI.Interfaces
{
    public interface IMeetingService
    {
        Task<List<Meeting>> GetAllMeetingsAsync();
        Task<Meeting> GetMeetingByIdAsync(int id);
        Task<Meeting> CreateMeetingAsync(Meeting meeting);
        Task UpdateMeetingAsync(Meeting meeting);
        Task DeleteMeetingAsync(int id);
        Task<List<Meeting>> GetUserMeetingsAsync(int userId);
    }
} 