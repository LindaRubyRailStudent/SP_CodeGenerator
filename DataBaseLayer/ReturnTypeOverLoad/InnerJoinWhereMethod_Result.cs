using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class InnerJoinWhereMethod_Result : IEquatable<InnerJoinWhereMethod_Result>
    {
        public bool Equals(InnerJoinWhereMethod_Result other)
        {
            if (other == null) return false;
            if (
            this.CompanyName == other.CompanyName &&
            this.EmailAddress == other.EmailAddress &&
            this.AddressLine1 == other.AddressLine1 &&
            this.AddressLine2 == other.AddressLine2 ) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            InnerJoinWhereMethod_Result vResult = (InnerJoinWhereMethod_Result)obj;
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
