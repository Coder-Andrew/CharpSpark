INSERT INTO [UserInfo] ([Id], [FirstName], [LastName], [PhoneNumber], [Summary], [ProfilePicturePath], [ASPNetIdentityId]) VALUES
    (1, 'Adrian', 'Reynolds', '555-628-1234', 'A personal summary for Adrian', NULL, NULL),
    (2, 'Jasmine', 'Patel', '555-628-1234', 'A personal summary', NULL, NULL),
    (3, 'Emily', 'Mitchell', '555-555-5555', 'A personal summary', NULL, NULL);

INSERT INTO [ApplicationTracker] ([UserInfoId], [JobTitle], [CompanyName], [JobListingURL], [AppliedDate], [ApplicationDeadline], [Status], [Notes])
VALUES (1, 'Software Engineer', 'Company 1', 'http://example.com/job1', '2022-01-01', '2022-02-01', 'Applied', 'Test note 1'),
       (1, 'Web Developer', 'Company 2', 'http://example.com/job2', '2022-01-02', '2022-02-02', 'Interview', 'Test note 2'),
       (2, 'Data Scientist', 'Company 3', 'http://example.com/job3', '2022-01-03', '2022-02-03', 'Offer', 'Test note 3');
       