using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlTableRefExpression
    {
        public string _parentLocation;
        public string _location;
        public string _objectIdentifier;
        public string _alias;

        public SqlTableRefExpression(string parentLocation, string location, string objectIdentifier, string alias)
        {
            _parentLocation = parentLocation;
            _location = location;
            _objectIdentifier = objectIdentifier;
            _alias = alias;
        }

        public SqlTableRefExpression(string parentLocation, string location, string objectIdentifier)
        {
            _parentLocation = parentLocation;
            _location = location;
            _objectIdentifier = objectIdentifier;
        }

        public SqlTableRefExpression()
        {
        }

        public List<SqlTableRefExpression> getSqlTableRef(List<object> fromclause)
        {
            List<SqlTableRefExpression> sqlTableRefList = new List<SqlTableRefExpression>();
            foreach (var s in fromclause)
            {
                if (s.GetType().Name == "SqlTableRefExpression")
                {
                    sqlTableRefList.Add(s as SqlTableRefExpression);
                }
            }
            return sqlTableRefList;
        }
    }
}
