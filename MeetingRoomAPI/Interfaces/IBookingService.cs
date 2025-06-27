using MeetingRoomAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomAPI.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
        Task<List<Booking>> GetUserBookingsAsync(int userId);
        Task<bool> CheckAvailabilityAsync(int roomId, DateTime startTime, DateTime endTime);
    }
} 