BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240131193922_srakatest', N'7.0.13');
GO

COMMIT;
GO

