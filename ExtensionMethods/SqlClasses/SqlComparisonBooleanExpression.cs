using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlComparisonBooleanExpression
    {
        public string _parentLocation;
        public string _location;
        public string _comparisonOpertor;

        public SqlComparisonBooleanExpression(string parentLocation, string location, string comparisonOperator)
        {
            _parentLocation = parentLocation;
            _location = location;
            _comparisonOpertor = comparisonOperator;
        }

        public SqlComparisonBooleanExpression()
        {
        }

        public List<SqlComparisonBooleanExpression> getSqlCompBool(List<object> fromClause)
        {
            List<SqlComparisonBooleanExpression> sqlCompBoolList = new List<SqlComparisonBooleanExpression>();
            foreach (var s in fromClause)
            {
                if (s.GetType().Name == "SqlComparisonBooleanExpression")
                {
                    sqlCompBoolList.Add(s as SqlComparisonBooleanExpression);
                }
            }
            return sqlCompBoolList;
         }
    }
}
