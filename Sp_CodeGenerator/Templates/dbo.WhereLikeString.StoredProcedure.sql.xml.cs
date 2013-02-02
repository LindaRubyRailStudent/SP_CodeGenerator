
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;


 public class WhereLikeString {
	private AdventureWorksLT2008R2Entities db = new AdventureWorksLT2008R2Entities();

	public List<WhereLikeString_Result> WhereLikeString_Method(string @Name ){
				 var result = ( from p in db.Products

				 where p.Name.Contains( @Name )
				
				select new { p.ProductID, p.Name, p.Color, });
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

