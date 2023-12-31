BEGIN TRANSACTION;
GO

CREATE TABLE [LearningPlan] (
    [Id] int NOT NULL IDENTITY,
    [StudentName] nvarchar(max) NOT NULL,
    [SchoolName] nvarchar(max) NOT NULL,
    [SchoolGrade] nvarchar(max) NOT NULL,
    [StudentMark] nvarchar(max) NOT NULL,
    [AverageMark] nvarchar(max) NOT NULL,
    [LearningPattern] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_LearningPlan] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LearningPlan_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LearningPlan_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Lesson] (
    [Id] int NOT NULL IDENTITY,
    [LessonName] nvarchar(max) NULL,
    [LessonDescription] nvarchar(max) NULL,
    [LessonGrade] nvarchar(max) NULL,
    [LessonSubject] nvarchar(max) NULL,
    [LessonStatus] nvarchar(max) NULL,
    [LessonType] int NULL,
    [LearningPlanId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Lesson] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lesson_LearningPlan_LearningPlanId] FOREIGN KEY ([LearningPlanId]) REFERENCES [LearningPlan] ([Id]),
    CONSTRAINT [FK_Lesson_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Lesson_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [LessonTypeAudio] (
    [Id] int NOT NULL IDENTITY,
    [LessonId] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [LessonType] int NOT NULL,
    [AudioFileUrl] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_LessonTypeAudio] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LessonTypeAudio_Lesson_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lesson] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LessonTypeAudio_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LessonTypeAudio_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [LessonTypeText] (
    [Id] int NOT NULL IDENTITY,
    [LessonId] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [LessonType] int NOT NULL,
    [TextContent] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_LessonTypeText] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LessonTypeText_Lesson_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lesson] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LessonTypeText_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LessonTypeText_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [LessonTypeVideo] (
    [Id] int NOT NULL IDENTITY,
    [LessonId] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [LessonType] int NOT NULL,
    [VideoFileUrl] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_LessonTypeVideo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LessonTypeVideo_Lesson_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lesson] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LessonTypeVideo_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LessonTypeVideo_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_LearningPlan_CreatedByUserId] ON [LearningPlan] ([CreatedByUserId]);
GO

CREATE INDEX [IX_LearningPlan_UpdatedByUserId] ON [LearningPlan] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_Lesson_CreatedByUserId] ON [Lesson] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Lesson_LearningPlanId] ON [Lesson] ([LearningPlanId]);
GO

CREATE INDEX [IX_Lesson_UpdatedByUserId] ON [Lesson] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_LessonTypeAudio_CreatedByUserId] ON [LessonTypeAudio] ([CreatedByUserId]);
GO

CREATE INDEX [IX_LessonTypeAudio_LessonId] ON [LessonTypeAudio] ([LessonId]);
GO

CREATE INDEX [IX_LessonTypeAudio_UpdatedByUserId] ON [LessonTypeAudio] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_LessonTypeText_CreatedByUserId] ON [LessonTypeText] ([CreatedByUserId]);
GO

CREATE INDEX [IX_LessonTypeText_LessonId] ON [LessonTypeText] ([LessonId]);
GO

CREATE INDEX [IX_LessonTypeText_UpdatedByUserId] ON [LessonTypeText] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_LessonTypeVideo_CreatedByUserId] ON [LessonTypeVideo] ([CreatedByUserId]);
GO

CREATE INDEX [IX_LessonTypeVideo_LessonId] ON [LessonTypeVideo] ([LessonId]);
GO

CREATE INDEX [IX_LessonTypeVideo_UpdatedByUserId] ON [LessonTypeVideo] ([UpdatedByUserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230514093436_EduArkTenant00002', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [User] ADD [ProfileImageUrl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230522135120_EduArkTenant00003', N'7.0.5');
GO

COMMIT;
GO

