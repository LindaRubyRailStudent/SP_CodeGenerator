using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    /// <summary>
    /// Defines the class Token and it's properties
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The Location of the Token in the XML file
        /// </summary>
        public string Location;
        /// <summary>
        /// The Id of the Token
        /// </summary>
        public string Id;
        /// <summary>
        /// The Type of Token as defined by the Sql Parser
        /// </summary>
        public string TokenType;
        /// <summary>
        /// The value contained at the XML node
        /// </summary>
        public string Value;
        /// <summary>
        /// The DataType of the value
        /// </summary>
        public string DataType;
        /// <summary>
        /// A code to identify the SQL command eg. FROM 
        /// </summary>
        public string Sql;
        /// <summary>
        /// Creates a Token Object from the Parsed SQL Token XML 
        /// </summary>
        /// <param name="location"> Where the Token can be found in the XML document</param>
        /// <param name="id"> The tokens ID</param>
        /// <param name="tokenType"> The XML definition of the token </param>
        /// <param name="value"> The value of the XML node </param>
        /// <param name="dataType"> The data type of the XML value eg. String, Int, binary</param>
        /// <param name="sql"> A reference to the SQL command eg, Select or From or Where etc</param>
        public Token(string location, string id, string tokenType, string value, string dataType, string sql)
        {
            Location = location;
            Id = id;
            TokenType = tokenType;
            Value = value;
            DataType = dataType;
            Sql = sql;
        }
    }
}
