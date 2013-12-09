USE [SpeedySpots]
GO

/****** Object:  Table [dbo].[Worker_InvoiceNoticeSent]    Script Date: 04/03/2012 20:41:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Worker_InvoiceNoticeSent](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[ImportID] [int] NULL,
	[InvoiceNumber] [nvarchar](50) NULL,
	[SentTo] [nvarchar](MAX) NULL,
	[CCEmailAddresses] [nvarchar](MAX) NULL,
	[SentDateTime] [datetime] NULL,
	[Status] [nvarchar](75) NULL,
	[PostMarkErrorCode] [nvarchar](75) NULL,
	[PostMarkMessage] [nvarchar](MAX) NULL,
	[PostMarkSubmittedAt] [datetime] NULL,
	[ServiceErrorMessage] [nvarchar](MAX) NULL,
	[PostMarkMessageID] [nvarchar](50) NULL,
 CONSTRAINT [PK_Worker_InvoiceNoticeSent] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

