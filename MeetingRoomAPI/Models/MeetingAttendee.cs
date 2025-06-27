namespace MeetingRoomAPI.Models
{
    public class MeetingAttendee
    {
        public int AttendeeId { get; set; }
        public int MeetingId { get; set; }
        public int UserId { get; set; }
        public AttendeeStatus Status { get; set; }
        public AttendeeRole Role { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 