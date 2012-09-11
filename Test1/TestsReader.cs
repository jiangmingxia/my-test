using System;
using System.IO;
using System.Collections;


namespace Test1
{
    class TestsReader
    {
        //Copy dll to class path 
        private static void copyDlls(string dllLocation)
        { 
            //Todo
        }

        //Get dll location so that test can copy the dll to current environment to use it
        private static string getLocation(string line)
        { 
            //Todo
            return line;
        }

        //Get test name from a line in test set
        private static string getTestName(string line)
        { 
            //Todo
            return line.Trim();
        }

        //
        private static Test generateTest(string line)
        {
            string testName = getTestName(line);
            int startIndex = line.IndexOf("{");
            int endIndex = line.IndexOf("}");
            //do not have cases ID defined in testset
            if (startIndex < 0 || endIndex < 0) {
                return new Test(testName);
            }
            ArrayList ids = new ArrayList();
            return null;
            //int[] caseIDs = new int[];
            
        }

        internal static Test[] getTests(string path)
        {
            ArrayList tests = new ArrayList();
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;
                    // Read all lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        //skip comments
                        if (line.StartsWith("#")) continue;
                        //TestsReader.copyDlls(TestsReader.getLocation(line));                        
                        tests.Add(TestsReader.generateTest(line));
                    }
                }

                //copy to Test[]
                Test[] results = new Test[tests.Count];
                for (int i = 0; i < tests.Count; i++)
                {
                    results[i] = (Test)tests[i];
                }
                return results;
            }
            catch (Exception e)
            {
                string msg = "Specified file not found: " + path;
                DFunc.log(msg);
                DFunc.log(e.Message);
                Console.WriteLine(msg);
                return null;
            }

           
        }
    }
}
