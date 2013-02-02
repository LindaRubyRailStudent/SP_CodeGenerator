using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ExtensionMethods;

namespace ExtensionMethods
{
    public class WalkNodes
    {
        public WalkNodes()
        {

        }

        public List<Object> ReadNodes(XmlDocument xmldocument, string clause)
        {
            List<Object> fromList = new List<object>();
            String location = "Root";
            if (xmldocument.GetElementsByTagName(clause).Count != 0)
            {
                string comment = xmldocument.GetElementsByTagName(clause).Item(0).FirstChild.Value;
                fromList.Add(comment);
            }
            //Composite comp = new Composite("root");
            fromList = TraverseNodes(xmldocument.GetElementsByTagName(clause), fromList, location);
            return fromList;
        }

        /// <summary>
        /// Recursively navigates the fromClause Nodes of the XmlDocument
        /// </summary>
        /// <param name="nodeList"> collection of Nodes found within the FromClause of the XmlDocument </param>
        /// <param name="dict"> an initialised List of objects </param>
        /// <param name="locationparam"> the location attribute at the current index </param>
        /// <returns> List of Objects with xmlattributes as properties of the object </returns>
        /// <remarks> calls the checkTypes method </remarks>
        public static List<object> TraverseNodes(XmlNodeList nodeList, List<Object> dict, String locationparam)
        {
            foreach (XmlNode xNode in nodeList)
            {                
                XmlAttributeCollection attributes = xNode.Attributes;
                if (xNode.Attributes != null)
                {
                    //Object x = getType(xNode, locationparam);
                    //Composite child = new Composite(x);
                    //comp.Add(new Composite(x));
                    checkTypes(xNode, dict, locationparam);
                    String location = xNode.Attributes[0].Value;
                    if (xNode.HasChildNodes)
                    {
                        TraverseNodes(xNode.ChildNodes, dict, location);
                    }
                }
            }
            return dict;
        }

