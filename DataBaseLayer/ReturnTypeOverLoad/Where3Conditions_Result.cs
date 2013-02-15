using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class Where3Conditions_Result : IEquatable<Where3Conditions_Result>
    {

        public bool Equals(Where3Conditions_Result other)
        {
            if (other == null) return false;
            if (this.ProductID == other.ProductID &&
            this.Name == other.Name) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Where3Conditions_Result vResult = (Where3Conditions_Result)obj;
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
