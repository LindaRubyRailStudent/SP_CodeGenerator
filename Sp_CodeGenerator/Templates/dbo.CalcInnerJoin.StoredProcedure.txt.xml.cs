
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class CalcInnerJoin {
	private AWorksLTEntities db = new AWorksLTEntities();

	public CalcInnerJoin_Result CalcInnerJoin_Method(){
				 var result = ( from p in db.Products
 join sod in  db.SalesOrderDetails on p.ProductID equals sod.ProductID
				
				orderby ProductName
				//Index was out of range. Must be non-negative and less than the size of the collection. Parameter name: index
// SQL code to be interpreted
// SELECT p.Name AS ProductName,  NonDiscountSales = (OrderQty * UnitPrice), Discounts = ((OrderQty * UnitPrice) * UnitPriceDiscount)
			return result as CalcInnerJoin_Result;
			}
		}
	}

