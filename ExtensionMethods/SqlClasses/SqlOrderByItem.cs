using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public class SqlOrderByItem
    {
        public string _parentLocation;
        public string _location;
        public string _sortOrder;

        public SqlOrderByItem()
        {

        }

        public SqlOrderByItem(string parentlocation, string location, string sortOrder)
        {
            _parentLocation = parentlocation;
            _location = location;
            _sortOrder = sortOrder;
        }

        public List<SqlOrderByItem> getSqlOrderBy(List<object> orderbyNodes)
        {
            List<SqlOrderByItem> orderbyList = new List<SqlOrderByItem>();
            foreach (var o in orderbyNodes)
            {
                if (o.GetType().Name == "SqlOrderByItem")
                {
                    orderbyList.Add(o as SqlOrderByItem);
                }
            }
            return orderbyList;       
        }
    }
}
