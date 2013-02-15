using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class CInnerJoin_Result : IEquatable<CInnerJoin_Result>
    {
        public bool Equals(CInnerJoin_Result other)
        {
            if (other == null) return false;
            if (this.ProductName == other.ProductName &&
            this.NonDiscountSales == other.NonDiscountSales) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            CInnerJoin_Result vResult = (CInnerJoin_Result)obj;
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
