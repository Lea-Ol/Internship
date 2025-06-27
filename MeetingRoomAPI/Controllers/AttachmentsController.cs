using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomAPI.Interfaces;
using MeetingRoomAPI.Models;

namespace MeetingRoomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Attachment>>> GetAllAttachments()
        {
            var attachments = await _attachmentService.GetAllAttachmentsAsync();
            return Ok(attachments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attachment>> GetAttachment(int id)
        {
            var attachment = await _attachmentService.GetAttachmentByIdAsync(id);
            if (attachment == null) return NotFound();
            return Ok(attachment);
        }

        [HttpPost]
        public async Task<ActionResult<Attachment>> CreateAttachment(Attachment attachment)
        {
            var createdAttachment = await _attachmentService.CreateAttachmentAsync(attachment);
            return CreatedAtAction(nameof(GetAttachment), new { id = createdAttachment.AttachmentId }, createdAttachment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAttachment(int id, Attachment attachment)
        {
            if (id != attachment.AttachmentId) return BadRequest();
            await _attachmentService.UpdateAttachmentAsync(attachment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttachment(int id)
        {
            await _attachmentService.DeleteAttachmentAsync(id);
            return NoContent();
        }

        [HttpGet("meeting/{meetingId}")]
        public async Task<ActionResult<List<Attachment>>> GetMeetingAttachments(int meetingId)
        {
            var attachments = await _attachmentService.GetMeetingAttachmentsAsync(meetingId);
            return Ok(attachments);
        }
    }
} 