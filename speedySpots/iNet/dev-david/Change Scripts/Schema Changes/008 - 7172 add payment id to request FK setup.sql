/*
   Wednesday, August 7, 20139:14:35 AM
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
ALTER TABLE dbo.IACustomerCreditCard SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.IARequest ADD
	IACustomerCreditCardID int NOT NULL CONSTRAINT DF_IARequest_IACustomerCreditCardID DEFAULT -1
GO
--ALTER TABLE dbo.IARequest ADD CONSTRAINT
--	FK_IARequest_IACustomerCreditCard FOREIGN KEY
--	(
--	IACustomerCreditCardID
--	) REFERENCES dbo.IACustomerCreditCard
--	(
--	IACustomerCreditCardID
--	) ON UPDATE  NO ACTION 
--	 ON DELETE  NO ACTION 
	
--GO
--ALTER TABLE dbo.IARequest SET (LOCK_ESCALATION = TABLE)
--GO
COMMIT
