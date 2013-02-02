using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Sp_CodeGenerator;

namespace Sp_CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            selectDistinctTest sdTest = new selectDistinctTest();
            sdTest.runTest();

            SelectMultipleDistinctTest smdTest = new SelectMultipleDistinctTest();           
            smdTest.runTest();

            SelectSubsetTest subsetTest = new SelectSubsetTest();
            subsetTest.runTest();

            CInnerJoin cinnerJoin = new CInnerJoin();
            cinnerJoin.CInnerJoin_Method();
            //System.Diagnostics.Debug.Print(result.ProductName);
        }
    }
}
