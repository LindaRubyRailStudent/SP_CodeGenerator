using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DataBaseLayer;

namespace Sp_CodeGenerator
{
    public class SelectMultipleDistinctTest
    {
        public SelectMultipleDistinctTest()
        {

        }

        public void runTest(){

            //Create a stopwatch
            Stopwatch linqStopWatch = new Stopwatch();
            linqStopWatch.Start();

            // Call the linq method
            SelectMultipleDistinct selectMultipleD = new SelectMultipleDistinct();
            List<SelectMultipleDistinct_Result> multiplelist = selectMultipleD.SelectMultipleDistinct_Method();

            linqStopWatch.Stop();

            Stopwatch efStopWatch = new Stopwatch();
            efStopWatch.Start();

            var context = new AWorksLTEntities();
            List<SelectMultipleDistinct_Result> multipleResults = new List<SelectMultipleDistinct_Result>();
            foreach (SelectMultipleDistinct_Result mdr in context.SelectMultipleDistinct())
            {
                multipleResults.Add(mdr);
            }
            efStopWatch.Stop();

            if (multiplelist.Count() == multipleResults.Count())
            {
                writeToFile(multiplelist, multipleResults, linqStopWatch, efStopWatch);
            }
        }

        public void writeToFile(
            List<SelectMultipleDistinct_Result> linqresults,
            List<SelectMultipleDistinct_Result> Efresults,
            Stopwatch linqStopWatch,
            Stopwatch efStopWatch)
        {
            StreamWriter testResult;
            if (!File.Exists("selectMultipleDistinct_Result.csv"))
            {
                testResult = new StreamWriter("selectMultipleDistinct_Result.csv");
            }
            else
            {
                testResult = File.AppendText("selectMultipleDistinct_Result.csv");
            }

            var result = linqresults.OrderBy(l => l.CompanyName).ThenBy(l => l.FirstName).ThenBy(l => l.SalesPerson).Zip(Efresults.OrderBy(l => l.CompanyName).ThenBy(l => l.FirstName).ThenBy(l => l.SalesPerson), (l, e) => new { Linq = l, EF = e });
            testResult.WriteLine("Linq Time, " + linqStopWatch.ElapsedMilliseconds.ToString());
            testResult.WriteLine("Ef Time , " + efStopWatch.ElapsedMilliseconds.ToString());

            foreach (var r in result)
            {
                testResult.WriteLine(SelectMultipleDistinct_Result.Equals(r.Linq, r.EF));
                var linqLine = ("Linq Results ," + r.Linq.CompanyName +"," + r.Linq.FirstName +","+ r.Linq.SalesPerson);
                var efLine = ("Ef Results, " + r.EF.CompanyName+"," + r.EF.FirstName+ ","+ r.EF.SalesPerson);
                testResult.WriteLine(linqLine);
                testResult.WriteLine(efLine);
            }
            testResult.Close();

        }
    }
}
