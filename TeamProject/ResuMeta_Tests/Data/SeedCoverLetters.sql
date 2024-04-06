INSERT INTO [UserInfo] ([Id], [FirstName], [LastName], [PhoneNumber], [Summary], [ProfilePicturePath], [ASPNetIdentityId]) VALUES
    (1, 'Adrian', 'Reynolds', '555-628-1234', 'A personal summary for Adrian', NULL, NULL),
    (2, 'Jasmine', 'Patel', '555-628-1234', 'A personal summary', NULL, NULL),
    (3, 'Emily', 'Mitchell', '555-555-5555', 'A personal summary', NULL, NULL);

INSERT INTO [CoverLetter] ([Id], [UserInfoId], [Title], [HiringManager], [Body], [CoverLetter]) VALUES
    (1, 2, 'Cover Letter 1', 'Mrs. Dawn', 'Test', '%3Cp%3EDear%20Mrs.%20Dawn%2C%3C%2Fp%3E%3Cp%3ETest%3C%2Fp%3E%3Cp%3ESincerely%2C%3C%2Fp%3E%3Cp%3EJasmine%20Patel%3C%2Fp%3E'),
    (2, 2, 'Cover Letter 2', 'Ms. Dawn', 'This is just a test cover letter body. It is written to be a longer length since a cover letter is likely to be on the longer side, considering you are writing out why you''re a perfect fit for the position. I believe that I''m a perfect fit for this position. I''ve worked several jobs in my lifetime and have acquired a lot of skills that are specific to this job. I am excited to hear back.', '%3Cp%3EDear%20Ms.%20Dawn%2C%3C%2Fp%3E%3Cp%3EThis%20is%20just%20a%20test%20cover%20letter%20body.%20It%20is%20written%20to%20be%20a%20longer%20length%20since%20a%20cover%20letter%20is%20likely%20to%20be%20on%20the%20longer%20side%2C%20considering%20you%20are%20writing%20out%20why%20you''re%20a%20perfect%20fit%20for%20the%20position.%20I%20believe%20that%20I''m%20a%20perfect%20fit%20for%20this%20position.%20I''ve%20worked%20several%20jobs%20in%20my%20lifetime%20and%20have%20acquired%20a%20lot%20of%20skills%20that%20are%20specific%20to%20this%20job.%20I%20am%20excited%20to%20hear%20back.%3C%2Fp%3E%3Cp%3ESincerely%2C%3C%2Fp%3E%3Cp%3EJasmine%20Patel%3C%2Fp%3E'),
    (3, 2, 'Cover Letter 3', NULL, NULL, NULL),
    (4, 1, 'Cover Letter 4', 'Mr. Smith', 'Testing', '%3Cp%3EDear%20Mr.%20Smith%2C%3C%2Fp%3E%3Cp%3ETesting%3C%2Fp%3E%3Cp%3ESincerely%2C%3C%2Fp%3E%3Cp%3EAdrian%20Reynolds%3C%2Fp%3E'),
    (5, 1, 'Cover Letter 5', 'Mr. Smith', 'Test', '%3Cp%3EDear%20Mr.%20Smith%2C%3C%2Fp%3E%3Cp%3ETest%3C%2Fp%3E%3Cp%3ESincerely%2C%3C%2Fp%3E%3Cp%3EAdrian%20Reynolds%3C%2Fp%3E'),
    (8, 3, 'Cover Letter 8', NULL, NULL, NULL);
