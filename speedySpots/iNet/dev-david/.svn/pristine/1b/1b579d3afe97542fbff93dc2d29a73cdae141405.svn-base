CREATE TABLE [dbo].[MPLogSecurity]
(
[MPLogSecurityID] [uniqueidentifier] NOT NULL,
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPLogSecurity_MPUserGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[SessionID] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_LogSecurity_SessionID] DEFAULT (''),
[IPAddress] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPLogSecurity_IPAddress] DEFAULT (''),
[Type] [int] NOT NULL CONSTRAINT [DF_LogSecurity_Type] DEFAULT ((0)),
[Text] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_LogSecurity_Text] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPLogSecurity_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPLogSecurity] ADD CONSTRAINT [PK_MPLogSecurity] PRIMARY KEY CLUSTERED  ([MPLogSecurityID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect''s main security event logging table.', 'SCHEMA', N'dbo', 'TABLE', N'MPLogSecurity', NULL, NULL
GO
