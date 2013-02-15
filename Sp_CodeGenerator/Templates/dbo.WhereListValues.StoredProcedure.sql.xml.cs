
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class WhereListValues {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<WhereListValues_Result> WhereListValues_Method(string @Name ){
				 var result = ( from p in db.Products

				
				
				select new {p.ProductID,p.Name,p.Color, });
				List<WhereListValues_Result> listresult = new List<WhereListValues_Result>();
foreach ( var r in result )
{
     WhereListValues_Result s = new WhereListValues_Result();
s.ProductID = r.ProductID;
s.Name = r.Name;
s.Color = r.Color;
listresult.Add(s);
}return listresult;
			}
		}
	}

