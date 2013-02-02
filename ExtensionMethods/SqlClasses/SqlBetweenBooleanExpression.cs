using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExtensionMethods
{
    public class SqlBetweenBooleanExpression
    {
        public string _location;
        public string _hasNot;

        public SqlBetweenBooleanExpression()
        {

        }

        public List<SqlBetweenBooleanExpression> getBetweenBoolean(XmlDocument xmlDoc)
        {
            List<SqlBetweenBooleanExpression> betweenList = new List<SqlBetweenBooleanExpression>();
            
            XmlNodeList betweenBoolNodes = xmlDoc.GetElementsByTagName("SqlBetweenBooleanExpression");

            foreach (XmlNode b in betweenBoolNodes)
            {
                SqlBetweenBooleanExpression betweenBool = new SqlBetweenBooleanExpression();
                betweenBool._location = b.Attributes[0].Value;
                betweenBool._hasNot = b.Attributes[0].Value;
                betweenList.Add(betweenBool);
            }
            return betweenList;    
        }

        public String getBetweenBool(XmlDocument xmlDoc)
        {
            StringBuilder sb = new StringBuilder();
            XmlNodeList betweenNodes = xmlDoc.GetElementsByTagName("SqlBetweenBooleanExpression");
            foreach(XmlNode b in betweenNodes){
                string columnRef = b.SelectSingleNode("descendant::SqlScalarRefExpression").Attributes[2].Value;
                XmlNodeList scalarVariables = b.SelectNodes("descendant::SqlScalarVariableRefExpression");
                sb.Append(" where " + columnRef + " >= " + scalarVariables.Item(0).Attributes[1].Value).AppendLine();
                sb.Append(" where " + columnRef + " <= " + scalarVariables.Item(1).Attributes[1].Value);
            }
            return sb.ToString();
        }
    }
}
