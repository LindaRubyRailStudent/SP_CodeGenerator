﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using DataBaseLayer;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Data.Metadata.Edm;

namespace ExtensionMethods
{
    /// <summary>
    /// Replaces whitespace in strings with an underscore
    /// </summary>
    public class addUnderscore
    {
        /// <summary>
        /// Removes the spaces in the Method Name and replaces with an underscore
        /// </summary>
        /// <param name="name">The Sql Method Name</param>
        /// <returns> A string Method name with whitespace removed and replaced with an Underscore</returns>
        public static string ObjectNameToEntityName(string name)
        {
            string objectName = "";
            string pattern = "([A-Z]+[s])|([A-Z](?=[a-z]))|((?<=[a-z])[A-Z])";

            /// Seperate out each word with spaces
            objectName = System.Text.RegularExpressions.Regex.Replace(name, pattern, " $&");

            // Replace spaces with underscores
            objectName = objectName.Trim().Replace(" ", "");

            return objectName;
        }
    }
    /// <summary>
    /// Defines A Where object with properties, Column, Comparison, Literal, Type, and MultipartID
    /// To be used with the nested XML
    /// </summary>
    public class WhereClause
    {
        private string Column;
        private string Comparison;
        private string Literal;
        private string Type;
        private string MultipartID;

        /// <summary>
        /// Creates a WhereClause object from the nested XML generated by the Parser
        /// the object has the properties Column, Comparison, Literal, Type and MultipartID
        /// </summary>
        /// <param name="c"> Column </param>
        /// <param name="d"> Comparison </param>
        /// <param name="e"> Literal </param>
        /// <param name="f"> Type </param>
        /// <param name="g"> MultipartID </param>
        public WhereClause(string c, string d, string e, string f, string g)
        {
            Column = c;
            Comparison = d;
            Literal = e;
            Type = f;
            MultipartID = g;
        }
    }
    /// <summary>
    /// Converts comparison operators from being expressed in literal English to mathematical equivalent
    /// </summary>
    public static class CompOperator
    {
        /// <summary>
        /// Converts the Xml String Comparison converter into a comparison converter that can be used in a c# class.
        /// </summary>
        /// <param name="l"> ComparisonOperator expressed in English from the parsed SQL</param>
        /// <returns>String ComparisonOperator expressed in Mathematical format for use in C# classes</returns>
        public static string convertOperator(this String l)
        {
            l = l.Replace("Equals", " == ");
            l = l.Replace("GreaterThanOrEqual", " >= ");
            l = l.Replace("LessThanOrEqual", " <= ");
            l = l.Replace("NotEqual", " != ");
            l = l.Replace("NotLessThan", " !< ");
            l = l.Replace("NotGreaterThan", " !> ");
            return l;
        }
        /// <summary>
        /// Replaces the operands GreaterThan and LessThan with the mathematical equivalents
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static string GreaterOrEqual(this string g)
        {
            g = g.Replace("GreaterThan", " > ");
            g = g.Replace("LessThan", " < ");
            return g;
        }
    }
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
    /// <summary>
    /// Class that converts the parsed Sql to Token objects
    /// </summary>
    public class GetToken
    {
        /// <summary>
        /// Creates a new List of Tokens generated from the parsed Sql XML document.  
        /// An XDocument which can be traversed from node to node is passed to the method
        /// so that the corresponding node attributes and values can be used to create the Token Objects 
        /// </summary>
        /// <param name="XmlDoc">The parsed XML is passed to the method as an XDocument</param>
        /// <returns> A List of Token Objects are returned </returns>
        public static List<Token> getToken(XDocument XmlDoc)
        {
            IEnumerable<XAttribute> location = from t in XmlDoc.Descendants("Token").Attributes("location") select t;
            List<string> locationList = new List<string>();
            foreach (var l in location)
            {
                locationList.Add(l.Value);
            }

            IEnumerable<XAttribute> id = from i in XmlDoc.Descendants("Token").Attributes("id") select i;
            List<string> idList = new List<string>();
            foreach (var i in id)
            {
                idList.Add(i.Value);
            }

            IEnumerable<XAttribute> type = from tp in XmlDoc.Descendants("Token").Attributes("type") select tp;
            List<string> typeList = new List<string>();
            foreach (var t in type)
            {
                typeList.Add(t.Value);
            }

            IEnumerable<XElement> value = from v in XmlDoc.Descendants("Token") select v;
            List<string> valueList = new List<string>();
            foreach (var vl in value)
            {
                valueList.Add(vl.Value);
            }


            List<Token> list = new List<Token>();
            for (int i = 0; i < locationList.Count; i++)
            {
                Token tk = new Token(locationList[i], idList[i], typeList[i], valueList[i], "notknown", "sqlnotKnown");
                list.Add(tk);
            }
            return list;
        }
    }
    /// <summary>
    /// Class that identifies the columnnames to be selected
    /// </summary>
    public class GetParams
    {
        /// <summary>
        /// The purpose of this method is to obtain the parameters that will be passed to the c# methods.
        /// It will loop through the XML tokens until it reaches the "TOKEN_FROM" token and will extract value from nodes that contain
        /// an attribute "TOKEN_ID".  These values are the parameters to be passed to the C# Methods.
        /// Once the "TOKEN_FROM" is found, the loop is exited and the Method "getFrom" is called.
        /// </summary>
        /// <param name="list"> A full list of all token objects from the XML document are passed to the method</param>
        /// <returns> A list of Tokens that have their SQL property completed.</returns>
        public static List<Token> gParams(List<Token> list)
        {
            List<Token> param = new List<Token>();
            int i = 0;
            while (list[i].TokenType != "TOKEN_FROM")
            {
                if (list[i].TokenType == "TOKEN_ID")
                {
                    Token tk = new Token(list[i].Location, list[i].Id, list[i].TokenType, list[i].Value, "notknown", "param");
                    param.Add(tk);
                }
                i++;
            }
            List<Token> from = getFrom.gFrom(list, i);
            param.AddRange(from);
            return param;
        }
    }

