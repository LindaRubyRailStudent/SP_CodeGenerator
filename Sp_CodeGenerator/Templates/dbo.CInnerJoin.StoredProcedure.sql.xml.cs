
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;


 public class CInnerJoin {
	private AdventureWorksLT2008R2Entities db = new AdventureWorksLT2008R2Entities();

	public List<CInnerJoin_Result> CInnerJoin_Method(){
				 var result = ( from p in db.Products
 join sod in  db.SalesOrderDetails on p.ProductID equals sod.ProductID
				
				orderby p.Name descending
				select new {p.Name,NonDiscountSales =  ( sod.OrderQty  *  sod.UnitPrice ) });
				List<CInnerJoin_Result> listresult = new List<CInnerJoin_Result>();
foreach ( var r in result )
{
     CInnerJoin_Result s = new CInnerJoin_Result();
s.ProductName = r.Name;
s.NonDiscountSales = r.NonDiscountSales;
listresult.Add(s);
}return listresult;
			}
		}
	}

