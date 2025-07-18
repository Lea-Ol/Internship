using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingRoomAPI.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public bool HasProjector { get; set; }
        public bool HasVideoConference { get; set; }
        public bool HasWhiteboard { get; set; }
        public string OtherFeatures { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 