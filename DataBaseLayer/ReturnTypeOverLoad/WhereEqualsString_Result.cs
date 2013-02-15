using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class WhereEqualsString_Result : IEquatable<WhereEqualsString_Result>
    {
        public bool Equals(WhereEqualsString_Result other)
        {
            if (other == null) return false;
            if (this.Name == other.Name &&
            this.Description == other.Description) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            WhereEqualsString_Result vResult = (WhereEqualsString_Result)obj;
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
