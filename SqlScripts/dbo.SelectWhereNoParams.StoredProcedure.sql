USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[SelectWhereNoParams]    Script Date: 02/13/2013 18:25:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SelectWhereNoParams] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT p.Name, p.ProductNumber, p.ListPrice
FROM SalesLT.Product AS p
WHERE p.Name = 'HL Road Frame - Red, 62' 
AND p.Weight > 4
ORDER BY p.Name ASC;
END
