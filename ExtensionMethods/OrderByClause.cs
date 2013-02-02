using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ExtensionMethods
{
    public class OrderByClause
    {
        public List<KeyValuePair<string, string>> _orderby;

        public OrderByClause(List<KeyValuePair<string, string>> orderList)
        {
            _orderby = orderList;
        }

        public static OrderByClause getOrderBy(XDocument xmlDoc)
        {
            List<string> _sortOrderList = new List<string>();
            List<string> _columnNameList = new List<string>();

            IEnumerable<XAttribute> sortorder = from i in xmlDoc.Descendants("SqlOrderByClause").Descendants("SqlOrderByItem").Attributes("SortOrder") select i;
            IEnumerable<XAttribute> columName = from c in xmlDoc.Descendants("SqlOrderByClause").Descendants("SqlOrderByItem").Descendants("SqlColumnRefExpression").Attributes("ColumnName") select c;
            List<KeyValuePair<string, string>> dictionaryList = new List<KeyValuePair<string, string>>();

            var result = sortorder.Zip(columName, (s, c) => new { Key = c.Value, Value = s.Value });
            int number = 0;
            foreach (var item in result)
            {
                dictionaryList.Insert(number, new KeyValuePair<string,string>(item.Key.ToString(), item.Value.ToString()));
                number++;
            }

            OrderByClause newOrderByClause = new OrderByClause(dictionaryList);
            return newOrderByClause;
        }

        public OrderByClause()
        {

        }

        public string writeOrderByClause(
            List<SqlOrderByItem> orderbyList, 
            List<SqlColumnRefExpression> columnlist, 
            List<SqlScalarRefExpression> orderByScalarRef,
            String orderByComment)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (columnlist.Count != 0)
                {
                    sb.Append(orderByColumn(orderbyList, columnlist));
                }
                else
                {
                    sb.Append(orderByScalar(orderbyList, orderByScalarRef));
                }
                return sb.ToString();
            }
            catch
            {
                sb.Append(orderByComment);
            }
            return sb.ToString();
        }

        public string orderByColumn(List<SqlOrderByItem> orderbyList, List<SqlColumnRefExpression> columnlist)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var o in orderbyList)
            {
                foreach (var c in columnlist)
                {
                    if (o._location == c._parentLocation)
                    {
                        sb.Append("orderby " + c._columnName + " " + o._sortOrder.ToLower());
                    }
                }
            }
            return sb.ToString();
        }

        public string orderByScalar(List<SqlOrderByItem> orderbyList, List<SqlScalarRefExpression> orderByScalarRef)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var o in orderbyList)
            {
                foreach (var c in orderByScalarRef)
                {
                    if (o._location == c._parentLocation)
                    {
                        sb.Append("orderby " + c._multipartIdentifier + " " + o._sortOrder.ToLower());
                    }
                }
            }
            return sb.ToString();
        }

        public string getOrderByComment(List<object> orderByNodes)
        {
            if (orderByNodes.Count != 0)
            {
                return orderByNodes.FirstOrDefault().ToString();
            }
            else
            {
                return "OrderBy comment is not available";
            }
        }
    }
}
