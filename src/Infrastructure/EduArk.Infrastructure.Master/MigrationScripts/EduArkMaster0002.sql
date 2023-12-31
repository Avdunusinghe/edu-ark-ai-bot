BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tenant]') AND [c].[name] = N'TenantId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Tenant] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Tenant] ADD DEFAULT '0c7ce11c-62a1-4d1c-9e89-0b4321a56c01' FOR [TenantId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tenant]') AND [c].[name] = N'SecretKey');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Tenant] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Tenant] ADD DEFAULT '54c15297-78bb-4e6e-8f19-ceea3f0eebbe' FOR [SecretKey];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tenant]') AND [c].[name] = N'APIKey');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Tenant] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Tenant] ADD DEFAULT 'e8c2e273-a55c-42de-bbb3-590b7090875c' FOR [APIKey];
GO

ALTER TABLE [Tenant] ADD [SpecialNotes] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230517130701_EduArkMaster00002', N'7.0.5');
GO

COMMIT;
GO

