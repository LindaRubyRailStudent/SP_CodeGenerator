using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
   public partial class SelectWhereNoParams_Result : IEquatable<SelectWhereNoParams_Result>
    {
        public bool Equals(SelectWhereNoParams_Result other)
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
            SelectWhereNoParams_Result vResult = (SelectWhereNoParams_Result)obj;
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
