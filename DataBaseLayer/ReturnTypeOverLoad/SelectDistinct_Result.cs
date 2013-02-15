using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class SelectDistinct_Result : IEquatable<SelectDistinct_Result>
    {
        public bool Equals(SelectDistinct_Result other)
        {
            if (other == null) return false;
            if (this.SalesPerson == other.SalesPerson) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SelectDistinct_Result vResult = (SelectDistinct_Result)obj;
            if (vResult == null)
            {
                return false;
            }
            else return Equals(vResult);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
