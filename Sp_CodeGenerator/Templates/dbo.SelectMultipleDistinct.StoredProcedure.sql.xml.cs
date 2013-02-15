
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class SelectMultipleDistinct {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<SelectMultipleDistinct_Result> SelectMultipleDistinct_Method(){
				 var result = ( from c in db.Customers

				
				orderby c.SalesPerson ascending
				select new {c.SalesPerson,c.CompanyName,c.FirstName, }).Distinct();
				List<SelectMultipleDistinct_Result> listresult = new List<SelectMultipleDistinct_Result>();
foreach ( var r in result )
{
     SelectMultipleDistinct_Result s = new SelectMultipleDistinct_Result();
s.SalesPerson = r.SalesPerson;
s.CompanyName = r.CompanyName;
s.FirstName = r.FirstName;
listresult.Add(s);
}return listresult;
			}
		}
	}

