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

            var result = linqResults.OrderBy(l => l.Name).ThenBy(l => l.Price).OrderBy(l => l.ProductNumber).Zip(efResults.OrderBy(e => e.Name).ThenBy(e => e.Price).ThenBy(e => e.ProductNumber), (l, e) => new { Linq = l, Ef = e });
            testResult.WriteLine("Linq operation took " + linqStopWatch.Elapsed.ToString());
            testResult.WriteLine("EF operation took " + efStopWatch.Elapsed.ToString());

            foreach (var r in result)
            {
                testResult.WriteLine(SelectSubset_Result.Equals(r.Linq, r.Ef));
            }
        }
    }
}
