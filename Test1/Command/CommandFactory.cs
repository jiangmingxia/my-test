using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class CommandFactory
    {

        public static ICommand getCommand(string command)
        {
            if (command.Equals(SetupCommand.NAME)) return new SetupCommand();
            if (command.Equals(ExecCommand.NAME)) return new ExecCommand();
            if (command.Equals(ReleaseCommand.NAME)) return new ReleaseCommand();
            if (command.Equals(UpdateCommand.NAME)) return new UpdateCommand();            
            return null;
        }
    }
}
