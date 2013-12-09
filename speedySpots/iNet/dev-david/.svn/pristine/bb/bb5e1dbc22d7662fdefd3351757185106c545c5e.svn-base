CREATE TABLE [dbo].[IAOrderLineItem]
(
[IAOrderLineItemID] [int] NOT NULL IDENTITY(1, 1),
[IAOrderID] [int] NOT NULL CONSTRAINT [DF_IAOrderLineItem_IAOrderID] DEFAULT ((0)),
[Description] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrderLineItem_Description] DEFAULT (''),
[Quantity] [int] NOT NULL CONSTRAINT [DF_IAOrderLineItem_Quantity] DEFAULT ((0)),
[Price] [decimal] (18, 0) NOT NULL CONSTRAINT [DF_Table_1_Cost] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAOrderLineItem] ADD CONSTRAINT [PK_IAOrderLineItem] PRIMARY KEY CLUSTERED  ([IAOrderLineItemID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAOrderLineItem] ADD CONSTRAINT [FK_IAOrderLineItem_IAOrder] FOREIGN KEY ([IAOrderID]) REFERENCES [dbo].[IAOrder] ([IAOrderID]) ON DELETE CASCADE
GO
