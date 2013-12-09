CREATE TABLE [dbo].[tblCustomer]
(
[ID] [int] NOT NULL CONSTRAINT [DF__tblCustomer__ID__3C89F72A] DEFAULT (CONVERT([int],CONVERT([varbinary](4000),newid(),(0)),(0))),
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PathUser] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PathMediaDL] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PathMediaUL] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Disabled] [bit] NOT NULL CONSTRAINT [DF__tblCustom__Disab__3D7E1B63] DEFAULT ((0)),
[Inactive] [bit] NOT NULL CONSTRAINT [DF__tblCustom__Inact__3E723F9C] DEFAULT ((0)),
[RatesHide] [bit] NOT NULL CONSTRAINT [DF__tblCustom__Rates__3F6663D5] DEFAULT ((0)),
[IDRateCard] [tinyint] NOT NULL CONSTRAINT [DF__tblCustom__IDRat__405A880E] DEFAULT ((1)),
[PayFirst] [bit] NULL CONSTRAINT [DF__tblCustom__PayFi__414EAC47] DEFAULT ((0)),
[SSMA_TimeStamp] [timestamp] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCustomer] ADD CONSTRAINT [tblCustomer$PrimaryKey] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [tblCustomer$ID] ON [dbo].[tblCustomer] ([ID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [tblCustomer${C6AC5C6A-82F5-4535-A1AF-FDD38A39F74F}] ON [dbo].[tblCustomer] ([IDRateCard]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [tblCustomer$IDRateCard] ON [dbo].[tblCustomer] ([IDRateCard]) ON [PRIMARY]
GO
