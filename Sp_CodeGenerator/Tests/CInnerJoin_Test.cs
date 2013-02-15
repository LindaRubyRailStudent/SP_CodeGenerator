using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class CInnerJoin_Test
    {
        public CInnerJoin_Test()
        {

        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();
            CInnerJoin cInnerJoin = new CInnerJoin();
            List<CInnerJoin_Result> cJoinList = cInnerJoin.CInnerJoin_Method();
            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();
            var context = new AWorksLTEntities();
            List<CInnerJoin_Result> cInnerJoinList = new List<CInnerJoin_Result>();
            foreach (CInnerJoin_Result c in context.CInnerJoin())
            {
                cInnerJoinList.Add(c);
            }
            efStopWatch.Stop();

            if (cJoinList.Count() == cInnerJoinList.Count())
            {
                writeToFile(cJoinList, cInnerJoinList, linqStopWatch, efStopWatch);
            }

        }

        public void writeToFile(
            List<CInnerJoin_Result> linqList,
            List<CInnerJoin_Result> efList,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResult;

            if (!File.Exists("cInnerJoin_Result.txt"))
            {
                testResult = new StreamWriter("cInnerJoinResult.csv");
                //testResult = new StreamWriter("cInnerJoin_Result.txt");
            }
            else
            {
                testResult = File.AppendText("cInnerJoinResult.csv");
               // testResult = File.AppendText("cInnerJoin_Result.txt");
            }

            var orderedLinqResults = linqList.OrderByDescending(l => l.NonDiscountSales).OrderByDescending(l => l.ProductName);
            var orderedEfResults = efList.OrderByDescending(e => e.NonDiscountSales).OrderByDescending(e => e.ProductName);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResult.WriteLine("Linq Time ,  " + linqStopWatch.ElapsedMilliseconds);
            testResult.WriteLine("Ef Time , " + efStopWatch.ElapsedMilliseconds);

            foreach (var r in result)
            {
                testResult.WriteLine(CInnerJoin_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("LinqResults , " + r.Linq.NonDiscountSales + " , " + r.Linq.ProductName);
                var efLine = ("EfResults , " + r.Ef.NonDiscountSales + " , " + r.Ef.ProductName );
                //var linqLine = string.Format("{0}\t{1}\t{2}", "LinqResults", r.Linq.NonDiscountSales, r.Linq.ProductName);
                //var efLine = string.Format("{0}\t{1}\t{2}", "EfResults", r.Ef.NonDiscountSales, r.Ef.ProductName);
                testResult.WriteLine(linqLine);
                testResult.WriteLine(efLine);
            }
            testResult.Close();
        }
    }
}
