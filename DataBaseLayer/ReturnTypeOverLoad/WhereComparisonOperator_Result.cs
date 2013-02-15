using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class WhereComparisonOperator_Result : IEquatable<WhereComparisonOperator_Result>
    {
        public bool Equals(WhereComparisonOperator_Result other)
        {
            if (other == null) return false;
            if (this.ProductID == other.ProductID &&
            this.Name == other.Name) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            WhereComparisonOperator_Result vResult = (WhereComparisonOperator_Result)obj;
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
