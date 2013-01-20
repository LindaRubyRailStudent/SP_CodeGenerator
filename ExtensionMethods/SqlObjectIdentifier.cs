using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlObjectIdentifier
    {
        public string _parentLocation;
        public string _location;
        public string _schemaName;
        public string _objectName;

        public SqlObjectIdentifier(string parentLocation, string location, string schemaName, string objectName)
        {
            _parentLocation = parentLocation;
            _location = location;
            _schemaName = schemaName;
            _objectName = objectName;

        }

        public SqlObjectIdentifier(string parentLocation, string location, string objectName)
        {
            _parentLocation = parentLocation;
            _location = location;
            _objectName = objectName;
        }

        public SqlObjectIdentifier()
        {
        }

        public static List<SqlObjectIdentifier> getSqlObjectIdentifier(List<object> fromclause)
        {
            List<SqlObjectIdentifier> sqlObjectIdList = new List<SqlObjectIdentifier>();
            foreach (var s in fromclause)
            {
                if (s.GetType().Name == "SqlObjectIdentifier")
                {
                    sqlObjectIdList.Add(s as SqlObjectIdentifier);
                }
            }
            return sqlObjectIdList;
        }

    }
}
