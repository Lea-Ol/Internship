using MeetingRoomAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomAPI.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int id);
        Task<List<DateTime>> GetRoomAvailabilityAsync(int roomId, DateTime date);
    }
} 