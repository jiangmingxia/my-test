using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Test1
{
    public class DFunc
    {
        private static string dir;
        private const string JOURNAL = "journal.txt";
        private static StreamWriter streamWriter = null;
        private static string journal_dir = "";   

        //log init
        internal static string Dir
        {
            set
            {
                dir = value;
                journal_dir = dir + @"\" + JOURNAL;
                File.Delete(journal_dir);
            }
        }

        //print result to log
        internal static void printResultInlog(string testName, int No, Result r)
        {
            string content = testName + "{" + No + "}: " + r.Status_string;
            log(content);
        }


        #region public methods
        //log to journal
        public static void log(string s)
        {
            FileStream objFileStream = new FileStream(DFunc.journal_dir, FileMode.Append, FileAccess.Write);            
            streamWriter = new StreamWriter(objFileStream);                
            streamWriter.WriteLine(s);
            streamWriter.Close();
        }
       

        //Test return PASS
        public static void returnPASS()
        {
            throw new Result(Result.PASS);
        }

        //Test return FAIL
        public static void returnFAIL()
        {
            throw new Result(Result.FAIL);
        }

        #endregion public methods
    }
}
