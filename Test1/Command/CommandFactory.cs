using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class CommandFactory
    {
        public const string SETUP_CMD = "setup";
        public const string EXEC_CMD = "exec";
        public const string RELEASE_CMD = "release";

        public static ICommand getCommand(string command, Dictionary<string,string> options)
        {
            if (command.Equals(SETUP_CMD)) return new SetupCommand(options);
            if (command.Equals(EXEC_CMD)) return new ExecCommand(options);
            if (command.Equals(RELEASE_CMD)) return new ReleaseCommand(options);
            Console.WriteLine("No such command: "+command);
            return null;
        }
    }
}
