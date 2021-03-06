USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[Where3Conditions]    Script Date: 02/12/2013 23:41:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Where3Conditions]
	-- Add the parameters for the stored procedure here
		@Name nvarchar(50) = n,
	@ProductID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT p.ProductID, p.Name
FROM SalesLT.Product AS p
WHERE p.ProductID = 2
OR p.ProductID = @ProductID
OR p.Name = @Name ;
END
