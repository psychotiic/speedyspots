CREATE TABLE [dbo].[IAProperty]
(
[IAPropertyID] [int] NOT NULL IDENTITY(1, 1),
[SiteName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_SiteName] DEFAULT (''),
[SiteDomain] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_SiteDomain] DEFAULT (''),
[HostName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_HostName] DEFAULT (''),
[EmailNewAccount] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_EmailNewAccount] DEFAULT (''),
[EmailAddressFrom] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_EmailAddressFrom] DEFAULT (''),
[EmailAddressFromName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_EmailAddressFromName] DEFAULT (''),
[EmailBillings] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_EmailBillings] DEFAULT (''),
[FeedbackEmailProblem] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_FeedbackEmailProblem] DEFAULT (''),
[FeedbackEmailQuestion] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_FeedbackEmailQuestion] DEFAULT (''),
[ClosedMessage] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursMessage] DEFAULT (''),
[ClosedMessageDisplayAlways] [bit] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursOverride] DEFAULT ((0)),
[MondayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursMondayStartDateTime] DEFAULT (''),
[MondayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursMondayEndDateTime] DEFAULT (''),
[TuesdayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursTuesdayStartDateTime] DEFAULT (''),
[TuesdayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursTuesdayEndDateTime] DEFAULT (''),
[WednesdayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursWednesdayStartDateTime] DEFAULT (''),
[WednesdayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursWednesdayEndDateTime] DEFAULT (''),
[ThursdayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursThursdayStartDateTime] DEFAULT (''),
[ThursdayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursThursdayEndDateTime] DEFAULT (''),
[FridayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursFridayStartDateTime] DEFAULT (''),
[FridayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_OfficeHoursFridayEndDateTime] DEFAULT (''),
[AuthorizeNetIsDebug] [bit] NOT NULL CONSTRAINT [DF_IAProperty_AuthorizeNetIsDebug] DEFAULT ((0)),
[AuthorizeNetLoginID] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_AuthorizeNetLoginID] DEFAULT (''),
[AuthorizeNetTransactionKey] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProperty_AuthorizeNetTransactionKey] DEFAULT (''),
[QBLastInvoiceImportDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProperty_QBLastInvoiceImportDateTime] DEFAULT (''),
[RecutRequestThreshold] [int] NOT NULL CONSTRAINT [DF_IAProperty_RecutRequestThreshold] DEFAULT ((0))
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAProperty] ADD CONSTRAINT [PK_IAProperty] PRIMARY KEY CLUSTERED  ([IAPropertyID]) ON [PRIMARY]
GO
