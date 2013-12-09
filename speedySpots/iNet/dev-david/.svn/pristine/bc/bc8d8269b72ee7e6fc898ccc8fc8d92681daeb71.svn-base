USE [SpeedySpots]
GO

/****** Object:  Table [dbo].[Worker_InvoicePackage]    Script Date: 04/03/2012 20:41:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Worker_InvoicePackage](
	[ImportID] [int] IDENTITY(1,1) NOT NULL,
	[ArrivedDateTime] [datetime] NULL,
	[ZipFilePath] [nvarchar](200) NULL,
	[InvoiceReceived] [int] NULL,
	[InvoiceProcessed] [int] NULL,
	[InvoiceEmailsSent] [int] NULL,
	[InvoiceEmailError] [int] NULL,
	[ProcessedDateTime] [datetime] NULL,
	[Status] [nvarchar](20) NULL,
	[ErrorMessage] [nvarchar](MAX) NULL,
 CONSTRAINT [PK_Worker_InvoicePackage] PRIMARY KEY CLUSTERED 
(
	[ImportID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

