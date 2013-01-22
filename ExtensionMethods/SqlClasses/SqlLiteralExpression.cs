using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlLiteralExpression
    {
        public string _parentLocation;
        public string _location;
        public string _value;
        public string _type;

        public SqlLiteralExpression()
        {

        }

        public SqlLiteralExpression(string parentLocation, string location, string value, string type)
        {
            _parentLocation = parentLocation;
            _location = location;
            _value = value;
            _type = type;
        }

        public List<SqlLiteralExpression> getSqlLiteral(List<object> whereClauseList){
            List<SqlLiteralExpression> literalList = new List<SqlLiteralExpression>();
            foreach (var w in whereClauseList)
            {
                if (w.GetType().Name == "SqlLiteralExpression")
                {
                    literalList.Add(w as SqlLiteralExpression);
                }
            }
            return literalList;
        }
    }
}
