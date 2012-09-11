using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class ReleaseCommand:ICommand
    {
        internal static string NAME = "release";

        public bool validateOptions(Dictionary<string, string> options)
        {
            return true;
        }

        public bool execute(Dictionary<string, string> options)
        {
            if (!validateOptions(options)) return false;

            CommandOutput.commandSeperationOutput("Release BEGIN");


            CommandOutput.commandSeperationOutput("Release END");
            return true;
        }

        public string getName()
        {
            return ReleaseCommand.NAME;
        }
        
    }
}
