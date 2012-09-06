using System;
using System.Text;
using System.Windows.Automation;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems;
using White.Core.UIItems.Finders;


namespace Test2.UI_Dialogs
{
    class projectMain
    {
        private Button logout_button;
        private Window mainWin;
        private Window welcomeWin;


        public projectMain(Window win)
        {
            mainWin = win;
            get_logout_button();
            if (logout_button == null)
            {
                throw new Exception("Fail to init project main window");            
            }
        }

        private void get_logout_button ()
        {
            int maxTime = 180;
            logout_button = Utility.getItem_wait<Button>(mainWin,SearchCriteria.ByAutomationId("m_logoutButton"),180);
            if (logout_button == null) 
            {
                Logger.WriteLine("fail to get logout button after waiting for " + maxTime + " seconds.");                
            }            
        }

        public bool isProjectMainReady()
        {
            if (logout_button == null)
            {
                get_logout_button();
            }
            if (logout_button == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int getWelcomeWindow()
        {
            welcomeWin = mainWin.Get<Window>(SearchCriteria.ByAutomationId("WelcomeForm"));
            if (welcomeWin == null)
            {
                Logger.WriteLine("No welcome page.");
                return 0;
            }
            Logger.WriteLine("Welcome page exists.");
            return 1;
        }

        public bool isWelcomePageExist()
        {
            if (getWelcomeWindow() == 1) return true;
            return false;
        }
        

        //when successfully logout, return 1
        //else return 0
        public int logout()
        {
            logout_button.Click();
            UI_Dialogs.Logins.ProjectLogin p = new UI_Dialogs.Logins.ProjectLogin(mainWin.AutomationElement);
            if (p.login_pane_ready())
            {
                return 1;
            }
            else 
            {
                return 0;
            }

        }
    }
}
