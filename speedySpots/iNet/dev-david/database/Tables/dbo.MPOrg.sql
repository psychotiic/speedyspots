CREATE TABLE [dbo].[MPOrg]
(
[MPOrgID] [uniqueidentifier] NOT NULL,
[MPOrgTypeID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPOrg_MPOrgTypeGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[SMSID] [int] NOT NULL IDENTITY(10, 1),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Org_Name] DEFAULT (''),
[IsIPLockdown] [bit] NOT NULL CONSTRAINT [DF_Org_IsIPLockDown] DEFAULT ((0)),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Org_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPOrg] ADD CONSTRAINT [PK_MPOrg] PRIMARY KEY CLUSTERED  ([MPOrgID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPOrg] ADD CONSTRAINT [FK_MPOrg_MPOrgType] FOREIGN KEY ([MPOrgTypeID]) REFERENCES [dbo].[MPOrgType] ([MPOrgTypeID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds organization information', 'SCHEMA', N'dbo', 'TABLE', N'MPOrg', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Datetime field that stores the date the organization record was created', 'SCHEMA', N'dbo', 'TABLE', N'MPOrg', 'COLUMN', N'CreatedOn'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Boolean value to specify whether an organization has enabled IP Lock Down for logon.', 'SCHEMA', N'dbo', 'TABLE', N'MPOrg', 'COLUMN', N'IsIPLockdown'
GO
