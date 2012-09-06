using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class ExecCommand:ICommand
    {
        private string config;
        private string testset;

        #region construct
        internal ExecCommand(Dictionary<string,string> options) 
        {
            
        }
        #endregion construct       

        public bool execute()
        {
            return false;
        }
    }
}
