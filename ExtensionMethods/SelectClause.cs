using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ExtensionMethods
{
    public class SelectClause
    {
        public string _all;
        public string _distinct;
        public List<string> _columnName;
        public string _expression;

        public SelectClause(string all, string distinct, List<string> columnName, string expression)
        {
            _all = all;
            _distinct = distinct;
            _columnName = columnName;
            _expression = expression;
        }

        public static SelectClause getSelectClause(XDocument xmlDoc)
        {
            string allv;
            string distinctv;

            if (xmlDoc.Descendants("SqlSelectStatement").Descendants("SqlSelectStarExpression").Count() != 0)
            {
                allv = "true";
            }
            else
            {
                allv = "false";

            }

            if (xmlDoc.Descendants("SqlSelectStatement").Descendants("SqlSelectClause").Attributes("IsDistinct").FirstOrDefault().Value == "True")
            {
                 distinctv = "true";
            }
            else
            {
                distinctv = "false";
            }

            // aliases have not been included - some SqlSelectScalarExpressions do not contain the attribute Alias so in a foreach loop will not enumerate correctly against another list.
            List<string> columnNameList = new List<string>();
            IEnumerable<XAttribute> fromColumn = from i in xmlDoc.Descendants("SqlSelectClause").Descendants("SqlSelectScalarExpression").Descendants("SqlColumnRefExpression").Attributes("ColumnName") select i;
            foreach (var c in fromColumn)
            {
                columnNameList.Add(c.Value);
            }

            // this expression section needs to be completed at a later date.
            String expression = "";

            SelectClause newSelectClause = new SelectClause(allv, distinctv, columnNameList, expression);

            return newSelectClause;
        }
    }
}
