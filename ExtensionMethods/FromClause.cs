using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ExtensionMethods;

namespace ExtensionMethods
{
    public class FromClause
    {
        public List<string> _tableSource;
        public List<string> _schemaName;
        public List<string> _tableAlias;

        public FromClause(List<string> table_source, List<string> schema_name, List<string> table_alias)
        {
            _tableSource = table_source;
            _schemaName = schema_name;
            _tableAlias = table_alias;
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
    }
}
