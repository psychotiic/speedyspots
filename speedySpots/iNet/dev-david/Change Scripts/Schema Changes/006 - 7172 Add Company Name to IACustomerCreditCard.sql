/*
   Wednesday, June 12, 20132:04:34 PM
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
ALTER TABLE dbo.IACustomerCreditCard ADD
	CreditCardCompanyName nvarchar(100) NOT NULL CONSTRAINT DF_IACustomerCreditCard_CreditCardCompanyName DEFAULT ''
GO
ALTER TABLE dbo.IACustomerCreditCard SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
