CREATE TABLE [dbo].[IATalentSchedule]
(
[IATalentScheduleID] [int] NOT NULL IDENTITY(1, 1),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IATalentSchedule_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MondayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeMondayIn] DEFAULT (''),
[MondayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeMondayOut] DEFAULT (''),
[TuesdayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeTuesdayIn] DEFAULT (''),
[TuesdayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeTuesdayOut] DEFAULT (''),
[WednesdayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeWednesdayIn] DEFAULT (''),
[WednesdayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_IATalentSchedule_WednesdayOutDateTime] DEFAULT (''),
[ThursdayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeThursdayIn] DEFAULT (''),
[ThursdayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeThursdayOut] DEFAULT (''),
[FridayInDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeFridayIn] DEFAULT (''),
[FridayOutDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_DateTimeFridayOut] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IATalentSchedule] ADD CONSTRAINT [PK_IATalentSchedule] PRIMARY KEY CLUSTERED  ([IATalentScheduleID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IATalentSchedule] ADD CONSTRAINT [FK_IATalentSchedule_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
