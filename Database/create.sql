-- Create the database
CREATE DATABASE MasterPiece;
GO

-- Use the new database
USE MasterPiece;
GO

-- Create EventCategories table
CREATE TABLE EventCategories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(255)
);
GO

-- Create Events table
CREATE TABLE Events (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255),
    Description NVARCHAR(MAX),
    Date DATE,
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
    location_url NVARCHAR(255),
    FOREIGN KEY (CategoryID) REFERENCES EventCategories(CategoryID),
    FOREIGN KEY (OrganizerID) REFERENCES Users(UserID),
    FOREIGN KEY (TypeID) REFERENCES EventTypes(TypeID)
);
GO

-- Create EventTypes table
CREATE TABLE EventTypes (
    TypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(255)
);
GO

-- Create Tickets table
CREATE TABLE Tickets (
    TicketID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT,
    UserID INT,
    PurchaseDate DATETIME,
    Price DECIMAL(10, 2),
    FOREIGN KEY (EventID) REFERENCES Events(EventID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Create Users table
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    Email NVARCHAR(255),
    PasswordHash NVARCHAR(255),
    Salt NVARCHAR(255),
    PhoneNumber NVARCHAR(20),
    Username NVARCHAR(255),
    Biography NVARCHAR(MAX)
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
);
GO

