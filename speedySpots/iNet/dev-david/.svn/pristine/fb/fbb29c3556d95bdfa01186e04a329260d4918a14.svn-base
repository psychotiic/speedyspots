CREATE TABLE [dbo].[IASpot]
(
[IASpotID] [int] NOT NULL IDENTITY(1, 1),
[IAProductionOrderID] [int] NOT NULL CONSTRAINT [DF_IASpot_IAProductionOrderID] DEFAULT ((0)),
[IASpotStatusID] [int] NOT NULL CONSTRAINT [DF_IASpot_IASpotStatusID] DEFAULT ((0)),
[IASpotTypeID] [int] NOT NULL CONSTRAINT [DF_IASpot_IASpotTypeID] DEFAULT ((0)),
[PurchaseOrderNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpot_PurchaseOrderNumber] DEFAULT (''),
[DueDateTime] [datetime] NOT NULL CONSTRAINT [DF_IASpot_DueDateTime] DEFAULT (''),
[Length] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpot_SpotLength] DEFAULT (''),
[LengthActual] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpot_LengthActual] DEFAULT (''),
[Title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpot_Title] DEFAULT (''),
[ProductionNotes] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpot_ProductionNotes] DEFAULT (''),
[Script] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpot_Script] DEFAULT (''),
[IsAsap] [bit] NOT NULL CONSTRAINT [DF_IASpot_IsAsap] DEFAULT ((0)),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IASpot_CreatedDateTime] DEFAULT (''),
[CompletedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IASpot_CompletedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IASpot] ADD CONSTRAINT [PK_IASpot] PRIMARY KEY CLUSTERED  ([IASpotID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IASpot] ADD CONSTRAINT [FK_IASpot_IAProductionOrder] FOREIGN KEY ([IAProductionOrderID]) REFERENCES [dbo].[IAProductionOrder] ([IAProductionOrderID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IASpot] ADD CONSTRAINT [FK_IASpot_IASpotStatus] FOREIGN KEY ([IASpotStatusID]) REFERENCES [dbo].[IASpotStatus] ([IASpotStatusID])
GO
ALTER TABLE [dbo].[IASpot] ADD CONSTRAINT [FK_IASpot_IASpotType] FOREIGN KEY ([IASpotTypeID]) REFERENCES [dbo].[IASpotType] ([IASpotTypeID])
GO
