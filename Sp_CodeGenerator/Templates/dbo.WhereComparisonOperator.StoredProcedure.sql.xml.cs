
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class WhereComparisonOperator {
	private AWorksLTEntities db = new AWorksLTEntities();

	public WhereComparisonOperator_Result WhereComparisonOperator_Method(){
				 var result = ( from p in db.Products

				 where  p.ProductID  <=  12
				
				select new { p.ProductID, p.Name,

 });
			return result as WhereComparisonOperator_Result;
			}
		}
	}

