using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Sp_CodeGenerator
{
    public class WhereBetweenValuesTest
    {
        public WhereBetweenValuesTest()
        {

        }

        public void runTest()
        {
            //Create a stopwatch
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            WhereBetweenValues whereBetween = new WhereBetweenValues();
            List<WhereBetweenValues_Result> linqSubset = whereBetween.WhereBetweenValues_Method(680, 713);

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AdventureWorksLT2008R2Entities();
            List<WhereBetweenValues_Result> efWhereBetween = new List<WhereBetweenValues_Result>();

            foreach (WhereBetweenValues_Result w in context.WhereBetweenValues(680, 713))
            {
                efWhereBetween.Add(w);
            }
            efStopWatch.Stop();

            if (linqSubset.Count() == efWhereBetween.Count())
            {
                writeToFile(linqSubset, efWhereBetween, linqStopWatch, efStopWatch);
            }
        }

        private void writeToFile(
            List<WhereBetweenValues_Result> linqResults,
            List<WhereBetweenValues_Result> efResults,
            Stopwatch linqStopwatch,
            Stopwatch efStopwatch)
        {
            StreamWriter testResults;
            if(!File.Exists("WhereBetweenValues_Result.txt"))
            {
                testResults = new StreamWriter("WhereBetweenValues_Result.txt");
            } 
            else
            {
                testResults = File.AppendText("WhereBetweenValues_Result.txt");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.Color).OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductID);
            var orderedEfResults = efResults.OrderByDescending(e => e.Color).OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductID);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq operation took " + linqStopwatch.Elapsed.ToString());
            testResults.WriteLine("Ef operation took " + efStopwatch.Elapsed.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(WhereBetweenValues_Result.Equals(r.Linq, r.Ef));
                var linqLine = string.Format("{0}\t{1}\t{2}\t{3}", "Linq Results", r.Linq.Color, r.Linq.Name, r.Linq.ProductID);
                var efLine = string.Format("{0}\t{1}\t{2}\t{3}", "Ef Results", r.Ef.Color, r.Ef.Name, r.Ef.ProductID);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();
        }
    }
}
