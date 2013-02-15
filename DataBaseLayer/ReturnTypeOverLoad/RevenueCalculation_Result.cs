using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class RevenueCalculation_Result : IEquatable<RevenueCalculation_Result>
    {
        public bool Equals(RevenueCalculation_Result other)
        {
            if (other == null) return false;
            if (this.Column1 == other.Column1 &&
            this.Column2 == other.Column2 &&
            this.Column3 == other.Column3 &&
            this.ProductName == other.ProductName) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            RevenueCalculation_Result vResult = (RevenueCalculation_Result)obj;
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
