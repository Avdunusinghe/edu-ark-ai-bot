BEGIN TRANSACTION;
GO

ALTER TABLE [User] ADD [ProfileImageUrl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230522135120_EduArkTenant00003', N'7.0.5');
GO

COMMIT;
GO

