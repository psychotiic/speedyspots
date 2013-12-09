CREATE TABLE [dbo].[MPUserDataEncrypt]
(
[MPUserDataEncryptID] [uniqueidentifier] NOT NULL,
[ColumnName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_UserDataEncrypt_ColumnName] DEFAULT (''),
[IsEncrypted] [bit] NOT NULL CONSTRAINT [DF_UserDataEncrypt_Encrypt] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserDataEncrypt] ADD CONSTRAINT [PK_MPUserDataEncrypt] PRIMARY KEY CLUSTERED  ([MPUserDataEncryptID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds information about which columns in UserData are encrypted', 'SCHEMA', N'dbo', 'TABLE', N'MPUserDataEncrypt', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The column name of UserData information', 'SCHEMA', N'dbo', 'TABLE', N'MPUserDataEncrypt', 'COLUMN', N'ColumnName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Whether or not the column data is encrypted', 'SCHEMA', N'dbo', 'TABLE', N'MPUserDataEncrypt', 'COLUMN', N'IsEncrypted'
GO
