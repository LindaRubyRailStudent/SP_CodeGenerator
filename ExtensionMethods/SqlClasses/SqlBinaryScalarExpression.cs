using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlBinaryScalarExpression
    {
        public string _parentLocation;
        public string _location;
        public string _operator;

        public SqlBinaryScalarExpression(string parentLocation, string location, string binOperator)
        {
            _parentLocation = parentLocation;
            _location = location;
            _operator = binOperator;
        }

        public SqlBinaryScalarExpression()
        {

        }

        public List<SqlBinaryScalarExpression> getSqlBinaryExpression(List<object> selectClauseList)
        {
            List<SqlBinaryScalarExpression> binaryScalarList = new List<SqlBinaryScalarExpression>();
            foreach (var s in selectClauseList)
            {
                if (s.GetType().Name == "SqlBinaryScalarExpression")
                {
                    binaryScalarList.Add(s as SqlBinaryScalarExpression);
                }
            }
            return binaryScalarList;

        }
   
    }
}
