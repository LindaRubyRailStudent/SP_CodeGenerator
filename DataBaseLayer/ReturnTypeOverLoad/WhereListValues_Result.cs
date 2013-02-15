using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class WhereListValues_Result : IEquatable<WhereListValues_Result>
    {
        public bool Equals(WhereListValues_Result other)
        {
            if (other == null) return false;
            if (this.ProductID == other.ProductID &&
            this.Name == other.Name &&
            this.Color == other.Color) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            WhereListValues_Result vResult = (WhereListValues_Result)obj;
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
