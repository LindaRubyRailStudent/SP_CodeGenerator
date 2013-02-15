using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class InnerJoinWhereMethod_Test
    {
        public InnerJoinWhereMethod_Test()
        {

        }

        public void runTest()
        {
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            InnerJoinWhereMethod inJoinMethod = new InnerJoinWhereMethod();
            List<InnerJoinWhereMethod_Result> linqResults = inJoinMethod.InnerJoinWhereMethod_Method();

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AWorksLTEntities();
            List<InnerJoinWhereMethod_Result> efResults = new List<InnerJoinWhereMethod_Result>();

            foreach (InnerJoinWhereMethod_Result i in context.InnerJoinWhereMethod())
            {
                efResults.Add(i);
            }
            efStopWatch.Stop();

            if (linqResults.Count == efResults.Count)
            {
                writeToFile(linqResults, efResults, linqStopWatch, efStopWatch);
            }
        }

        private void writeToFile(
            List<InnerJoinWhereMethod_Result> linqResults,
            List<InnerJoinWhereMethod_Result> efResults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResults;
            if (!File.Exists("InnerJoinWhere.csv"))
            {
                testResults = new StreamWriter("InnerJoinWhere.csv");
            }
            else
            {
                testResults = File.AppendText("InnerJoinWhere.csv");
            }

            var orderedLinqResults = linqResults.OrderByDescending(l => l.AddressLine1).OrderByDescending(l => l.AddressLine2).OrderByDescending(l => l.CompanyName).OrderByDescending(l => l.EmailAddress);
            var orderedEfResults = efResults.OrderByDescending(e => e.AddressLine1).OrderByDescending(e => e.AddressLine2).OrderByDescending(e => e.CompanyName).OrderByDescending(e => e.EmailAddress);

            var result = orderedLinqResults.Zip(orderedEfResults,(l,e) => new {Linq = l, Ef = e});
            testResults.WriteLine("Linq Time ," + linqStopWatch.ElapsedMilliseconds.ToString());
            testResults.WriteLine("Ef Time , " + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResults.WriteLine(InnerJoinWhereMethod_Result.Equals(r.Linq, r.Ef));
                var linqLine = ("LinqResults , " + r.Linq.AddressLine1 + "," + r.Linq.AddressLine2 + "," + r.Linq.CompanyName + "," + r.Linq.EmailAddress);
                var efLine = ("EfResults," + r.Ef.AddressLine1 + "," + r.Ef.AddressLine2 + "," + r.Ef.CompanyName + "," + r.Ef.EmailAddress);
                testResults.WriteLine(linqLine);
                testResults.WriteLine(efLine);
            }
            testResults.Close();
        }
    }
}
