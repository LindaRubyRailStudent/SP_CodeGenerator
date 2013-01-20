using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlColumnRefExpression
    {
        public string _parentLocation;
        public string _location;
        public string _columnName;
        public string _multipartIdentifier;

        public SqlColumnRefExpression(string parentlocation, string location, string columnName, string multipartIdentifier)
        {
            _parentLocation = parentlocation;
            _location = location;
            _columnName = columnName;
            _multipartIdentifier = multipartIdentifier;
        }

        public SqlColumnRefExpression()
        {

        }
        public static List<SqlColumnRefExpression> getSqlColumnRefExpression(List<object> selectClauseList)
        {
            List<SqlColumnRefExpression> columnRefList = new List<SqlColumnRefExpression>();
            foreach (var s in selectClauseList)
            {
                if (s.GetType().Name == "SqlColumnRefExpression")
                {
                    columnRefList.Add(s as SqlColumnRefExpression);
                }
            }
            return columnRefList;
        }
    }
}
