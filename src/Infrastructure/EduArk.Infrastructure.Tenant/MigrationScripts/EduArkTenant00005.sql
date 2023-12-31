BEGIN TRANSACTION;
GO

CREATE TABLE [Semester] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Semester] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SubjectTargetSetting] (
    [Id] int NOT NULL IDENTITY,
    [SubjectId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    [SemesterId] int NOT NULL,
    [StudentId] int NOT NULL,
    [PredictedMark] decimal(18,2) NOT NULL,
    [TeacherTargetScore] decimal(18,2) NULL,
    CONSTRAINT [PK_SubjectTargetSetting] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubjectTargetSetting_AcademicLevel_AcademicLevelId] FOREIGN KEY ([AcademicLevelId]) REFERENCES [AcademicLevel] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectTargetSetting_AcademicYear_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYear] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectTargetSetting_Semester_SemesterId] FOREIGN KEY ([SemesterId]) REFERENCES [Semester] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SubjectTargetSetting_Student_AcademicLevelId] FOREIGN KEY ([AcademicLevelId]) REFERENCES [Student] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectTargetSetting_Subject_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Subject] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_SubjectTargetSetting_AcademicLevelId] ON [SubjectTargetSetting] ([AcademicLevelId]);
GO

CREATE INDEX [IX_SubjectTargetSetting_AcademicYearId] ON [SubjectTargetSetting] ([AcademicYearId]);
GO

CREATE INDEX [IX_SubjectTargetSetting_SemesterId] ON [SubjectTargetSetting] ([SemesterId]);
GO

CREATE INDEX [IX_SubjectTargetSetting_StudentId] ON [SubjectTargetSetting] ([StudentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230801063952_EduArkTenant00005', N'7.0.5');
GO

COMMIT;
GO

