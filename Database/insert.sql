-- Use the MasterPiece database
USE MasterPiece;
GO

-- Insert sample data into EventCategories
INSERT INTO EventCategories (CategoryName) VALUES
('Music'), ('Technology'), ('Business'), ('Sports'), ('Art');

-- Insert sample data into EventTypes
INSERT INTO EventTypes (TypeName) VALUES
('Conference'), ('Concert'), ('Workshop'), ('Seminar'), ('Exhibition');

-- Insert sample data into Users (10 users)
INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Salt, PhoneNumber, Username, Biography, IsOrganizer) VALUES
('John', 'Doe', 'john@example.com', 'hash1', 'salt1', '1234567890', 'johndoe', 'Event enthusiast', 0),
('Jane', 'Smith', 'jane@example.com', 'hash2', 'salt2', '0987654321', 'janesmith', 'Professional organizer', 1),
('Alice', 'Johnson', 'alice@example.com', 'hash3', 'salt3', '1122334455', 'alicej', 'Music lover', 0),
('Bob', 'Brown', 'bob@example.com', 'hash4', 'salt4', '5566778899', 'bobb', 'Tech geek', 1),
('Charlie', 'Davis', 'charlie@example.com', 'hash5', 'salt5', '2233445566', 'charlied', 'Sports fanatic', 0),
('Diana', 'Wilson', 'diana@example.com', 'hash6', 'salt6', '6677889900', 'dianaw', 'Art curator', 1),
('Ethan', 'Miller', 'ethan@example.com', 'hash7', 'salt7', '3344556677', 'ethanm', 'Business consultant', 0),
('Fiona', 'Taylor', 'fiona@example.com', 'hash8', 'salt8', '7788990011', 'fionat', 'Seminar speaker', 1),
('George', 'Anderson', 'george@example.com', 'hash9', 'salt9', '4455667788', 'georgea', 'Workshop facilitator', 0),
('Hannah', 'Thomas', 'hannah@example.com', 'hash10', 'salt10', '8899001122', 'hannaht', 'Conference organizer', 1);

