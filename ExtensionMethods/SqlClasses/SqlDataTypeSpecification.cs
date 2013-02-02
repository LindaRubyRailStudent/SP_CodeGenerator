using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExtensionMethods
{
    public class SqlDataTypeSpecification
    {
        public string _parentLocation;
        public string _location;

        public SqlDataTypeSpecification()
        {

        }

        public SqlDataTypeSpecification(string parentLocation, string location)
        {
            _parentLocation = parentLocation;
            _location = location;
        }

        public List<SqlDataTypeSpecification> getDataTypeSpec(XmlDocument xmlDoc)
        {
            XmlNodeList paramDec = xmlDoc.GetElementsByTagName("SqlParameterDeclaration");
            XmlNodeList DataTypeSpec = xmlDoc.GetElementsByTagName("SqlDataTypeSpecification");
            List<SqlDataTypeSpecification> listDataTypes = new List<SqlDataTypeSpecification>();
            foreach (XmlNode p in paramDec)
            {
                SqlDataTypeSpecification dataTypeSpec = new SqlDataTypeSpecification();
                XmlAttributeCollection parentAttributes = p.Attributes;
                foreach (XmlNode d in DataTypeSpec)
                {
                    XmlAttributeCollection attributes = d.Attributes;
                    dataTypeSpec._parentLocation = parentAttributes[0].Value;
                    dataTypeSpec._location = attributes[0].Value;
                    listDataTypes.Add(dataTypeSpec);
                }
            }
            return listDataTypes;
        }
    }
}
