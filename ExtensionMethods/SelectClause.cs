using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ExtensionMethods;
using System.Xml.XPath;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public class SelectClause
    {
        public string _all;
        public string _distinct;
        public List<string> _columnName;
        public string _expression;
        public List<Dictionary<string, Dictionary<string, string>>> dictionList = new List<Dictionary<string, Dictionary<string, string>>>();

        public SelectClause()
        {

        }

        public SelectClause(string all, string distinct, List<string> columnName, string expression)
        {
            _all = all;
            _distinct = distinct;
            _columnName = columnName;
            _expression = expression;
        }       

        public string writeSelectClause(
            List<SqlSelectScalarExpression> selectScalarList, 
            List<SqlScalarRefExpression> scalarRefList, 
            List<SqlIdentifier> sqlIdentifierList, 
            List<SqlBinaryScalarExpression> binaryScalarList, 
            List<SqlColumnRefExpression> columnRefList, 
            List<SqlSelectStarExpression> starList, 
            SqlSelectClause sqlSelectClause,
            String selectComment)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (starList.Count() != 0)
                {
                    sb.Append(" select p );").AppendLine();
                }
                else
                {
                    sb.Append(loopSqlSelectScalar(selectScalarList, columnRefList, scalarRefList, binaryScalarList, sqlIdentifierList, sqlSelectClause));
                }
                return sb.ToString();
            }
            catch(Exception ex)
            {
                sb.Append("//" + ex.Message.Replace("\r\n", " ")).AppendLine();
                sb.Append("// " + "SQL code to be interpreted").AppendLine();
                sb.Append("// " + selectComment.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                return sb.ToString();
            }
        }

        public string loopSqlSelectScalar(
            List<SqlSelectScalarExpression> selectScalarList, 
            List<SqlColumnRefExpression> columnRefList, 
            List<SqlScalarRefExpression> scalarRefList, 
            List<SqlBinaryScalarExpression> binaryScalarList,
            List<SqlIdentifier> sqlIdentifierList, 
            SqlSelectClause sqlSelect)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select new {");
            if (columnRefList.Count != 0)
            {
                sb.Append(checkColumnRef(selectScalarList, columnRefList));
            }
            sb.Append(checkScalarRef(selectScalarList, scalarRefList));
            sb.Append(checkBinaryScalar(selectScalarList, binaryScalarList, columnRefList,sqlIdentifierList, scalarRefList ));
            sb.Append(isDistinct(sqlSelect));
            return sb.ToString();        
        }

        public string checkColumnRef(List<SqlSelectScalarExpression> selectScalarList, List<SqlColumnRefExpression> columnRefList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var selectScalar in selectScalarList)
            {
                string location = selectScalar._location;
                foreach (var c in columnRefList)
                {
                    if (c._parentLocation == location)
                    {
                        if (selectScalar._alias != null)
                        {
                            sb.Append(selectScalar._alias + " = ");
                        }
                        sb.Append(" p." + c._columnName + ",");
                    }
                }
            }
            return sb.ToString();
        }

        public string checkScalarRef(List<SqlSelectScalarExpression> selectScalarList, List<SqlScalarRefExpression> scalarRefList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var selectScalar in selectScalarList)
            {
                string location = selectScalar._location;
                foreach (var scalarRef in scalarRefList)
                {
                    if (scalarRef._parentLocation == location)
                    {
                        if (selectScalar._alias != null)
                        {
                            sb.Append(selectScalar._alias + " = ");
                        }
                        sb.Append(scalarRef._multipartIdentifier + ",");
                    }
                }
            }
            return sb.ToString();
        }

        public string checkBinaryScalar(
            List<SqlSelectScalarExpression> selectScalarList, 
            List<SqlBinaryScalarExpression> binarySclarList, 
            List<SqlColumnRefExpression> columnRefList,
            List<SqlIdentifier> sqlIdentifierList,
            List<SqlScalarRefExpression> scalarRefList)
        {
            StringBuilder sb = new StringBuilder();
            string binaryOperator = "";
            string binaryLocation = "";
            foreach (var s in selectScalarList)
            {
                string location = s._location;
                foreach (var b in binarySclarList)
                {
                    if (b._parentLocation == location)
                    {
                        binaryOperator = b._operator.convertOperator();
                        binaryLocation = b._location;
                        sb.Append(findIdentifier(sqlIdentifierList, location));
                        Condition condition = new Condition();
                        if (columnRefList.Count != 0)
                        {
                            condition = findBinaryColumns(binaryLocation, columnRefList, binaryOperator);
                        }
                        else
                        {
                            condition = findBinaryColumns(binaryLocation, scalarRefList, binaryOperator);
                        }
                        sb.Append(" ( " + condition._conditionA + " " + condition._operator + " " + condition._conditionB + " )");
                    }
                }

            }
            return sb.ToString();        
        }

        public Condition findBinaryColumns(string binarylocation, List<SqlColumnRefExpression> columnRefList, string binaryOpertor)
        {
            Condition condition = new Condition();
            List<string> columnList = new List<string>();
            foreach (var c in columnRefList)
            {
                if (c._parentLocation == binarylocation)
                {
                    columnList.Add(c._columnName);
                }
            }
            condition._conditionA = columnList[0];
            condition._operator = binaryOpertor;
            condition._conditionB = columnList[1];
            return condition;
        }

        public Condition findBinaryColumns(string binarylocation, List<SqlScalarRefExpression> scalarList, string binaryOpertor)
        {
            Condition condition = new Condition();
            List<string> columnList = new List<string>();
            foreach (var c in scalarList)
            {
                if (c._parentLocation == binarylocation)
                {
                    columnList.Add(c._multipartIdentifier);
                }
            }
            condition._conditionA = columnList[0];
            condition._operator = binaryOpertor;
            condition._conditionB = columnList[1];
            return condition;
        }

        public String findIdentifier(List<SqlIdentifier> sqlIdentifierList, string parentlocation)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in sqlIdentifierList)
            {
                if (s._parentLocation == parentlocation)
                {
                    sb.Append(s._value + " = ");
                }
            }
            return sb.ToString();
        }

        public String isDistinct(SqlSelectClause sqlSelect)
        {
            StringBuilder sb = new StringBuilder();
            if (sqlSelect._isDistinct == "True")
            {
                sb.Append(" }).Distinct();");
            }
            else
            {
                sb.Append(" });");
            }
            return sb.ToString();
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

            IEnumerable<XElement> sqlSelectScalarExpression = from sc in xmlDoc.Descendants("SqlSelectClause").Descendants("SqlSelectScalarExpression") select sc;
            List<Dictionary<string, Dictionary<string, string>>> selectScalarList = new List<Dictionary<string, Dictionary<string, string>>>();

            foreach (var x in sqlSelectScalarExpression)
            {
                Dictionary<string, Dictionary<string, string>> sqlSelectScalarDict = new Dictionary<string, Dictionary<string, string>>();
                string dicName = x.Name.ToString();
                IEnumerable<XAttribute> atts = x.Attributes();
                IEnumerable<XElement> children = sqlSelectScalarExpression.Descendants();
                Dictionary<string, string> attDictionary = new Dictionary<string, string>();
                String location = sqlSelectScalarExpression.Attributes("Location").FirstOrDefault().Value.ToString();

                foreach (var a in atts)
                {
                    attDictionary.Add(a.Name.ToString(), a.Value.ToString());
                }
                sqlSelectScalarDict.Add(dicName, attDictionary);
                selectScalarList.Add(sqlSelectScalarDict);
                getChildren(location, children);
            }

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

        public static List<Dictionary<string, Dictionary<string, string>>> getChildren(string location, IEnumerable<XElement> children)
        {
            List<XElement> grandchildren = new List<XElement>();
            List<List<XElement>> grandchildrenList = new List<List<XElement>>();
            return new List<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
