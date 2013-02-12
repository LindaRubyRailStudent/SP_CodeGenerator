
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class WhereListValues
    {
        private AdventureWorksLT2008R2Entities db = new AdventureWorksLT2008R2Entities();

        public List<WhereListValues_Result> WhereListValues_Method(string @Name)
        {
            var result = (from p in db.Products



                          select new { p.ProductID, p.Name, p.Color, });
            List<WhereListValues_Result> listresult = new List<WhereListValues_Result>();
            foreach (var r in result)
            {
                WhereListValues_Result s = new WhereListValues_Result();
                s.ProductID = r.ProductID;
                s.Name = r.Name;
                s.Color = r.Color;
                listresult.Add(s);
            } return listresult;
        }
    }
}

