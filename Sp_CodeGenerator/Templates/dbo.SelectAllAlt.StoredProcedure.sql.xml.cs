
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataBaseLayer;


    public class SelectAllAlt
    {
        private AWorksLTEntities db = new AWorksLTEntities();

        public List<SelectAllAlt_Result> SelectAllAlt_Method()
        {
            var result = (from p in db.Products


                          orderby p.Name ascending
                          select p);

            List<SelectAllAlt_Result> listresult = new List<SelectAllAlt_Result>();
            foreach (var r in result)
            {
                SelectAllAlt_Result s = new SelectAllAlt_Result();
                listresult.Add(s);
            } return listresult;
        }
    }
}

