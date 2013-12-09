SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fn_System_Split](@List ntext,@Delimiter nchar(1) = N',')

RETURNS @ValueTable TABLE
(
	ID int identity(1, 1) NOT NULL,
	value varchar(4000),
	nvalue nvarchar(2000)
) AS

BEGIN

	declare @pos int,
	@textpos  int,
	@chunklen smallint,
	@tmpstr   nvarchar(4000),
	@leftover nvarchar(4000),
	@tmpval   nvarchar(4000)

	SET @textpos = 1
	SET @leftover = ''

	WHILE @textpos <= datalength(@List) / 2
	BEGIN
		SET @chunklen = 4000 - datalength(@leftover) / 2
		SET @tmpstr = @leftover + substring(@List, @textpos, @chunklen)
		SET @textpos = @textpos + @chunklen
		SET @pos = charindex(@Delimiter, @tmpstr)
		
		WHILE @pos > 0
		BEGIN
			SET @tmpval = ltrim(rtrim(left(@tmpstr, @pos - 1)))
			
			INSERT @ValueTable (value, nvalue) VALUES(@tmpval, @tmpval)

			SET @tmpstr = substring(@tmpstr, @pos + 1, len(@tmpstr))

			SET @pos = charindex(@Delimiter, @tmpstr)

		END

		SET @leftover = @tmpstr
	END

	INSERT @ValueTable(value, nvalue) VALUES (ltrim(rtrim(@leftover)), ltrim(rtrim(@leftover)))
	RETURN
END

GO
