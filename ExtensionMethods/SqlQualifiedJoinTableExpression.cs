using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlQualifiedJoinTableExpression
    {
        public string _Location;
        public string _JoinOperator;
        public string _parentLocation;

        public SqlQualifiedJoinTableExpression(string location, string joinOperator, string parentLocation)
        {
            _Location = location;
            _JoinOperator = joinOperator;
            _parentLocation = parentLocation;
        }

        public SqlQualifiedJoinTableExpression(string location, string parentLocation)
        {
            _Location = location;
            _parentLocation = parentLocation;
        }

        public SqlQualifiedJoinTableExpression()
        {

        }

        public static List<SqlQualifiedJoinTableExpression> getSqlQualifiedJoin(List<Object> fromClause)
        {
            List<SqlQualifiedJoinTableExpression> sqlQualifiedJoin = new List<SqlQualifiedJoinTableExpression>();
            foreach (var s in fromClause)
            {
                if (s.GetType().Name == "SqlQualifiedJoinTableExpression")
                {
                    sqlQualifiedJoin.Add(s as SqlQualifiedJoinTableExpression);
                }
            }
            return sqlQualifiedJoin;
        }
    }
}
