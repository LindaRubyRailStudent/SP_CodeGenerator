using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlBinaryBooleanExpression
    {
        public string _parentLocation;
        public string _location;
        public string _operator;

        public SqlBinaryBooleanExpression()
        {

        }

        public SqlBinaryBooleanExpression(string parentlocaion, string location, string boolOperator)
        {
            _parentLocation = parentlocaion;
            _location = location;
            _operator = boolOperator;
        }

        public List<SqlBinaryBooleanExpression>getBinaryBoolean(List<object> whereClauseList)
        {
            List<SqlBinaryBooleanExpression> binaryBoolList = new List<SqlBinaryBooleanExpression>();
            foreach (var w in whereClauseList)
            {
                if (w.GetType().Name == "SqlBinaryBooleanExpression")
                {
                    binaryBoolList.Add(w as SqlBinaryBooleanExpression);
                }
            }
            return binaryBoolList;       
        }
    }
}
