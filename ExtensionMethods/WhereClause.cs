using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ExtensionMethods;

namespace ExtensionMethods
{
    public class WhereClause
    {
        public string _searchCondition;
        public string _operator;
        public KeyValuePair<string, string> _expression;
        public string _binaryBoolean;

        public WhereClause(string searchCond, string operat, KeyValuePair<string, string> expression, string binaryBoolean)
        {
            _searchCondition = searchCond;
            _operator = operat;
            _expression = expression;
            _binaryBoolean = binaryBoolean;
        }

        public static List<WhereClause> getWhereClause(XDocument xmlDoc)
        {
            IEnumerable<XAttribute> operatorVar = from co in xmlDoc.Descendants("SqlWhereClause").Descendants("SqlComparisonBooleanExpression").Attributes("ComparisonOperator") select co;
            IEnumerable<XAttribute> columnName = from cn in xmlDoc.Descendants("SqlWhereClause").Descendants("SqlColumnRefExpression").Attributes("ColumnName") select cn;
            IEnumerable<XAttribute> expression = from ex in xmlDoc.Descendants("SqlWhereClause").Descendants("SqlLiteralExpression").Attributes("Value") select ex;
            IEnumerable<XAttribute> type = from tp in xmlDoc.Descendants("SqlWhereClause").Descendants("SqlLiteralExpression").Attributes("Type") select tp;
            IEnumerable<XAttribute> binaryBoolean = from bb in xmlDoc.Descendants("SqlWhereClause").Descendants("SqlBinaryBooleanExpression").Attributes("Operator") select bb;

            List<WhereClause> listWhereClause = new List<WhereClause>();
            string binBool = "Where";
            KeyValuePair<string, string> express = new KeyValuePair<string, string>();

            for (int i = 0; i < operatorVar.Count(); i++)
            {
                string searchCond = operatorVar.ElementAt(i).Value.convertOperator().GreaterOrEqual();
                string columnN = columnName.ElementAt(i).Value;
                
                if (type.ElementAt(i).Value == "String")
                {
                    express = new KeyValuePair<string, string>(expression.ElementAt(i).Value.ToInvertedCommas(), type.ElementAt(i).Value);
                }
                else
                {
                    express = new KeyValuePair<string, string>(expression.ElementAt(i).Value, type.ElementAt(i).Value);
                }
                

                if (i == 0)
                {
                    binBool = "where";
                }
                else
                {
                    binBool = binaryBoolean.ElementAt(i - 1).Value.ToLower().convertBinBool();
                }
                WhereClause wClause = new WhereClause(searchCond, columnN, express, binBool);
                listWhereClause.Add(wClause);
            }
                return listWhereClause;
        }
    }
}
