-- Use the new database
USE MasterPiece;
GO

-- Create EventCategories table
CREATE TABLE EventCategories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(255)
);
GO

-- Create EventTypes table
CREATE TABLE EventTypes (
    TypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(255)
);
GO

-- Create Users table (users can also be organizers)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE,
    PasswordHash NVARCHAR(255),
    Salt NVARCHAR(255),
    PhoneNumber NVARCHAR(20),
    Username NVARCHAR(255),
    Biography NVARCHAR(MAX),
    IsOrganizer BIT DEFAULT 0   -- 0 = Normal User, 1 = Organizer
);
GO

-- Create Plans table for organizing events
CREATE TABLE Plans (
    PlanID INT IDENTITY(1,1) PRIMARY KEY,
    PlanName NVARCHAR(255),
    DurationInDays INT,
    CommissionAmount DECIMAL(10, 2)
);
GO

-- Insert Plans data
INSERT INTO Plans (PlanName, DurationInDays, CommissionAmount)
VALUES 
    ('15 Days', 15, 50.00),
    ('1 Month', 30, 100.00),
    ('3 Months', 90, 250.00);
GO

-- Create Events table
CREATE TABLE Events (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255),
    Description NVARCHAR(MAX),
    EventDate DATE,
    StartTime TIME(7),
    EndTime TIME(7),
    Location NVARCHAR(255),
    TotalSeats INT,
    AvailableSeats INT,
    TicketPrice DECIMAL(10, 2),
    CategoryID INT,
    TypeID INT,
    OrganizerID INT,
    BannerURL NVARCHAR(255),
    Location_URL NVARCHAR(255),
    Status NVARCHAR(50) DEFAULT 'Pending',  -- Status for admin approval
    PlanID INT,  -- Foreign key for the selected plan
    FOREIGN KEY (CategoryID) REFERENCES EventCategories(CategoryID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION,
    FOREIGN KEY (OrganizerID) REFERENCES Users(UserID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION,
    FOREIGN KEY (TypeID) REFERENCES EventTypes(TypeID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION,
    FOREIGN KEY (PlanID) REFERENCES Plans(PlanID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION
);
GO

-- Create Tickets table
CREATE TABLE Tickets (
    TicketID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT,
    UserID INT,
    QRCode NVARCHAR(255) UNIQUE,  -- Unique QR code string
    TicketType NVARCHAR(50) CHECK (TicketType IN ('Free', 'Paid', 'Donation')),  -- Types of tickets
    DonationLimi DECIMAL(10, 2),  -- Amount for donation
    PurchaseDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Price DECIMAL(10, 2),
    FOREIGN KEY (EventID) REFERENCES Events(EventID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION
);
GO

-- Create Payments table for event payments
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT,
    UserID INT,
    TransactionID NVARCHAR(255),
    PaymentAmount DECIMAL(10, 2),
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EventID) REFERENCES Events(EventID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION
);
GO

-- Create Reviews table
CREATE TABLE Reviews (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT,
    UserID INT,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),  -- Rating between 1 and 5
    ReviewText NVARCHAR(MAX),
    ReviewDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EventID) REFERENCES Events(EventID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION
);
GO

-- Create EventSessions table
CREATE TABLE EventSessions (
    SessionID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT,
    SessionTime NVARCHAR(50),
    SessionTitle NVARCHAR(255),
    SessionDescription NVARCHAR(MAX),
    SessionType NVARCHAR(50),  -- Morning, Afternoon, Evening
    FOREIGN KEY (EventID) REFERENCES Events(EventID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION
);
GO

-- Create Announcements table
CREATE TABLE Announcements (
    AnnouncementID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT,
    OrganizerID INT,
    Message NVARCHAR(MAX),
    DateSent DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EventID) REFERENCES Events(EventID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION,
    FOREIGN KEY (OrganizerID) REFERENCES Users(UserID) 
        ON DELETE NO ACTION 
        ON UPDATE NO ACTION
);
GO

-- Use the MasterPiece database
USE MasterPiece;
GO

-- Create Speakers table
CREATE TABLE Speakers (
    SpeakerID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255),
    Role NVARCHAR(255),
    Bio NVARCHAR(MAX),  -- Brief biography/description
    FacebookURL NVARCHAR(255),  -- Social media links
    TwitterURL NVARCHAR(255),
    WhatsAppURL NVARCHAR(255),
    InstagramURL NVARCHAR(255),
    ProfileImageURL NVARCHAR(255)  -- URL for speaker's profile image
);
GO

-- Create EventSpeakers junction table
CREATE TABLE EventSpeakers (
    EventID INT,
    SpeakerID INT,
    PRIMARY KEY (EventID, SpeakerID),
    FOREIGN KEY (EventID) REFERENCES Events(EventID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (SpeakerID) REFERENCES Speakers(SpeakerID) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

ALTER TABLE Events
ADD ThumbnailURL NVARCHAR(255);
GO

ALTER TABLE Events
ADD ThumbnailURL NVARCHAR(255);
GO

-- Add new columns to the Events table
ALTER TABLE Events
ADD WhatToExpect NVARCHAR(MAX),
    Highlights NVARCHAR(MAX);
GO
