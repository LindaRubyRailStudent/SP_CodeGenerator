
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class SelectAll {
	private AWorksLTEntities db = new AWorksLTEntities();

	public SelectAll_Result SelectAll_Method(){
				 var result = ( from p in db.Products

				
				orderby Name
				 select p;

			return result as SelectAll_Result;
			}
		}
	}

