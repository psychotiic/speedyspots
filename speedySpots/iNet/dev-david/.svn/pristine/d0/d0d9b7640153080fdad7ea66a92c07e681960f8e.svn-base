CREATE TABLE [dbo].[MPUserData]
(
[MPUserDataID] [uniqueidentifier] NOT NULL,
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPUserData_MPUserGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[FirstName] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_FirstName] DEFAULT (''),
[LastName] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_LastName] DEFAULT (''),
[Phone] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_Phone] DEFAULT (''),
[PhoneExtension] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_PhoneExtension] DEFAULT (''),
[MobilePhone] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_MobilePhone] DEFAULT (''),
[Comments] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_Comments] DEFAULT (''),
[Department] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_Deparment] DEFAULT (''),
[NotificationEmails] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_NotificationEmails] DEFAULT (''),
[IsCustomer] [nvarchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_IsCustomer] DEFAULT (''),
[IsStaff] [nvarchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_IsProducer] DEFAULT (''),
[IsTalent] [nvarchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_IsTalent] DEFAULT (''),
[IsAdmin] [nvarchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_IsAdmin] DEFAULT (''),
[DefaultTab] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_DefaultTab] DEFAULT (''),
[Notes] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_Notes] DEFAULT (''),
[GridPageSize] [nvarchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPUserData_GridPageSize] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserData] ADD CONSTRAINT [PK_MPUserData] PRIMARY KEY CLUSTERED  ([MPUserDataID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserData] ADD CONSTRAINT [FK_MPUserData_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'Holds individual user profile elements. Each column represents a single user profile item. MP will automatically encrypt columns based on settings in MPUserDataEncrypt', 'SCHEMA', N'dbo', 'TABLE', N'MPUserData', NULL, NULL
GO
