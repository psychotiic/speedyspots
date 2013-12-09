CREATE TABLE [dbo].[IATalentUnavailability]
(
[IATalentUnavailabilityID] [int] NOT NULL IDENTITY(1, 1),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IATalentUnavailability_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPUserIDProducer] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IATalentUnavailability_MPUserIDProducer] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[FromDateTime] [datetime] NOT NULL CONSTRAINT [DF_IATalentUnavailability_FromDateTime] DEFAULT (''),
[ToDateTime] [datetime] NOT NULL CONSTRAINT [DF_IATalentUnavailability_ToDateTime] DEFAULT (''),
[Notes] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IATalentUnavailability_Notes] DEFAULT (''),
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IATalentUnavailability_Status] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IATalentUnavailability_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IATalentUnavailability] ADD CONSTRAINT [PK_IATalentUnavailability] PRIMARY KEY CLUSTERED  ([IATalentUnavailabilityID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IATalentUnavailability] ADD CONSTRAINT [FK_IATalentUnavailability_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
