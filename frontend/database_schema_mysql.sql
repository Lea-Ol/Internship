DROP TABLE IF EXISTS NotificationType;
DROP TABLE IF EXISTS ActionStatus;
DROP TABLE IF EXISTS Priority;
DROP TABLE IF EXISTS AttendeeRole;
DROP TABLE IF EXISTS AttendeeStatus;
DROP TABLE IF EXISTS MeetingStatus;
DROP TABLE IF EXISTS BookingStatus;
DROP TABLE IF EXISTS UserRole;
DROP TABLE IF EXISTS Notifications;
DROP TABLE IF EXISTS Attachments;
DROP TABLE IF EXISTS ActionItems;
DROP TABLE IF EXISTS MeetingMinutes;
DROP TABLE IF EXISTS MeetingAttendees;
DROP TABLE IF EXISTS Meetings;
DROP TABLE IF EXISTS Bookings;
DROP TABLE IF EXISTS Rooms;
DROP TABLE IF EXISTS Users;

CREATE TABLE UserRole (
    Id INT PRIMARY KEY,
    Name VARCHAR(20) NOT NULL
);
INSERT INTO UserRole VALUES (0, 'Admin'), (1, 'Employee'), (2, 'Guest');

CREATE TABLE BookingStatus (
    Id INT PRIMARY KEY,
    Name VARCHAR(20) NOT NULL
);
INSERT INTO BookingStatus VALUES (0, 'Confirmed'), (1, 'Cancelled'), (2, 'Completed');

CREATE TABLE MeetingStatus (
    Id INT PRIMARY KEY,
    Name VARCHAR(20) NOT NULL
);
INSERT INTO MeetingStatus VALUES (0, 'Scheduled'), (1, 'InProgress'), (2, 'Completed'), (3, 'Cancelled');

CREATE TABLE AttendeeStatus (
    Id INT PRIMARY KEY,
    Name VARCHAR(20) NOT NULL
);
INSERT INTO AttendeeStatus VALUES (0, 'Invited'), (1, 'Accepted'), (2, 'Declined'), (3, 'Tentative');

CREATE TABLE AttendeeRole (
    Id INT PRIMARY KEY,
    Name VARCHAR(20) NOT NULL
);
INSERT INTO AttendeeRole VALUES (0, 'Organizer'), (1, 'Required'), (2, 'Optional');

CREATE TABLE Priority (
    Id INT PRIMARY KEY,
    Name VARCHAR(20) NOT NULL
);
INSERT INTO Priority VALUES (0, 'Low'), (1, 'Medium'), (2, 'High');

CREATE TABLE ActionStatus (
    Id INT PRIMARY KEY,
    Name VARCHAR(20) NOT NULL
);
INSERT INTO ActionStatus VALUES (0, 'Open'), (1, 'InProgress'), (2, 'Completed'), (3, 'Cancelled');

CREATE TABLE NotificationType (
    Id INT PRIMARY KEY,
    Name VARCHAR(30) NOT NULL
);
INSERT INTO NotificationType VALUES (0, 'MeetingInvite'), (1, 'BookingConfirmation'), (2, 'ActionItem'), (3, 'Reminder');


DROP TABLE IF EXISTS Users;
CREATE TABLE Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(191) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Phone VARCHAR(30),
    Role INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    IsActive TINYINT(1) NOT NULL,
    FOREIGN KEY (Role) REFERENCES UserRole(Id)
);


DROP TABLE IF EXISTS Rooms;
CREATE TABLE Rooms (
    RoomId INT AUTO_INCREMENT PRIMARY KEY,
    RoomName VARCHAR(100) NOT NULL,
    Location VARCHAR(100),
    Capacity INT NOT NULL,
    Description VARCHAR(255),
    HasProjector TINYINT(1) NOT NULL,
    HasVideoConference TINYINT(1) NOT NULL,
    HasWhiteboard TINYINT(1) NOT NULL,
    OtherFeatures VARCHAR(255),
    IsActive TINYINT(1) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL
);


