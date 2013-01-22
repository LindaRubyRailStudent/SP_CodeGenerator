


// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class MyGeneratedClass{
	private AWorksLTEntities db = new AWorksLTEntities();

	
	public IList<Product> CInnerJoin()
	{
		var products  = from p in db.Products
					orderby p.ProductName descending
					select new {p.OrderQty, p.UnitPrice, p.OrderQty, p.UnitPrice, p.UnitPriceDiscount, };
					return products as IList<Product>;
	}
  }
}

