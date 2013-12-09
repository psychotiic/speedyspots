CREATE TABLE [dbo].[MPOrgType]
(
[MPOrgTypeID] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_OrgType_Name] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPOrgType_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPOrgType] ADD CONSTRAINT [PK_MPOrgType] PRIMARY KEY CLUSTERED  ([MPOrgTypeID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds organization type information', 'SCHEMA', N'dbo', 'TABLE', N'MPOrgType', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The name of the organization type', 'SCHEMA', N'dbo', 'TABLE', N'MPOrgType', 'COLUMN', N'Name'
GO