DROP TABLE IF EXISTS Bookings;
CREATE TABLE Bookings (
    BookingId INT AUTO_INCREMENT PRIMARY KEY,
    RoomId INT NOT NULL,
    BookedByUserId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Status INT NOT NULL,
    Notes VARCHAR(255),
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    FOREIGN KEY (RoomId) REFERENCES Rooms(RoomId),
    FOREIGN KEY (BookedByUserId) REFERENCES Users(UserId),
    FOREIGN KEY (Status) REFERENCES BookingStatus(Id)
);


DROP TABLE IF EXISTS Meetings;
CREATE TABLE Meetings (
    MeetingId INT AUTO_INCREMENT PRIMARY KEY,
    BookingId INT NOT NULL,
    Title VARCHAR(255) NOT NULL,
    Agenda TEXT,
    Description TEXT,
    ScheduledStart DATETIME NOT NULL,
    ScheduledEnd DATETIME NOT NULL,
    ActualStart DATETIME,
    ActualEnd DATETIME,
    Status INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId),
    FOREIGN KEY (Status) REFERENCES MeetingStatus(Id)
);


DROP TABLE IF EXISTS MeetingAttendees;
CREATE TABLE MeetingAttendees (
    AttendeeId INT AUTO_INCREMENT PRIMARY KEY,
    MeetingId INT NOT NULL,
    UserId INT NOT NULL,
    Status INT NOT NULL,
    Role INT NOT NULL,
    ResponseDate DATETIME,
    CreatedAt DATETIME NOT NULL,
    FOREIGN KEY (MeetingId) REFERENCES Meetings(MeetingId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (Status) REFERENCES AttendeeStatus(Id),
    FOREIGN KEY (Role) REFERENCES AttendeeRole(Id)
);


DROP TABLE IF EXISTS MeetingMinutes;
CREATE TABLE MeetingMinutes (
    MinuteId INT AUTO_INCREMENT PRIMARY KEY,
    MeetingId INT NOT NULL,
    CreatedByUserId INT NOT NULL,
    DiscussionPoints TEXT,
    DecisionsMade TEXT,
    NextSteps TEXT,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    FOREIGN KEY (MeetingId) REFERENCES Meetings(MeetingId),
    FOREIGN KEY (CreatedByUserId) REFERENCES Users(UserId)
);


DROP TABLE IF EXISTS ActionItems;
CREATE TABLE ActionItems (
    ActionId INT AUTO_INCREMENT PRIMARY KEY,
    MinuteId INT NOT NULL,
    AssignedToUserId INT NOT NULL,
    Title VARCHAR(255) NOT NULL,
    Description TEXT,
    DueDate DATETIME NOT NULL,
    Priority INT NOT NULL,
    Status INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    FOREIGN KEY (MinuteId) REFERENCES MeetingMinutes(MinuteId),
    FOREIGN KEY (AssignedToUserId) REFERENCES Users(UserId),
    FOREIGN KEY (Priority) REFERENCES Priority(Id),
    FOREIGN KEY (Status) REFERENCES ActionStatus(Id)
);


DROP TABLE IF EXISTS Attachments;
CREATE TABLE Attachments (
    AttachmentId INT AUTO_INCREMENT PRIMARY KEY,
    MeetingId INT NOT NULL,
    FileName VARCHAR(255) NOT NULL,
    FilePath VARCHAR(255) NOT NULL,
    FileType VARCHAR(50),
    FileSize INT,
    UploadedByUserId INT NOT NULL,
    UploadedAt DATETIME NOT NULL,
    FOREIGN KEY (MeetingId) REFERENCES Meetings(MeetingId),
    FOREIGN KEY (UploadedByUserId) REFERENCES Users(UserId)
);


DROP TABLE IF EXISTS Notifications;
CREATE TABLE Notifications (
    NotificationId INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    Title VARCHAR(255) NOT NULL,
    Message TEXT,
    Type INT NOT NULL,
    IsRead TINYINT(1) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    ReadAt DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (Type) REFERENCES NotificationType(Id)
); 