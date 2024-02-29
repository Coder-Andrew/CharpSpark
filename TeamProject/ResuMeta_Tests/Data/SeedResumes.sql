INSERT INTO [UserInfo] ([Id], [FirstName], [LastName], [PhoneNumber], [Summary], [ProfilePicturePath], [ASPNetIdentityId]) VALUES
    (1, 'Adrian', 'Reynolds', '555-628-1234', NULL, NULL, NULL),
    (2, 'Jasmine', 'Patel', '555-628-1234', NULL, NULL, NULL),
    (3, 'Emily', 'Mitchell', '555-555-5555', NULL, NULL, NULL);

INSERT INTO [Resume] ([Id], [UserInfoId], [Title], [Resume]) VALUES
    (1, 1, 'Resume 1', '%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E'),
    (2, 1, 'Resume 2', '%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E'),
    (3, 1, 'Resume 3', NULL),
    (4, 2, 'Resume 4', '%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E'),
    (5, 2, 'Resume 5', '%3Ch1%3EAdrian%20Reynolds%3C%2Fh1%3E%3Cp%3Ereynoldsa%40mail.com%3C%2Fp%3E%3Cp%3E555-628-1234%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EEducation%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EInstitution%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3ESummary%3A%3C%2Fstrong%3E%20WOU%3C%2Fp%3E%3Cp%3E%3Cstrong%3EDates%3A%3C%2Fstrong%3E%20Jan%202024%20-%20Mar%202024%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EDegree%3C%2Fh2%3E%3Cp%3E%3Cstrong%3EType%3A%3C%2Fstrong%3E%20Bachelor%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMajor%3A%3C%2Fstrong%3E%20CS%3C%2Fp%3E%3Cp%3E%3Cstrong%3EMinor%3A%3C%2Fstrong%3E%20IS%3C%2Fp%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3ESkills%3C%2Fh2%3E%3Cul%3E%3Cli%3EPython%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EAchievements%3C%2Fh2%3E%3Cul%3E%3Cli%3EHonor%20Roll%20-%203.7%20GPA%3C%2Fli%3E%3C%2Ful%3E%3Cp%3E%3Cbr%3E%3C%2Fp%3E%3Ch2%3EProjects%3C%2Fh2%3E%3Ch4%3E%3Cstrong%3EresuMeta%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22https%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3Ehttps%3A%2F%2Fwww.github.com%2FCoder-Andrew%2FCh%3C%2Fa%3E%3Ca%20href%3D%22www.github.com%2FCoder-Andrew%2FCharpSpark%22%20target%3D%22_blank%22%3EarpSpark%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ESenior%20Sequence%20Project%3C%2Fli%3E%3C%2Ful%3E%3Ch4%3E%3Cstrong%3EDD%26amp%3BBB%20-%20%3C%2Fstrong%3E%20%3Ca%20href%3D%22about%3Ablank%22%20target%3D%22_blank%22%3Elocalhost%3Axxxx%3C%2Fa%3E%3C%2Fh4%3E%3Cul%3E%3Cli%3ETesting%3C%2Fli%3E%3C%2Ful%3E'),
    (6, 2, 'Resume 6', NULL),
    (7, 3, NULL, NULL),
    (8, 3, 'Resume 8', NULL);

INSERT INTO [Education] ([Id], [UserInfoId], [ResumeId], [Institution], [EducationSummary], [StartDate], [EndDate], [Completion]) VALUES
    (1, 1, 1, 'WOU', 'Summary Here', '2024-01-01', '2024-03-01', 1),
    (2, 1, 2, 'WOU', 'Summary Here', '2024-01-01', '2024-03-01', 1),
    (3, 2, 4, 'WOU', 'Summary Here', '2024-01-01', '2024-03-01', 1),
    (4, 2, 5, 'WOU', 'Summary Here', '2024-01-01', '2024-03-01', 1);

INSERT INTO [Degree] ([Id], [EducationId], [Type], [Major], [Minor]) VALUES
    (1, 1, 'Bachelor', 'CS', 'IS'),
    (2, 2, 'Bachelor', 'Computer Science', 'Information Systems'),
    (3, 3, 'Master', 'Computer Science', 'N/A'),
    (4, 4, 'Bachelor', 'Computer Science', 'Mathematics');

INSERT INTO [Skills] ([Id], [SkillName]) VALUES
    (1, "Python"),
    (2, "Java"),
    (3, "C#"),
    (4, "C++"),
    (5, "JavaScript");

INSERT INTO [UserSkill] ([Id], [UserInfoId], [ResumeId], [SkillId]) VALUES
    (1, 1, 1, 1),
    (2, 1, 1, 2),
    (3, 1, 2, 5),
    (4, 2, 4, 1),
    (5, 2, 5, 1);

INSERT INTO [Achievements] ([Id], [UserInfoId], [ResumeId], [Achievement], [Summary]) VALUES
    (1, 1, 1, "Honor Roll", "4.0 GPA"),
    (2, 1, 2, "Honor Roll", "4.0 GPA"),
    (3, 2, 4, "Honor Roll", "4.0 GPA"),
    (4, 2, 5, "Honor Roll", "4.0 GPA");

INSERT INTO [Projects] ([Id], [UserInfoId], [ResumeId], [Name], [Link], [Summary]) VALUES
    (1, 1, 1, "resuMeta", "https://www.github.com/Coder-Andrew/CharpSpark", "Senior Sequence Project"),
    (2, 1, 2, "DD&BB", "localhost:xxxx", "Testing"),
    (3, 2, 4, "resuMeta", "https://www.github.com/Coder-Andrew/CharpSpark", "Senior Sequence Project"),
    (4, 2, 5, "DD&BB", "localhost:xxxx", "Testing");


