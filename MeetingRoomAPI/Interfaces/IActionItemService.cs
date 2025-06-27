using MeetingRoomAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomAPI.Interfaces
{
    public interface IActionItemService
    {
        Task<List<ActionItem>> GetAllActionItemsAsync();
        Task<ActionItem> GetActionItemByIdAsync(int id);
        Task<ActionItem> CreateActionItemAsync(ActionItem action);
        Task UpdateActionItemAsync(ActionItem action);
        Task DeleteActionItemAsync(int id);
        Task<List<ActionItem>> GetUserActionItemsAsync(int userId);
        Task<List<ActionItem>> GetOverdueActionItemsAsync();
    }
} 