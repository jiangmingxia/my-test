using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class CommandEngine
    {
        public static bool run(string command)
        {
            ICommand firstCommand = CommandFactory.getCommand(command);
            if (firstCommand == null)
            {
                CommandOutput.commandErrorOutput("No such command!");
            }
            //status = status.moveto(string cmd)
            //while status != exit
            //if status is 2run, run this command
            //after run is done, status=status.moveto(cmd(""),runresult,resource status)
            return false;
        }
    }
}
