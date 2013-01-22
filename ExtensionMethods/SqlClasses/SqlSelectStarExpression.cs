using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace ExtensionMethods
{
    public class SqlSelectStarExpression
    {
        string _location;
        string _parentLocation;

        public SqlSelectStarExpression(string parentlocation, string location)
        {
            _parentLocation = parentlocation;
            _location = location;
        }

        public SqlSelectStarExpression()
        {

        }

        public List<SqlSelectStarExpression> getSqlSelectStar(List<object> selectClauseList)
        {
            List<SqlSelectStarExpression> selectStarList = new List<SqlSelectStarExpression>();

            foreach (var s in selectClauseList)
            {
                if (s.GetType().Name == "SqlSelectStarExpression")
                {
                    selectStarList.Add(s as SqlSelectStarExpression);
                }
            }
            return selectStarList;
        }

    }
}
