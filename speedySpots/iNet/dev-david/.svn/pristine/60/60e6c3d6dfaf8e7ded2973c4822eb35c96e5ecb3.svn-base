CREATE TABLE [dbo].[MPOrgDataEncrypt]
(
[MPOrgDataEncryptID] [uniqueidentifier] NOT NULL,
[ColumnName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_OrgDataEncrypt_ColumnName] DEFAULT (''),
[IsEncrypted] [bit] NOT NULL CONSTRAINT [DF_OrgDataEncrypt_IsEncrypted] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPOrgDataEncrypt] ADD CONSTRAINT [PK_MPOrgDataEncrypt] PRIMARY KEY CLUSTERED  ([MPOrgDataEncryptID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds information about which OrgData columns are encrypted', 'SCHEMA', N'dbo', 'TABLE', N'MPOrgDataEncrypt', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The column name of columns in OrgData', 'SCHEMA', N'dbo', 'TABLE', N'MPOrgDataEncrypt', 'COLUMN', N'ColumnName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Whether or not the column data is encrypted', 'SCHEMA', N'dbo', 'TABLE', N'MPOrgDataEncrypt', 'COLUMN', N'IsEncrypted'
GO
