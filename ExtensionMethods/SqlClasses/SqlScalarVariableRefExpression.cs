using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExtensionMethods
{
    public class SqlScalarVariableRefExpression
    {
        public string _parentLocation;
        public string _location;
        public string _VariableName;
        public string _DataType;

        public SqlScalarVariableRefExpression()
        {

        }

        public List<SqlScalarVariableRefExpression> getVariables(XmlDocument xmlDoc, List<SqlParameter> parameters)
        {
            List<SqlScalarVariableRefExpression> variableList = new List<SqlScalarVariableRefExpression>();
            XmlNodeList scalarVariables = xmlDoc.GetElementsByTagName("SqlScalarVariableRefExpression");
            XmlNodeList comparisonBoolean = xmlDoc.GetElementsByTagName("SqlComparisonBooleanExpression");
            for (int i = 0; i < scalarVariables.Count; i++)
            {
                SqlScalarVariableRefExpression variable = new SqlScalarVariableRefExpression();
                variable._DataType = parameters[i]._dataType;
                variable._location = scalarVariables[i].Attributes[0].Value;
                variable._VariableName = scalarVariables[i].Attributes[1].Value;
                variableList.Add(variable);               
            }
            return variableList;
        }

    }
}
