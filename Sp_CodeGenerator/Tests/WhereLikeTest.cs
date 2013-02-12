using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Sp_CodeGenerator
{
    public class WhereLikeTest
    {
        public WhereLikeTest(){}

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            WhereLikeString whereLikeString = new WhereLikeString();
            List<WhereLikeString_Result> linqResults = whereLikeString.WhereLikeString_Method("LL Road Frame - Red, 52");

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AdventureWorksLT2008R2Entities();
            List<WhereLikeString_Result> efLikeString = new List<WhereLikeString_Result>();

            foreach (WhereLikeString_Result w in context.WhereLikeString("LL Road Frame - Red, 52"))
            {
                efLikeString.Add(w);
            }

            efStopWatch.Stop();

            if (linqResults.Count() == efLikeString.Count())
            {
                writeToFile(linqResults, efLikeString, linqStopWatch, efStopWatch);
            }
        }

        private void writeToFile(
            List<WhereLikeString_Result> linqresults,
            List<WhereLikeString_Result> efresults,
            Stopwatch linqStopwatch,
            Stopwatch efStopwatch)
        {
            StreamWriter testResults;
            if (!File.Exists("WhereLikeString_result.txt"))
            {
                testResults = new StreamWriter("WhereLikeString_result.txt");
            }
            else
            {
                testResults = File.AppendText("WhereLikeString_result.txt");
            }

            var orderedLinqResults = linqresults.OrderByDescending(l => l.Color).OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductID);
            var orderedEfResults = efresults.OrderByDescending(e => e.Color).OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductID);

            var result = orderedLinqResults.Zip(orderedEfResults,(l,e)=> new {Linq = l, Ef = e});
            testResults.WriteLine("Linq operation took " + linqStopwatch.Elapsed.ToString());
            testResults.WriteLine("Ef operation took " + efStopwatch.Elapsed.ToString());

            foreach(var r in result){
                testResults.WriteLine(WhereLikeString_Result.Equals(r.Linq, r.Ef));
                var linqLine = string.Format("{0}\t{1}\t{2}\t{3}", "LinqResults", r.Linq.Color, r.Linq.Name, r.Linq.ProductID);
                var efLine = string.Format("{0}\t{1}\t{2}\t{3}", "EFResults", r.Ef.Color, r.Ef.Name, r.Ef.ProductID);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();
        }
    }
}
