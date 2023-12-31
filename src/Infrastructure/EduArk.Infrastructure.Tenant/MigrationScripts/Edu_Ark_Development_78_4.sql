BEGIN TRANSACTION;
GO

ALTER TABLE [Exam] DROP CONSTRAINT [FK_Exam_ExamType_ExamTypeId];
GO

ALTER TABLE [ExamMark] DROP CONSTRAINT [FK_ExamMark_AcademicYear_AcademicYearId];
GO

ALTER TABLE [ExamMark] DROP CONSTRAINT [FK_ExamMark_Semester_SemesterId];
GO

DROP INDEX [IX_ExamMark_AcademicYearId] ON [ExamMark];
GO

DROP INDEX [IX_ExamMark_SemesterId] ON [ExamMark];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ExamMark]') AND [c].[name] = N'AcademicYearId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ExamMark] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ExamMark] DROP COLUMN [AcademicYearId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ExamMark]') AND [c].[name] = N'SemesterId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ExamMark] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ExamMark] DROP COLUMN [SemesterId];
GO

ALTER TABLE [Exam] ADD [SemesterId] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_Exam_AcademicYearId] ON [Exam] ([AcademicYearId]);
GO

CREATE INDEX [IX_Exam_SemesterId] ON [Exam] ([SemesterId]);
GO

ALTER TABLE [Exam] ADD CONSTRAINT [FK_Exam_AcademicYear_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYear] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Exam] ADD CONSTRAINT [FK_Exam_ExamType_ExamTypeId] FOREIGN KEY ([ExamTypeId]) REFERENCES [ExamType] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Exam] ADD CONSTRAINT [FK_Exam_Semester_SemesterId] FOREIGN KEY ([SemesterId]) REFERENCES [Semester] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230831164727_Edu_Ark_Development_78_4', N'7.0.5');
GO

COMMIT;
GO

