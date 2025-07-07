using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingRoomAPI.Models
{
    [Table("MeetingMinutes")]
    public class MeetingMinute
    {
        [Key]
        public int MinuteId { get; set; }
        public int MeetingId { get; set; }
        public int CreatedByUserId { get; set; }
        public string DiscussionPoints { get; set; }
        public string DecisionsMade { get; set; }
        public string NextSteps { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 