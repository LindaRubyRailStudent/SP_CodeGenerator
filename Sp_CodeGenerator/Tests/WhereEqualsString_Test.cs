using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class WhereEqualsString_Test
    {
        public WhereEqualsString_Test()
        {

        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();
            WhereEqualsString wEqualString = new WhereEqualsString();
            List<WhereEqualsString_Result> linqResults = wEqualString.WhereEqualsString_Method();
            linqStopWatch.Stop();

            Stopwatch efStopW = new Stopwatch();
            efStopW.Start();
            var context = new AWorksLTEntities();
            List<WhereEqualsString_Result> efResults = new List<WhereEqualsString_Result>();
            foreach(WhereEqualsString_Result w in context.WhereEqualsString())
            {
                efResults.Add(w);
            }
            efStopW.Stop();

            if (linqResults.Count == efResults.Count)
            {
                writeToFile(linqResults, efResults, linqStopWatch, efStopW);
            }
        }

        private void writeToFile(
            List<WhereEqualsString_Result> linqResults,
            List<WhereEqualsString_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResults;
            if (!File.Exists("WhereEqualsString.csv"))
            {
                testResults = new StreamWriter("WhereEqualsString.csv");
            }
            else 
            {
                testResults = File.AppendText("WhereEqualsString.csv");
            }
            var orderedLinqResults = linqResults.OrderByDescending(l => l.Description).ThenByDescending(l => l.Name);
            var orderedEfResults = efResults.OrderByDescending(e => e.Description).ThenByDescending(e => e.Name);
            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq Time, " + linqStopWatch.ElapsedMilliseconds.ToString());
            testResults.WriteLine("EF Time," + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(SelectSubset_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("Linq Results," + r.Linq.Description +","+ r.Linq.Name);
                var efLine = ("Ef Results,"+ r.Ef.Description +","+ r.Ef.Name);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();
        }
    }
}
