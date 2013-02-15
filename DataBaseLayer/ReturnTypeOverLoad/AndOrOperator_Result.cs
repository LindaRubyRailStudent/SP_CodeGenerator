using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class AndOrOperator_Result : IEquatable<AndOrOperator_Result>
    {
        public bool Equals(AndOrOperator_Result other)
        {
            if (other == null) return false;
            if (this.Weight == other.Weight &&
            this.Color == other.Color &&
            this.Name == other.Name) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            AndOrOperator_Result vResult = (AndOrOperator_Result)obj;
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
