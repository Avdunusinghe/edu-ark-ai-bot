BEGIN TRANSACTION;
GO

CREATE TABLE [ExamMark] (
    [Id] int NOT NULL IDENTITY,
    [ExamId] int NOT NULL,
    [StudentId] int NOT NULL,
    [SubjectId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    [SemesterId] int NOT NULL,
    [Marks] decimal(18,2) NOT NULL,
    [Grade] nvarchar(max) NULL,
    CONSTRAINT [PK_ExamMark] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ExamMark_AcademicLevel_AcademicLevelId] FOREIGN KEY ([AcademicLevelId]) REFERENCES [AcademicLevel] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ExamMark_AcademicYear_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYear] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ExamMark_Exam_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exam] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ExamMark_Semester_SemesterId] FOREIGN KEY ([SemesterId]) REFERENCES [Semester] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ExamMark_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ExamMark_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_ExamMark_AcademicLevelId] ON [ExamMark] ([AcademicLevelId]);
GO

CREATE INDEX [IX_ExamMark_AcademicYearId] ON [ExamMark] ([AcademicYearId]);
GO

CREATE INDEX [IX_ExamMark_ExamId] ON [ExamMark] ([ExamId]);
GO

CREATE INDEX [IX_ExamMark_SemesterId] ON [ExamMark] ([SemesterId]);
GO

CREATE INDEX [IX_ExamMark_StudentId] ON [ExamMark] ([StudentId]);
GO

CREATE INDEX [IX_ExamMark_SubjectId] ON [ExamMark] ([SubjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230831092840_Edu_Ark_Development_78_3', N'7.0.5');
GO

COMMIT;
GO

