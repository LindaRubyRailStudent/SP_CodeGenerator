
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;


 public class WhereSeveralConditions {
	private AdventureWorksLT2008R2Entities db = new AdventureWorksLT2008R2Entities();

	public List<WhereSeveralConditions_Result> WhereSeveralConditions_Method(string @Name ,string @Color ){
				 var result = ( from p in db.Products

				//Object reference not set to an instance of an object.
// SQL code to be interpreted
// WHERE Name LIKE ('%@Name%') AND Name LIKE ('HL%') AND Color = 'Red'
				
				select new { p.ProductID, p.Name, p.Color, });
				List<WhereSeveralConditions_Result> listresult = new List<WhereSeveralConditions_Result>();
foreach ( var r in result )
{
     WhereSeveralConditions_Result s = new WhereSeveralConditions_Result();
s.ProductID = r.ProductID;
s.Name = r.Name;
s.Color = r.Color;
listresult.Add(s);
}return listresult;
			}
		}
	}

