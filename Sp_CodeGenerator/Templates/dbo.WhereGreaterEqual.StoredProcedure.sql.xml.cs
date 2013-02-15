
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataBaseLayer;


 public class GreaterThanOrEqual {
	private AWorksLTEntities db = new AWorksLTEntities();

	public List<GreaterThanOrEqual_Result> GreaterThanOrEqual_Method(DateTime @date ){
				 var result = ( from p in db.SalesOrderHeaders

				where  p.OrderDate  <=  @date
				
				select new {p.Status,p.Comment, });
				List<GreaterThanOrEqual_Result> listresult = new List<GreaterThanOrEqual_Result>();
foreach ( var r in result )
{
     GreaterThanOrEqual_Result s = new GreaterThanOrEqual_Result();
s.Status = r.Status;
s.Comment = r.Comment;
listresult.Add(s);
}return listresult;
			}
		}
	}

