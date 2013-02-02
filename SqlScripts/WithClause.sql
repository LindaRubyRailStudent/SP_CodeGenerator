-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
-- Define the CTE expression name and column List
WITH Sales_CTE (SalesOrderID, OrderDate, ShipDate, SubTotal)
AS
-- Define the CTE query
(SELECT SalesOrderID, ShipDate, SubTotal, YEAR(OrderDate) AS SalesYear
FROM SalesLT.SalesOrderHeader
WHERE ShipDate IS NOT NULL
)
--Define the outer query referencing the CTE name
SELECT SalesOrderID, COUNT(SubTotal) AS TotalSales, OrderDate
FROM Sales_CTE
GROUP BY ShipDate, OrderDate, SalesOrderID
ORDER BY SalesOrderID
GO
