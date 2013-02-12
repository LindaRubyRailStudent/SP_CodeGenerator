using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

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
            var context = new AdventureWorksLT2008R2Entities();
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
                testResult = new StreamWriter("cInnerJoin_Result.txt");
            }
            else
            {
                testResult = File.AppendText("cInnerJoin_Result.txt");
            }

            var orderedLinqResults = linqList.OrderByDescending(l => l.NonDiscountSales).OrderByDescending(l => l.ProductName);
            var orderedEfResults = efList.OrderByDescending(e => e.NonDiscountSales).OrderByDescending(e => e.ProductName);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResult.WriteLine("Linq operation took " + linqStopWatch.Elapsed.ToString());
            testResult.WriteLine("Ef operation took " + efStopWatch.Elapsed.ToString());

            foreach (var r in result)
            {
                testResult.WriteLine(CInnerJoin_Result.Equals(r.Linq, r.Ef));
                var linqLine = string.Format("{0}\t{1}\t{2}", "LinqResults", r.Linq.NonDiscountSales, r.Linq.ProductName);
                var efLine = string.Format("{0}\t{1}\t{2}", "EfResults", r.Ef.NonDiscountSales, r.Ef.ProductName);
                testResult.WriteLine(linqLine);
                testResult.WriteLine(efLine);
            }
            testResult.Close();
        }
    }
}
