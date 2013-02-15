using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class SelectSubset_Result : IEquatable<SelectSubset_Result>
    {
        public bool Equals(SelectSubset_Result other)
        {
            if (other == null) return false;
            if (this.Name == other.Name &&
            this.ProductNumber == other.ProductNumber &&
            this.ListPrice == other.ListPrice) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SelectSubset_Result vResult = (SelectSubset_Result)obj;
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
