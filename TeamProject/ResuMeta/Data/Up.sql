CREATE TABLE [UserInfo] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [ASPNetIdentityId] nvarchar(450),
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [PhoneNumber] nvarchar(12),
  [Email] nvarchar(100),
  [Summary] nvarchar(250),
  [ProfilePicturePath] VARBINARY(MAX),
);

CREATE TABLE [ResumeTemplate] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [Title] nvarchar(100),
  [Template] nvarchar(MAX)
);

CREATE TABLE [Education] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer NULL,
  [Institution] nvarchar(100),
  [EducationSummary] nvarchar(250),
  [StartDate] date,
  [EndDate] date,
  [Completion] bit
);

CREATE TABLE [Degree] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [EducationId] integer,
  [Type] nvarchar(100),
  [Major] nvarchar(50),
  [Minor] nvarchar(50)
);

CREATE TABLE [EmploymentHistory] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer NULL,
  [Company] nvarchar(100),
  [Description] nvarchar(250),
  [Location] nvarchar(100),
  [JobTitle] nvarchar(100),
  [StartDate] date,
  [EndDate] date
);

CREATE TABLE [ReferenceContactInfo] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [EmploymentHistoryId] integer,
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [PhoneNumber] nvarchar(12)
);

CREATE TABLE [UserSkill] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer,
  [SkillId] integer,
  [MonthDuration] int
);

CREATE TABLE [Skills] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [SkillName] nvarchar(100) NOT NULL UNIQUE
);

CREATE TABLE [Projects] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer NULL,
  [Name] nvarchar(100),
  [Link] nvarchar(250),
  [Summary] nvarchar(250)
);

CREATE TABLE [Achievements] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer NULL,
  [Achievement] nvarchar(100),
  [Summary] nvarchar(250)
);

CREATE TABLE [Resume] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [Title] nvarchar(100),
  [Resume] nvarchar(MAX)
);

CREATE TABLE [ApplicationTracker] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [JobTitle] nvarchar(100),
  [CompanyName] nvarchar(100),
  [JobListingURL] nvarchar(max),
  [AppliedDate] date,
  [ApplicationDeadline] date,
  [Status] nvarchar(100),
  [Notes] nvarchar(250)
);

CREATE TABLE [CoverLetter] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [Title] nvarchar(100),
  [HiringManager] nvarchar(100),
  [Body] nvarchar(4000),
  [CoverLetter] nvarchar(MAX)
);

CREATE TABLE [Profile] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [Resume] nvarchar(MAX),
  [ResumeId] integer,
  [Description] nvarchar(250)
);

CREATE TABLE [Vote] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [VoteValue] nvarchar(10)
);

CREATE TABLE [UserVote] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer,
  [VoteId] integer,
  [Timestamp] DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [ProfileViews] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [ProfileId] integer,
  [ViewCount] integer
);

CREATE TABLE [Follower] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [ProfileId] integer,
  [FollowerId] integer,
  [Timestamp] DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE [Resume] ADD CONSTRAINT [Fk Resume UserInfo Id] 
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [UserSkill] ADD CONSTRAINT [Fk UserSkill Resume Id] 
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Education] ADD CONSTRAINT [Fk Education Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE SET NULL ON UPDATE NO ACTION;

ALTER TABLE [Projects] ADD CONSTRAINT [Fk Projects Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE SET NULL ON UPDATE NO ACTION;

ALTER TABLE [EmploymentHistory] ADD CONSTRAINT [Fk EmploymentHistory Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE SET NULL ON UPDATE NO ACTION;

ALTER TABLE [Achievements] ADD CONSTRAINT [Fk Achievements Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE SET NULL ON UPDATE NO ACTION;

ALTER TABLE [Education] ADD CONSTRAINT [Fk Education UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [EmploymentHistory] ADD CONSTRAINT [Fk EmploymentHistory UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [UserSkill] ADD CONSTRAINT [Fk UserSkill UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [UserSkill] ADD CONSTRAINT [Fk UserSkill Skill Id]
  FOREIGN KEY ([SkillId]) REFERENCES [Skills] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Projects] ADD CONSTRAINT [Fk Projects UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Achievements] ADD CONSTRAINT [Fk Achievements UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Degree] ADD CONSTRAINT [Fk Degree Education Id]
  FOREIGN KEY ([EducationId]) REFERENCES [Education] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [ReferenceContactInfo] ADD CONSTRAINT [Fk ReferenceContactInfo EmploymentHistory Id] 
  FOREIGN KEY ([EmploymentHistoryId]) REFERENCES [EmploymentHistory] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [ApplicationTracker] ADD CONSTRAINT [Fk ApplicationTracker UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [CoverLetter] ADD CONSTRAINT [Fk CoverLetter UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Profile] ADD CONSTRAINT [Fk Profile UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Profile] ADD CONSTRAINT [Fk Profile Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [UserVote] ADD CONSTRAINT [Fk UserVote UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [UserVote] ADD CONSTRAINT [Fk UserVote Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [UserVote] ADD CONSTRAINT [Fk UserVote Vote Id]
  FOREIGN KEY ([VoteId]) REFERENCES [Vote] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [ProfileViews] ADD CONSTRAINT [Fk ProfileViews Profile Id]
  FOREIGN KEY ([ProfileId]) REFERENCES [Profile] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Follower] ADD CONSTRAINT [Fk Follower Profile Id]
  FOREIGN KEY ([ProfileId]) REFERENCES [Profile] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Follower] ADD CONSTRAINT [Fk Follower FollowerProfile Id]
  FOREIGN KEY ([FollowerId]) REFERENCES [Profile] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;