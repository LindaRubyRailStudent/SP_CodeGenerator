
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class WhereSeveralConditions {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<WhereSeveralConditions_Result> WhereSeveralConditions_Method(string @Name ,string @Color ){
				 var result = ( from p in db.Products

				//Object reference not set to an instance of an object.
// SQL code to be interpreted
// WHERE p.Name LIKE ('%@Name%') AND p.Color = @Color
				
				select new {p.ProductID,p.Name,p.Color, });
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

