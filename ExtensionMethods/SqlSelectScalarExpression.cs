using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlSelectScalarExpression
    {
        public string _parentLocation;
        public string _location;
        public string _alias;

        public SqlSelectScalarExpression(string parentlocation, string location, string alias)
        {
            _parentLocation = parentlocation;
            _location = location;
            _alias = alias;
        }

        public SqlSelectScalarExpression()
        {

        }

        public static List<SqlSelectScalarExpression> getSqlSelectScalarExpression(List<object> selectClauseList)
        {
            List<SqlSelectScalarExpression> selectScalarExpList = new List<SqlSelectScalarExpression>();

            foreach (var s in selectClauseList)
            {
                if (s.GetType().Name == "SqlSelectScalarExpression")
                {
                    selectScalarExpList.Add(s as SqlSelectScalarExpression);
                }
            }
            return selectScalarExpList;
        }
    }
}
