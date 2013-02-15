
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class WhereLikeString {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<WhereLikeString_Result> WhereLikeString_Method(string @Name ){
				 var result = ( from p in db.Products

				 where p.Name.Contains( @Name)
				
				select new {p.ProductID,p.Name,p.Color, });
				List<WhereLikeString_Result> listresult = new List<WhereLikeString_Result>();
foreach ( var r in result )
{
     WhereLikeString_Result s = new WhereLikeString_Result();
s.ProductID = r.ProductID;
s.Name = r.Name;
s.Color = r.Color;
listresult.Add(s);
}return listresult;
			}
		}
	}

