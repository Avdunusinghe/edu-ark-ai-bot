BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Student]') AND [c].[name] = N'ImportantFactorsAcademicPerformance');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Student] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Student] DROP COLUMN [ImportantFactorsAcademicPerformance];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Student]') AND [c].[name] = N'StudyHours');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Student] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [Student] SET [StudyHours] = 0 WHERE [StudyHours] IS NULL;
ALTER TABLE [Student] ALTER COLUMN [StudyHours] int NOT NULL;
ALTER TABLE [Student] ADD DEFAULT 0 FOR [StudyHours];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Student]') AND [c].[name] = N'ConfidentAcademicPerformance');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Student] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [Student] SET [ConfidentAcademicPerformance] = 0 WHERE [ConfidentAcademicPerformance] IS NULL;
ALTER TABLE [Student] ALTER COLUMN [ConfidentAcademicPerformance] int NOT NULL;
ALTER TABLE [Student] ADD DEFAULT 0 FOR [ConfidentAcademicPerformance];
GO

ALTER TABLE [Student] ADD [ClassAttendance] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Student] ADD [PersonalMotivation] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Student] ADD [PriorKnowledgeOfTheSubject] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Student] ADD [StudyEnvironment] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Student] ADD [TeacherInstructorQuality] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Student] ADD [TimeManagementSkills] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231018130200_Edu_Ark_Development_85_2', N'7.0.5');
GO

COMMIT;
GO

