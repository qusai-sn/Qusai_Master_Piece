INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Salt, PhoneNumber, Username, Biography)
VALUES ('Qusai', 'Omar', 'qusayomar20@gmail.com', 'hashedPassword', 'randomSalt', '1234567890', 'qusaiOmar', 'Passionate about event planning and management.');

INSERT INTO EventCategories (CategoryName) VALUES ('Business');
INSERT INTO EventCategories (CategoryName) VALUES ('Technology');
INSERT INTO EventCategories (CategoryName) VALUES ('Health & Wellness');

INSERT INTO EventTypes (TypeName) VALUES ('Conference');
INSERT INTO EventTypes (TypeName) VALUES ('Workshop');
INSERT INTO EventTypes (TypeName) VALUES ('Seminar');

-- Example: Assuming CategoryID 1 is for Business, TypeID 1 for Conference, OrganizerID 1 is Qusai Omar
INSERT INTO Events (Title, Description, Date, StartTime, EndTime, Location, TotalSeats, AvailableSeats, TicketPrice, CategoryID, TypeID, OrganizerID, BannerURL)
VALUES ('Annual Business Conference', 'A comprehensive conference covering all aspects of business management and innovation.', '2024-10-05', '09:00:00', '17:00:00', 'Convention Center, Downtown', 300, 300, 200.00, 1, 1, 1, 'http://example.com/banner.jpg');

-- Example: Assuming EventID 1 for the Annual Business Conference, UserID 1 for Qusai Omar
INSERT INTO Tickets (EventID, UserID, PurchaseDate, Price)
VALUES (1, 1, '2024-09-01 12:00:00', 200.00);

 INSERT INTO Events (Title, Description, Date, StartTime, EndTime, Location, TotalSeats, AvailableSeats, TicketPrice, CategoryID, TypeID, OrganizerID, BannerURL)
VALUES ('Digital Marketing Masterclass', 'An advanced masterclass on the strategies and tools used in modern digital marketing.', '2025-01-20', '09:30:00', '16:30:00', 'Marketing Arena, Downtown', 250, 200, 120.00, 3, 2, 1, 'http://example.com/digitalmarketing-banner.jpg');


INSERT INTO Events (Title, Description, Date, StartTime, EndTime, Location, TotalSeats, AvailableSeats, TicketPrice, CategoryID, TypeID, OrganizerID, BannerURL)
VALUES ('Tech Innovations Summit', 'A summit focused on the latest technological innovations and trends.', '2024-11-15', '10:00:00', '18:00:00', 'Tech Park, Silicon Valley', 500, 450, 150.00, 2, 1, 1, 'http://example.com/techsummit-banner.jpg');