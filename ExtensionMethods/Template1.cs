
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DataBaseLayer;
using ExtensionMethods;

namespace ExtensionMethods
{
    public class GetRepVariable
    {
        public static List<string> gRepVariable(List<Token> stinglist)
        {
            List<string> StringList = new List<String>();
            foreach (var s in stinglist)
            {
                if (s.Sql == "from")
                {
                    string repVariable = s.Value + "Repository";
                    StringList.Add(repVariable);
                }
            }
            return StringList;
        }
    }
}

