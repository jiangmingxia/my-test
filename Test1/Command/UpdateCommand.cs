﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class UpdateCommand : ICommand
    {
        internal static string NAME = "update";

        public bool validateOptions(Dictionary<string, string> options)
        {
            return false;
        }

        public bool execute(Dictionary<string, string> options)
        {
            if (!validateOptions(options)) return false;
            return false;
        }

        public string getName()
        {
            return UpdateCommand.NAME;
        }

    }
}