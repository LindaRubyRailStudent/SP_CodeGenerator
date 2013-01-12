using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.SqlServer.Management.Smo;

namespace ExtensionMethods
{
    public class ReadStoredProc
    {
       public static object ServerAccess()
        {
            Server server = new Server();
            Database db = server.Databases["AdventureWorksLT2008R2"];
            List<SmoObjectBase> list = new List<SmoObjectBase>();
            DataTable dataTable = db.EnumObjects(DatabaseObjectTypes.StoredProcedure);
           object o = new object();
           return  o;

        }
    }
}
