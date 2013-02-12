using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Sp_CodeGenerator
{
    public class SelectAll_Test
    {
        public SelectAll_Test()
        {

        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();
            SelectAll selectAll = new SelectAll();
            List<SelectAll_Result> selectAllList = selectAll.SelectAll_Method();
            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();
            var context = new AdventureWorksLT2008R2Entities();
            List<SelectAll_Result> efSelectAllList = new List<SelectAll_Result>();
            foreach (SelectAll_Result s in context.SelectAll())
            {
                efSelectAllList.Add(s);
            }
            efStopWatch.Stop();

            if (selectAllList.Count() == efSelectAllList.Count())
            {
                writeToFile(selectAllList, efSelectAllList, linqStopWatch, efStopWatch);
            }
        }

        public void writeToFile(
            List<SelectAll_Result> linqList,
            List<SelectAll_Result> efList,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch
            )
        {
            StreamWriter testResult;
            if (!File.Exists("SelectAllResult.txt"))
            {
                testResult = new StreamWriter("SelectAllResult.txt");
            }
            else
            {
                testResult = File.AppendText("SelectAllResult.txt");
            }

            var orderedLinqResults = linqList.OrderByDescending(l => l.ProductID).OrderByDescending(l=> l.Color).OrderByDescending(l=> l.DiscontinuedDate).OrderByDescending(l=> l.ListPrice).OrderByDescending(l => l.ModifiedDate).OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductCategoryID).OrderByDescending(l => l.ProductModelID).OrderByDescending(l => l.ProductNumber).OrderByDescending(l => l.SellEndDate).OrderByDescending(l => l.SellStartDate).OrderByDescending(l => l.Size).OrderByDescending(l => l.StandardCost).OrderByDescending(l => l.Weight);
            var orderedEfResults = efList.OrderByDescending(e => e.ProductID).OrderByDescending(e => e.Color).OrderByDescending(l => l.DiscontinuedDate).OrderByDescending(l => l.ListPrice).OrderByDescending(e => e.ModifiedDate).OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductCategoryID).OrderByDescending(e => e.ProductModelID).OrderByDescending(e => e.ProductNumber).OrderByDescending(e => e.SellEndDate).OrderByDescending(e => e.SellStartDate).OrderByDescending(e => e.Size).OrderByDescending(e => e.StandardCost).OrderByDescending(e => e.Weight); 

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResult.WriteLine("Linq operation took " + linqStopWatch.Elapsed.ToString());
            testResult.WriteLine("Ef operation took " + efStopWatch.Elapsed.ToString());

            foreach (var r in result)
            {
                testResult.WriteLine(SelectAll_Result.Equals(r.Linq, r.Ef));
                var linqLine = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}", "LinqResults", r.Linq.Color, r.Linq.DiscontinuedDate, r.Linq.ListPrice, r.Linq.ModifiedDate, r.Linq.Name, r.Linq.ProductCategoryID, r.Linq.ProductID, r.Linq.ProductModelID, r.Linq.ProductNumber, r.Linq.rowguid, r.Linq.SellEndDate, r.Linq.SellStartDate, r.Linq.Size, r.Linq.StandardCost, r.Linq.ThumbNailPhoto, r.Linq.ThumbnailPhotoFileName, r.Linq.Weight);
                var efLine = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}", "EfResults", r.Ef.Color, r.Ef.DiscontinuedDate, r.Ef.ListPrice, r.Ef.ModifiedDate, r.Ef.Name, r.Ef.ProductCategoryID, r.Ef.ProductID, r.Ef.ProductModelID, r.Ef.ProductNumber, r.Ef.rowguid, r.Ef.SellEndDate, r.Ef.SellStartDate, r.Ef.Size, r.Ef.StandardCost, r.Ef.ThumbNailPhoto, r.Ef.ThumbnailPhotoFileName, r.Ef.Weight);

                testResult.WriteLine(linqLine);
                testResult.WriteLine(efLine);
            }
            testResult.Close();
        }

    }
}
