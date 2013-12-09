CREATE TABLE [dbo].[MPProperty]
(
[MPPropertyID] [uniqueidentifier] NOT NULL,
[Key] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPProperty_EncryptKey] DEFAULT (''),
[License] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPProperty_License] DEFAULT (''),
[TimespanBeforePasswordsExpire] [bigint] NOT NULL CONSTRAINT [DF_MPProperty_PasswordExpireAge] DEFAULT ((0)),
[TimespanBeforePasswordsExpireToNotifyUsers] [bigint] NOT NULL CONSTRAINT [DF_MPProperty_PasswordExpireNotify] DEFAULT ((0)),
[TimespanBeforeTempPasswordsExpire] [bigint] NOT NULL CONSTRAINT [DF_MPProperty_PasswordTempExpireAge] DEFAULT ((0)),
[TimespanSinceLastLoginBeforeLockout] [bigint] NOT NULL CONSTRAINT [DF_MPProperty_LastLoginThreshold] DEFAULT ((0)),
[TimespanToKeepLogs] [bigint] NOT NULL CONSTRAINT [DF_MPProperty_DaysToKeepLogs] DEFAULT ((0)),
[MaxPasswordHistoryCount] [int] NOT NULL CONSTRAINT [DF_MPProperty_PasswordTrackCount] DEFAULT ((0)),
[MaxFailedLoginAttemptsBeforeLockout] [int] NOT NULL CONSTRAINT [DF_MPProperty_LockoutThreshold] DEFAULT ((0)),
[SmtpHost] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPProperty_SmtpHost] DEFAULT (''),
[SmtpPort] [int] NOT NULL CONSTRAINT [DF_MPProperty_SmtpPort] DEFAULT ((0)),
[SmtpUsername] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPProperty_SmtpUsername] DEFAULT (''),
[SmtpPassword] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPProperty_SmtpPassword] DEFAULT (''),
[RBASystemTrackCount] [int] NOT NULL CONSTRAINT [DF_MPProperty_RBASystemTrackCount] DEFAULT ((0)),
[DatabaseScriptsPath] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPProperty_DatabaseScriptsPath] DEFAULT (''),
[ApplicationID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPProperty_ApplicationID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[LastValidatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_MPProperty_LastValidatedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPProperty] ADD CONSTRAINT [PK_MPProperty_1] PRIMARY KEY CLUSTERED  ([MPPropertyID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds Member Protect properties that affect the system as a whole', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The encryption key associated with this installation of MemberProtect (NOT USED)', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', 'COLUMN', N'Key'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The license value associated with this installation of MemberProtect (NOT USED)', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', 'COLUMN', N'License'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The amount of invalid logins that may occur before the user is said to be locked out', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', 'COLUMN', N'MaxFailedLoginAttemptsBeforeLockout'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The amount of ''old'' passwords to track when a user changes their password', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', 'COLUMN', N'MaxPasswordHistoryCount'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The amount of hours since password creation/resetting at which point passwords will expire', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', 'COLUMN', N'TimespanBeforePasswordsExpire'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The amount of hours before the expiration time to notify the user to change their password', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', 'COLUMN', N'TimespanBeforePasswordsExpireToNotifyUsers'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The amount of hours a temporary password is allowed to exist, after which it expires', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', 'COLUMN', N'TimespanBeforeTempPasswordsExpire'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The max amount of days between logins that if exceeded will force security questions to be asked as a precaution', 'SCHEMA', N'dbo', 'TABLE', N'MPProperty', 'COLUMN', N'TimespanSinceLastLoginBeforeLockout'
GO
