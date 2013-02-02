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
        /// Creates a string to be inserted in the 
        /// </summary>
        /// <param name="sqlComparBoolean"> List of SqlComparisonBooleanExpression objects </param>
        /// <param name="sqlConditionClause"> List of SqlConditionClause objects </param>
        /// <param name="sqlObjectIdentifier">List of SqlObjectIdentifier objects </param>
        /// <param name="sqlQualifiedJoinExp"> List of SqlQualifiedJoinEspression objects </param>
        /// <param name="sqlScalarRef">List of SqlScalarRefExpression objects </param>
        /// <param name="sqlTablRef"> List of SqlTableRefExpression objects </param>
        /// <returns> a string representing the from statement of the c# method </returns>
        public String writeFromClause(
            List<SqlComparisonBooleanExpression> sqlComparBoolean, 
            List<SqlConditionClause> sqlConditionClause, 
            List<SqlObjectIdentifier> sqlObjectIdentifier, 
            List<SqlQualifiedJoinTableExpression> sqlQualifiedJoinExp, 
            List<SqlScalarRefExpression> sqlScalarRef, 
            List<SqlTableRefExpression> sqlTablRef, 
            String fromComment)
        {
            StringBuilder sb = new StringBuilder();
            try
            {               
                Dictionary<string, string> tables = getTables(sqlTablRef, sqlObjectIdentifier);
                sb.Append(" var result = ( " );
                sb.Append(loopFrom(sqlTablRef, sqlObjectIdentifier, sqlConditionClause));
                if (sqlConditionClause.Count > 0)
                {
                    Condition conditions = getConditions(sqlConditionClause, sqlComparBoolean, sqlScalarRef);
                    if (sqlQualifiedJoinExp.Count() > 0)
                    {
                        sb.Append(" join " + tables.ElementAt(1).Key + " in " + " db." + tables.ElementAt(1).Value.Pluralise() + " on " + conditions._conditionA + " " + conditions._operator.ToLower() + " " + conditions._conditionB);
                    }
                }
                return sb.ToString();
            }
            catch
            {
                sb.Append(fromComment);
                return sb.ToString();
            }
        }

        public static Dictionary<string, string> getTables(List<SqlTableRefExpression> tableRefs, List<SqlObjectIdentifier> objectList)
        {
            Dictionary<string, string> tables = new Dictionary<string, string>();
            foreach (var t in tableRefs)
            {
                foreach (var o in objectList)
                {
                    if (t._location == o._parentLocation)
                    {
                        if (t._alias == null)
                        {
                            tables.Add("null", o._objectName);
                        }
                        else
                        {
                            tables.Add(t._alias, o._objectName);
                        }
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
        public Condition getConditions(
            List<SqlConditionClause> sqlConditionClause, 
            List<SqlComparisonBooleanExpression> sqlComparBoolean, 
            List<SqlScalarRefExpression> sqlScalarRef)
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

        public String loopFrom(
            List<SqlTableRefExpression> tableRef, 
            List<SqlObjectIdentifier> sqlObjectIdentifier, 
            List<SqlConditionClause> conditionClause)
        {
            StringBuilder sb = new StringBuilder();
            var result = tableRef.Zip(sqlObjectIdentifier, (t, o) => new { Key = o._objectName, Value = t._alias });
            if (conditionClause.Count == 0)
            {               
                foreach (var r in result)
                {
                    sb.Append("from " + r.Value + " in db." + r.Key.Pluralise()).AppendLine();
                }
            }
            else
            {
                sb.Append("from " + result.FirstOrDefault().Value + " in db." + result.FirstOrDefault().Key.Pluralise()).AppendLine();
            }
            return sb.ToString();            
        }
    } 
}
