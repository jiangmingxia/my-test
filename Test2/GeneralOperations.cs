using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using White.Core;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.WindowsAPI;
using System.Diagnostics;

using Test2.UI_Dialogs.Logins;


namespace Test2
{
    public class GeneralOperations

    {        
        private string IEPath = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe";

        public Window openIE(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo(IEPath);
            Application app = Application.AttachOrLaunch(psi);
            string winName = "Blank Page";
            Window mainWin = Utility.getWindow_wait(winName);
            if (mainWin == null)
            {
                Logger.WriteLine("Fail to find window: " + winName + "after open new IE");
                return null;
            }
           
            TextBox addressBar = Utility.getItem_wait<TextBox>(mainWin, SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Address"));

            if (addressBar != null)
            {                
                addressBar.SetValue(url);
                addressBar.KeyIn(KeyboardInput.SpecialKeys.RETURN);
                return mainWin;
            }
            
            Logger.WriteLine("IE address bar not found!");
            return null;            
        }

        public Window open_ALM(string alm_server) 
        {
            Window mainWin = openIE(alm_server);
            if (mainWin == null)
            {
                Logger.WriteLine("Cannot open ALM.");
                return null;
            }
            AutomationElement mainWin_AE = mainWin.AutomationElement;

            ProjectLogin p = new ProjectLogin(mainWin_AE);
            if (p.login_pane_ready() == true)
            {
                //if warning exist, click ok to close warning window              
                //var desktop = AutomationElement.RootElement;
                var condition = new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "MessageForm"),
                  new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window));
                var warnWin = mainWin_AE.FindFirst(TreeScope.Descendants, condition);
                if (warnWin != null)
                {
                    condition = new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                        new PropertyCondition(AutomationElement.NameProperty, "OK"));
                    var okButton = warnWin.FindFirst(TreeScope.Descendants, condition);
                    var clickPattern = (InvokePattern)okButton.GetCurrentPattern(InvokePattern.Pattern);
                    clickPattern.Invoke();
                }
                else
                {
                    Logger.WriteLine("Warnning message window not popup");
                }

                //TODO close restart window

                return mainWin;
            }
            return null;
        }
    }
}
