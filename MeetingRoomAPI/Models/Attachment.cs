namespace MeetingRoomAPI.Models
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public int MeetingId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public int FileSize { get; set; }
        public int UploadedByUserId { get; set; }
        public DateTime UploadedAt { get; set; }
    }
} 