using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExtensionMethods
{
    public class ReturnResult
    {
        public ReturnResult()
        {

        }

        public String getReturnResult(XmlDocument xmlDoc)
        {
            StringBuilder sb = new StringBuilder();
            String procDef = getProcDef(xmlDoc);
            sb.Append("List<" + procDef + "_Result> listresult = new List<" + procDef + "_Result>();").AppendLine();
            sb.Append("foreach ( var r in result )").AppendLine();
            sb.Append("{").AppendLine();
            sb.Append("     " + procDef + "_Result s = new " + procDef + "_Result();").AppendLine();
            sb.Append(getProperties(xmlDoc));
            sb.Append("listresult.Add(s);").AppendLine();
            sb.Append("}");
            sb.Append("return listresult;");
            return sb.ToString();
        }

        public String getProcDef(XmlDocument xmlDoc)
        {
            try
            {
                String procdef = xmlDoc.SelectSingleNode("descendant::SqlProcedureDefinition/SqlObjectIdentifier").Attributes[2].Value;
                return procdef;
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("// " + ex.Message.ToString()).AppendLine();
                sb.Append(" RETURN STATMENT COULD NOT BE WRITTEN ");
                return sb.ToString();
            }
        }

        public String getProperties(XmlDocument xmlDoc)
        {
            StringBuilder sb = new StringBuilder();
            XmlNodeList selectClause = xmlDoc.GetElementsByTagName("SqlSelectScalarExpression");
            foreach (XmlNode s in selectClause)
            {
                sb.Append("s." + columnName(s) + " = r." + entityColumn(s) + ";").AppendLine();

            }
            return sb.ToString();
        }

        public String columnName(XmlNode node)
        {
            if (node.Attributes["Alias"] != null)
            {
                String colName = node.Attributes["Alias"].Value;
                return colName;
            }
            else
            {
                String colName = "";
                if (node.SelectNodes("SqlColumnRefExpression").Count != 0) 
                { 
                    colName = node.SelectSingleNode("descendant::SqlColumnRefExpression").Attributes[1].Value;
                }
                if (node.SelectNodes("SqlScalarRefExpression").Count != 0)
                {
                    colName = node.SelectSingleNode("descendant::SqlScalarRefExpression").Attributes[1].Value;
                }
                return colName;
            }
        }

        public String entityColumn(XmlNode node)
        {
            string entityName = "";
            if (node.SelectNodes("SqlColumnRefExpression").Count != 0)
            {
                entityName = node.SelectSingleNode("descendant::SqlColumnRefExpression").Attributes[1].Value;
            }
            if (node.SelectNodes("SqlScalarRefExpression").Count != 0)
            {
                entityName = node.SelectSingleNode("descendant::SqlScalarRefExpression").Attributes[1].Value;
            }
            if (node.ChildNodes[1].Name == "SqlIdentifier")
            {
                entityName = node.SelectSingleNode("descendant::SqlIdentifier").Attributes[1].Value;
            }
            return entityName;
        }

    }
}
