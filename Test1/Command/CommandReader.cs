using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class CommandReader
    {
        private static string command;
        private static Dictionary<string, string> options;

        public static bool load(string[] args)
        {
            options = new Dictionary<string, string>();
            if (args.Length < 1)
            {
                Console.WriteLine("Please provide a command.");
                return false;
            }
            command = args[0].Trim();

            options = new Dictionary<string, string>();
            string option;            
            //each option can have only one option value or no option value, like: -option1 value1 -option2
            for (int i = 1; i < args.Length; i++)
            {                
                option = args[i].Trim();
                if (!option.StartsWith("-"))
                {
                    CommandOutput.commandErrorOutput("Option must begin with -");
                    return false;
                }
                option = option.Substring(1).Trim();
                options.Add(option, "");
                if (i + 1 < args.Length) 
                {
                    args[i+1]=args[i+1].Trim();
                    if (!args[i+1].StartsWith("-"))
                    {
                        options[option] = args[i + 1];
                        i++;
                    }
                }
            }
            return true;
        }        

        internal static string Command
        {
            get { return command; }
        }

        internal static Dictionary<string, string> Options
        {
            get { return options; }
        }
    }
}
