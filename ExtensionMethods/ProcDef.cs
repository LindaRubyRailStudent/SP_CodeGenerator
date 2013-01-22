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

        public ProcDef()
        {

        }

        public static ProcDef getProcDef(XDocument xmlDoc){
            string definition = xmlDoc.Descendants("SqlProcedureDefinition").Attributes("Name").FirstOrDefault().Value;
            string schemaName = xmlDoc.Descendants("SqlProcedureDefinition").Descendants("SqlObjectIdentifier").Attributes("SchemaName").FirstOrDefault().Value;
            string objectName = xmlDoc.Descendants("SqlProcedureDefinition").Descendants("SqlObjectIdentifier").Attributes("ObjectName").FirstOrDefault().Value;
            ProcDef def = new ProcDef(definition, schemaName, objectName);
            return def;
        }

        public string getProcDef(XmlDocument xmlDoc)
        {
            string procdef = "";
            XmlNodeList definition = xmlDoc.GetElementsByTagName("SqlProcedureDefinition");
            XmlAttributeCollection attributes = definition.Item(0).FirstChild.NextSibling.Attributes;
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].Name == "ObjectName")
                {
                    procdef = attributes[i].Value;
                }
            }
            return procdef;
        }

        public string getReturnType(String procedureDef, XmlDocument edmxFile)
        {
            string returnType = "";
            XmlNodeList functions = edmxFile.GetElementsByTagName("Function");
            foreach (XmlNode f in functions)
            {
                XmlAttributeCollection attributes = f.Attributes;
                foreach (XmlAttribute a in attributes)
                {
                    if (a.Value == procedureDef)
                    {
                    }
                }
            }
            
            return returnType;
        }
    }
}
