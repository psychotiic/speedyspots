CREATE TABLE [dbo].[IAProductionOrder]
(
[IAProductionOrderID] [int] NOT NULL IDENTITY(1, 1),
[IAJobID] [int] NOT NULL CONSTRAINT [DF_IAProductionOrder_IAOrderID] DEFAULT ((0)),
[IAProductionOrderStatusID] [int] NOT NULL CONSTRAINT [DF_IAProductionOrder_IAProductionOrderStatusID] DEFAULT ((0)),
[IATalentTypeID] [int] NOT NULL CONSTRAINT [DF_IAProductionOrder_TalentTypeID] DEFAULT ((0)),
[MPUserIDTalent] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAProductionOrder_MPUserTalentID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPUserIDProducer] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAProductionOrder_MPUserIDProducer] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPUserIDOnHold] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAProductionOrder_MPUserIDOnHold] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Notes] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAProductionOrder_Notes] DEFAULT (''),
[HasBeenViewedByTalent] [bit] NOT NULL CONSTRAINT [DF_IAProductionOrder_HasBeenViewedByTalent] DEFAULT ((0)),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProductionOrder_CreatedDateTime] DEFAULT (''),
[CompletedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProductionOrder_CompletedDateTime] DEFAULT (''),
[ProductionDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProductionOrder_ProductionDateTime] DEFAULT (''),
[OnHoldDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAProductionOrder_OnHoldDateTime] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAProductionOrder] ADD CONSTRAINT [PK_IAProductionOrder] PRIMARY KEY CLUSTERED  ([IAProductionOrderID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAProductionOrder] ADD CONSTRAINT [FK_IAProductionOrder_IAJob] FOREIGN KEY ([IAJobID]) REFERENCES [dbo].[IAJob] ([IAJobID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IAProductionOrder] ADD CONSTRAINT [FK_IAProductionOrder_IAProductionOrderStatus] FOREIGN KEY ([IAProductionOrderStatusID]) REFERENCES [dbo].[IAProductionOrderStatus] ([IAProductionOrderStatusID])
GO
ALTER TABLE [dbo].[IAProductionOrder] ADD CONSTRAINT [FK_IAProductionOrder_MPUser] FOREIGN KEY ([MPUserIDTalent]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
