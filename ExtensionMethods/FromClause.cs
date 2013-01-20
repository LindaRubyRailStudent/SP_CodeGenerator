using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ExtensionMethods;

namespace ExtensionMethods
{
    /// <summary>
    /// A representation of the nodes found within the FromClause of the parsed Sql
    /// </summary>
    public class FromClause
    {
        public List<string> _tableSource;
        public List<string> _schemaName;
        public List<string> _tableAlias;
        public List<Dictionary<string, Dictionary<string, string>>> _dictionList = new List<Dictionary<string, Dictionary<string, string>>>();

        /// <summary>
        /// Initialises a new instance of the FromClause class
        /// </summary>
        /// <param name="table_source">the table in the database </param>
        /// <param name="schema_name">the database name </param>
        /// <param name="table_alias">alias for the table</param>
        public FromClause(List<string> table_source, List<string> schema_name, List<string> table_alias)
        {
            _tableSource = table_source;
            _schemaName = schema_name;
            _tableAlias = table_alias;
        }

        /// <summary>
        /// Initialises a new instance with no parameters
        /// </summary>
        public FromClause()
        {

        }

        public static FromClause getFromClause(XDocument xmlDoc)
        {
            List<string> tableSourceList = new List<string>();
            List<string> schemaNameList = new List<string>();
            List<string> tableAliasList = new List<string>();

            IEnumerable<XAttribute> ts = from t in xmlDoc.Descendants("SqlFromClause").Descendants("SqlTableRefExpression").Attributes("ObjectIdentifier") select t;
            foreach (var t in ts)
            {
                tableSourceList.Add(t.Value);
            }
            

            IEnumerable<XAttribute> tsch = from t in xmlDoc.Descendants("SqlFromClause").Descendants("SqlObjectIdentifier").Attributes("SchemaName") select t;
            foreach (var s in tsch)
            {
                schemaNameList.Add(s.Value);
            }

            IEnumerable<XAttribute> tali = from t in xmlDoc.Descendants("SqlFromClause").Descendants("SqlObjectIdentifier").Attributes("ObjectName") select t;
            foreach (var ta in tali)
            {
                tableAliasList.Add(ta.Value);
            }

            FromClause newFromClause = new FromClause(tableSourceList, schemaNameList, tableAliasList);
            return newFromClause;
        }

        /// <summary>
        /// Creates a list of objects that represent each node within the fromClause in the XmlDocument
        /// </summary>
        /// <param name="xmldocument">The Sql Stored Procedure represented in the XmlDocument class</param>
        /// <returns>List of Generic objects </returns>
        /// <remarks> Calls the TraverseNodes method </remarks>
        public static List<Object> ReadNodes(XmlDocument xmldocument){
            List<Object> fromList = new List<object>();
            String location = "Root";
            fromList = TraverseNodes(xmldocument.GetElementsByTagName("SqlFromClause"), fromList, location);
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
                if (xNode.Attributes != null)
                {
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
                SqlObjectIdentifier sqlObjID = new SqlObjectIdentifier(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value, xNode.Attributes[2].Value);
                dict.Add(sqlObjID);
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
                SqlScalarRefExpression sqlScalarRef = new SqlScalarRefExpression(locationparam, xNode.Attributes[0].Value, xNode.Attributes[1].Value,xNode.Attributes[2].Value);
                dict.Add(sqlScalarRef);
            }
            return dict;
        }

        /// <summary>
        /// Creates a string to be inserted in the 
        /// </summary>
        /// <param name="sqlComparBoolean"> List of SqlComparisonBooleanExpression objects </param>
        /// <param name="sqlConditionClause"> List of SqlConditionClause objects </param>
        /// <param name="sqlObjectIdentifier">List of SqlObjectIdentifier objects </param>
        /// <param name="sqlQualifiedJoinExp"> List of SqlQualifiedJoinEspression objects </param>
        /// <param name="sqlScalarRef">List of SqlScalarRefExpression objects </param>
        /// <param name="sqlTablRef"> List of SqlTableRefExpression objects </param>
        /// <returns> a string representing the from statement of the c# method </returns>
        public String writeFromClause(List<SqlComparisonBooleanExpression> sqlComparBoolean, List<SqlConditionClause> sqlConditionClause, List<SqlObjectIdentifier> sqlObjectIdentifier, List<SqlQualifiedJoinTableExpression> sqlQualifiedJoinExp, List<SqlScalarRefExpression> sqlScalarRef,List<SqlTableRefExpression> sqlTablRef)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> tables = getTables(sqlTablRef, sqlObjectIdentifier);
            Condition conditions = getConditions(sqlConditionClause, sqlComparBoolean, sqlScalarRef);
            sb.Append(" var result = from " + sqlTablRef.FirstOrDefault()._alias + " in db." + sqlObjectIdentifier.FirstOrDefault()._objectName.Pluralise()).AppendLine();
            if (sqlQualifiedJoinExp.Count() > 0)
            {
                sb.Append(" join " + tables.ElementAt(1).Key + " in " + " db." + tables.ElementAt(1).Value.Pluralise() + " on " + conditions._conditionA + " " + conditions._operator.ToLower() + " " + conditions._conditionB);
            }
            return sb.ToString(); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableRefs"></param>
        /// <param name="objectList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> getTables(List<SqlTableRefExpression> tableRefs, List<SqlObjectIdentifier> objectList)
        {
            Dictionary<string, string> tables = new Dictionary<string, string>();
            foreach (var t in tableRefs)
            {
                foreach (var o in objectList)
                {
                    if (t._location == o._parentLocation)
                    {
                        tables.Add(t._alias, o._objectName);
                    }
                }
            }
            return tables;
        }

        /// <summary>
        /// instantiates a Condition object.
        /// </summary>
        /// <returns> A Condition object </returns>
        /// <remarks> finds both multipartidentifiers and places them in an object with the relevant operator </remarks>
        public Condition getConditions(List<SqlConditionClause> sqlConditionClause, List<SqlComparisonBooleanExpression> sqlComparBoolean, List<SqlScalarRefExpression> sqlScalarRef)
        {
            Condition conditions = new Condition();
            String parentlocation = sqlConditionClause[0]._location;
            foreach (var cb in sqlComparBoolean)
            {
                if (cb._parentLocation == parentlocation)
                {
                    conditions._conditionA = sqlScalarRef[0]._multipartIdentifier;
                    conditions._conditionB = sqlScalarRef[1]._multipartIdentifier;
                    conditions._operator = cb._comparisonOpertor;
                }
            }
            return conditions;
        }
    }
}
