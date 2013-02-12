using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Sp_CodeGenerator
{
    class WhereCompOpTest
    {
        public WhereCompOpTest(){}

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            WhereComparisonOperator whereCompOp = new WhereComparisonOperator();
            List<WhereComparisonOperator_Result> linqresult = whereCompOp.WhereComparisonOperator_Method("LL Road Frame - Red, 52", 2);

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AdventureWorksLT2008R2Entities();
            List<WhereComparisonOperator_Result> efWhereCompArg = new List<WhereComparisonOperator_Result>();

            foreach (WhereComparisonOperator_Result w in context.WhereComparisonOperator("LL Road Frame - Red, 52", 2))
            {
                efWhereCompArg.Add(w);
            }

            efStopWatch.Stop();

            if (linqresult.Count() == efWhereCompArg.Count())
            {
                writeToFile(linqresult, efWhereCompArg, linqStopWatch, efStopWatch);
            }
        }

        private void writeToFile(
            List<WhereComparisonOperator_Result> linqResults,
            List<WhereComparisonOperator_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResults;
            if (!File.Exists("WhereComparisonOperator_Result.txt"))
            {
                testResults = new StreamWriter("WhereComparisonOperator_Result.txt");
            }
            else
            {
                testResults = File.AppendText("WhereComparisonOperator_Result.txt");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductID);
            var orderedEfResults = efResults.OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductID);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq operation took " + linqStopWatch.Elapsed.ToString());
            testResults.WriteLine("Ef operation took " + efStopWatch.Elapsed.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(WhereClauseOneArgument_Result.Equals(r.Linq, r.Ef));
                var linqLine = string.Format("{0}\t{1}\t{2} ", "Linq Results", r.Linq.Name, r.Linq.ProductID);
                var efLine = string.Format("{0}\t{1}\t{2}", "Ef Results", r.Ef.Name, r.Ef.ProductID);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();

        }
    }
}
