using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test1
{
    class DTest
    {
        private int a1 = 1, a2 = 2;

        public void test_1()
        {
            a1 = 5;
            a2 = 6;
            int a = a1+a2;
            DFunc.log("The result is "+a);
            DFunc.returnPASS();
        }

        public void test_1_undo()
        {
            a1 = 1;
            a2 = 2;
        }

        public void test_2()
        {
            int a = a1 + a2;
            DFunc.log("The result is changed to " + a);
            DFunc.returnFAIL();
        }

        public void test_2_undo()
        {
            DFunc.log("Undo");
        }


    }
}
