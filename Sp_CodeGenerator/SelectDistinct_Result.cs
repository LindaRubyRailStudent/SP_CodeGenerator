//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Sp_CodeGenerator;

namespace Sp_CodeGenerator
{
    public partial class SelectDistinct_Result : IEquatable<SelectDistinct_Result>
    {
        public string SalesPerson { get; set; }

        public bool Equals(SelectDistinct_Result other)
        {
            if (other == null) return false;
            if (this.SalesPerson == other.SalesPerson) return true;
            else
                return false;
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            SelectDistinct_Result sdResult = other as SelectDistinct_Result;
            if (sdResult == null)
            {
                return false;
            }
            else return Equals(sdResult);
        }

        public override int GetHashCode()
        {
            return this.SalesPerson.GetHashCode();
        }

        public static bool operator == (SelectDistinct_Result sdResult1, SelectDistinct_Result sdResult2)
        {
            if ((object)sdResult1 == null || ((object)sdResult2) == null)
                return Object.Equals(sdResult1, sdResult2);
            return sdResult1.Equals(sdResult2);
        }

        public static bool operator != (SelectDistinct_Result sdResult1, SelectDistinct_Result sdResult2)
        {
            if (sdResult1 == null || sdResult2 == null)
                return !Object.Equals(sdResult1, sdResult2);
            return !(sdResult1.Equals(sdResult2));
        }
    }
}