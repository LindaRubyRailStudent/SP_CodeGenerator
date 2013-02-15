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

        public WhereClause()
        {

        }

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

        public string writeWhereClause(
            List<SqlBinaryBooleanExpression> binaryBooleanList,
            List<SqlComparisonBooleanExpression> comparisonBoolList,
            List<SqlLiteralExpression> literalList,
            List<SqlColumnRefExpression> columnRefExpression,
            List<SqlScalarRefExpression> whereScalarRef,
            List<SqlScalarVariableRefExpression> variables,
            string whereComment,
            XmlDocument xmlDoc,
            List<SqlBetweenBooleanExpression> betweenBool,
            List<SqlLikeBooleanExpression> likeBool)
        {
            StringBuilder sb = new StringBuilder();
            try
            {

                if (binaryBooleanList.Count != 0)
                {
                    sb.Append(binBoolWhere(binaryBooleanList, comparisonBoolList, columnRefExpression, literalList, whereScalarRef, variables));
                    return sb.ToString().Trim(new char[] { '&', '|' }).Trim(new char[] { '&', '|' });
                }
                if (betweenBool.Count != 0)
                {
                    sb.Append(new SqlBetweenBooleanExpression().getBetweenBool(xmlDoc));
                }
                if (likeBool.Count != 0)
                {
                    sb.Append(new SqlLikeBooleanExpression().getLikeBool(xmlDoc));
                }
                else
                {
                    sb.Append(compBoolWhere(comparisonBoolList, columnRefExpression, literalList, whereScalarRef, variables));
                }
                return sb.ToString().Trim(new char[] { '&', '|' }).Trim(new char[] { '&', '|' });
            }
            catch(Exception ex)
            {
                sb.Append("//" + ex.Message.Replace("\"r\n", " ")).AppendLine();
                sb.Append("// " + "SQL code to be interpreted").AppendLine();
                sb.Append("// " + whereComment.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                return sb.ToString();
            }
        }

        public String binBoolWhere(
                List<SqlBinaryBooleanExpression> binaryBooleanList,
                List<SqlComparisonBooleanExpression> comparisonBoolList,
                List<SqlColumnRefExpression> columnRefExpression,
                List<SqlLiteralExpression> literalList,
                List<SqlScalarRefExpression> whereScalarRef,
                List<SqlScalarVariableRefExpression> variables)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" where ");
            foreach (var b in binaryBooleanList)
            {
                string location = b._location;
                foreach (var c in comparisonBoolList)
                {
                    if (c._parentLocation == location)
                    {
                        if (columnRefExpression.Count != 0 && literalList.Count != 0)
                        {
                            Condition condition = getComparison(c._location, c._comparisonOpertor, literalList, columnRefExpression);
                            sb.Append(writeCondition(condition, b));
                            
                        }
                        if(columnRefExpression.Count != 0 && variables.Count != 0)
                        {
                            Condition condition = getComparison(c._location, c._comparisonOpertor, columnRefExpression, variables);
                            sb.Append(writeCondition(condition, b));
                        }
                        if(whereScalarRef.Count != 0 && variables.Count !=0)
                        {
                            Condition condition = getComparison(c._location, c._comparisonOpertor, whereScalarRef, variables);
                            sb.Append(writeCondition(condition, b));
                            
                         }
                        else
                        {
                            if (literalList.Count != 0) { 
                            Condition condition = getComparison(c._location, c._comparisonOpertor, literalList, whereScalarRef);
                            sb.Append(writeCondition(condition, b));
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }

        public String compBoolWhere(
                List<SqlComparisonBooleanExpression> comparisonBoolList,
                List<SqlColumnRefExpression> columnRefExpression,
                List<SqlLiteralExpression> literalList,
                List<SqlScalarRefExpression> whereScalarRef,
            List<SqlScalarVariableRefExpression> variables)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in comparisonBoolList)
            {
                
                if (columnRefExpression.Count != 0 )
                {
                    sb.Append(" where ");
                    Condition condition = getComparison(c._location, c._comparisonOpertor, literalList, columnRefExpression);
                    sb.Append(" " + condition._conditionA + " " + condition._operator.convertOperator().GreaterOrEqual() + " " + condition._conditionB);
                }
                if (whereScalarRef.Count == 2 && comparisonBoolList.Count == 1)
                {
                    sb.Append("where ");
                    sb.Append(" " + whereScalarRef[0]._multipartIdentifier + " " + c._comparisonOpertor.convertOperator().GreaterOrEqual() + " " + whereScalarRef[1]._multipartIdentifier);
                }
                if (variables.Count != 0)
                {
                    sb.Append("where ");
                    sb.Append(" " + whereScalarRef[0]._multipartIdentifier + " " + c._comparisonOpertor.convertOperator().GreaterOrEqual() + " " + variables[0]._VariableName);
                }
                else
                {
                    if (literalList.Count != 0)
                    {
                        sb.Append(" where ");
                        Condition condition = getComparison(c._location, c._comparisonOpertor, literalList, whereScalarRef);
                        sb.Append(" " + condition._conditionA + " " + condition._operator.convertOperator().GreaterOrEqual() + " " + condition._conditionB);
                    }
                }
            }
            return sb.ToString();
        }

        public Condition getComparison(
                string location,
                string comparisonOpertor,
                List<SqlLiteralExpression> literalList,
                List<SqlColumnRefExpression> columnRefExpression)
        {
            Condition condition = new Condition();
            var result = literalList.Zip(columnRefExpression, (l, c) => new { Key = l, Value = c });
            foreach (var r in result)
            {
                if (r.Key._parentLocation == location)
                {
                    condition._conditionA = r.Value._columnName;
                    condition._operator = comparisonOpertor;
                    if (r.Key._type == "String")
                    {
                        condition._conditionB = r.Key._value.ToInvertedCommas();
                    }
                    else
                    {
                        condition._conditionB = r.Key._value;
                    }

                }
            }
            return condition;
        }

        public Condition getComparison(
                string location,
                string comparisonOperator,
                List<SqlLiteralExpression> literalList,
                List<SqlScalarRefExpression> whereScalarRef)
        {
            Condition condition = new Condition();
            var result = literalList.Zip(whereScalarRef, (w, c) => new { Key = w, Value = c });
            foreach (var r in result)
            {
                if (r.Key._parentLocation == location)
                {
                    condition._conditionA = r.Value._multipartIdentifier;
                    condition._operator = comparisonOperator;
                    if (r.Key._type == "String")
                    {
                        condition._conditionB = r.Key._value.ToInvertedCommas();
                    }
                    else
                    {
                        condition._conditionB = r.Key._value;
                    }
                }
            }
            return condition;
        }

        public Condition getComparison(
            string location,
            string comparisonOperator,
            List<SqlColumnRefExpression> columnRefExpression,
            List<SqlScalarVariableRefExpression> sqlScalarVariable)
        {
            Condition condition = new Condition();
            var result = sqlScalarVariable.Zip(columnRefExpression, (s, c) => new { Key = s, Value = c });
            foreach (var r in result)
            {
                if (r.Value._parentLocation == location)
                {
                    condition._conditionA = r.Value._multipartIdentifier;
                    condition._operator = comparisonOperator;
                    condition._conditionB = r.Key._VariableName;
                }
            }
            return condition;
        }

        public Condition getComparison(
            string location,
            string comparisonOperator,
            List<SqlScalarRefExpression> whereScalarRef,
            List<SqlScalarVariableRefExpression> variables)
        {
            Condition condition = new Condition();
            var result = whereScalarRef.Zip(variables, (w, v) => new { Key = w, Value = v });
            foreach (var r in result)
            {
                if (r.Key._parentLocation == location)
                {
                    condition._conditionA = r.Key._multipartIdentifier;
                    condition._operator = comparisonOperator;
                    condition._conditionB = r.Value._VariableName;
                }
            }
            return condition;
        }

        public Condition getComparison(
            string location,
            string comparisonOpertor,
            List<SqlScalarRefExpression> whereScalarRef)
         {
             Condition condition = new Condition();
             foreach (var w in whereScalarRef)
             {
                 if (w._parentLocation == location)
                 {
                     condition._conditionA = w._multipartIdentifier;
                     condition._operator = comparisonOpertor;
                     condition._conditionB = w._multipartIdentifier;
                 }
             }
             return condition;
         }

        public String getWhereComment(List<object> whereNodes)
        {
            if (whereNodes.Count != 0)
            {
                return whereNodes.FirstOrDefault().ToString();
            }
            else
            {
                return "Where Clause comment could not be obtained";
            }
        }

        public String writeCondition(Condition condition, SqlBinaryBooleanExpression b)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" " + condition._conditionA + " " + condition._operator.convertOperator().GreaterOrEqual() + " " + condition._conditionB);
            sb.Append(" " + b._operator.convertOperator());
            return sb.ToString();

        }
    }
}
