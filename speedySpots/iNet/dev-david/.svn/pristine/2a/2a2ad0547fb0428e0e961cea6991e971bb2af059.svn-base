CREATE TABLE [dbo].[MPOrgData]
(
[MPOrgDataID] [uniqueidentifier] NOT NULL,
[MPOrgID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPOrgData_MPOrgGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[IARateCardID] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_IARateCardID] DEFAULT (''),
[Address1] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_Address1] DEFAULT (''),
[Address2] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_Address2] DEFAULT (''),
[City] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_City] DEFAULT (''),
[State] [nvarchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_State] DEFAULT (''),
[Zip] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_Zip] DEFAULT (''),
[Phone] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_Phone] DEFAULT (''),
[Fax] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_Fax] DEFAULT (''),
[BillingAddress1] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_BillingAddress1] DEFAULT (''),
[BillingAddress2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_BillingAddress2] DEFAULT (''),
[BillingCity] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_BillingCity] DEFAULT (''),
[BillingState] [nvarchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_BillingState] DEFAULT (''),
[BillingZip] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_BillingZip] DEFAULT (''),
[BillingName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_BillingName] DEFAULT (''),
[BillingPhone] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_BillingPhone] DEFAULT (''),
[EmailInvoice] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_EmailInvoice] DEFAULT (''),
[IsPayFirst] [nvarchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_IsPayFirst] DEFAULT (''),
[Notes] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_Notes] DEFAULT (''),
[IsVerified] [nvarchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_MPOrgData_IsVerified] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPOrgData] ADD CONSTRAINT [PK_MPOrgData] PRIMARY KEY CLUSTERED  ([MPOrgDataID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPOrgData] ADD CONSTRAINT [FK_MPOrgData_MPOrg] FOREIGN KEY ([MPOrgID]) REFERENCES [dbo].[MPOrg] ([MPOrgID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds data associated with specific organizations, this is application developer specific information, by default this table will only contain OrgID upon new installation.', 'SCHEMA', N'dbo', 'TABLE', N'MPOrgData', NULL, NULL
GO
