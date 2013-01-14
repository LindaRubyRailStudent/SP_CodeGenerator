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
        public List<Dictionary<string, string>> _columnName;
        public string _expression;

        public SelectClause(string all, string distinct, List<Dictionary<string, string>> columnName, string expression)
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

            /// to be filled out once I have XML examples of columnName
            List<Dictionary<string, string>> DictionaryColumnName = new List<Dictionary<string, string>>();
            String expression = "";

            SelectClause newSelectClause = new SelectClause(allv, distinctv, DictionaryColumnName, expression);

            return newSelectClause;
        }
    }
}
