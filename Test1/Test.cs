using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Test1
{
    class Test
    {
        private string fullTestName;
        private string testName;
        private Type testType;
        private const string PACKAGE = "Test1";
        //same order as execute order;
        private int[] caseIDs;        
        private const string CASEPATTERN = "^test_([0-9]+)$";

        internal static string getCaseNamebyID(int id)
        {
            return "test_" + id;
        }
                
        internal string TestName
        {
            get { return testName; }
        }

        internal Type TestType
        {
            get { return testType; }
        }

        internal string FullTestName
        {
            get { return fullTestName; }
        }

        internal int[] CaseIDs
        {
            get { return caseIDs; }
        }

        //testName is class Name
        //xxx{2}
        internal string getTestPrintName(int caseID)
        {
            return testName + "{" + caseID + "}";
        }

        #region construct Test

        //all testcases are going to be executed
        internal Test(string testName)
        {
            this.testName = testName;
            Assembly t = Assembly.Load(testName);
            testType = t.GetType();
            MethodInfo[] mi = testType.GetMethods();
            int[] ids = new int[mi.Length];
            int length = 0;
            foreach (MethodInfo m in mi)
            {
                if (Regex.IsMatch(m.Name, CASEPATTERN))
                {
                    ids[length] = int.Parse(Regex.Split(m.Name, CASEPATTERN)[1]);
                    length++;
                }                
            }

            //copy ids to caseIDs
            caseIDs = new int[length];            
            for (int i = 0; i < length; i++)
            {
                caseIDs[i] = ids[i];
            }
        }

        internal Test(string testName, int[] ids)
        {
            this.testName = testName;
            int[] existIDs = new int[ids.Length];
            int length = 0;
            
            Assembly t = Assembly.Load(testName);
            testType = t.GetType();
            foreach (int i in ids)
            {
                if (testType.GetMethod(Test.getCaseNamebyID(i)) != null)
                {
                    existIDs[length] = i;
                    length++;
                }
            }

            //copy to caseIDs
            caseIDs = new int[length];
            for (int i = 0; i < length; i++)
            {
                caseIDs[i] = existIDs[i];
            }
        }

        #endregion construct Test
    }
}
