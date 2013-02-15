using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class Sum_Result : IEquatable<Sum_Result>
    {
        public bool Equals(Sum_Result other)
        {
            if (other == null) return false;
            if (this.Name == other.Name &&
            this.Total == other.Total) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Sum_Result vResult = (Sum_Result)obj;
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
