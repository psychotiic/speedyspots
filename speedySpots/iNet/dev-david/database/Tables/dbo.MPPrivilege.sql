CREATE TABLE [dbo].[MPPrivilege]
(
[MPPrivilegeID] [uniqueidentifier] NOT NULL,
[MPPrivilegeCategoryID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPPrivilege_MPPrivilegeCategoryGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Code] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Privilege_Code] DEFAULT (''),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Privilege_Name] DEFAULT (''),
[Description] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Privilege_Description] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Privilege_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPPrivilege] ADD CONSTRAINT [PK_MPPrivilege] PRIMARY KEY CLUSTERED  ([MPPrivilegeID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPPrivilege] ADD CONSTRAINT [FK_MPPrivilege_MPPrivilegeCategory] FOREIGN KEY ([MPPrivilegeCategoryID]) REFERENCES [dbo].[MPPrivilegeCategory] ([MPPrivilegeCategoryID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds privilege information for the MP security system', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilege', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The privilege code', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilege', 'COLUMN', N'Code'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The description of the privilege', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilege', 'COLUMN', N'Description'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The name of the privilege', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilege', 'COLUMN', N'Name'
GO
