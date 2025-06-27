using MeetingRoomAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomAPI.Interfaces
{
    public interface IAttachmentService
    {
        Task<List<Attachment>> GetAllAttachmentsAsync();
        Task<Attachment> GetAttachmentByIdAsync(int id);
        Task<Attachment> CreateAttachmentAsync(Attachment attachment);
        Task UpdateAttachmentAsync(Attachment attachment);
        Task DeleteAttachmentAsync(int id);
        Task<List<Attachment>> GetMeetingAttachmentsAsync(int meetingId);
    }
} 