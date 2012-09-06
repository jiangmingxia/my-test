using System;
using System.Text;
using System.Windows.Automation;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems;


namespace Test2.UI_Dialogs.Logins
{
    public class ProjectLogin
    {
        private AutomationElement login_main_pane;
        private AutomationElement login_input;
        private AutomationElement password_input;
        private AutomationElement domain_input;
        private AutomationElement project_input;
        private AutomationElement authenticate_button;
        private AutomationElement login_button; 

        public ProjectLogin(AutomationElement root_window)
        {
            int maxWaitTime = 600;
            Condition condition = new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "m_loginTabControl"),
              new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tab));
            login_main_pane = Utility.findFirst_wait(root_window, TreeScope.Descendants, condition,maxWaitTime);
            if (login_main_pane == null)
            {
                throw new Exception("Login init fail, login main pane is not found");
            }
        }

        public bool login_pane_ready()
        {
            if (this.get_login_button() != null) 
            {
                return true;
            }
            return false;        
        }

        //return 1 when successfully set loginname
        //return 0 when any UI element not found
        public int set_loginname(string loginName)
        {
            if (login_input == null)
            {
                var condition = new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "m_user"),
                                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
                login_input = login_main_pane.FindFirst(TreeScope.Descendants, condition);
                if (login_input == null)
                {
                    Logger.WriteLine("login pane is not found!");
                    return 0;
                }
            }
            
            login_input.SetFocus();

            AutomationElement login_edit = Utility.findFirst_wait(login_input, TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
            if (login_edit == null)
            {
                Logger.WriteLine("login name edit is not found!");
                return 0;
            }
            ValuePattern vp = (ValuePattern)login_edit.GetCurrentPattern(ValuePattern.Pattern);
            vp.SetValue(loginName);
            return 1;
        }

        //return 1 when successfully set password
        //return 0 when any UI element not found
        public int set_password(string pwd)
        {
            if (password_input == null)
            {
                var condition = new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "m_password"),
                                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
                password_input = login_main_pane.FindFirst(TreeScope.Descendants, condition);
                if (password_input == null)
                {
                    Logger.WriteLine("password pane is not found!");
                    return 0;
                }
            }
            
            password_input.SetFocus();

            AutomationElement password_edit = Utility.findFirst_wait(password_input, TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
            if (password_edit == null)
            {
                Logger.WriteLine("password edit is not found!");
                return 0;
            }
            ValuePattern vp = (ValuePattern)password_edit.GetCurrentPattern(ValuePattern.Pattern);
            vp.SetValue(pwd);
            return 1;
        }

        //return 1 when successfully set domain
        //return 0 when any UI element not found
        public int set_domain(string domain)
        {
            if (domain_input == null)
            {
                Condition condition = new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "m_domains"),
              new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ComboBox));
                domain_input = login_main_pane.FindFirst(TreeScope.Descendants, condition);
                if (domain_input == null)
                {
                    Logger.WriteLine("domain combobox is not found.");
                    return 0;
                }
            }
            
            ValuePattern vp = (ValuePattern)domain_input.GetCurrentPattern(ValuePattern.Pattern);
            vp.SetValue(domain);
            return 1;
        }

        //return 1 when successfully set project
        //return 0 when any UI element not found
        public int set_project(string project)
        {
            if (project_input == null)
            {
                Condition condition = new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "m_projects"),
              new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ComboBox));
                project_input = login_main_pane.FindFirst(TreeScope.Descendants, condition);
                if (project_input == null)
                {
                    Logger.WriteLine("project combobox is not found.");
                    return 0;
                }
            }
            
            ValuePattern vp = (ValuePattern)project_input.GetCurrentPattern(ValuePattern.Pattern);
            vp.SetValue(project);
            return 1;
        }

        //return 1 if successfully do authenticate
        //return 0 if any UI element not found
        public int login_authenticate()
        {
            if (authenticate_button == null)
            {
                get_auth_button();
                if (authenticate_button == null) return 0;
            }
            
            InvokePattern vp = (InvokePattern)authenticate_button.GetCurrentPattern(InvokePattern.Pattern);
            vp.Invoke();
            return 1;
        }

        private AutomationElement get_auth_button()
        {
            Condition condition = new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "m_authenticate"),
              new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button));
            authenticate_button = login_main_pane.FindFirst(TreeScope.Descendants, condition);
            if (authenticate_button == null)
            {
                Logger.WriteLine("authenticate button is not found.");
                return null;
            }
            return authenticate_button;
        }

        //return 1 if successfully login
        //return 0 if any UI element not found
        public int do_login()
        {
            if (login_button == null)
            {
                get_login_button();
                if (login_button == null) return 0;
            }

            InvokePattern vp = (InvokePattern)login_button.GetCurrentPattern(InvokePattern.Pattern);
            vp.Invoke();
            return 1;
        }

        private AutomationElement get_login_button()
        {
            Condition condition = new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "m_login"),
              new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button));
            login_button = login_main_pane.FindFirst(TreeScope.Descendants, condition);
            if (login_button == null)
            {
                Logger.WriteLine("Login button is not found.");
                return null;
            }
            return login_button;
        }
    }
}
