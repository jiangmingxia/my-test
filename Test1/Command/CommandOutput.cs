using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1.Command
{
    class CommandOutput
    {
        internal static string HalfSeperation = "========";

        internal static void commandErrorOutput(string s)
        {
            Console.WriteLine(s);
        }

        internal static void commandMessageOutput(string s)
        {
            Console.WriteLine(s);
        }

        internal static void commandSeperationOutput(string s)
        {
            string output = HalfSeperation + s + HalfSeperation;
            Console.WriteLine(output);
        }
    }
}
