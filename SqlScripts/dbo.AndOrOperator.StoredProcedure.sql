USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[AndOrOperator]    Script Date: 02/12/2013 23:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AndOrOperator]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT sod.Weight, sod.Color, sod.Name
	FROM SalesLT.Product as Sod
	WHERE Weight >= 24
	AND sod.Color = 'Red'
	OR sod.Name = 'Tofu'
END
