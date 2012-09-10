using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test1;
using Test1.QC;

namespace Test1.Command
{
    class CommandEngine
    {
        public static bool run(string command, Dictionary<string, string> options)
        {
            ICommand cmd = CommandFactory.getCommand(command);
            if (cmd == null)
            {
                CommandOutput.commandErrorOutput("No such command!");
                return false;
            }

            StatusMachine stateMachine = new StatusMachine();
            ChangeCondition condition = stateMachine.mapToCondition(cmd);
            ProcessState currentState = stateMachine.MoveNext(condition);

            Boolean runResult, isResourceCreated;
            while (currentState != ProcessState.Terminated)
            {
                if (stateMachine.isRunningState(currentState) == false)
                {
                    //command is not going to run, must be wrong input
                    CommandOutput.commandErrorOutput(stateMachine.Messages[currentState]);
                }
                else 
                {
                    //need to run the command
                    runResult = cmd.execute(options);
                    if (cmd.GetType() == typeof(SetupCommand))
                    {
                        isResourceCreated = Resource.isResourceCreated();
                        condition = stateMachine.mapToCondition(cmd, runResult, isResourceCreated);
                    }
                    else
                    {
                        condition = stateMachine.mapToCondition(cmd, runResult, null);
                    }
                    currentState=stateMachine.MoveNext(condition);
                    if (currentState == ProcessState.Terminated) break;
                }
                //Fetch a command
                string input = Console.ReadLine();




            }

            //status = status.moveto(string cmd)
            //while status != exit
            //if status is 2run, run this command
            //after run is done, status=status.moveto(cmd(""),runresult,resource status)
            return false;
        }
    }
}