        /// <summary>
        /// Checks the xNode name to determine the type of object to be created
        /// Instantiates a new object of type x and adds it to the List of objects
        /// </summary>
        /// <param name="xNode"> a single node </param>
        /// <param name="dict"> a List of Objects </param>
        /// <param name="locationparam"> the location attribute of the current index </param>
        /// <returns> a List of objects with the object of type x added to the list </returns>
        public static List<object> checkTypes(XmlNode xNode, List<object> dict, String locationparam)
        {
            String elementName = xNode.Name;
            if (elementName == "SqlSelectClause")
            {
                SqlSelectClause selectClause = new SqlSelectClause(xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(selectClause);
            }
            if (elementName == "SqlQualifiedJoinTableExpression")
            {
                SqlQualifiedJoinTableExpression sqCompBoolExp = new SqlQualifiedJoinTableExpression(xNode.Attributes[0].Value, xNode.Attributes[1].Value, locationparam);
                dict.Add(sqCompBoolExp);
            }
            if (elementName == "SqlTableRefExpression")
            {
                if (xNode.Attributes.Count == 3)
                {
                    SqlTableRefExpression sqlTableRefExp = new SqlTableRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                    dict.Add(sqlTableRefExp);
                }
                else
                {
                    SqlTableRefExpression sqlTableRefExp = new SqlTableRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, "p");
                    dict.Add(sqlTableRefExp);
                }
            }
            if (elementName == "SqlObjectIdentifier")
            {
                if (xNode.Attributes.Count == 3)
                {
                    SqlObjectIdentifier sqlObjID = new SqlObjectIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                    dict.Add(sqlObjID);
                }
                else
                {
                    SqlObjectIdentifier sqlObjID = new SqlObjectIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                    dict.Add(sqlObjID);
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
            if (elementName == "SqlBinaryBooleanExpression")
            {
                SqlBinaryBooleanExpression binaryBoolean = new SqlBinaryBooleanExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(binaryBoolean);
            }
            if (elementName == "SqlLiteralExpression")
            {
                SqlLiteralExpression literalExpression = new SqlLiteralExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                dict.Add(literalExpression);
            }
            if (elementName == "SqlColumnRefExpression")
            {
                SqlColumnRefExpression columnRefExpression = new SqlColumnRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                dict.Add(columnRefExpression);
            }
            if (elementName == "SqlSelectScalarExpression")
            {
                SqlSelectScalarExpression selectScalarExpression = new SqlSelectScalarExpression(locationparam, xNode.Attributes[0].Value);
                dict.Add(selectScalarExpression);
            }
            if (elementName == "SqlBinaryScalarExpression")
            {
                SqlBinaryScalarExpression binaryScalarExpression = new SqlBinaryScalarExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(binaryScalarExpression);
            }
            if (elementName == "SqlSelectStarExpression")
            {
                SqlSelectStarExpression binaryScalarExpression = new SqlSelectStarExpression(locationparam, xNode.Attributes[0].Value);
                dict.Add(binaryScalarExpression);
            }
            if (elementName == "SqlIdentifier")
            {
                SqlIdentifier sqlIdentifier = new SqlIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(sqlIdentifier);
            }
            if (elementName == "SqlOrderByItem")
            {
                SqlOrderByItem orderbyItem = new SqlOrderByItem(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                dict.Add(orderbyItem);
            }
            return dict;
        }

        public static Object getType(XmlNode xNode, string locationparam)
        {
            String elementName = xNode.Name;
            if (elementName == "SqlSelectClause")
            {
                SqlSelectClause selectClause = new SqlSelectClause(xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                return selectClause as Object;
            }
            if (elementName == "SqlQualifiedJoinTableExpression")
            {
                SqlQualifiedJoinTableExpression sqCompBoolExp = new SqlQualifiedJoinTableExpression(xNode.Attributes[0].Value, xNode.Attributes[1].Value, locationparam);
                return sqCompBoolExp as Object;
            }
            if (elementName == "SqlTableRefExpression")
            {
                if (xNode.Attributes.Count == 3)
                {
                    SqlTableRefExpression sqlTableRefExp = new SqlTableRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                    return sqlTableRefExp as Object;
                }
                else
                {
                    SqlTableRefExpression sqlTableRefExp = new SqlTableRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, "p");
                    return sqlTableRefExp as Object;
                }
            }
            if (elementName == "SqlObjectIdentifier")
            {
                if (xNode.Attributes.Count == 3)
                {
                    SqlObjectIdentifier sqlObjID = new SqlObjectIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                    return sqlObjID as Object;
                }
                else
                {
                    SqlObjectIdentifier sqlObjID = new SqlObjectIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                    return sqlObjID as Object;
                }
            }
            if (elementName == "SqlConditionClause")
            {
                SqlConditionClause sqlConditCl = new SqlConditionClause(locationparam, xNode.Attributes[0].Value);
                return sqlConditCl as Object;
            }
            if (elementName == "SqlComparisonBooleanExpression")
            {
                SqlComparisonBooleanExpression sqlComparBool = new SqlComparisonBooleanExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                return sqlComparBool as Object;
            }
            if (elementName == "SqlScalarRefExpression")
            {
                SqlScalarRefExpression sqlScalarRef = new SqlScalarRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                return sqlScalarRef as Object;
            }
            if (elementName == "SqlBinaryBooleanExpression")
            {
                SqlBinaryBooleanExpression binaryBoolean = new SqlBinaryBooleanExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                return binaryBoolean as Object;
            }
            if (elementName == "SqlLiteralExpression")
            {
                SqlLiteralExpression literalExpression = new SqlLiteralExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                return literalExpression as Object;
            }
            if (elementName == "SqlColumnRefExpression")
            {
                SqlColumnRefExpression columnRefExpression = new SqlColumnRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                return columnRefExpression as Object;
            }
            if (elementName == "SqlSelectScalarExpression")
            {
                SqlSelectScalarExpression selectScalarExpression = new SqlSelectScalarExpression(locationparam, xNode.Attributes[0].Value);
                return selectScalarExpression as Object;
            }
            if (elementName == "SqlBinaryScalarExpression")
            {
                SqlBinaryScalarExpression binaryScalarExpression = new SqlBinaryScalarExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                return binaryScalarExpression as Object;
            }
            if (elementName == "SqlSelectStarExpression")
            {
                SqlSelectStarExpression binaryScalarExpression = new SqlSelectStarExpression(locationparam, xNode.Attributes[0].Value);
                return binaryScalarExpression as Object;
            }
            if (elementName == "SqlIdentifier")
            {
                SqlIdentifier sqlIdentifier = new SqlIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                return sqlIdentifier as Object;
            }
            if (elementName == "SqlOrderByItem")
            {
                SqlOrderByItem orderbyItem = new SqlOrderByItem(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value);
                return orderbyItem as Object;
            }
            return null as Object;
        }
    }
}

