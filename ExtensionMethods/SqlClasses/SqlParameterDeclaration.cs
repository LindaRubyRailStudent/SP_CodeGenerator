using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExtensionMethods.SqlClasses
{
    public class SqlParameterDeclaration
    {
        string _parentLocation;
        string _location;
        string _isOutput;
        string _name;

        public SqlParameterDeclaration(){}

        public SqlParameterDeclaration(string location, string isOutput, string name)
        {
            _location = location;
            _isOutput = isOutput;
            _name = name;
        }

        public List<SqlParameterDeclaration> getSqlParams(XmlDocument xmlDoc) {
            XmlNodeList paramNodes = xmlDoc.GetElementsByTagName("SqlParameterDeclaration");
            List<SqlParameterDeclaration> paramList = new List<SqlParameterDeclaration>();
            foreach (XmlNode p in paramNodes)
            {
                XmlAttributeCollection attributes = p.Attributes;
                SqlParameterDeclaration paramDeclaration = new SqlParameterDeclaration(attributes[0].ToString(), attributes[1].ToString(), attributes[2].ToString());
                paramList.Add(paramDeclaration);
            }
            return paramList;        
        }
    }
}
