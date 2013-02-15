using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

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

            var context = new AWorksLTEntities();
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
            if (!File.Exists("selectSubset_Result.csv"))
            {
                testResult = new StreamWriter("selectSubset_Result.csv");
            }
            else
            {
                testResult = File.AppendText("selectSubset_Result.csv");
            }
            var orderedLinqResults = linqResults.OrderByDescending(l => l.Name).ThenByDescending(l => l.ListPrice).ThenByDescending(l => l.ProductNumber);
            var orderedEfResults = efResults.OrderByDescending(e => e.Name).ThenByDescending(e => e.ListPrice).ThenByDescending(e => e.ProductNumber);
            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResult.WriteLine("Linq Time , " + linqStopWatch.ElapsedMilliseconds.ToString());
            testResult.WriteLine("EF Time , " + efStopWatch.ElapsedMilliseconds.ToString());
            testResult.WriteLine(efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResult.WriteLine(SelectSubset_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("Linq Results ," + r.Linq.Name +","+ r.Linq.ListPrice+","+ r.Linq.ProductNumber);
                var efLine = ("Ef Results ,"+ r.Ef.Name+","+ r.Ef.ListPrice+","+ r.Ef.ProductNumber);
                testResult.WriteLine(linqLine);
                testResult.WriteLine(efLine);
            }
            testResult.Close();
        }
    }
}
