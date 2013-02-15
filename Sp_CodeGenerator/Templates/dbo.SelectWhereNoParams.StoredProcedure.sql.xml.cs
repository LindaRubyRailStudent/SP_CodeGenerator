
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataBaseLayer;


    public class SelectWhereNoParams
    {
        private AWorksLTEntities db = new AWorksLTEntities();

        public List<SelectWhereNoParams_Result> SelectWhereNoParams_Method()
        {
            var result = (from p in db.Products

                          where p.Name == "R" && p.Weight < 4
                          orderby p.Name ascending
                          select new { p.Name, p.ProductNumber, p.ListPrice, });
            List<SelectWhereNoParams_Result> listresult = new List<SelectWhereNoParams_Result>();
            foreach (var r in result)
            {
                SelectWhereNoParams_Result s = new SelectWhereNoParams_Result();
                s.Name = r.Name;
                s.ProductNumber = r.ProductNumber;
                s.ListPrice = r.ListPrice;
                listresult.Add(s);
            } return listresult;
        }
    }
}

