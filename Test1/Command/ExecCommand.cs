using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class ExecCommand:ICommand
    {
        private string config;
        private string testset;
        internal static string NAME = "exec";

        public bool validateOptions(Dictionary<string, string> options)
        {
            return true;
        }

        public bool execute(Dictionary<string, string> options)
        {
            if (!validateOptions(options)) return false;

            CommandOutput.commandSeperationOutput("Exec BEGIN");


            CommandOutput.commandSeperationOutput("Exec END");

            return false;
        }

        public string getName()
        {
            return ExecCommand.NAME;
        }
    }
}
