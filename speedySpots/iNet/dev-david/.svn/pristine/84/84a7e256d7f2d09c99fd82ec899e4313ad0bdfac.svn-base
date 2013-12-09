CREATE TABLE [dbo].[MPPrivilegePrivilegeType]
(
[MPPrivilegePrivilegeTypeID] [uniqueidentifier] NOT NULL,
[MPPrivilegeID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPPrivilegePrivilegeType_MPPrivilegeGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPPrivilegeTypeID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPPrivilegePrivilegeType_MPPrivilegeTypeGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPPrivilegePrivilegeType_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPPrivilegePrivilegeType] ADD CONSTRAINT [PK_MPPrivilegePrivilegeType] PRIMARY KEY CLUSTERED  ([MPPrivilegePrivilegeTypeID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPPrivilegePrivilegeType] ADD CONSTRAINT [FK_MPPrivilegePrivilegeType_MPPrivilege] FOREIGN KEY ([MPPrivilegeID]) REFERENCES [dbo].[MPPrivilege] ([MPPrivilegeID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MPPrivilegePrivilegeType] ADD CONSTRAINT [FK_MPPrivilegePrivilegeType_MPPrivilegePrivilegeType] FOREIGN KEY ([MPPrivilegePrivilegeTypeID]) REFERENCES [dbo].[MPPrivilegePrivilegeType] ([MPPrivilegePrivilegeTypeID])
GO
ALTER TABLE [dbo].[MPPrivilegePrivilegeType] ADD CONSTRAINT [FK_MPPrivilegePrivilegeType_MPPrivilegeType] FOREIGN KEY ([MPPrivilegeTypeID]) REFERENCES [dbo].[MPPrivilegeType] ([MPPrivilegeTypeID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - A joiner table between Privilege and PrivilegeType', 'SCHEMA', N'dbo', 'TABLE', N'MPPrivilegePrivilegeType', NULL, NULL
GO
