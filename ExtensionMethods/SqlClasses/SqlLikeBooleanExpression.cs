using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExtensionMethods
{
    public class SqlLikeBooleanExpression
    {
        public string _location;
        public string _HasNot;

        public SqlLikeBooleanExpression()
        {

        }

        public List<SqlLikeBooleanExpression> getSqlLikeBoolean(XmlDocument xmlDoc)
        {
            List<SqlLikeBooleanExpression> likeBoolList = new List<SqlLikeBooleanExpression>();
            XmlNodeList likeNodes = xmlDoc.GetElementsByTagName("SqlLikeBooleanExpression");
            foreach (XmlNode l in likeNodes)
            {
                SqlLikeBooleanExpression likeBool = new SqlLikeBooleanExpression();
                likeBool._location = l.Attributes[0].Value;
                likeBool._HasNot = l.Attributes[1].Value;
                likeBoolList.Add(likeBool);
            }
            return likeBoolList;
        }

        public String getLikeBool(XmlDocument xmlDoc)
        {
            StringBuilder sb = new StringBuilder();
            XmlNodeList likeBoolNodes = xmlDoc.GetElementsByTagName("SqlLikeBooleanExpression");
            foreach (XmlNode l in likeBoolNodes)
            {
                if (l.SelectSingleNode("descendant::SqlLiteralExpression") != null)
                {
                    string ScalarRef = l.SelectSingleNode("descendant::SqlScalarRefExpression").Attributes[2].Value;
                    string literal = l.SelectSingleNode("descendant::SqlLiteralExpression").Attributes[1].Value;
                    sb.Append(" where " + ScalarRef + ".Contains( " + literal + " )");
                }
                else
                {
                    string ScalarRef = l.SelectSingleNode("descendant::SqlScalarRefExpression").Attributes[2].Value;
                    string variable = l.SelectSingleNode("descendant::SqlScalarVariableRefExpression").Attributes[1].Value;
                    sb.Append(" where " + ScalarRef + ".Contains( " + variable + ")");
                }
                
            }
            return sb.ToString();
        }
    }
}
