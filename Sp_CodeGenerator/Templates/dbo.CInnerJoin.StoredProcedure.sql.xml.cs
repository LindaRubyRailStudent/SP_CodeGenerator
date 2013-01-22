
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class CInnerJoin {
	private AWorksLTEntities db = new AWorksLTEntities();

	public CInnerJoin_Result CInnerJoin_Method(){
				 var result = ( from p in db.Products
 join sod in  db.SalesOrderDetails on p.ProductID equals sod.ProductID
				
				orderby ProductName
				select new {
p.Name,
NonDiscountSales =  ( OrderQty  *  UnitPrice ) });
			return result as CInnerJoin_Result;
			}
		}
	}

