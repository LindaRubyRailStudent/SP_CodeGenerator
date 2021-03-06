USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[SelectMultipleDistinct]    Script Date: 01/29/2013 15:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SelectMultipleDistinct]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT c.SalesPerson, c.CompanyName, c.FirstName
	FROM SalesLT.Customer AS c
	ORDER BY c.SalesPerson ASC;
END
