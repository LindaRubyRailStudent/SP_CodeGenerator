
//// This is the output code from your template
//// you only get syntax-highlighting here - not intellisense
//namespace Sp_CodeGenerator{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using DataBaseLayer;


// public class RevenueCalcuation {
//    private AWorksLTEntities db = new AWorksLTEntities();

//    public List<RevenueCalcuation_Result> RevenueCalcuation_Method(){
//                 var result = ( from p in db.Products
// join sod in  db.SalesOrderDetails on p.ProductID equals sod.ProductID
				
//                orderby p.Name ascending
//                //Index was out of range. Must be non-negative and less than the size of the collection. Parameter name: index
//// SQL code to be interpreted
//// SELECT 'Total income is', ((sod.OrderQty * sod.UnitPrice) * (1.0 - sod.UnitPriceDiscount)), ' for ', p.Name AS ProductName
//                List<RevenueCalcuation_Result> listresult = new List<RevenueCalcuation_Result>();
//foreach ( var r in result )
//{
//     RevenueCalcuation_Result s = new RevenueCalcuation_Result();
//s. = r.;
//s. = r.;
//s. = r.;
//s.ProductName = r.Name;
//listresult.Add(s);
//}return listresult;
//            }
//        }
//    }

