namespace MeetingRoomAPI.Models
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        public int BookingId { get; set; }
        public string Title { get; set; }
        public string Agenda { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledStart { get; set; }
        public DateTime ScheduledEnd { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualEnd { get; set; }
        public MeetingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 