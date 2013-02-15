
// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace Sp_CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataBaseLayer;


    public class WhereClauseOneArgument
    {
        private AWorksLTEntities db = new AWorksLTEntities();

        public List<WhereClauseOneArgument_Result> WhereClauseOneArgument_Method(string @Name, string @Color)
        {
            var result = (from p in db.Products
                          where p.Name == @Name && p.Color == @Color
                          select new { p.ProductID, p.Name, });
            List<WhereClauseOneArgument_Result> listresult = new List<WhereClauseOneArgument_Result>();
            foreach (var r in result)
            {
                WhereClauseOneArgument_Result s = new WhereClauseOneArgument_Result();
                s.ProductID = r.ProductID;
                s.Name = r.Name;
                listresult.Add(s);
            } 
            return listresult;
        }
    }
}

