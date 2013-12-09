CREATE TABLE [dbo].[IASpotFeeType]
(
[IASpotFeeTypeID] [int] NOT NULL,
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpotFeeType_Name] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IASpotFeeType] ADD CONSTRAINT [PK_IASpotFeeType] PRIMARY KEY CLUSTERED  ([IASpotFeeTypeID]) ON [PRIMARY]
GO