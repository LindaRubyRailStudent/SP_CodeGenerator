using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class MultipleTables_Result : IEquatable<MultipleTables_Result>
    {
        public bool Equals(MultipleTables_Result other)
        {
            if (other == null) return false;
            if (this.FirstName == other.FirstName &&
            this.LastName == other.LastName &&
            this.AddressLine1 == other.AddressLine1) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            MultipleTables_Result vResult = (MultipleTables_Result)obj;
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
