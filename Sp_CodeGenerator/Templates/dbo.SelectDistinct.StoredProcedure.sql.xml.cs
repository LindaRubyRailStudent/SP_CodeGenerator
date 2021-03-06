
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataBaseLayer;


    public class SelectDistinct
    {
        private AWorksLTEntities db = new AWorksLTEntities();

        public List<SelectDistinct_Result> SelectDistinct_Method()
        {
            var result = (from c in db.Customers


                          orderby c.SalesPerson ascending
                          select new { c.SalesPerson, }).Distinct();
            List<SelectDistinct_Result> listresult = new List<SelectDistinct_Result>();
            foreach (var r in result)
            {
                SelectDistinct_Result s = new SelectDistinct_Result();
                s.SalesPerson = r.SalesPerson;
                listresult.Add(s);
            } return listresult;
        }
    }
}

