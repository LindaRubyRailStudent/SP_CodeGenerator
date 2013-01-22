USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[CInnerJoin]    Script Date: 01/21/2013 10:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[CInnerJoin]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT p.Name AS ProductName, 
NonDiscountSales = (OrderQty * UnitPrice)
FROM SalesLT.Product AS p 
INNER JOIN SalesLt.SalesOrderDetail AS sod
ON p.ProductID = sod.ProductID 
ORDER BY ProductName DESC;
END
