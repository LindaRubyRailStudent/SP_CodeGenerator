
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class SelectDistinct {
	private AWorksLTEntities db = new AWorksLTEntities();

	public SelectDistinct_Result SelectDistinct_Method(){
				 var result = ( from p in db.Customers

				
				orderby LastName
				select new { p.LastName,

 }).Distinct();
			return result as SelectDistinct_Result;
			}
		}
	}

