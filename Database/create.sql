CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    Email NVARCHAR(255),
    PasswordHash NVARCHAR(255),
    Salt NVARCHAR(255),
    PhoneNumber NVARCHAR(20),
    Username NVARCHAR(255),
    Biography NVARCHAR(MAX)
);


CREATE TABLE EventCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(255)
);
CREATE TABLE EventTypes (
    TypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(255)
);

CREATE TABLE Events (
    EventID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255),
    Description NVARCHAR(MAX),
    Date DATE,
    StartTime TIME,
    EndTime TIME,
    Location NVARCHAR(255),
    TotalSeats INT,
    AvailableSeats INT,
    TicketPrice DECIMAL(10, 2),
    CategoryID INT,
    TypeID INT,
    OrganizerID INT,  -- References UserID in Users Table
    BannerURL NVARCHAR(255),
    FOREIGN KEY (OrganizerID) REFERENCES Users(UserID),
    FOREIGN KEY (CategoryID) REFERENCES EventCategories(CategoryID),
    FOREIGN KEY (TypeID) REFERENCES EventTypes(TypeID)
);

CREATE TABLE Tickets (
    TicketID INT PRIMARY KEY IDENTITY(1,1),
    EventID INT,
    UserID INT,
    PurchaseDate DATETIME,
    Price DECIMAL(10, 2),
    FOREIGN KEY (EventID) REFERENCES Events(EventID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);