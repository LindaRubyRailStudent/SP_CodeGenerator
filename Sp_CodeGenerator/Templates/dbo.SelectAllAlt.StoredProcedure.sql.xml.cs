
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class SelectAllAlt {
	private AWorksLTEntities db = new AWorksLTEntities();

	public SelectAllAlt_Result SelectAllAlt_Method(){
				 var result = ( from p in db.Products

				
				orderby Name
				 select p;

			return result as SelectAllAlt_Result;
			}
		}
	}

