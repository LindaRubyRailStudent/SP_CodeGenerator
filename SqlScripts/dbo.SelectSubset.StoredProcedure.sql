USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[SelectSubset]    Script Date: 02/12/2013 23:53:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SelectSubset]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT p.Name, p.ProductNumber, p.ListPrice
FROM SalesLT.Product AS p
ORDER BY p.Name ASC;
END
