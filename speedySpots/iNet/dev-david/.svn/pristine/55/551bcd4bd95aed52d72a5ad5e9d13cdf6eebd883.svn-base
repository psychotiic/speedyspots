CREATE TABLE [dbo].[tblSecUser]
(
[ID] [int] NOT NULL CONSTRAINT [DF__tblSecUser__ID__49E3F248] DEFAULT (CONVERT([int],CONVERT([varbinary](4000),newid(),(0)),(0))),
[NameFirst] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NameLast] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IDUser] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Password] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Company] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IDCustomerDepartment] [int] NULL,
[Address1] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address2] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [nvarchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PostalCode] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Phone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneFax] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneMobile] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailNotifyList] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Pager] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InstantMessenger] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Website] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[QuickbooksAccount] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PathUser] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PathMediaDL] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PathMediaUL] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Note] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Inactive] [bit] NOT NULL CONSTRAINT [DF__tblSecUse__Inact__4AD81681] DEFAULT ((0)),
[RatesHide] [bit] NOT NULL CONSTRAINT [DF__tblSecUse__Rates__4BCC3ABA] DEFAULT ((0)),
[Validated] [bit] NULL CONSTRAINT [DF__tblSecUse__Valid__4CC05EF3] DEFAULT ((0)),
[SSMA_TimeStamp] [timestamp] NOT NULL,
[IsUpgraded] [bit] NOT NULL CONSTRAINT [DF_tblSecUser_IsUpgraded] DEFAULT ((0)),
[MPUserID] [uniqueidentifier] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE NONCLUSTERED INDEX [tblSecUser$ID] ON [dbo].[tblSecUser] ([ID]) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [tblSecUser$IDCustomerDepartment] ON [dbo].[tblSecUser] ([IDCustomerDepartment]) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [tblSecUser$tblCustomerDepartmenttblSecUser] ON [dbo].[tblSecUser] ([IDCustomerDepartment]) ON [PRIMARY]

CREATE UNIQUE NONCLUSTERED INDEX [tblSecUser$IDUser] ON [dbo].[tblSecUser] ([IDUser]) ON [PRIMARY]

ALTER TABLE [dbo].[tblSecUser] WITH NOCHECK ADD
CONSTRAINT [tblSecUser$tblCustomerDepartmenttblSecUser] FOREIGN KEY ([IDCustomerDepartment]) REFERENCES [dbo].[tblCustomerDepartment] ([ID]) ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tblSecUser] ADD CONSTRAINT [tblSecUser$PrimaryKey] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
