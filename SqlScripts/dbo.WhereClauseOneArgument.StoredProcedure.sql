USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[WhereClauseOneArgument]    Script Date: 02/12/2013 23:40:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[WhereClauseOneArgument] 
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50) = n, 
	@Color nvarchar(15) = n
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.ProductID, p.Name
	FROM SalesLT.Product As p
	WHERE p.Name = @Name AND p.Color = @Color;
END
