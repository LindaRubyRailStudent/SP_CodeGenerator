USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[SelectAllAlt]    Script Date: 01/24/2013 19:28:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SelectAllAlt]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT p.*
FROM SalesLT.Product AS p
ORDER BY p.Name ASC;
END
