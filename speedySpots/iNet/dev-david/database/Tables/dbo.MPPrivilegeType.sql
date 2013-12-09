CREATE TABLE [dbo].[MPPrivilegeType]
(
[MPPrivilegeTypeID] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_PrivilegeType_Name] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPPrivilegeType_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPPrivilegeType] ADD CONSTRAINT [PK_MPPrivilegeType] PRIMARY KEY CLUSTERED  ([MPPrivilegeTypeID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds privilege type information', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilegeType', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The name of the privilege type', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilegeType', 'COLUMN', N'Name'
GO
