CREATE TABLE [dbo].[IARequestStatus]
(
[IARequestStatusID] [int] NOT NULL CONSTRAINT [DF_IARequestStatus_IARequestStatusID] DEFAULT ((0)),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestStatus_Name] DEFAULT (''),
[SortOrder] [int] NOT NULL CONSTRAINT [DF_IARequestStatus_SortOrder] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestStatus] ADD CONSTRAINT [PK_IARequestStatus] PRIMARY KEY CLUSTERED  ([IARequestStatusID]) ON [PRIMARY]
GO
