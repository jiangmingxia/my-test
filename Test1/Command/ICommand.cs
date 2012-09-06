using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    public interface ICommand
    {
        internal static string COMMAND_NAME;
        bool execute(Dictionary<string, string> options);
        
    }
}
