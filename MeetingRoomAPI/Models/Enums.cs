namespace MeetingRoomAPI.Models
{
    public enum UserRole { Admin, Employee, Guest }
    public enum BookingStatus { Confirmed, Cancelled, Completed }
    public enum MeetingStatus { Scheduled, InProgress, Completed, Cancelled }
    public enum AttendeeStatus { Invited, Accepted, Declined, Tentative }
    public enum AttendeeRole { Organizer, Required, Optional }
    public enum Priority { Low, Medium, High }
    public enum ActionStatus { Open, InProgress, Completed, Cancelled }
    public enum NotificationType { MeetingInvite, BookingConfirmation, ActionItem, Reminder }
} 