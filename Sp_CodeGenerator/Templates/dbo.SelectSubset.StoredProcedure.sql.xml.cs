
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataBaseLayer;


    public class SelectSubset
    {
        private AWorksLTEntities db = new AWorksLTEntities();

        public List<SelectSubset_Result> SelectSubset_Method()
        {
            var result = (from p in db.Products


                          orderby p.Name ascending
                          select new { p.Name, p.ProductNumber, p.ListPrice, });
            List<SelectSubset_Result> listresult = new List<SelectSubset_Result>();
            foreach (var r in result)
            {
                SelectSubset_Result s = new SelectSubset_Result();
                s.Name = r.Name;
                s.ProductNumber = r.ProductNumber;
                s.ListPrice = r.ListPrice;
                listresult.Add(s);
            } return listresult;
        }
    }
}

