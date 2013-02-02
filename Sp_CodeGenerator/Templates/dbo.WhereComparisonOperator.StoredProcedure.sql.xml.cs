
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;


 public class WhereComparisonOperator {
	private AdventureWorksLT2008R2Entities db = new AdventureWorksLT2008R2Entities();

	public List<WhereComparisonOperator_Result> WhereComparisonOperator_Method(string @Name ,int @ProductID ){
				 var result = ( from p in db.Products

				 where  p.ProductID  <=  12
				
				select new { p.ProductID, p.Name, });
				List<WhereComparisonOperator_Result> listresult = new List<WhereComparisonOperator_Result>();
foreach ( var r in result )
{
     WhereComparisonOperator_Result s = new WhereComparisonOperator_Result();
s.ProductID = r.ProductID;
s.Name = r.Name;
listresult.Add(s);
}return listresult;
			}
		}
	}

