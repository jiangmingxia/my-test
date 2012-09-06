using System;
using System.Reflection;
using System.Threading;
using System.Text.RegularExpressions;

namespace Test1
{
    class DRun
    {
        //testName is class Name
        //xxx{2}
        private static string getTestPrintName(string testName, int caseID)
        {
            return testName + "{" + caseID + "}";
        }

        //xxxx{1}:PASS
        //xxxx{2}:FAIL
        private static string putResultToConsole(string testName, int No, Result r)
        {
            return getTestPrintName(testName,No) +": "+ r.Status_string;
        }

        private static void runTest(Type testType, object test, int caseID, string testName)
        {
            string caseName = "test_" + caseID;
            string undoName = "test_" + caseID + "_undo";
            string printName = getTestPrintName(testName, caseID);

            try
            {
                MethodInfo testcase = testType.GetMethod(caseName);
                if (testcase == null)
                {
                    throw new Exception("Test method " + caseName + " is not found.");
                }
                testcase.Invoke(test, null);
            }
            catch (Exception e)
            {
                Result r;
                if (e.InnerException != null && e.InnerException is Result)
                {
                    r = (Result)e.InnerException;
                }
                else
                {
                    Console.WriteLine(e.Message);
                    r = new Result(Result.UNRESOLVED);
                }

                Console.WriteLine(putResultToConsole(testName,caseID, r));
                DFunc.printResultInlog(testName, caseID, r);
            }
            finally
            {
                MethodInfo c1u = testType.GetMethod(undoName);
                c1u.Invoke(test, null);
            }

        }

        static void Main(string[] args)
        {
            DFunc.Dir = System.Environment.CurrentDirectory;
           

            int i;
            //TODO:
            String testsetPath = "Testset.txt";
            Test[] tests = TestsReader.getTests(testsetPath);

            String testName = "DTest";

            

            //Get class type
            Type testType = typeof(Test1.DTest);

            //Get class instance
            object test = Activator.CreateInstance(testType);

            //Get case1 and exec
            i = 1;
            runTest(testType, test, i, testName);
            i = 2;
            runTest(testType, test, i, testName);
            

            Thread.Sleep(10000);           
        }
    }
}
