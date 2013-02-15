using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class WhereGreaterOrEqual_Test
    {
        public WhereGreaterOrEqual_Test()
        {
            
        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            GreaterThanOrEqual gOrEqual = new GreaterThanOrEqual();
            List<GreaterThanOrEqual_Result> linqResults = gOrEqual.GreaterThanOrEqual_Method(DateTime.Now);
            linqStopWatch.Stop();

            Stopwatch efStopW = new Stopwatch();
            efStopW.Start();

            var context = new AWorksLTEntities();
            List<GreaterThanOrEqual_Result> efResults = new List<GreaterThanOrEqual_Result>();

            foreach (GreaterThanOrEqual_Result g in context.GreaterThanOrEqual(DateTime.Now))
            {
                efResults.Add(g);
            }
            efStopW.Stop();

            if (linqResults.Count == efResults.Count)
            {
                writeToFile(linqResults, efResults, linqStopWatch, efStopW);
            }
        }

        private void writeToFile(
            List<GreaterThanOrEqual_Result> linqResults,
            List<GreaterThanOrEqual_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResults;
            if (!File.Exists("GreaterThanOrEqual.csv"))
            {
                testResults = new StreamWriter("GreaterThanOrEqual.csv");
            }
            else
            {
                testResults = File.AppendText("GreaterThanOrEqual.csv");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.Comment).ThenBy(l => l.Status);
            var orderedEfResults = efResults.OrderByDescending(e => e.Comment).ThenBy(e => e.Status);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq Time , " + linqStopWatch.ElapsedMilliseconds.ToString());
            testResults.WriteLine("Ef Time ," + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(GreaterThanOrEqual_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("Linq Results ," + r.Linq.Comment + "," + r.Linq.Status);
                var efLine = ("EF Results ," + r.Ef.Comment + "," + r.Ef.Status);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();
        }
    }
}
