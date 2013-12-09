CREATE TABLE [dbo].[MPIPLockdown]
(
[MPIPLockdownID] [uniqueidentifier] NOT NULL,
[MPOrgID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPIPTable_MPOrgGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[IPNetwork] [nvarchar] (11) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IPTable_IPNetwork] DEFAULT (''),
[StartNode] [nvarchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IPTable_StartNode] DEFAULT (''),
[EndNode] [nvarchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IPTable_EndNode] DEFAULT (''),
[Description] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IPTable_Description] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPIPLockdown_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPIPLockdown] ADD CONSTRAINT [PK_MPIPTable] PRIMARY KEY CLUSTERED  ([MPIPLockdownID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPIPLockdown] ADD CONSTRAINT [FK_MPIPTable_MPOrg] FOREIGN KEY ([MPOrgID]) REFERENCES [dbo].[MPOrg] ([MPOrgID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'Holds ranges of IP addresses that are used when an org has enabled IP Lockdown', 'SCHEMA', N'dbo', 'TABLE', N'MPIPLockdown', NULL, NULL
GO
