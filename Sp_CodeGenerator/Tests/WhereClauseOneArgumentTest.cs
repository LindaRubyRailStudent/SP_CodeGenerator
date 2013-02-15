using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    class WhereClauseOneArgumentTest
    {
        public WhereClauseOneArgumentTest()
        {

        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            WhereClauseOneArgument whereOneArg = new WhereClauseOneArgument();
            List<WhereClauseOneArgument_Result> linqresult = whereOneArg.WhereClauseOneArgument_Method("LL Road Frame - Red, 52", "red");

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AWorksLTEntities();
            List<WhereClauseOneArgument_Result> efWhereOneArg = new List<WhereClauseOneArgument_Result>();

            foreach (WhereClauseOneArgument_Result w in context.WhereClauseOneArgument("LL Road Frame - Red, 52", "red"))
            {
                efWhereOneArg.Add(w);
            }

            efStopWatch.Stop();

            if (linqresult.Count() == efWhereOneArg.Count())
            {
                writeToFile(linqresult, efWhereOneArg, linqStopWatch, efStopWatch);
            }

        }

        private void writeToFile(
            List<WhereClauseOneArgument_Result> linqResults,
            List<WhereClauseOneArgument_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResults;
            if (!File.Exists("WhereOneArgument_Result.csv"))
            {
                testResults = new StreamWriter("WhereOneArgument_Result.csv");
            }
            else
            {
                testResults = File.AppendText("WhereOneArgument_Result.csv");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductID);
            var orderedEfResults = efResults.OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductID);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq Time, " + linqStopWatch.ElapsedMilliseconds.ToString());
            testResults.WriteLine("Ef Time , " + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(WhereClauseOneArgument_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("Linq Results ,"+ r.Linq.Name+","+ r.Linq.ProductID);
                var efLine = ("Ef Results,"+ r.Ef.Name+","+ r.Ef.ProductID);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();
        }
    }
}
