USE [AdventureWorksLT2008R2]
GO
/****** Object:  StoredProcedure [dbo].[InnerJoinWhereMethod]    Script Date: 02/12/2013 23:44:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[InnerJoinWhereMethod] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT c.CompanyName, c.EmailAddress, a.AddressLine1, a.AddressLine2
	FROM SalesLT.Customer as c, SalesLT.Address as a
	WHERE c.CustomerID = a.AddressID
END
