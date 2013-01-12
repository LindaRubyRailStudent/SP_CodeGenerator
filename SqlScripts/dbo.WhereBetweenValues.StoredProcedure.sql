USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[WhereBetweenValues]    Script Date: 01/12/2013 13:23:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[WhereBetweenValues]
	-- Add the parameters for the stored procedure here
	@Idval1 int,
	@Idval2 int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT ProductID, Name, Color
FROM SalesLT.Product
WHERE ProductID BETWEEN @Idval1 AND @Idval2;
END
GO
