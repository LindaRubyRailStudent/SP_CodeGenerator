using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseLayer
{
    public partial class SelectAll_Result : IEquatable<SelectAll_Result>
    {
        public bool Equals(SelectAll_Result other)
        {
            if (other == null) return false;
            if (this.ProductID == other.ProductID &&
            this.Name == other.Name &&
            this.ProductNumber == other.ProductNumber &&
            this.Color == other.Color &&
            this.StandardCost == other.StandardCost &&
            this.ListPrice == other.ListPrice &&
            this.Size == other.Size &&
            this.Weight == other.Weight &&
            this.ProductCategoryID == other.ProductCategoryID &&
            this.ProductModelID == other.ProductModelID &&
            this.SellStartDate == other.SellStartDate &&
            this.SellEndDate == other.SellEndDate &&
            this.DiscontinuedDate == other.DiscontinuedDate &&
            this.ThumbNailPhoto == other.ThumbNailPhoto &&
            this.ThumbnailPhotoFileName == other.ThumbnailPhotoFileName &&
            this.rowguid == other.rowguid &&
            this.ModifiedDate == other.ModifiedDate) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SelectAll_Result vResult = (SelectAll_Result)obj;
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
