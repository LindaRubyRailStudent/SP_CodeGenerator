using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class selectDistinctTest
    {
        public selectDistinctTest()
        {
        }

        public void runTest()
        {
            // Call the generated Linq method and count
            Stopwatch linqstopwatch = new Stopwatch();
            linqstopwatch.Start();
            SelectDistinct selectdistinct = new SelectDistinct();
            List<SelectDistinct_Result> distinctList = selectdistinct.SelectDistinct_Method();
            linqstopwatch.Stop();
   


            // Call via the Stored Procedures
            Stopwatch efstopwatch = new Stopwatch();
            efstopwatch.Start();
            var context = new AWorksLTEntities();
            List<SelectDistinct_Result> sdResultList = new List<SelectDistinct_Result>();
            foreach (SelectDistinct_Result sdRes in context.SelectDistinct())
            {
                sdResultList.Add(sdRes);
            }
            efstopwatch.Stop();


            if (distinctList.Count() == sdResultList.Count())
            {
                writeToFile(distinctList, sdResultList, linqstopwatch, efstopwatch);

            }
        }

        public void writeToFile(
            List<SelectDistinct_Result> distinctList, 
            List<SelectDistinct_Result> sdResultList,
            Stopwatch linqstopwatch,
            Stopwatch efstopwatch)
        {
            StreamWriter testResult;

            if (!File.Exists("selectDistinctTestResult.csv"))
            {
                testResult = new StreamWriter("selectDistinctTestResult.csv");
            }
            else
            {
                testResult = File.AppendText("selectDistinctTestResult.csv");
            }

            var result = distinctList.Zip(sdResultList, (d, s) => new { Key = d, Value = s });
            testResult.WriteLine("Linq Time ," + linqstopwatch.ElapsedMilliseconds.ToString());
            testResult.WriteLine("EF Time , " + efstopwatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResult.WriteLine(SelectDistinct_Result.Equals(r.Key, r.Value));
                var linqLine = ("Linq Results, " + r.Key.SalesPerson);
                var efLine = ("Ef Results ," + r.Value.SalesPerson);
                testResult.WriteLine(linqLine);
                testResult.WriteLine(efLine);
            }
            testResult.Close();
        }
    }
}

