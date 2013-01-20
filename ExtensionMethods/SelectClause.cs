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

        public  List<Object> Readnodes(XmlDocument xmldocument)
        {
            List<Object> selectList = new List<Object>();
            String location = "Root";
            selectList = TraverseNodes(xmldocument.GetElementsByTagName("SqlSelectClause"), selectList, location);
            return selectList;
        }

        public  List<object> TraverseNodes(XmlNodeList nodeList, List<Object> selectList, String locationparam)
        {
            foreach (XmlNode xNode in nodeList)
            {
                if (xNode.Attributes != null)
                {
                    checkTypes(xNode, selectList, locationparam);
                    String location = xNode.Attributes[0].Value;
                    if (xNode.HasChildNodes)
                    {
                        TraverseNodes(xNode.ChildNodes, selectList, location);
                    }
                }
            }
            return selectList;
        }

        public  List<object> checkTypes(XmlNode xNode, List<object> dict, String locationparam)
        {
            String elementName = xNode.Name;
            if (elementName == "SqlQualifiedJoinTableExpression")
            {
                SqlQualifiedJoinTableExpression sqCompBoolExp = new SqlQualifiedJoinTableExpression(xNode.Attributes[0].Value, xNode.Attributes[1].Value, locationparam);
                dict.Add(sqCompBoolExp);
            }
            if (elementName == "SqlTableRefExpression")
            {
                SqlTableRefExpression sqlTableRefExp = new SqlTableRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                dict.Add(sqlTableRefExp);
            }
            if (elementName == "SqlObjectIdentifier")
            {
                if (xNode.Attributes.Count == 3)
                {
                    SqlObjectIdentifier sqlObjID = new SqlObjectIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                    dict.Add(sqlObjID);
                }
                if (xNode.Attributes.Count == 2)
                {
                    SqlObjectIdentifier sqlObjID = new SqlObjectIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                }
                else
                {
                    SqlObjectIdentifier sqlObjID = new SqlObjectIdentifier();
                }
            }
            if (elementName == "SqlConditionClause")
            {
                SqlConditionClause sqlConditCl = new SqlConditionClause(locationparam, xNode.Attributes[0].Value);
                dict.Add(sqlConditCl);
            }
            if (elementName == "SqlComparisonBooleanExpression")
            {
                SqlComparisonBooleanExpression sqlComparBool = new SqlComparisonBooleanExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(sqlComparBool);
            }
            if (elementName == "SqlScalarRefExpression")
            {
                SqlScalarRefExpression sqlScalarRef = new SqlScalarRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                dict.Add(sqlScalarRef);
            }
            if (elementName == "SqlBinaryScalarExpression")
            {
                SqlBinaryScalarExpression sqlBinaryScalar = new SqlBinaryScalarExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(sqlBinaryScalar);
            }
            if (elementName == "SqlColumnRefExpression")
            {
                SqlColumnRefExpression sqlColumnRef = new SqlColumnRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                dict.Add(sqlColumnRef);
            }
            if (elementName == "SqlIdentifier")
            {
                SqlIdentifier sqlIdentifer = new SqlIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(sqlIdentifer);
            }
            if (elementName == "SqlSelectScalarExpression")
            {
                SqlSelectScalarExpression sqlSelectScalar = new SqlSelectScalarExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(sqlSelectScalar);
            }
            return dict;
        }
    }
}
