using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlIdentifier
    {
        public string _parentLocation;
        public string _location;
        public string _value;

        public SqlIdentifier(string parentLocation, string location, string value)
        {
            _parentLocation = parentLocation;
            _location = location;
            _value = value;
        }

        public SqlIdentifier()
        {

        }

        public List<SqlIdentifier> getSqlIdentifier(List<object> selectClauseList)
        {
            List<SqlIdentifier> sqlIdentifierlist = new List<SqlIdentifier>();
            foreach (var s in selectClauseList)
            {
                if (s.GetType().Name == "SqlIdentifier")
                {
                    sqlIdentifierlist.Add(s as SqlIdentifier);
                }
            }
            return sqlIdentifierlist;
        }
    }
}
