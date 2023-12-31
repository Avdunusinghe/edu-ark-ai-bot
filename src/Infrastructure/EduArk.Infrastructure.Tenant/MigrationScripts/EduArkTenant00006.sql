BEGIN TRANSACTION;
GO

DROP INDEX [IX_Student_AdmissionNo] ON [Student];
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Student]') AND [c].[name] = N'AdmissionNo');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Student] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Student] ALTER COLUMN [AdmissionNo] nvarchar(450) NOT NULL;
CREATE UNIQUE INDEX [IX_Student_AdmissionNo] ON [Student] ([AdmissionNo]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230803031951_EduArkTenant00006', N'7.0.5');
GO

COMMIT;
GO

