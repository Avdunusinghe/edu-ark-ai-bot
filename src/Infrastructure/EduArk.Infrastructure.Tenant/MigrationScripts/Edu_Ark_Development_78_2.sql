BEGIN TRANSACTION;
GO

ALTER TABLE [SubjectTargetSetting] DROP CONSTRAINT [FK_SubjectTargetSetting_Subject_StudentId];
GO

CREATE INDEX [IX_SubjectTargetSetting_SubjectId] ON [SubjectTargetSetting] ([SubjectId]);
GO

ALTER TABLE [SubjectTargetSetting] ADD CONSTRAINT [FK_SubjectTargetSetting_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230827072825_Edu_Ark_Development_78_2', N'7.0.5');
GO

COMMIT;
GO

