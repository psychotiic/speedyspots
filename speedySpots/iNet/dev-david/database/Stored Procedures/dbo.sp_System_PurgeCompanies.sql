
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[sp_System_PurgeCompanies]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    delete		
    from		MPOrg
    where MPOrgID in
    (
		SELECT		MPOrg.MPOrgID
		FROM		MPOrg
		INNER JOIN	MPOrgData on
					MPOrgData.MPOrgID = MPOrg.MPOrgID
		WHERE		MPOrgData.IsVerified <> 'Y' and
					MPOrg.MPOrgID not in
					(
						select		distinct MPOrgUser.MPOrgID
						from		MPOrgUser
					)
	)
END
GO
