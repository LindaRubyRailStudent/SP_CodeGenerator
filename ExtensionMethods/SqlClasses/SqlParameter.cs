using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ExtensionMethods;

namespace ExtensionMethods
{
    public class SqlParameter
    {
        public string _location;
        public string _name;
        public string _dataType;

        public SqlParameter(){
        
        }

        public List<SqlParameter> getSqlParameter(XmlDocument XmlDoc)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            XmlNodeList paramNodes = XmlDoc.GetElementsByTagName("SqlParameterDeclaration");
            XmlNodeList dataTypeChildren = XmlDoc.GetElementsByTagName("SqlDataType");
            for(int i= 0; i< paramNodes.Count; i++){
                SqlParameter parameter = new SqlParameter();
                parameter._dataType = dataTypeChildren[i].Attributes[1].Value;
                parameter._name = paramNodes[i].Attributes[2].Value;
                parameter._location = dataTypeChildren[i].Attributes[0].Value;
                paramList.Add(parameter);
            }
            return paramList;
        }

        public String getArguments(List<SqlParameter> parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var p in parameters)
            {
                sb.Append(p._dataType.convertDataType() + " " + p._name + " ,");
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
