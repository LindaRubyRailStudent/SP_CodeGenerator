using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlScalarRefExpression
    {
        public string _parentLocation;
        public string _location;
        public string _columnNameOrPropertyName;
        public string _multipartIdentifier;

        public SqlScalarRefExpression(string parentLocation, string location, string columnNameOrPropertName, string multipartIdentifier)
        {
            _parentLocation = parentLocation;
            _location = location;
            _columnNameOrPropertyName = columnNameOrPropertName;
            _multipartIdentifier = multipartIdentifier;
        }
        public SqlScalarRefExpression(string parentLocation, string location, string columnNameOrPropertName)
        {
            _parentLocation = parentLocation;
            _location = location;
            _columnNameOrPropertyName = columnNameOrPropertName;
        }
        public SqlScalarRefExpression()
        {

        }

        public List<SqlScalarRefExpression> getSqlScalarRef(List<object> fromclause)
        {
            List<SqlScalarRefExpression> sqlScalarList = new List<SqlScalarRefExpression>();
            foreach (var s in fromclause)
            {
                if (s.GetType().Name == "SqlScalarRefExpression")
                {
                    sqlScalarList.Add(s as SqlScalarRefExpression);
                }
            }
            return sqlScalarList;
        }
                  
    }
}
