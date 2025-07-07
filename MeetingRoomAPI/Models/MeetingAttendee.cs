using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingRoomAPI.Models
{
    [Table("MeetingAttendees")]
    public class MeetingAttendee
    {
        [Key]
        public int AttendeeId { get; set; }
        public int MeetingId { get; set; }
        public int UserId { get; set; }
        public AttendeeStatus Status { get; set; }
        public AttendeeRole Role { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 