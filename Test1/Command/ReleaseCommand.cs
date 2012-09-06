using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class ReleaseCommand:ICommand
    {        

        #region construct
        internal ReleaseCommand(Dictionary<string, string> options) 
        {
            
        }
        #endregion construct

        public bool execute()
        {
            return false;
        }
        
    }
}
