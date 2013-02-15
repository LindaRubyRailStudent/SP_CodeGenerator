
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class WhereSimpleEquality {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<WhereSimpleEquality_Result> WhereSimpleEquality_Method(){
				 var result = ( from p in db.Products

				 where  p.Name  ==  "Blade"
				
				select new {p.ProductID,p.Name, });
				List<WhereSimpleEquality_Result> listresult = new List<WhereSimpleEquality_Result>();
foreach ( var r in result )
{
     WhereSimpleEquality_Result s = new WhereSimpleEquality_Result();
s.ProductID = r.ProductID;
s.Name = r.Name;
listresult.Add(s);
}return listresult;
			}
		}
	}

