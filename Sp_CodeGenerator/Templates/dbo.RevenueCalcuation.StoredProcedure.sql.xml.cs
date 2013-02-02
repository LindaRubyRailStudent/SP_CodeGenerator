
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class RevenueCalcuation {
	private AWorksLTEntities db = new AWorksLTEntities();

	public RevenueCalcuation_Result RevenueCalcuation_Method(){
				 var result = ( from p in db.Products
 join sod in  db.SalesOrderDetails on p.ProductID equals sod.ProductID
				
				orderby p.Name
				//Index was out of range. Must be non-negative and less than the size of the collection. Parameter name: index
// SQL code to be interpreted
// SELECT 'Total income is', ((OrderQty * UnitPrice) * (1.0 - UnitPriceDiscount)), ' for ', p.Name AS ProductName
			return result as RevenueCalcuation_Result;
			}
		}
	}

