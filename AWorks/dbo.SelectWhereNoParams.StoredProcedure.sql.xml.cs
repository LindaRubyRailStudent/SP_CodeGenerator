
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class SelectWhereNoParams {
	private AWorksLTEntities db = new AWorksLTEntities();

	public SelectWhereNoParams_Result SelectWhereNoParams_Method(){
				 var result = ( from p in db.Products

				 where  Name  ==  "R"  && Weight  <  4  
				orderby Name
				select new { p.Name, p.ProductNumber, p.ListPrice,

 });
			return result as SelectWhereNoParams_Result;
			}
		}
	}

