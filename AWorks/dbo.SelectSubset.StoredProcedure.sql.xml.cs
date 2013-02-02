
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class SelectSubset {
	private AWorksLTEntities db = new AWorksLTEntities();

	public SelectSubset_Result SelectSubset_Method(){
				 var result = ( from p in db.Products

				
				orderby Name
				select new { p.Name, p.ProductNumber, p.ListPrice,

 });
			return result as SelectSubset_Result;
			}
		}
	}

