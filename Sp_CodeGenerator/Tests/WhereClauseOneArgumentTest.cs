using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

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

            var context = new AdventureWorksLT2008R2Entities();
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
            if (!File.Exists("WhereOneArgument_Result.txt"))
            {
                testResults = new StreamWriter("WhereOneArgument_Result.txt");
            }
            else
            {
                testResults = File.AppendText("WhereOneArgument_Result.txt");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.Name).OrderByDescending(l => l.ProductID);
            var orderedEfResults = efResults.OrderByDescending(e => e.Name).OrderByDescending(e => e.ProductID);

            var result = orderedLinqResults.Zip(orderedEfResults, (l, e) => new { Linq = l, Ef = e });
            testResults.WriteLine("Linq operation took " + linqStopWatch.Elapsed.ToString());
            testResults.WriteLine("Ef operation took " + efStopWatch.Elapsed.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(WhereClauseOneArgument_Result.Equals(r.Linq, r.Ef));
                var linqLine = string.Format("{0}\t{1}\t{2}", "Linq Results", r.Linq.Name, r.Linq.ProductID);
                var efLine = string.Format("{0}\t{1}\t{2}", "Ef Results", r.Ef.Name, r.Ef.ProductID);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();
        }
    }
}
