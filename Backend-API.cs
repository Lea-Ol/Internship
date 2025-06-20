public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class Room
{
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

public class Booking
{
    public int BookingId { get; set; }
    public int RoomId { get; set; }
    public int BookedByUserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public BookingStatus Status { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

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

public class MeetingMinute
{
    public int MinuteId { get; set; }
    public int MeetingId { get; set; }
    public int CreatedByUserId { get; set; }
    public string DiscussionPoints { get; set; }
    public string DecisionsMade { get; set; }
    public string NextSteps { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class ActionItem
{
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

public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
}

public enum UserRole { Admin, Employee, Guest }
public enum BookingStatus { Confirmed, Cancelled, Completed }
public enum MeetingStatus { Scheduled, InProgress, Completed, Cancelled }
public enum AttendeeStatus { Invited, Accepted, Declined, Tentative }
public enum AttendeeRole { Organizer, Required, Optional }
public enum Priority { Low, Medium, High }
public enum ActionStatus { Open, InProgress, Completed, Cancelled }
public enum NotificationType { MeetingInvite, BookingConfirmation, ActionItem, Reminder }

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, User user)
    {
        if (id != user.UserId) return BadRequest();
        await _userService.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}


[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Room>>> GetAllRooms()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> GetRoom(int id)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom(Room room)
    {
        var createdRoom = await _roomService.CreateRoomAsync(room);
        return CreatedAtAction(nameof(GetRoom), new { id = createdRoom.RoomId }, createdRoom);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRoom(int id, Room room)
    {
        if (id != room.RoomId) return BadRequest();
        await _roomService.UpdateRoomAsync(room);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRoom(int id)
    {
        await _roomService.DeleteRoomAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/availability")]
    public async Task<ActionResult<List<DateTime>>> GetRoomAvailability(int id, DateTime date)
    {
        var availability = await _roomService.GetRoomAvailabilityAsync(id, date);
        return Ok(availability);
    }
}


[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Booking>>> GetAllBookings()
    {
        var bookings = await _bookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetBooking(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        if (booking == null) return NotFound();
        return Ok(booking);
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
    {
        var createdBooking = await _bookingService.CreateBookingAsync(booking);
        return CreatedAtAction(nameof(GetBooking), new { id = createdBooking.BookingId }, createdBooking);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBooking(int id, Booking booking)
    {
        if (id != booking.BookingId) return BadRequest();
        await _bookingService.UpdateBookingAsync(booking);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBooking(int id)
    {
        await _bookingService.DeleteBookingAsync(id);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Booking>>> GetUserBookings(int userId)
    {
        var bookings = await _bookingService.GetUserBookingsAsync(userId);
        return Ok(bookings);
    }

    [HttpPost("check-availability")]
    public async Task<ActionResult<bool>> CheckAvailability(int roomId, DateTime startTime, DateTime endTime)
    {
        var isAvailable = await _bookingService.CheckAvailabilityAsync(roomId, startTime, endTime);
        return Ok(isAvailable);
    }
}

[ApiController]
[Route("api/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly IMeetingService _meetingService;

    public MeetingsController(IMeetingService meetingService)
    {
        _meetingService = meetingService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Meeting>>> GetAllMeetings()
    {
        var meetings = await _meetingService.GetAllMeetingsAsync();
        return Ok(meetings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Meeting>> GetMeeting(int id)
    {
        var meeting = await _meetingService.GetMeetingByIdAsync(id);
        if (meeting == null) return NotFound();
        return Ok(meeting);
    }

    [HttpPost]
    public async Task<ActionResult<Meeting>> CreateMeeting(Meeting meeting)
    {
        var createdMeeting = await _meetingService.CreateMeetingAsync(meeting);
        return CreatedAtAction(nameof(GetMeeting), new { id = createdMeeting.MeetingId }, createdMeeting);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMeeting(int id, Meeting meeting)
    {
        if (id != meeting.MeetingId) return BadRequest();
        await _meetingService.UpdateMeetingAsync(meeting);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMeeting(int id)
    {
        await _meetingService.DeleteMeetingAsync(id);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Meeting>>> GetUserMeetings(int userId)
    {
        var meetings = await _meetingService.GetUserMeetingsAsync(userId);
        return Ok(meetings);
    }
}

[ApiController]
[Route("api/[controller]")]
public class MeetingAttendeesController : ControllerBase
{
    private readonly IMeetingAttendeeService _attendeeService;

    public MeetingAttendeesController(IMeetingAttendeeService attendeeService)
    {
        _attendeeService = attendeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MeetingAttendee>>> GetAllAttendees()
    {
        var attendees = await _attendeeService.GetAllAttendeesAsync();
        return Ok(attendees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MeetingAttendee>> GetAttendee(int id)
    {
        var attendee = await _attendeeService.GetAttendeeByIdAsync(id);
        if (attendee == null) return NotFound();
        return Ok(attendee);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingAttendee>> CreateAttendee(MeetingAttendee attendee)
    {
        var createdAttendee = await _attendeeService.CreateAttendeeAsync(attendee);
        return CreatedAtAction(nameof(GetAttendee), new { id = createdAttendee.AttendeeId }, createdAttendee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAttendee(int id, MeetingAttendee attendee)
    {
        if (id != attendee.AttendeeId) return BadRequest();
        await _attendeeService.UpdateAttendeeAsync(attendee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAttendee(int id)
    {
        await _attendeeService.DeleteAttendeeAsync(id);
        return NoContent();
    }

    [HttpGet("meeting/{meetingId}")]
    public async Task<ActionResult<List<MeetingAttendee>>> GetMeetingAttendees(int meetingId)
    {
        var attendees = await _attendeeService.GetMeetingAttendeesAsync(meetingId);
        return Ok(attendees);
    }
}


[ApiController]
[Route("api/[controller]")]
public class MeetingMinutesController : ControllerBase
{
    private readonly IMeetingMinuteService _minuteService;

    public MeetingMinutesController(IMeetingMinuteService minuteService)
    {
        _minuteService = minuteService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MeetingMinute>>> GetAllMinutes()
    {
        var minutes = await _minuteService.GetAllMinutesAsync();
        return Ok(minutes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MeetingMinute>> GetMinute(int id)
    {
        var minute = await _minuteService.GetMinuteByIdAsync(id);
        if (minute == null) return NotFound();
        return Ok(minute);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingMinute>> CreateMinute(MeetingMinute minute)
    {
        var createdMinute = await _minuteService.CreateMinuteAsync(minute);
        return CreatedAtAction(nameof(GetMinute), new { id = createdMinute.MinuteId }, createdMinute);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMinute(int id, MeetingMinute minute)
    {
        if (id != minute.MinuteId) return BadRequest();
        await _minuteService.UpdateMinuteAsync(minute);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMinute(int id)
    {
        await _minuteService.DeleteMinuteAsync(id);
        return NoContent();
    }

    [HttpGet("meeting/{meetingId}")]
    public async Task<ActionResult<List<MeetingMinute>>> GetMeetingMinutes(int meetingId)
    {
        var minutes = await _minuteService.GetMeetingMinutesAsync(meetingId);
        return Ok(minutes);
    }
}


[ApiController]
[Route("api/[controller]")]
public class ActionItemsController : ControllerBase
{
    private readonly IActionItemService _actionService;

    public ActionItemsController(IActionItemService actionService)
    {
        _actionService = actionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActionItem>>> GetAllActionItems()
    {
        var actions = await _actionService.GetAllActionItemsAsync();
        return Ok(actions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActionItem>> GetActionItem(int id)
    {
        var action = await _actionService.GetActionItemByIdAsync(id);
        if (action == null) return NotFound();
        return Ok(action);
    }

    [HttpPost]
    public async Task<ActionResult<ActionItem>> CreateActionItem(ActionItem action)
    {
        var createdAction = await _actionService.CreateActionItemAsync(action);
        return CreatedAtAction(nameof(GetActionItem), new { id = createdAction.ActionId }, createdAction);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateActionItem(int id, ActionItem action)
    {
        if (id != action.ActionId) return BadRequest();
        await _actionService.UpdateActionItemAsync(action);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActionItem(int id)
    {
        await _actionService.DeleteActionItemAsync(id);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<ActionItem>>> GetUserActionItems(int userId)
    {
        var actions = await _actionService.GetUserActionItemsAsync(userId);
        return Ok(actions);
    }

    [HttpGet("overdue")]
    public async Task<ActionResult<List<ActionItem>>> GetOverdueActionItems()
    {
        var actions = await _actionService.GetOverdueActionItemsAsync();
        return Ok(actions);
    }
}

[ApiController]
[Route("api/[controller]")]
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

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Notification>>> GetAllNotifications()
    {
        var notifications = await _notificationService.GetAllNotificationsAsync();
        return Ok(notifications);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Notification>> GetNotification(int id)
    {
        var notification = await _notificationService.GetNotificationByIdAsync(id);
        if (notification == null) return NotFound();
        return Ok(notification);
    }

    [HttpPost]
    public async Task<ActionResult<Notification>> CreateNotification(Notification notification)
    {
        var createdNotification = await _notificationService.CreateNotificationAsync(notification);
        return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.NotificationId }, createdNotification);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateNotification(int id, Notification notification)
    {
        if (id != notification.NotificationId) return BadRequest();
        await _notificationService.UpdateNotificationAsync(notification);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNotification(int id)
    {
        await _notificationService.DeleteNotificationAsync(id);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Notification>>> GetUserNotifications(int userId)
    {
        var notifications = await _notificationService.GetUserNotificationsAsync(userId);
        return Ok(notifications);
    }

    [HttpPut("{id}/mark-read")]
    public async Task<ActionResult> MarkAsRead(int id)
    {
        await _notificationService.MarkAsReadAsync(id);
        return NoContent();
    }
}

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}

public interface IRoomService
{
    Task<List<Room>> GetAllRoomsAsync();
    Task<Room> GetRoomByIdAsync(int id);
    Task<Room> CreateRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task DeleteRoomAsync(int id);
    Task<List<DateTime>> GetRoomAvailabilityAsync(int roomId, DateTime date);
}

public interface IBookingService
{
    Task<List<Booking>> GetAllBookingsAsync();
    Task<Booking> GetBookingByIdAsync(int id);
    Task<Booking> CreateBookingAsync(Booking booking);
    Task UpdateBookingAsync(Booking booking);
    Task DeleteBookingAsync(int id);
    Task<List<Booking>> GetUserBookingsAsync(int userId);
    Task<bool> CheckAvailabilityAsync(int roomId, DateTime startTime, DateTime endTime);
}

public interface IMeetingService
{
    Task<List<Meeting>> GetAllMeetingsAsync();
    Task<Meeting> GetMeetingByIdAsync(int id);
    Task<Meeting> CreateMeetingAsync(Meeting meeting);
    Task UpdateMeetingAsync(Meeting meeting);
    Task DeleteMeetingAsync(int id);
    Task<List<Meeting>> GetUserMeetingsAsync(int userId);
}

public interface IMeetingAttendeeService
{
    Task<List<MeetingAttendee>> GetAllAttendeesAsync();
    Task<MeetingAttendee> GetAttendeeByIdAsync(int id);
    Task<MeetingAttendee> CreateAttendeeAsync(MeetingAttendee attendee);
    Task UpdateAttendeeAsync(MeetingAttendee attendee);
    Task DeleteAttendeeAsync(int id);
    Task<List<MeetingAttendee>> GetMeetingAttendeesAsync(int meetingId);
}

public interface IMeetingMinuteService
{
    Task<List<MeetingMinute>> GetAllMinutesAsync();
    Task<MeetingMinute> GetMinuteByIdAsync(int id);
    Task<MeetingMinute> CreateMinuteAsync(MeetingMinute minute);
    Task UpdateMinuteAsync(MeetingMinute minute);
    Task DeleteMinuteAsync(int id);
    Task<List<MeetingMinute>> GetMeetingMinutesAsync(int meetingId);
}

public interface IActionItemService
{
    Task<List<ActionItem>> GetAllActionItemsAsync();
    Task<ActionItem> GetActionItemByIdAsync(int id);
    Task<ActionItem> CreateActionItemAsync(ActionItem action);
    Task UpdateActionItemAsync(ActionItem action);
    Task DeleteActionItemAsync(int id);
    Task<List<ActionItem>> GetUserActionItemsAsync(int userId);
    Task<List<ActionItem>> GetOverdueActionItemsAsync();
}

public interface IAttachmentService
{
    Task<List<Attachment>> GetAllAttachmentsAsync();
    Task<Attachment> GetAttachmentByIdAsync(int id);
    Task<Attachment> CreateAttachmentAsync(Attachment attachment);
    Task UpdateAttachmentAsync(Attachment attachment);
    Task DeleteAttachmentAsync(int id);
    Task<List<Attachment>> GetMeetingAttachmentsAsync(int meetingId);
}

public interface INotificationService
{
    Task<List<Notification>> GetAllNotificationsAsync();
    Task<Notification> GetNotificationByIdAsync(int id);
    Task<Notification> CreateNotificationAsync(Notification notification);
    Task UpdateNotificationAsync(Notification notification);
    Task DeleteNotificationAsync(int id);
    Task<List<Notification>> GetUserNotificationsAsync(int userId);
    Task MarkAsReadAsync(int id);
}