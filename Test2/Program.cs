using System;
using System.Collections.Generic;
using System.Text;
using White.Core.InputDevices;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.Core.WindowsAPI;
using White.Core.Factory;
using White.Core;
using System.Windows.Automation;



namespace Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.init();
            string alm_server = "http://apps006:8081/qcbin/start_a.jsp";
            string username = "sa";
            string pwd = "";
            string domain = "DEFAULT";
            string project1 = "006_p1_all_exts";
            string project2 = "vm24_p3_all_exts";

            Test2.ALM_Operations.MainOperations mainOperation = new Test2.ALM_Operations.MainOperations();
            Window mainWin1 = mainOperation.login_alm(alm_server,username,pwd,domain,project1);
            //Window mainWin2 = mainOperation.login_alm(alm_server, username, pwd, domain, project2);
            //mainOperation.logout_alm(mainWin2);
            mainOperation.logout_alm(mainWin1);
        }
    }
}
