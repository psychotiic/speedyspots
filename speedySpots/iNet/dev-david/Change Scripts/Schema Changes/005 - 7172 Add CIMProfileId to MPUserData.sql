/*
   Tuesday, May 7, 201310:01:32 AM
   User: 
   Server: MATT-W530
   Database: SpeedySpots
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MPUserData ADD
	CIMProfileId int NOT NULL CONSTRAINT DF_MPUserData_CIMProfileId DEFAULT 0
GO
ALTER TABLE dbo.MPUserData SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
