using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using DataBaseLayer;
using System.Data.Metadata.Edm;
using AWorks;

namespace ExtensionMethods
{
    public class ProcDef
    {
        public string Definition;
        public string SchemaName;
        public string ObjectName;

        public ProcDef(string def, string schema, string objName)
        {
            Definition = def;
            SchemaName = schema;
            ObjectName = objName;
        }

        public static ProcDef getProcDef(XDocument xmlDoc){
            string definition = xmlDoc.Descendants("SqlProcedureDefinition").Attributes("Name").FirstOrDefault().Value;
            string schemaName = xmlDoc.Descendants("SqlProcedureDefinition").Descendants("SqlObjectIdentifier").Attributes("SchemaName").FirstOrDefault().Value;
            string objectName = xmlDoc.Descendants("SqlProcedureDefinition").Descendants("SqlObjectIdentifier").Attributes("ObjectName").FirstOrDefault().Value;
            ProcDef def = new ProcDef(definition, schemaName, objectName);
            return def;
        }
    }
}
