using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class WhereSeveralConditions_Test
    {
        public WhereSeveralConditions_Test()
        {

        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            WhereSeveralConditions whSevCond = new WhereSeveralConditions();
            List<WhereSeveralConditions_Result> linqResults = whSevCond.WhereSeveralConditions_Method("p", "red");

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AWorksLTEntities();
            List<WhereSeveralConditions_Result> efResults = new List<WhereSeveralConditions_Result>();

            foreach (WhereSeveralConditions_Result w in context.WhereSeveralConditions("p", "red"))
            {
                efResults.Add(w);
            }

            efStopWatch.Stop();

            if (linqResults.Count() == efResults.Count())
            {
                writeToFile(linqResults, efResults, linqStopWatch, efStopWatch);
            }
        }

        public void writeToFile(
            List<WhereSeveralConditions_Result>linqResults,
            List<WhereSeveralConditions_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResults;
            if (!File.Exists("WhereSeveralConditions_Result.csv"))
            {
                testResults = new StreamWriter("WhereSeveralConditions_Result.csv");
            }
            else
            {
                testResults = File.AppendText("WhereSeveralConditions_Result.csv");
            }

            var orderLinqResults = linqResults.OrderByDescending(l => l.Color).OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductID);
            var orderedEfResults = efResults.OrderByDescending(e => e.Color).OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductID);

            var results = orderLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq Time ," + linqStopWatch.ElapsedMilliseconds.ToString());
            testResults.WriteLine("Ef Time , " + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in results)
            {
                testResults.WriteLine(WhereSeveralConditions_Result.Equals(r.Linq, r.Ef));
                var linqLine = ( "LinqResults ," + r.Linq.Color + "," + r.Linq.Name + "," + r.Linq.ProductID);
                var EfLine = ("EfResults ," + r.Linq.Color + "," + r.Linq.Name + "," + r.Linq.ProductID);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(EfLine);
            }
            testResults.Close();
        }
    }
}
