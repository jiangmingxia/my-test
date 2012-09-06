using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using White.Core.UIItems.WindowItems;


namespace Test2.ALM_Operations
{
    class MainOperations
    {
        GeneralOperations gos = new GeneralOperations();

        public Window login_alm(string alm_server, string username, string password, string domain, string project)
        {
            Window mainWin = gos.open_ALM(alm_server);            

            if (mainWin == null)
            {
                throw new Exception("Open ALM server page failed.");     
            }

            AutomationElement mainWin_AE = mainWin.AutomationElement;
            UI_Dialogs.Logins.ProjectLogin p = new UI_Dialogs.Logins.ProjectLogin(mainWin_AE);

            if (p.set_loginname(username) == 0)
            {
                Logger.WriteLine("Fail to set login name");
                return null;
            }
            if (p.set_password(password) == 0)
            {
                Logger.WriteLine("Fail to set password");
                return null;
            }

            if (p.login_authenticate() == 0)
            {
                Logger.WriteLine("Fail to authenticate");
                return null;
            }

            if (p.set_domain(domain) == 0)
            {
                Logger.WriteLine("Fail to select domain");
                return null;
            }
            if (p.set_project(project) == 0)
            {
                Logger.WriteLine("Fail to select project");
                return null;
            }
            if (p.do_login() == 0)
            {
                Logger.WriteLine("Fail to login");
                return null;
            }

            //wait until login finish
            UI_Dialogs.projectMain projectMainWin = new UI_Dialogs.projectMain(mainWin);
            if (projectMainWin.isProjectMainReady())
            {
                return mainWin;
            }

            return null;                           
        }

        public int logout_alm(Window win)
        {
            UI_Dialogs.projectMain projectMainWin = new UI_Dialogs.projectMain(win);
            if (projectMainWin.logout() == 1)
            {
                Logger.WriteLine("Successfully logout ALM.");
                return 1;
            }
            else 
            {
                Logger.WriteLine("Fail to logout ALM.");
                return 0;
            }
        }
    }
}
