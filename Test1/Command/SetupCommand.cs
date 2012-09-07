using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class SetupCommand:ICommand
    {        
        internal const string CONFIG_OPTION = "config";
        internal const string TESTSET_OPTION = "testset";
        internal const string NAME = "setup";
        internal static string commandSample = NAME + " -" + CONFIG_OPTION + " <config file> -" + TESTSET_OPTION + " <testset file>"; 

        public bool execute(Dictionary<string, string> options)
        {
            if (!validateOptions(options)) return false;
            string config = options[CONFIG_OPTION];
            string testset = options[TESTSET_OPTION];

            //set isSetupCalled = true when any resource is created.
            CommandOutput.commandSeperationOutput("SETUP BEGIN");


            CommandOutput.commandSeperationOutput("SETUP END");
            return false;
        }

        //validate given options
        //the number of options must be 2: config and testset
        public bool validateOptions(Dictionary<string, string> options)
        {
            string config="";
            string testset="";
            
            if (options.Count != 2 || !options.TryGetValue(CONFIG_OPTION, out config) || !options.TryGetValue(TESTSET_OPTION, out testset))
            {
                CommandOutput.commandErrorOutput("Command should be: " + commandSample);
                config = "";
                testset = "";
                return false;
            }

            if (config.Equals("") || testset.Equals(""))
            {
                CommandOutput.commandErrorOutput("Command should be: " + commandSample);
                return false;
            }

            return true;
        }

        public string getName()
        {
            return SetupCommand.NAME;
        }
    }
}
