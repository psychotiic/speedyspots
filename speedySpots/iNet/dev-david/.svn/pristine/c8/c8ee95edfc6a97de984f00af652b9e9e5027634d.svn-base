CREATE TABLE [dbo].[MPUser]
(
[MPUserID] [uniqueidentifier] NOT NULL,
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_Username] DEFAULT (''),
[Password] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_Password] DEFAULT (''),
[TempPassword] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_PasswordTemp] DEFAULT (''),
[TempPasswordExpiresOn] [datetime] NOT NULL CONSTRAINT [DF_User_PasswordTempExpiresOn] DEFAULT (''),
[IsForcePasswordChange] [bit] NOT NULL CONSTRAINT [DF_User_PasswordForceChange] DEFAULT ((0)),
[HasTempPasswordBeenUsed] [bit] NOT NULL CONSTRAINT [DF_User_IsPasswordTempUsed] DEFAULT ((0)),
[IsLocked] [bit] NOT NULL CONSTRAINT [DF_User_IsLocked] DEFAULT ((0)),
[Salt] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_Salt] DEFAULT (''),
[FailedLoginAttempts] [int] NOT NULL CONSTRAINT [DF_User_InvalidAttempts] DEFAULT ((0)),
[SessionId] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_SessionId] DEFAULT (''),
[MPRBAImageID] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RBAPhrase] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_RBAPhraseId] DEFAULT (''),
[IPAddress] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_IPAddress] DEFAULT (''),
[BrowserAgent] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_BrowserAgent] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_User_CreatedOn] DEFAULT (getdate()),
[LastLoginOn] [datetime] NOT NULL CONSTRAINT [DF_User_LoggedOn] DEFAULT (''),
[LastFailedLoginOn] [datetime] NOT NULL CONSTRAINT [DF_MPUser_LastFailedLoginOn] DEFAULT (''),
[PasswordChangedOn] [datetime] NOT NULL CONSTRAINT [DF_User_PasswordChangedOn] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUser] ADD CONSTRAINT [PK_MPUser] PRIMARY KEY CLUSTERED  ([MPUserID]) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Username] ON [dbo].[MPUser] ([Username]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Hold user account information within the MP system.', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The last browser agent string of the user', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'BrowserAgent'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The date/time the user profile record was created', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'CreatedOn'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Tracks the amount of invalid login attempts', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'FailedLoginAttempts'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Whether or not the temporary password was used to login', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'HasTempPasswordBeenUsed'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The last IP address of the user', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'IPAddress'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Whether or not the user should be prompted to change their password on their next login', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'IsForcePasswordChange'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Whether or not the user is locked out of the system', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'IsLocked'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The date/time the user last logged onto the system', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'LastLoginOn'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The password of the user, this is encrypted', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'Password'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The date/time the user last changed their password', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'PasswordChangedOn'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The RBA phrase associated with the user', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'RBAPhrase'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Salt appended into the users passwords to cause mass confusion to would be attackers', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'Salt'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Holds the ASP.NET session ID', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'SessionId'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The temporary password of the user, this is encrypted and will expire', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'TempPassword'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The date/time the users temporary password will expire on and no longer be useful', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'TempPasswordExpiresOn'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The username of the user, used to be called UserID in MP 4.3', 'SCHEMA', N'dbo', 'TABLE', N'MPUser', 'COLUMN', N'Username'
GO
