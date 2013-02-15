using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseLayer;
using System.IO;
using System.Diagnostics;

namespace Sp_CodeGenerator
{
    class SelectWhereNoParams_Test
    {
        public SelectWhereNoParams_Test()
        {

        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();
            SelectWhereNoParams whereNoParam = new SelectWhereNoParams();
            List<SelectWhereNoParams_Result> linqResults = whereNoParam.SelectWhereNoParams_Method();
            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();
            var context = new AWorksLTEntities();
            List<SelectWhereNoParams_Result> efResults = new List<SelectWhereNoParams_Result>();
            foreach (SelectWhereNoParams_Result s in context.SelectWhereNoParams())
            {
                efResults.Add(s);
            }
            efStopWatch.Stop();

            if (linqResults.Count == efResults.Count)
            {
                writeToFile(linqResults, efResults, linqStopWatch, efStopWatch);
            }           
        }

        private void writeToFile(
            List<SelectWhereNoParams_Result> linqResults,
            List<SelectWhereNoParams_Result> efResults,
            Stopwatch linqStopW,
            Stopwatch efStopW)
        {
            StreamWriter testResults;

            if (!File.Exists("SelectWhereNoParams.csv"))
            {
                testResults = new StreamWriter("SelectWhereNoParams.csv");
            }

            else
            {
                testResults = File.AppendText("SelectWhereNoParams.csv");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.ListPrice).OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductNumber);
            var orderedEfResults = efResults.OrderByDescending(e => e.ListPrice).OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductNumber);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq Time , " + linqStopW.ElapsedMilliseconds.ToString());
            testResults.WriteLine("Ef Time ," + efStopW.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(SelectWhereNoParams_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("LinqResults ," + r.Linq.ListPrice +","+ r.Linq.Name+","+ r.Linq.ProductNumber);
                var efLine = ("EfResults ,"+ r.Ef.ListPrice+","+ r.Ef.Name+","+ r.Linq.ProductNumber);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();

        }
    }
}
