using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class WhereSimpleEqualityTest
    {
        public WhereSimpleEqualityTest()
        {

        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();
            WhereSimpleEquality wSimpleE = new WhereSimpleEquality();
            List<WhereSimpleEquality_Result> linqResult = wSimpleE.WhereSimpleEquality_Method();
            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();
            var context = new AWorksLTEntities();
            List<WhereSimpleEquality_Result> efResults = new List<WhereSimpleEquality_Result>();
            foreach (WhereSimpleEquality_Result w in context.WhereSimpleEquality())
            {
                efResults.Add(w);
            }
            efStopWatch.Stop();

            if (linqResult.Count == efResults.Count)
            {
                writeToFile(linqResult, efResults, linqStopWatch, efStopWatch);
            }
        }

        private void writeToFile(
            List<WhereSimpleEquality_Result> linqResults,
            List<WhereSimpleEquality_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResults;
            if (!File.Exists("WhereSimpleEquality.csv"))
            {
                testResults = new StreamWriter("WhereSimpleEquality.csv");
            }
            else
            {
                testResults = File.AppendText("WhereSimpleEquality.csv");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.Name).ThenBy(l => l.ProductID);
            var orderedEfResults = efResults.OrderByDescending(e => e.Name).ThenBy(e => e.ProductID);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq Time ," + linqStopWatch.ElapsedMilliseconds.ToString());
            testResults.WriteLine("Ef Time ," + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(WhereSimpleEquality_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("Linq Results ," + r.Linq.Name + "," + r.Linq.ProductID);
                var efLine = ("EF Results ," + r.Ef.Name + "," + r.Ef.ProductID);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }

        }
    }
}
