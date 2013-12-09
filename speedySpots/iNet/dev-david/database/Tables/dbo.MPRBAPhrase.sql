CREATE TABLE [dbo].[MPRBAPhrase]
(
[MPRBAPhraseID] [uniqueidentifier] NOT NULL,
[Phrase] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_RBAPhrase_Phrase] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPRBAPhrase_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRBAPhrase] ADD CONSTRAINT [PK_MPRBAPhrase] PRIMARY KEY CLUSTERED  ([MPRBAPhraseID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds RBA phrase information, these phrases are stock phrases used to spoof would-be attackers, these are NOT used by users', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAPhrase', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The generic RBA phrase used to throw off attackers', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAPhrase', 'COLUMN', N'Phrase'
GO
