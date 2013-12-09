IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'memberprotectdbuser')
CREATE LOGIN [memberprotectdbuser] WITH PASSWORD = 'p@ssw0rd'
GO
CREATE USER [memberprotectdbuser] FOR LOGIN [memberprotectdbuser] WITH DEFAULT_SCHEMA=[dbo]
GO
GRANT CONNECT TO [memberprotectdbuser]
