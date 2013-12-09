CREATE TABLE [dbo].[tblCustomerDepartment]
(
[ID] [int] NOT NULL CONSTRAINT [DF__tblCustomerD__ID__4336F4B9] DEFAULT (CONVERT([int],CONVERT([varbinary](4000),newid(),(0)),(0))),
[IDCustomer] [int] NOT NULL CONSTRAINT [DF__tblCustom__IDCus__442B18F2] DEFAULT ((0)),
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Addressee] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address1] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PostalCode] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Country] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Phone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneFax] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[QBAccount] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Billable] [bit] NOT NULL CONSTRAINT [DF__tblCustom__Billa__451F3D2B] DEFAULT ((0)),
[Inactive] [bit] NOT NULL CONSTRAINT [DF__tblCustom__Inact__46136164] DEFAULT ((0)),
[PeerView] [bit] NULL CONSTRAINT [DF__tblCustom__PeerV__4707859D] DEFAULT ((0)),
[PeerShare] [bit] NULL CONSTRAINT [DF__tblCustom__PeerS__47FBA9D6] DEFAULT ((0)),
[SSMA_TimeStamp] [timestamp] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCustomerDepartment] ADD CONSTRAINT [tblCustomerDepartment$PrimaryKey] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [tblCustomerDepartment$ID] ON [dbo].[tblCustomerDepartment] ([ID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [tblCustomerDepartment${1AE46A44-CD48-4E1B-B626-E7FF6679FC8A}] ON [dbo].[tblCustomerDepartment] ([IDCustomer]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [tblCustomerDepartment$IDCompany] ON [dbo].[tblCustomerDepartment] ([IDCustomer]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [tblCustomerDepartment$PostalCode] ON [dbo].[tblCustomerDepartment] ([PostalCode]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCustomerDepartment] WITH NOCHECK ADD CONSTRAINT [tblCustomerDepartment${1AE46A44-CD48-4E1B-B626-E7FF6679FC8A}] FOREIGN KEY ([IDCustomer]) REFERENCES [dbo].[tblCustomer] ([ID]) ON UPDATE CASCADE
GO
