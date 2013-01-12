USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[Where3Conditions]    Script Date: 01/12/2013 13:23:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Where3Conditions]
	-- Add the parameters for the stored procedure here
		@Name nvarchar(50) = n,
	@ProductID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT ProductID, Name
FROM SalesLT.Product
WHERE ProductID = 2
OR ProductID = @ProductID
OR Name = @Name ;
END
GO
