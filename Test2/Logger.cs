using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test2
{
    class Logger
    {
        public static string logPath = @"C:\Downloads\test\log.txt";        
        public const int LOGFILE = 1;
        public const int CONSOLE = 2;
        public const int FILE_CONSOLE = 3;
        public static int logOption;

        public static void init() 
        {
            logOption = FILE_CONSOLE;
            File.Create(logPath);
        }

        public static void WriteLine(string s)
        {
            if (logOption == LOGFILE)
            {
                FileStream objFileStream = new FileStream(logPath, FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter(objFileStream);
                objStreamWriter.WriteLine(s);
                objStreamWriter.Close();
            }

            if (logOption == CONSOLE)
            {
                Console.WriteLine(s);
            }

            if (logOption == FILE_CONSOLE)
            {
                FileStream objFileStream = new FileStream(logPath, FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter(objFileStream);
                objStreamWriter.WriteLine(s);
                objStreamWriter.Close();
                Console.WriteLine(s);
            }
        }
    }
}
