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

            WhereBetweenValuesTest whereBetweenTest = new WhereBetweenValuesTest();
            whereBetweenTest.runTest();

            WhereClauseOneArgumentTest whereOneArgTest = new WhereClauseOneArgumentTest();
            whereOneArgTest.runTest();

            WhereCompOpTest whereCompOpTest = new WhereCompOpTest();
            whereCompOpTest.runTest();

            WhereLikeTest whereLikeTest = new WhereLikeTest();
            whereLikeTest.runTest();

            WhereSeveralConditions_Test whereSeveralCond = new WhereSeveralConditions_Test();
            whereSeveralCond.runTest();

            CInnerJoin_Test cInnerJoinTest = new CInnerJoin_Test();
            cInnerJoinTest.runTest();

            SelectAll_Test selectAllTest = new SelectAll_Test();
            selectAllTest.runTest();

        }
    }
}
