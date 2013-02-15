
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class WhereEqualsString {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<WhereEqualsString_Result> WhereEqualsString_Method(){
				 var result = ( from p in db.Products
from pd in db.ProductDescriptions

				 where  p.Name  ==  "Mountain Bike Socks, L"
				
				select new {p.Name,pd.Description, });
				List<WhereEqualsString_Result> listresult = new List<WhereEqualsString_Result>();
foreach ( var r in result )
{
     WhereEqualsString_Result s = new WhereEqualsString_Result();
s.Name = r.Name;
s.Description = r.Description;
listresult.Add(s);
}return listresult;
			}
		}
	}

