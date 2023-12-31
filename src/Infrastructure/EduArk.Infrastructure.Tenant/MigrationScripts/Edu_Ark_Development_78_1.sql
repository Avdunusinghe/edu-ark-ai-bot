BEGIN TRANSACTION;
GO

ALTER TABLE [SubjectTargetSetting] DROP CONSTRAINT [FK_SubjectTargetSetting_Student_AcademicLevelId];
GO

ALTER TABLE [Lesson] ADD [FileUploadUrl] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [SubjectTargetSetting] ADD CONSTRAINT [FK_SubjectTargetSetting_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230827072213_Edu_Ark_Development_78_1', N'7.0.5');
GO

COMMIT;
GO

