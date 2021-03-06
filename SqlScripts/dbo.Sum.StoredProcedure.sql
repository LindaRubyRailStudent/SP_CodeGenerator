USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[Sum]    Script Date: 02/12/2013 23:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Sum]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.Name, Total=SUM(OD.UnitPrice)
	FROM SalesLT.Product P, SalesLT.SalesOrderDetail OD, SalesLt.Customer C
	WHERE C.CustomerID = OD.SalesOrderID
	GROUP BY P.Name
END
