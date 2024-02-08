CREATE TABLE [UserInfo] (
  [Id] integer PRIMARY KEY,
  [ASPNetIdentityId] nvarchar(450),
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [PhoneNumber] nvarchar(12),
  [Summary] nvarchar(250)
);

CREATE TABLE [Education] (
  [Id] integer PRIMARY KEY,
  [UserInfoId] integer,
  [Institution] nvarchar(100),
  [EducationSummary] nvarchar(250),
  [StartDate] date,
  [EndDate] date,
  [Completion] bool
);

CREATE TABLE [Degree] (
  [Id] integer PRIMARY KEY,
  [EducationId] integer,
  [Type] nvarchar(100),
  [Major] nvarchar(50),
  [Minor] nvarchar(50)
);

CREATE TABLE [EmployementHistory] (
  [Id] integer PRIMARY KEY,
  [UserInfoId] integer,
  [Company] nvarchar(100),
  [Description] nvarchar(250),
  [StartDate] date,
  [EndDate] date
);

CREATE TABLE [ReferenceContactInfo] (
  [Id] integer PRIMARY KEY,
  [EmployementHistoryId] integer,
  [FirstName] nvarchar(50),
  [LastName] nvarchar(50),
  [PhoneNumber] nvarchar(12)
);

CREATE TABLE [UserSkill] (
  [Id] integer PRIMARY KEY,
  [UserInfoId] integer,
  [SkillId] integer,
  [MonthDuration] int
);

CREATE TABLE [Skills] (
  [Id] integer PRIMARY KEY,
  [Skill] nvarchar(100)
);

CREATE TABLE [Projects] (
  [Id] integer PRIMARY KEY,
  [UserInfoId] integer,
  [Name] nvarchar(100),
  [Link] nvarchar(250),
  [Summary] nvarchar(250)
);

CREATE TABLE [Achievements] (
  [Id] integer PRIMARY KEY,
  [UserInfoId] integer,
  [Achievement] nvarchar(100),
  [Summary] nvarchar(250)
);

CREATE TABLE [Resume] (
  [Id] integer PRIMARY KEY,
  [UserInfoId] integer,
  [Resume] binary
);

ALTER TABLE [Resume] ADD CONSTRAINT [Fk Resume UserInfo Id] 
  FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

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
