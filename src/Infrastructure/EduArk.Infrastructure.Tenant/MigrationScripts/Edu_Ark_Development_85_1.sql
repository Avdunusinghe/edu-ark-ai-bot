BEGIN TRANSACTION;
GO

ALTER TABLE [Student] ADD [ConfidentAcademicPerformance] int NULL DEFAULT 0;
GO

ALTER TABLE [Student] ADD [ImportantFactorsAcademicPerformance] int NULL DEFAULT 0;
GO

ALTER TABLE [Student] ADD [StudyHours] int NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231018100234_Edu_Ark_Development_85_1', N'7.0.5');
GO

COMMIT;
GO