    /// <summary>
    /// class that identifies the Tables to be selected from
    /// </summary>
    public class getFrom
    {
        /// <summary>
        /// Called by the GetParams method.
        /// The purpose of this method is to obtain the tables that are being selected from.
        /// The method loops through the List of tokens starting at the first "TOKEN_FROM" and 
        /// exiting at the first "TOKEN_WHERE"
        /// </summary>
        /// <param name="list">The List of token objects</param>
        /// <param name="i"> The index of the tokenfrom ie where the loop exited from the previous method </param>
        /// <returns> a List of tokens with the SQL property completed and the whitespace XML tokens removed </returns>
        public static List<Token> gFrom(List<Token> list, int i)
        {
            List<Token> from = new List<Token>();
            while (list[i].TokenType != "TOKEN_WHERE")
            {
                if (list[i].TokenType == "TOKEN_ID")
                {
                    Token tk = new Token(list[i].Location, list[i].Id, list[i].TokenType, list[i].Value, "notknown", "from");
                    from.Add(tk);
                }
                i++;
            }
            List<Token> where = getWhere.gWhere(list, i);
            from.AddRange(where);
            return from;
        }
    }

    /// <summary>
    /// Class that gets the Arguments and their operands
    /// </summary>
    public class getWhere
    {
        /// <summary>
        /// Called by the GetFrom Method
        /// The purpose of this method is to obtain the Sql WHERE, AND, OR Tokens
        /// And their comparison operators
        /// </summary>
        /// <param name="list"> the List of Tokens </param>
        /// <param name="i">The index at where the WHERE Statement begins</param>
        /// <returns>A list of tokens with the SQL property completed with either "whereOrAnd" or "operand" </returns>
        public static List<Token> gWhere(List<Token> list, int i)
        {
            List<Token> where = new List<Token>();
            List<string> whereSql = new List<string>() { "TOKEN_AND", "TOKEN_OR", "TOKEN_WHERE" };
            List<string> operands = new List<string>() { "<", ">", "=", "!", "|" };
            StringBuilder sb = new StringBuilder();
            int length = list.Count;
            for (int l = 0; l < length; l++)
            {
                if (whereSql.Contains(list[l].TokenType))
                {
                    Token whereOrAndTk = new Token(list[l].Location, list[l].Id, list[l].TokenType, list[l].Value, "notknown", "whereOrAnd");
                    where.Add(whereOrAndTk);
                }
                if (operands.Contains(list[l].TokenType))
                {
                    sb.Append(list[l].TokenType);
                    if (operands.Contains(list[l + 1].TokenType))
                    {
                    }
                    else
                    {
                        Token operandTk = new Token(list[l].Location, list[l].Id, list[l].TokenType, sb.ToString(), "notknown", "operand");
                        sb.Clear();
                        where.Add(operandTk);
                    }
                }
            }
            return where;
        }
    }

