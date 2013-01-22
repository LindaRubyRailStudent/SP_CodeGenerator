using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlConditionClause
    {
        public string _parentLocation;
        public string _location;

        public SqlConditionClause(string parentLocation, string location)
        {
            _parentLocation = parentLocation;
            _location = location;
        }

        public SqlConditionClause()
        {
        }

        public List<SqlConditionClause> getSqlCondition(List<object> fromClause)
        {
            List<SqlConditionClause> sqlCondClauseList = new List<SqlConditionClause>();
            foreach (var s in fromClause)
            {
                if (s.GetType().Name == "SqlConditionClause")
                {
                    sqlCondClauseList.Add(s as SqlConditionClause);
                }
            }
            return sqlCondClauseList;
        
        }
    }
} 
