
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class Where3Conditions {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<Where3Conditions_Result> Where3Conditions_Method(string @Name ,int @ProductID ){
				 var result = ( from p in db.Products

				//Object reference not set to an instance of an object.
// SQL code to be interpreted
// WHERE p.ProductID = 2 OR p.ProductID = @ProductID OR p.Name = @Name
				
				select new {p.ProductID,p.Name, });
				List<Where3Conditions_Result> listresult = new List<Where3Conditions_Result>();
foreach ( var r in result )
{
     Where3Conditions_Result s = new Where3Conditions_Result();
s.ProductID = r.ProductID;
s.Name = r.Name;
listresult.Add(s);
}return listresult;
			}
		}
	}

