using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{   
    /// <summary>
    /// Represents a condition statement eg. xValue = yValue
    /// </summary>
    public class Condition
    {
        public string _conditionA;
        public string _conditionB;
        public string _operator;

        /// <summary>
        /// Initialises a new instance of the Condition class
        /// </summary>
        /// <param name="conditionA">string first condition</param>
        /// <param name="conditionB">string second condition</param>
        /// <param name="condOperator"> string operator </param>
        public Condition(string conditionA, string conditionB, string condOperator)
        {
            _conditionA = conditionA;
            _conditionB = conditionB;
            _operator = condOperator;
        }
        /// <summary>
        /// Initialises a new instance of the Condition class with no parameters
        /// </summary>
        public Condition()
        {
        }
    }
}
