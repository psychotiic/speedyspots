CREATE TABLE [dbo].[IALabel]
(
[IALabelID] [int] NOT NULL IDENTITY(1, 1),
[Text] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsCustomerVisible] [bit] NOT NULL CONSTRAINT [DF_IALabel_IsCustomerVisible] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IALabel] ADD CONSTRAINT [PK_IALabel] PRIMARY KEY CLUSTERED  ([IALabelID]) ON [PRIMARY]
GO
