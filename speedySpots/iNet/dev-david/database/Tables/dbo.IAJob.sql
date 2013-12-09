CREATE TABLE [dbo].[IAJob]
(
[IAJobID] [int] NOT NULL IDENTITY(1, 1),
[IARequestID] [int] NOT NULL CONSTRAINT [DF_IAJob_IARequestID] DEFAULT ((0)),
[IAJobStatusID] [int] NOT NULL CONSTRAINT [DF_IAJob_IAJobStatusID] DEFAULT ((0)),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAJob_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPUserIDCompleted] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAJob_MPUserIDCompleted] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Sequence] [int] NOT NULL CONSTRAINT [DF_IAJob_Sequence] DEFAULT ((0)),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAJob_Name] DEFAULT (''),
[Language] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAJob_Language] DEFAULT (''),
[Notes] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAJob_Notes] DEFAULT (''),
[QuantityMusic] [int] NOT NULL CONSTRAINT [DF_IAJob_MusicQuantity] DEFAULT ((0)),
[QuantitySFX] [int] NOT NULL CONSTRAINT [DF_IAJob_SFXQuantity] DEFAULT ((0)),
[QuantityProduction] [int] NOT NULL CONSTRAINT [DF_IAJob_ProductionQuantity] DEFAULT ((0)),
[QuantityConvert] [int] NOT NULL CONSTRAINT [DF_IAJob_ConvertQuantity] DEFAULT ((0)),
[PriceMusic] [money] NOT NULL CONSTRAINT [DF_IAJob_PriceMusic] DEFAULT ((0)),
[PriceSFX] [money] NOT NULL CONSTRAINT [DF_IAJob_PriceSFX] DEFAULT ((0)),
[PriceProduction] [money] NOT NULL CONSTRAINT [DF_IAJob_PriceProduction] DEFAULT ((0)),
[PriceConvert] [money] NOT NULL CONSTRAINT [DF_IAJob_PriceConvert] DEFAULT ((0)),
[IsASAP] [bit] NOT NULL CONSTRAINT [DF_IAJob_IsASAP] DEFAULT ((0)),
[IsMusic] [bit] NOT NULL CONSTRAINT [DF_IAJob_IsProductionMusic] DEFAULT ((0)),
[IsSFX] [bit] NOT NULL CONSTRAINT [DF_IAJob_IsProductionSFX] DEFAULT ((0)),
[IsProduction] [bit] NOT NULL CONSTRAINT [DF_IAJob_IsProduction] DEFAULT ((0)),
[IsConvert] [bit] NOT NULL CONSTRAINT [DF_IAJob_IsConvert] DEFAULT ((0)),
[DueDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAJob_DueDateTime] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAJob_CreatedDateTime] DEFAULT (''),
[CompletedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAJob_CompletedDateTime] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAJob] ADD CONSTRAINT [PK_IAJob] PRIMARY KEY CLUSTERED  ([IAJobID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAJob] ADD CONSTRAINT [FK_IAJob_IAJobStatus] FOREIGN KEY ([IAJobStatusID]) REFERENCES [dbo].[IAJobStatus] ([IAJobStatusID])
GO
ALTER TABLE [dbo].[IAJob] ADD CONSTRAINT [FK_IAJob_IARequest] FOREIGN KEY ([IARequestID]) REFERENCES [dbo].[IARequest] ([IARequestID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IAJob] ADD CONSTRAINT [FK_IAJob_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID])
GO
