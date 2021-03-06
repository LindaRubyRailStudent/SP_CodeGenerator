USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[GreaterThanOrEqual]    Script Date: 02/12/2013 23:46:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GreaterThanOrEqual]
@date datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.Status, p.Comment
	FROM SalesLT.SalesOrderHeader p
	WHERE p.OrderDate <= @date
END
