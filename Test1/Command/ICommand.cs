using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    public interface ICommand
    {        
        bool execute(Dictionary<string, string> options);
        bool validateOptions(Dictionary<string, string> options);
        string getName();
        
    }
}
