CREATE TABLE [dbo].[IARequestLabel]
(
[IARequestLabelID] [int] NOT NULL IDENTITY(1, 1),
[IARequestID] [int] NOT NULL CONSTRAINT [DF_IARequestLabel_IARequestID] DEFAULT ((0)),
[IALabelID] [int] NOT NULL CONSTRAINT [DF_IARequestLabel_IALabelID] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestLabel] ADD CONSTRAINT [PK_IARequestLabel] PRIMARY KEY CLUSTERED  ([IARequestLabelID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestLabel] ADD CONSTRAINT [FK_IARequestLabel_IALabel] FOREIGN KEY ([IALabelID]) REFERENCES [dbo].[IALabel] ([IALabelID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IARequestLabel] ADD CONSTRAINT [FK_IARequestLabel_IARequest] FOREIGN KEY ([IARequestID]) REFERENCES [dbo].[IARequest] ([IARequestID]) ON DELETE CASCADE
GO
