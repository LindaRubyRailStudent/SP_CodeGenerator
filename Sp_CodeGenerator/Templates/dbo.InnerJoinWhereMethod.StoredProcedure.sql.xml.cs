
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class InnerJoinWhereMethod {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<InnerJoinWhereMethod_Result> InnerJoinWhereMethod_Method(){
				 var result = ( from c in db.Customers
from a in db.Addresses

				where  c.CustomerID  ==  a.AddressID
				
				select new {c.CompanyName,c.EmailAddress,a.AddressLine1,a.AddressLine2, });
				List<InnerJoinWhereMethod_Result> listresult = new List<InnerJoinWhereMethod_Result>();
foreach ( var r in result )
{
     InnerJoinWhereMethod_Result s = new InnerJoinWhereMethod_Result();
s.CompanyName = r.CompanyName;
s.EmailAddress = r.EmailAddress;
s.AddressLine1 = r.AddressLine1;
s.AddressLine2 = r.AddressLine2;
listresult.Add(s);
}return listresult;
			}
		}
	}

