using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWorks;

namespace Sp_CodeGenerator
{
    public class CInnerJoinTest
    {
        private AdWorksLTEntities db = new AdWorksLTEntities();

        public IQueryable<SelectAll_Result> CInnerJoinTest_Method()
        {
            var result = (from p in db.Products
                         select new SelectAll_Result(){ 
                             Color = p.Color,
                             DiscontinuedDate = p.DiscontinuedDate, 
                             ListPrice = p.ListPrice, 
                             ModifiedDate = p.ModifiedDate, 
                             Name = p.Name, 
                             ProductCategoryID = p.ProductCategoryID, 
                             ProductID = p.ProductID, 
                             ProductModelID = p.ProductModelID,
                             ProductNumber = p.ProductNumber,
                             rowguid = p.rowguid,
                             SellEndDate = p.SellEndDate,
                             SellStartDate = p.SellStartDate,
                             Size = p.Size,
                             StandardCost = p.StandardCost,
                             ThumbNailPhoto = p.ThumbNailPhoto,
                             ThumbnailPhotoFileName = p.ThumbnailPhotoFileName,
                             Weight = p.Weight});
            return result.AsQueryable();
        }

        // Select All variants in C#
        //public IQueryable<Product> getProductV1()  // Select All version 1 : IQueryable
        //{
        //    var result = from p in db.Products
        //                 select p;
        //    return result;
        //} 

        //public List<Product> getProductV2() // Select All version 2 : List
        //{
        //    var result = from p in db.Products
        //                 select p;
        //    return result.ToList();
        //}

        //public List<Product> getProductV3() // Select All version 3 : projection 
        //{
        //    var result = db.Products.Select(p => p);
        //    return result.ToList();
        //}

        //public List<Product> getProductV4() // Select ALL No projection
        //{
        //    var result = db.Products;
        //    return result.ToList();
        //}

        //public IOrderedQueryable<Product> SelectAll_Method()
        //{
        //    var result = (from p in db.Products
        //                  orderby p.Name ascending
        //                  select p);
        //    return result; 
        }  // return type based syntax highlighting

        // Select Multiple Tables in C#

        public class CustomerAdress
        {
            public string  _firstName { get; set; }
            public string  _lastName { get; set; }
            public string _address { get; set; }
        }

        //public IQueryable<CustomerAdress> MultipleTables_Method()
        //{
        //    var result = (from ad in db.Addresses
        //                  from c in db.Customers
        //                  select new CustomerAdress()
        //                  {   _firstName = c.FirstName, 
        //                      _lastName = c.LastName, 
        //                      _address =  ad.AddressLine1 });
        //    return result;
        //}
    }
