/*
   Saturday, July 13, 20136:52:27 PM
   User: 
   Server: .
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
ALTER TABLE dbo.IARequestEstimate ADD
	PreAuthorizedPaymentCharged bit NOT NULL CONSTRAINT DF_IARequestEstimate_PreAuthorizedPaymentCharged DEFAULT 0
GO
ALTER TABLE dbo.IARequestEstimate SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
