using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class WhereEqualsInteger_Result : IEquatable<WhereEqualsInteger_Result>
    {
        public bool Equals(WhereEqualsInteger_Result other)
        {
            if (other == null) return false;
            if (this.ListPrice == other.ListPrice &&
            this.Name == other.Name) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            WhereEqualsInteger_Result vResult = (WhereEqualsInteger_Result)obj;
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
