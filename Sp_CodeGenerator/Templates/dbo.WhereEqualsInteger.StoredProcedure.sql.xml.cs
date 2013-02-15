
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class WhereEqualsInteger {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<WhereEqualsInteger_Result> WhereEqualsInteger_Method(int @Price ){
				 var result = ( from p in db.Products

				where  p.ListPrice  ==  @Price
				
				select new {p.ListPrice,p.Name, });
				List<WhereEqualsInteger_Result> listresult = new List<WhereEqualsInteger_Result>();
foreach ( var r in result )
{
     WhereEqualsInteger_Result s = new WhereEqualsInteger_Result();
s.ListPrice = r.ListPrice;
s.Name = r.Name;
listresult.Add(s);
}return listresult;
			}
		}
	}

