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

namespace Sp_CodeGenerator
{
    public partial class WhereBetweenValues_Result :IEquatable<WhereBetweenValues_Result>
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public bool Equals(WhereBetweenValues_Result other)
        {
            if (other == null) return false;
            if (this.Color == other.Color &&
                this.Name == other.Name &&
                this.ProductID == other.ProductID) return true;
            else
                return false;
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            WhereBetweenValues_Result wbResult = other as WhereBetweenValues_Result;
            if (wbResult == null)
            {
                return false;
            }
            else return Equals(wbResult);
        }
    }


    
}
