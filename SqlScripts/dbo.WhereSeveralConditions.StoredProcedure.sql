USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[WhereSeveralConditions]    Script Date: 02/12/2013 23:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[WhereSeveralConditions]
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50) = n,
	@Color nvarchar(15)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT p.ProductID, p.Name, p.Color
FROM SalesLT.Product AS p
WHERE p.Name LIKE ('%@Name%')
AND p.Color = @Color ;
END
