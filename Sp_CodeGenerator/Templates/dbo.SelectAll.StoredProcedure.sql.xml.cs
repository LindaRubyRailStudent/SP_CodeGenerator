
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;


 public class SelectAll {
	private AdventureWorksLT2008R2Entities db = new AdventureWorksLT2008R2Entities();

	public List<SelectAll_Result> SelectAll_Method(){
				 var result = ( from p in db.Products

				
				orderby p.Name ascending
				 select p );

				List<SelectAll_Result> listresult = new List<SelectAll_Result>();
foreach ( var r in result )
{
     SelectAll_Result s = new SelectAll_Result();
listresult.Add(s);
}return listresult;
			}
		}
	}

