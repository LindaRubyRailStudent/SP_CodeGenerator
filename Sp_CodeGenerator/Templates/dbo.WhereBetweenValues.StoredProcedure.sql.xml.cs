
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;


 public class WhereBetweenValues {
	private AdventureWorksLT2008R2Entities db = new AdventureWorksLT2008R2Entities();

	public List<WhereBetweenValues_Result> WhereBetweenValues_Method(int @Idval1 ,int @Idval2 ){
				 var result = ( from p in db.Products

				 where p.ProductID >= @Idval1
 where p.ProductID <= @Idval2
				
				select new { p.ProductID, p.Name, p.Color, });
				List<WhereBetweenValues_Result> listresult = new List<WhereBetweenValues_Result>();
foreach ( var r in result )
{
     WhereBetweenValues_Result s = new WhereBetweenValues_Result();
s.ProductID = r.ProductID;
s.Name = r.Name;
s.Color = r.Color;
listresult.Add(s);
}return listresult;
			}
		}
	}

