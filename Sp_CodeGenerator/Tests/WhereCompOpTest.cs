using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

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

            var context = new AWorksLTEntities();
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
            if (!File.Exists("WhereComparisonOperator_Result.csv"))
            {
                testResults = new StreamWriter("WhereComparisonOperator_Result.csv");
            }
            else
            {
                testResults = File.AppendText("WhereComparisonOperator_Result.csv");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductID);
            var orderedEfResults = efResults.OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductID);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq Time , " + linqStopWatch.ElapsedMilliseconds.ToString());
            testResults.WriteLine("Ef Time ," + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(WhereClauseOneArgument_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("Linq Results,"+ r.Linq.Name+","+ r.Linq.ProductID);
                var efLine = ("Ef Results,"+ r.Ef.Name+","+ r.Ef.ProductID);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();

        }
    }
}
