-- Insert random data for UserInfo table
INSERT INTO [VOTE] ([VOTEVALUE]) VALUES
    ('UP'),
    ('DOWN');

-- INSERT INTO [UserInfo] (ASPNetIdentityId, FirstName, LastName, PhoneNumber, Email, Summary, ProfilePicturePath)
-- VALUES 
-- ('user1-id', 'John', 'Doe', '1234567890', 'john.doe@example.com', 'Experienced software developer', NULL),
-- ('user2-id', 'Jane', 'Smith', '2345678901', 'jane.smith@example.com', 'Expert in data analysis', NULL),
-- ('user3-id', 'Jim', 'Brown', '3456789012', 'jim.brown@example.com', 'Professional project manager', NULL),
-- ('user4-id', 'Jack', 'White', '4567890123', 'jack.white@example.com', 'Frontend developer', NULL),
-- ('user5-id', 'Jill', 'Green', '5678901234', 'jill.green@example.com', 'Backend developer', NULL),
-- ('user6-id', 'Joe', 'Black', '6789012345', 'joe.black@example.com', 'Database administrator', NULL),
-- ('user7-id', 'Jenny', 'Blue', '7890123456', 'jenny.blue@example.com', 'Network engineer', NULL),
-- ('user8-id', 'Jason', 'Gray', '8901234567', 'jason.gray@example.com', 'Cybersecurity specialist', NULL),
-- ('user9-id', 'Julia', 'Yellow', '9012345678', 'julia.yellow@example.com', 'Cloud architect', NULL),
-- ('user10-id', 'Jerry', 'Purple', '0123456789', 'jerry.purple@example.com', 'DevOps engineer', NULL),
-- ('user11-id', 'Jordan', 'Orange', '1234509876', 'jordan.orange@example.com', 'AI/ML engineer', NULL),
-- ('user12-id', 'Jamie', 'Pink', '2345609875', 'jamie.pink@example.com', 'UI/UX designer', NULL),
-- ('user13-id', 'Jude', 'Red', '3456709874', 'jude.red@example.com', 'QA engineer', NULL),
-- ('user14-id', 'Jess', 'Silver', '4567809873', 'jess.silver@example.com', 'Product manager', NULL),
-- ('user15-id', 'Joan', 'Gold', '5678909872', 'joan.gold@example.com', 'Technical writer', NULL);

-- Insert random data for Resume table
INSERT INTO [Resume] (UserInfoId, Title, Resume)
VALUES 
(1, 'John Doe Resume', 'Resume content for John Doe'),
(2, 'Jane Smith Resume', 'Resume content for Jane Smith'),
(3, 'Jim Brown Resume', 'Resume content for Jim Brown'),
(4, 'Jack White Resume', 'Resume content for Jack White'),
(5, 'Jill Green Resume', 'Resume content for Jill Green'),
(6, 'Joe Black Resume', 'Resume content for Joe Black'),
(7, 'Jenny Blue Resume', 'Resume content for Jenny Blue'),
(8, 'Jason Gray Resume', 'Resume content for Jason Gray'),
(9, 'Julia Yellow Resume', 'Resume content for Julia Yellow'),
(10, 'Jerry Purple Resume', 'Resume content for Jerry Purple'),
(11, 'Jordan Orange Resume', 'Resume content for Jordan Orange'),
(12, 'Jamie Pink Resume', 'Resume content for Jamie Pink'),
(13, 'Jude Red Resume', 'Resume content for Jude Red'),
(14, 'Jess Silver Resume', 'Resume content for Jess Silver'),
(15, 'Joan Gold Resume', 'Resume content for Joan Gold');

-- Insert random data for Profile table
INSERT INTO [Profile] (UserInfoId, Resume, ResumeId, Description)
VALUES 
(1, 'Detailed resume for John Doe', 1, 'Experienced software developer specializing in backend systems.'),
(2, 'Detailed resume for Jane Smith', 2, 'Data analyst with a focus on statistical analysis and big data.'),
(3, 'Detailed resume for Jim Brown', 3, 'Project manager with a background in IT and finance.'),
(4, 'Detailed resume for Jack White', 4, 'Frontend developer skilled in modern JavaScript frameworks.'),
(5, 'Detailed resume for Jill Green', 5, 'Backend developer with experience in microservices architecture.'),
(6, 'Detailed resume for Joe Black', 6, 'Database administrator with expertise in SQL and NoSQL databases.'),
(7, 'Detailed resume for Jenny Blue', 7, 'Network engineer experienced in designing and maintaining networks.'),
(8, 'Detailed resume for Jason Gray', 8, 'Cybersecurity specialist with a strong understanding of security protocols.'),
(9, 'Detailed resume for Julia Yellow', 9, 'Cloud architect with extensive experience in AWS and Azure.'),
(10, 'Detailed resume for Jerry Purple', 10, 'DevOps engineer with a focus on CI/CD pipelines and automation.'),
(11, 'Detailed resume for Jordan Orange', 11, 'AI/ML engineer with a background in machine learning and data science.'),
(12, 'Detailed resume for Jamie Pink', 12, 'UI/UX designer with a knack for creating intuitive user interfaces.'),
(13, 'Detailed resume for Jude Red', 13, 'QA engineer with experience in automated and manual testing.'),
(14, 'Detailed resume for Jess Silver', 14, 'Product manager with a proven track record of successful product launches.'),
(15, 'Detailed resume for Joan Gold', 15, 'Technical writer skilled in creating clear and concise documentation.');


-- Insert random data for UserVote table
INSERT INTO [UserVote] (UserInfoId, ResumeId, VoteId, Timestamp)
VALUES
(1, 1, 1, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(2, 2, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(3, 3, 1, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(4, 4, 1, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(5, 5, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(6, 6, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(7, 7, 1, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(8, 8, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(9, 9, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(10, 10, 1, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(11, 11, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(12, 12, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(13, 13, 1, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(14, 14, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(15, 15, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE()));

-- Insert random data for Follower table
INSERT INTO [Follower] (ProfileId, FollowerId, Timestamp)
VALUES
(1, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(1, 3, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(2, 1, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(2, 3, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(3, 1, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(3, 2, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(4, 5, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(4, 6, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(5, 4, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(5, 6, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(6, 4, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(6, 5, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(7, 8, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(7, 9, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(8, 7, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(8, 9, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(9, 7, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(9, 8, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(10, 11, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(10, 12, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(11, 10, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(11, 12, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(12, 10, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(12, 11, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(13, 14, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(13, 15, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(14, 13, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(14, 15, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(15, 13, DATEADD(day, -FLOOR(RAND() * 365), GETDATE())),
(15, 14, DATEADD(day, -FLOOR(RAND() * 365), GETDATE()));
