using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class SelectMultipleDistinct_Result : IEquatable<SelectMultipleDistinct_Result>
    {
        public bool Equals(SelectMultipleDistinct_Result other)
        {
            if (other == null) return false;
            if (this.SalesPerson == other.SalesPerson &&
            this.CompanyName == other.CompanyName &&
            this.FirstName == other.FirstName) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SelectMultipleDistinct_Result vResult = (SelectMultipleDistinct_Result)obj;
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
