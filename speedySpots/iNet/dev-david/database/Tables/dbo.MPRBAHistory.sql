CREATE TABLE [dbo].[MPRBAHistory]
(
[MPRBAHistoryID] [uniqueidentifier] NOT NULL,
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPRBAHistory_MPUserGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[IPAddress] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_RBAHistory_IPAddress] DEFAULT (''),
[BrowserAgent] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_RBAHistory_BrowserAgent] DEFAULT (''),
[Token] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_RBAHistory_CookieToken] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_RBAHistory_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRBAHistory] ADD CONSTRAINT [PK_MPRBAHistory] PRIMARY KEY CLUSTERED  ([MPRBAHistoryID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRBAHistory] ADD CONSTRAINT [FK_MPRBAHistory_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds Risk Based Authentication user history which is used with fingerprinting the users machine', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAHistory', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The browser agent string of the user', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAHistory', 'COLUMN', N'BrowserAgent'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The IP address of the user', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAHistory', 'COLUMN', N'IPAddress'
GO