    /// <summary>
    /// class that gets the associated properties and their datatypes of an object
    /// </summary>
    public class getDataType
    {
        /// <summary>
        /// This method finds all the associated properties of an object
        /// Then using reflection obtains the type of the property
        /// Then calls the method GetCoreType to fine the underlying type if the type is returned as Nullable.
        /// </summary>
        /// <param name="list">A list of Tokens </param>
        /// <returns>The list of tokens with the dataType property completed</returns>
        public static List<Token> gDataType(List<Token> list)
        {
            NorthwindEntities db = new NorthwindEntities();
            Product product = new Product();
            foreach (var t in list)
            {
                if (t.Sql == "param")
                {
                    foreach (var prop in product.GetType().GetProperties())
                    {
                        var proptype = prop.PropertyType;
                        var coretype = GetCoreType(proptype).Name;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Returns the underlying type of the property.
        /// The method is called by the gDataType which first obtains the type of the property.
        /// </summary>
        /// <param name="type">Type of the property</param>
        /// <returns>the type of the property</returns>
        private static Type GetCoreType(Type type)
        {
            if (type.IsGenericType &&
            type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Nullable.GetUnderlyingType(type);
            else
                return type;
        }
    }

    public class Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IEnumerable<Product> Index()
        {
            var products = unitOfWork.ProductRepository.Get();
            return products;
        }
    }

    /// <summary>
    /// creates a string representing the class variables for the database context
    /// </summary>
    public static class CreateRepositoryVariable
    {
        /// <summary>
        /// This method is used to create a string that can be used to create the Unit of Work for each Table to be selected from.
        /// The string represents class variables for the database context for each repository
        /// </summary>
        /// <example> private GenericRepository &lt;Product &gt; productRepository; </example>
        /// <param name="tokenlist"> The list of token objects </param>
        /// <returns>a string to be inserted into the Unit of work creating the repository variable</returns>
        public static String createRepVariable(List<Token> tokenlist)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var t in tokenlist)
            {
                if (t.Sql == "from")
                {
                    string single = Plurals.Singularise(t.Value);
                    string lowercase = single.ToLower();
                    sb.Append("private GenericRepository<" + single + "> " + lowercase + "Repository;").AppendLine();
                }
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// creates a string that will create code to instantiate a repository passing in the context instance
    /// </summary>
    public static class GetRepositoryObject
    {
        /// <summary>
        /// Finds the value for each token with an Sql property "from"
        /// singularises and lowercases the value
        /// uses Stringbuilder to create code that will instantiate the repository instance
        /// </summary>
        /// <param name="tokenlist">List of Token objects</param>
        /// <returns>code string to be used in Unit of Work T4 template</returns>
        public static String gRepObject(List<Token> tokenlist)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var t in tokenlist)
            {
                if (t.Sql == "from")
                {
                    string single = Plurals.Singularise(t.Value);
                    string lowercase = single.ToLower();
                    sb.Append("public GenericRepository<" + single + "> " + single + "Repository").AppendLine();
                    sb.Append("     {").AppendLine();
                    sb.Append("         get").AppendLine();
                    sb.Append("         {").AppendLine();
                    sb.Append("             if (this." + lowercase + "Repository == null)").AppendLine();
                    sb.Append("             {").AppendLine();
                    sb.Append("                 this." + lowercase + "Repository = new GenericRepository<" + single + ">(context);").AppendLine();
                    sb.Append("             }").AppendLine();
                    sb.Append("             return " + lowercase + "Repository;").AppendLine();
                    sb.Append("         }").AppendLine();
                    sb.Append("     }").AppendLine();
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// Pluralises or Singularises a string
    /// </summary>
    public static class Plurals
    {
        /// <summary>
        /// Pluralises
        /// </summary>
        /// <param name="s">this string</param>
        /// <returns>string pluralised</returns>
        public static String Pluralise(this String s)
        {
            PluralizationService plural = PluralizationService.CreateService(CultureInfo.CurrentCulture);
            return plural.Pluralize(s);
        }
        /// <summary>
        /// Singularises a String
        /// </summary>
        /// <param name="s">this string</param>
        /// <returns>this string singularised</returns>
        public static String Singularise(this String s)
        {
            PluralizationService single = PluralizationService.CreateService(CultureInfo.CurrentCulture);
            return single.Singularize(s);
        }
    }

    public static class GetRepositoryVariable
    {
        public static List<string> gRepVariable(List<Token> stinglist)
		{
            List<string> StringList = new List<String>();
			foreach(var s in stinglist)
			{
				if(s.Sql == "from")
				{
					string repVariable = s.Value.Singularise() + "Repository";
					StringList.Add(repVariable);
				}
			}
			return StringList;
		}
    }

}
