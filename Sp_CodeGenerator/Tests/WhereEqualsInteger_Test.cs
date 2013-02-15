using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class WhereEqualsInteger_Test
    {
        public WhereEqualsInteger_Test()
        {
        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            WhereEqualsInteger wEqualInt = new WhereEqualsInteger();
            List<WhereEqualsInteger_Result> linqResults = wEqualInt.WhereEqualsInteger_Method(9);

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AWorksLTEntities();
            List<WhereEqualsInteger_Result> efEqualInt = new List<WhereEqualsInteger_Result>();

            foreach (WhereEqualsInteger_Result w in context.WhereEqualsInteger(9))
            {
                efEqualInt.Add(w);
            }

            efStopWatch.Stop();

            if (linqResults.Count() == efEqualInt.Count())
            {
                writeToFile(linqResults, efEqualInt, linqStopWatch, efStopWatch);
            }
        }

        private void writeToFile(
            List<WhereEqualsInteger_Result> linqResults,
            List<WhereEqualsInteger_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResults;
            if (!File.Exists("WhereEqualsInteger.csv"))
            {
                testResults = new StreamWriter("WhereEqualsInteger.csv");
            }
            else
            {
                testResults = File.AppendText("WhereEqualsInteger.csv");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.ListPrice).OrderByDescending(l => l.Name);
            var orderedEfResults = efResults.OrderByDescending(e => e.ListPrice).OrderByDescending(e => e.Name);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq Time ," + linqStopWatch.ElapsedMilliseconds.ToString());
            testResults.WriteLine("Ef Time ," + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(WhereEqualsInteger_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("LinqResults ,"+ r.Linq.ListPrice+","+ r.Linq.Name);
                var EfLine = ("efResults ,"+r.Ef.ListPrice+"," +r.Ef.Name);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(EfLine);
            }
            testResults.Close();
        }
    }
}
