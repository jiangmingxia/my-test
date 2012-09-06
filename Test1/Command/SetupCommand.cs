using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class SetupCommand:ICommand
    {
        internal static bool isSetupCalled = false;
        internal static string commandSample = CommandFactory.SETUP_CMD + " -" + CONFIG_OPTION + " <config file> -" + TESTSET_OPTION + " <testset file>"; 
        internal const string CONFIG_OPTION = "config";
        internal const string TESTSET_OPTION = "testset";
        
        private string config="";
        private string testset="";
        

        #region construct
        internal SetupCommand(Dictionary<string, string> options) 
        {
            //no need to trim any value since it has been done in CommandReader

            //the number of options must be 2: config and testset
            if (options.Count != 2 || !options.TryGetValue(CONFIG_OPTION, out config) || !options.TryGetValue(TESTSET_OPTION, out testset))
            {
                CommandOutput.commandErrorOutput("Command should be: "+commandSample);
                config = "";
                testset = "";
                return;
            }
            
            if (config.Equals("") || testset.Equals(""))
            {
                CommandOutput.commandErrorOutput("Command should be: " + commandSample);      
            }
        }
        #endregion construct

        public bool execute()
        {
            if (config.Equals("") || testset.Equals("")) return false;
            if (isSetupCalled) 
            {
                CommandOutput.commandErrorOutput("Please exec release command before setup again.");
                return false;
            }
            if (!execSetup()) return false;
            
            isSetupCalled = false;
            return false;
        }

        /// <summary>
        /// Setup prepare part of tests in testset        
        /// </summary>
        /// <returns>true if setup succeed</returns>
        private bool execSetup()
        {
            //set isSetupCalled = true when any resource is created.
            CommandOutput.commandSeperationOutput("SETUP BEGIN");


            CommandOutput.commandSeperationOutput("SETUP END");
            
            return true;
        }

        
       
    }
}