-- Insert sample data into Events (10 events)
INSERT INTO Events (Title, Description, EventDate, StartTime, EndTime, Location, TotalSeats, AvailableSeats, TicketPrice, CategoryID, TypeID, OrganizerID, BannerURL, Location_URL, Status, PlanID, ThumbnailURL) VALUES
('TechCon 2024', 'Annual Technology Conference', '2024-09-15', '09:00:00', '18:00:00', 'Tech Center', 500, 500, 199.99, 2, 1, 2, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Approved', 3, 'assets/img/new/tach_event.jpg'),
('Rock Festival', 'Summer Rock Music Festival', '2024-07-20', '14:00:00', '23:00:00', 'City Park', 10000, 10000, 79.99, 1, 2, 4, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Approved', 2, 'assets/img/new/tach_event.jpg'),
('Business Leadership Seminar', 'Seminar on Effective Leadership', '2024-11-10', '10:00:00', '16:00:00', 'Grand Hotel', 200, 200, 299.99, 3, 4, 6, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Pending', 1, 'assets/img/new/tach_event.jpg'),
('Local Art Exhibition', 'Showcasing Local Artists', '2024-06-05', '11:00:00', '20:00:00', 'Community Gallery', 150, 150, 15.00, 5, 5, 8, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Approved', 2, 'assets/img/new/tach_event.jpg'),
('Sports Analytics Workshop', 'Learn about Sports Data Analysis', '2024-08-22', '13:00:00', '17:00:00', 'Sports Arena', 100, 100, 149.99, 4, 3, 10, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Approved', 1, 'assets/img/new/tach_event.jpg'),
('Classical Music Night', 'Evening of Classical Performances', '2024-10-30', '19:00:00', '22:00:00', 'Concert Hall', 300, 300, 89.99, 1, 2, 2, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Pending', 2, 'assets/img/new/tach_event.jpg'),
('Startup Pitch Competition', 'Pitch Your Startup Idea', '2024-05-18', '10:00:00', '18:00:00', 'Innovation Hub', 250, 250, 50.00, 3, 1, 4, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Approved', 3, 'assets/img/new/tach_event.jpg'),
('Digital Art Showcase', 'Exhibition of Digital Artworks', '2024-12-05', '11:00:00', '20:00:00', 'Modern Art Museum', 180, 180, 25.00, 5, 5, 6, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Pending', 2, 'assets/img/new/tach_event.jpg'),
('Fitness Boot Camp', 'Intensive Fitness Workshop', '2024-07-08', '07:00:00', '09:00:00', 'City Gym', 50, 50, 39.99, 4, 3, 8, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Approved', 1, 'assets/img/new/tach_event.jpg'),
('AI in Business Seminar', 'Exploring AI Applications in Business', '2024-09-28', '09:00:00', '17:00:00', 'Tech Institute', 150, 150, 199.99, 2, 4, 10, 'https://localhost:7293/media/eventBanners/banner_test1.jpg', 'https://www.google.com/maps/embed?pb=!1m17!1m12!1m3!1d3362.793407179993!2d35.85319992525625!3d32.55835849513006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m2!1m1!2zMzLCsDMzJzMwLjEiTiAzNcKwNTEnMDIuMyJF!5e0!3m2!1sar!2sjo!4v1727351631771!5m2!1sar!2sjo', 'Approved', 3, 'assets/img/new/tach_event.jpg');

-- Insert sample data into Tickets
INSERT INTO Tickets (EventID, UserID, QRCode, TicketType, PurchaseDate, Price) VALUES
(1, 1, 'QR001', 'Paid', GETDATE(), 199.99),
(2, 3, 'QR002', 'Paid', GETDATE(), 79.99),
(3, 5, 'QR003', 'Paid', GETDATE(), 299.99),
(4, 7, 'QR004', 'Paid', GETDATE(), 15.00),
(5, 9, 'QR005', 'Paid', GETDATE(), 149.99);

-- Insert sample data into Payments
INSERT INTO Payments (EventID, UserID, TransactionID, PaymentAmount, PaymentDate) VALUES
(1, 1, 'TRX001', 199.99, GETDATE()),
(2, 3, 'TRX002', 79.99, GETDATE()),
(3, 5, 'TRX003', 299.99, GETDATE()),
(4, 7, 'TRX004', 15.00, GETDATE()),
(5, 9, 'TRX005', 149.99, GETDATE());

 
-- Insert sample data into Reviews (continued)
INSERT INTO Reviews (EventID, UserID, Rating, ReviewText, ReviewDate) VALUES
(4, 8, 3, 'Interesting art, but limited selection', GETDATE()),
(5, 10, 4, 'Useful workshop, could be longer', GETDATE()),
(6, 1, 5, 'Beautiful classical performances', GETDATE()),
(7, 3, 4, 'Exciting pitches, great networking', GETDATE()),
(8, 5, 5, 'Mind-blowing digital art', GETDATE()),
(9, 7, 3, 'Intense workout, but effective', GETDATE()),
(10, 9, 4, 'Insightful AI seminar, well organized', GETDATE());

-- Insert sample data into EventSessions
INSERT INTO EventSessions (EventID, SessionTime, SessionTitle, SessionDescription, SessionType) VALUES
(1, '09:00 - 10:30', 'Keynote Speech', 'Opening remarks and industry trends', 'Morning'),
(1, '11:00 - 12:30', 'AI in Technology', 'Exploring the latest in AI advancements', 'Morning'),
(2, '14:00 - 15:30', 'Opening Act', 'Rising star performance', 'Afternoon'),
(2, '16:00 - 17:30', 'Main Performance', 'Headliner band live on stage', 'Afternoon'),
(3, '10:00 - 11:30', 'Leadership Principles', 'Core concepts of effective leadership', 'Morning'),
(3, '13:00 - 14:30', 'Team Building Workshop', 'Interactive session on team dynamics', 'Afternoon'),
(4, '11:00 - 13:00', 'Meet the Artists', 'Q&A session with featured artists', 'Morning'),
(5, '13:00 - 15:00', 'Data Analysis in Sports', 'Hands-on workshop with real sports data', 'Afternoon'),
(6, '19:00 - 20:00', 'String Quartet Performance', 'Classical masterpieces by local quartet', 'Evening'),
(7, '10:00 - 12:00', 'Pitch Presentations', 'Startup teams present their ideas', 'Morning');

-- Insert sample data into Announcements
INSERT INTO Announcements (EventID, OrganizerID, Message, DateSent) VALUES
(1, 2, 'Welcome to TechCon 2024! Don''t forget to visit our exhibitor hall.', GETDATE()),
(2, 4, 'Rock Festival starts in 1 hour! Get ready to rock!', GETDATE()),
(3, 6, 'Business Leadership Seminar: Lunch will be served at 12:30 PM.', GETDATE()),
(4, 8, 'Local Art Exhibition opens tomorrow. Early bird tickets still available!', GETDATE()),
(5, 10, 'Sports Analytics Workshop: Bring your laptops for hands-on session.', GETDATE()),
(6, 2, 'Classical Music Night: Doors open at 6:30 PM.', GETDATE()),
(7, 4, 'Startup Pitch Competition: Judging panel announced!', GETDATE()),
(8, 6, 'Digital Art Showcase: Virtual reality demos available.', GETDATE()),
(9, 8, 'Fitness Boot Camp: Remember to bring water and a towel.', GETDATE()),
(10, 10, 'AI in Business Seminar: Networking lunch included in ticket price.', GETDATE());

-- Insert sample data into Speakers
INSERT INTO Speakers (Name, Role, Bio, FacebookURL, TwitterURL, WhatsAppURL, InstagramURL, ProfileImageURL) VALUES
('Dr. Tech Guru', 'Chief Data Scientist', 'Leading expert in AI and machine learning', 'https://facebook.com/techguru', 'https://twitter.com/techguru', 'https://wa.me/1234567890', 'https://instagram.com/techguru', 'https://example.com/images/techguru.jpg'),
('Rock Star', 'Lead Singer', 'Grammy-winning artist with chart-topping hits', 'https://facebook.com/rockstar', 'https://twitter.com/rockstar', 'https://wa.me/0987654321', 'https://instagram.com/rockstar', 'https://example.com/images/rockstar.jpg'),
('Business Leader', 'CEO', 'Renowned business strategist and author', 'https://facebook.com/businessleader', 'https://twitter.com/businessleader', 'https://wa.me/1122334455', 'https://instagram.com/businessleader', 'https://example.com/images/businessleader.jpg'),
('Art Curator', 'Museum Director', 'Experienced curator with a passion for modern art', 'https://facebook.com/artcurator', 'https://twitter.com/artcurator', 'https://wa.me/2233445566', 'https://instagram.com/artcurator', 'https://example.com/images/artcurator.jpg'),
('Sports Analyst', 'Data Scientist', 'Former athlete turned sports data expert', 'https://facebook.com/sportsanalyst', 'https://twitter.com/sportsanalyst', 'https://wa.me/3344556677', 'https://instagram.com/sportsanalyst', 'https://example.com/images/sportsanalyst.jpg');

-- Insert sample data into EventSpeakers
INSERT INTO EventSpeakers (EventID, SpeakerID) VALUES
(1, 1), -- TechCon 2024 with Dr. Tech Guru
(2, 2), -- Rock Festival with Rock Star
(3, 3), -- Business Leadership Seminar with Business Leader
(4, 4), -- Local Art Exhibition with Art Curator
(5, 5), -- Sports Analytics Workshop with Sports Analyst
(6, 2), -- Classical Music Night with Rock Star (special appearance)
(7, 3), -- Startup Pitch Competition with Business Leader
(8, 4), -- Digital Art Showcase with Art Curator
(9, 5), -- Fitness Boot Camp with Sports Analyst
(10, 1); -- AI in Business Seminar with Dr. Tech Guru



-- Update existing events with WhatToExpect and Highlights
UPDATE Events
SET WhatToExpect = 'Engaging talks and workshops from industry leaders. Hands-on experience with cutting-edge technologies. Networking opportunities with tech professionals.',
    Highlights = 'Keynote by Dr. Tech Guru; Interactive AI workshops; Tech startup showcase; Expert panels on emerging technologies; Q&A with industry leaders'
WHERE Title = 'TechCon 2024';

UPDATE Events
SET WhatToExpect = 'A day filled with electrifying performances from top rock bands. Food and drink vendors. Merchandise stalls featuring band memorabilia.',
    Highlights = 'Headlining performance by Rock Star; Multiple stages for non-stop music; Meet and greet with bands; Air guitar competition; Local band showcase'
WHERE Title = 'Rock Festival';

UPDATE Events
SET WhatToExpect = 'Insightful presentations on effective leadership strategies. Interactive workshops on team management. Networking sessions with business leaders.',
    Highlights = 'Keynote by Business Leader; Leadership skills workshops; Case study analyses; Networking lunch; Panel discussion on future of leadership'
WHERE Title = 'Business Leadership Seminar';

UPDATE Events
SET WhatToExpect = 'A diverse showcase of local artistic talent. Interactive art installations. Live demonstrations by featured artists.',
    Highlights = 'Curated by Art Curator; Over 100 artworks on display; Live painting demonstrations; Artist meet and greets; Silent auction of select pieces'
WHERE Title = 'Local Art Exhibition';

UPDATE Events
SET WhatToExpect = 'In-depth exploration of data analytics in sports. Hands-on sessions with real sports data. Insights from professional sports analysts.',
    Highlights = 'Workshop led by Sports Analyst; Real-time data analysis exercises; Case studies from major sports leagues; Predictive modeling session; Career opportunities in sports analytics'
WHERE Title = 'Sports Analytics Workshop';

UPDATE Events
SET WhatToExpect = 'An evening of sublime classical music performances. Pre-concert talks on featured compositions. Meet the musicians after the show.',
    Highlights = 'Performances of Mozart, Beethoven, and Tchaikovsky; Guest appearance by Rock Star; Pre-concert lecture on classical music history; VIP reception with performers'
WHERE Title = 'Classical Music Night';

UPDATE Events
SET WhatToExpect = 'Exciting pitches from innovative startups. Feedback from experienced investors and entrepreneurs. Networking opportunities with potential investors.',
    Highlights = 'Judging panel led by Business Leader; 5-minute pitch sessions; Investor Q&A rounds; Startup exhibition area; Networking cocktail hour'
WHERE Title = 'Startup Pitch Competition';

UPDATE Events
SET WhatToExpect = 'Immersive digital art experiences. Virtual reality art installations. Talks on the future of digital art.',
    Highlights = 'Curated by Art Curator; VR art experiences; AI-generated art showcase; Digital art creation workshops; Panel on NFTs in the art world'
WHERE Title = 'Digital Art Showcase';

UPDATE Events
SET WhatToExpect = 'High-intensity workout sessions. Nutritional advice from fitness experts. Group exercises and team challenges.',
    Highlights = 'Led by Sports Analyst; HIIT workout sessions; Nutrition workshop; Team building exercises; Fitness goal setting seminar'
WHERE Title = 'Fitness Boot Camp';

UPDATE Events
SET WhatToExpect = 'Comprehensive overview of AI applications in business. Case studies of successful AI implementations. Discussions on ethical AI use in business.',
    Highlights = 'Keynote by Dr. Tech Guru; AI implementation case studies; Hands-on machine learning workshop; Ethics in AI panel discussion; Networking lunch with AI professionals'
WHERE Title = 'AI in Business Seminar';