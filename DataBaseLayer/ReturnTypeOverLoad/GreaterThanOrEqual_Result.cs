using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class GreaterThanOrEqual_Result : IEquatable<GreaterThanOrEqual_Result>
    {
        public bool Equals(GreaterThanOrEqual_Result other)
        {
            if (other == null) return false;
            if (this.Status == other.Status &&
            this.Comment == other.Comment) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            GreaterThanOrEqual_Result vResult = (GreaterThanOrEqual_Result)obj;
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
