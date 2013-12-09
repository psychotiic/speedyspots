CREATE TABLE [dbo].[MPPrivilegeCategory]
(
[MPPrivilegeCategoryID] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_PrivilegeCategory_Name] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPPrivilegeCategory_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPPrivilegeCategory] ADD CONSTRAINT [PK_MPPrivilegeCategory] PRIMARY KEY CLUSTERED  ([MPPrivilegeCategoryID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - The list of privilege categories in the security system', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilegeCategory', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The name of the privilege category', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilegeCategory', 'COLUMN', N'Name'
GO
