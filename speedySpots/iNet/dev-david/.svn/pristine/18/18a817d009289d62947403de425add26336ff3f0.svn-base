CREATE TABLE [dbo].[IARequestProduction]
(
[IARequestProductionID] [int] NOT NULL IDENTITY(1, 1),
[IARequestID] [int] NOT NULL CONSTRAINT [DF_IARequestProduction_IARequestID] DEFAULT ((0)),
[IAJobID] [int] NOT NULL CONSTRAINT [DF_IARequestProduction_IAJobID] DEFAULT ((0)),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IARequestProduction_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Notes] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestProduction_Notes] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IARequestProduction_CreatedDateTime] DEFAULT (''),
[HasRecutBeenRequested] [bit] NOT NULL CONSTRAINT [DF_IARequestProduction_HasRecutBeenRequested] DEFAULT ((0)),
[RecutRequestDateTime] [datetime] NOT NULL CONSTRAINT [DF_IARequestProduction_RecutRequestDateTime] DEFAULT (''),
[RecutNotes] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestProduction_RecutNotes] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestProduction] ADD CONSTRAINT [PK_IARequestProduction] PRIMARY KEY CLUSTERED  ([IARequestProductionID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestProduction] ADD CONSTRAINT [FK_IARequestProduction_IAJob] FOREIGN KEY ([IAJobID]) REFERENCES [dbo].[IAJob] ([IAJobID])
GO
ALTER TABLE [dbo].[IARequestProduction] ADD CONSTRAINT [FK_IARequestProduction_IARequest] FOREIGN KEY ([IARequestID]) REFERENCES [dbo].[IARequest] ([IARequestID]) ON DELETE CASCADE
GO
