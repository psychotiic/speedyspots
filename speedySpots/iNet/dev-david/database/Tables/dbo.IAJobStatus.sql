CREATE TABLE [dbo].[IAJobStatus]
(
[IAJobStatusID] [int] NOT NULL,
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAJobStatus_Name] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAJobStatus] ADD CONSTRAINT [PK_IAJobStatus] PRIMARY KEY CLUSTERED  ([IAJobStatusID]) ON [PRIMARY]
GO
