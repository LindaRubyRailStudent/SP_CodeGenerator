using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Sp_CodeGenerator
{
    public class SelectSubsetTest
    {
        public SelectSubsetTest()
        {

        }

        public void runTest()
        {
            //Create a stopwatch
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            SelectSubset selectSubset = new SelectSubset();
            List<SelectSubset_Result> linqSubset = selectSubset.SelectSubset_Method();

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AdventureWorksLT2008R2Entities();
            List<SelectSubset_Result> efSubset = new List<SelectSubset_Result>();
            foreach (SelectSubset_Result sR in context.SelectSubset())
            {
                efSubset.Add(sR);
            }

            efStopWatch.Stop();

            if (linqSubset.Count() == efSubset.Count())
            {
                writeToFile(linqSubset, efSubset, linqStopWatch, efStopWatch);
            }


        }

        private void writeToFile(
            List<SelectSubset_Result> linqResults,
            List<SelectSubset_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResult;
            if (!File.Exists("selectSubset_Result.txt"))
            {
                testResult = new StreamWriter("selectSubset_Result.txt");
            }
            else
            {
                testResult = File.AppendText("selectSubset_Result.txt");
            }
            var orderedLinqResults = linqResults.OrderByDescending(l => l.Name).ThenByDescending(l => l.Price).ThenByDescending(l => l.ProductNumber);
            var orderedEfResults = efResults.OrderByDescending(e => e.Name).ThenByDescending(e => e.Price).ThenByDescending(e => e.ProductNumber);
            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResult.WriteLine("Linq operation took " + linqStopWatch.Elapsed.ToString());
            testResult.WriteLine("EF operation took " + efStopWatch.Elapsed.ToString());

            foreach (var r in result)
            {
                testResult.WriteLine(SelectSubset_Result.Equals(r.Linq, r.Ef));
                var linqLine = string.Format("{0}\t{1}\t{2}\t{3}", "Linq Results", r.Linq.Name, r.Linq.Price, r.Linq.ProductNumber);
                var efLine = string.Format("{0}\t{1}\t{2}\t{3}", "Ef Results", r.Ef.Name, r.Ef.Price, r.Ef.ProductNumber);
                testResult.WriteLine(linqLine);
                testResult.WriteLine(efLine);
            }
            testResult.Close();
        }
    }
}
