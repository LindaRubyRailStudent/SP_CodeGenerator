


// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator{
	using System;
	using System.Collections.Generic;
	using AWorks;
	using System.Linq;


 public class MyGeneratedClass{
	private AWorksLTEntities db = new AWorksLTEntities();
	
	public IList<Product> SelectWhereNoParams()
	{
		var products  = from p in db.Products
					orderby p.Name ascending
					where   p.Name  ==  "R"
					 &&    p.Weight  <  4
					select new {p.Name, p.ProductNumber, p.ListPrice, };
					return products as IList<Product>;
	}
  }
}

