CREATE TABLE [dbo].[MPRBAImage]
(
[MPRBAImageID] [uniqueidentifier] NOT NULL,
[Image] [image] NOT NULL CONSTRAINT [DF_RBAImage_Image] DEFAULT (0x00000000),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPRBAImage_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRBAImage] ADD CONSTRAINT [PK_MPRBAImage] PRIMARY KEY CLUSTERED  ([MPRBAImageID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds RBA image information for the system, this is the list of images users are able to select from', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAImage', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The binary image information used within the system', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAImage', 'COLUMN', N'Image'
GO
