using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingRoomAPI.Models
{
    [Table("ActionItems")]
    public class ActionItem
    {
        [Key]
        public int ActionId { get; set; }
        public int MinuteId { get; set; }
        public int AssignedToUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public ActionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 