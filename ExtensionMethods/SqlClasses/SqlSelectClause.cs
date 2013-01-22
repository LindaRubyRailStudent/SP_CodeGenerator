using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlSelectClause
    {
        public string _location;
        public string _isDistinct;

        public SqlSelectClause()
        {
        }

        public SqlSelectClause(string location, string isDistinct)
        {
            _location = location;
            _isDistinct = isDistinct;
        }

        public SqlSelectClause gSelectClause(List<Object> selectClauseObjects){
            SqlSelectClause sqlSelect = new SqlSelectClause();
            foreach(var s in selectClauseObjects){
                if (s.GetType().Name == "SqlSelectClause"){
                    sqlSelect = s as SqlSelectClause;
                }
            }
            return sqlSelect;
        }
    }
}
