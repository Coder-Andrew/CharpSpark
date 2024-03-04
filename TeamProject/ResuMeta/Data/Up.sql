CREATE TABLE [UserInfo] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [ASPNetIdentityId] nvarchar(450),
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [PhoneNumber] nvarchar(12),
  [Summary] nvarchar(250),
  [ProfilePicturePath] nvarchar(2048),
);

CREATE TABLE [Education] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer,
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

CREATE TABLE [EmployementHistory] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer,
  [Company] nvarchar(100),
  [Description] nvarchar(250),
  [StartDate] date,
  [EndDate] date
);

CREATE TABLE [ReferenceContactInfo] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [EmployementHistoryId] integer,
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
  [ResumeId] integer,
  [Name] nvarchar(100),
  [Link] nvarchar(250),
  [Summary] nvarchar(250)
);

CREATE TABLE [Achievements] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [ResumeId] integer,
  [Achievement] nvarchar(100),
  [Summary] nvarchar(250)
);

CREATE TABLE [Resume] (
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserInfoId] integer,
  [Title] nvarchar(100),
  [Resume] nvarchar(MAX)
);

ALTER TABLE [Resume] ADD CONSTRAINT [Fk Resume UserInfo Id] 
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [UserSkill] ADD CONSTRAINT [Fk UserSkill Resume Id] 
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Education] ADD CONSTRAINT [Fk Education Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Projects] ADD CONSTRAINT [Fk Projects Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [EmployementHistory] ADD CONSTRAINT [Fk EmployementHistory Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Achievements] ADD CONSTRAINT [Fk Achievements Resume Id]
  FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Education] ADD CONSTRAINT [Fk Education UserInfo Id]
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [EmployementHistory] ADD CONSTRAINT [Fk EmployementHistory UserInfo Id]
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

ALTER TABLE [ReferenceContactInfo] ADD CONSTRAINT [Fk ReferenceContactInfo EmployementHistory Id] 
  FOREIGN KEY ([EmployementHistoryId]) REFERENCES [EmployementHistory] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;