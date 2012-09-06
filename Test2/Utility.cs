using System;
using White.Core.InputDevices;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.Core;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Diagnostics;

namespace Test2
{
    class Utility
    {
        public static int wait_time = 60;

        //Find Automation element according to given root, scope and condition
        public static AutomationElement findFirst_wait(AutomationElement root, TreeScope scope, Condition condition) 
        {
            int passedTime = 0;
            int maxWaitTime = wait_time;
            int interval = 2;
            AutomationElement element = null;
            while (passedTime < maxWaitTime)
            {
                element = root.FindFirst(scope, condition);
                if (element != null)
                {
                    Logger.WriteLine("Total time spent:" + passedTime + " for finding element with condition" + condition.ToString());
                    return element;
                }
                Thread.Sleep(interval * 1000);
                passedTime += interval;
            }
            Logger.WriteLine("Cannot find expected item after seconds: " + maxWaitTime);
            return null;
        }

        //Find Automation element according to given root, scope and condition, and max wait time
        public static AutomationElement findFirst_wait(AutomationElement root, TreeScope scope, Condition condition, int maxTime)
        {
            int passedTime = 0;
            int maxWaitTime = maxTime;
            int interval = 2;
            AutomationElement element = null;
            while (passedTime < maxWaitTime)
            {
                element = root.FindFirst(scope, condition);
                if (element != null)
                {
                    Logger.WriteLine("Total time spent:" + passedTime + " for finding element with condition" + condition.ToString());
                    return element;
                }
                Thread.Sleep(interval * 1000);
                passedTime += interval;
            }
            Logger.WriteLine("Cannot find expected item after seconds: " + maxWaitTime);
            return null;
        }

        //White: find white entity from a window according to give entity type and search criteria
        public static T getItem_wait<T>(Window w, SearchCriteria s) where T : UIItem
        {
            int passedTime = 0;
            int maxWaitTime = wait_time;
            int interval = 2;
            T item = null;
            while (passedTime < maxWaitTime) 
            {
                item = w.Get<T>(s);
                if (item != null)
                {
                    Logger.WriteLine("Total time spent:" + passedTime);
                    return item;
                }
                Thread.Sleep(interval * 1000);
                passedTime += interval; 
            }
            Logger.WriteLine("Cannot find expected item after seconds: " + maxWaitTime);
            return null;
        }

        //White: find white entity from a window according to give entity type, search criteria and specified max finding time
        public static T getItem_wait<T>(Window w, SearchCriteria s, int waitTime) where T : UIItem
        {
            int passedTime = 0;
            int maxWaitTime = waitTime;
            int interval = 2;
            T item = null;
            while (passedTime < maxWaitTime)
            {
                item = w.Get<T>(s);
                if (item != null)
                {
                    Logger.WriteLine("Total time spent:" + passedTime);
                    return item;
                }
                Thread.Sleep(interval * 1000);
                passedTime += interval;
            }
            Logger.WriteLine("Cannot find expected item after seconds: " + maxWaitTime);
            return null;
        }

        //White: search window from root(desktop) by window name keyword
        public static Window getWindow_wait (string windowName)
        {
            int passedTime = 0;
            int maxWaitTime = wait_time;
            int interval = 2;
            Window w = null;
            List<Window> windows = null; 
            while (passedTime < maxWaitTime)
            {
                windows = Desktop.Instance.Windows();
                w = windows.Find(obj => obj.Title.Contains(windowName));
                if (w != null)
                {
                    Logger.WriteLine("Total time spent for finding window " + windowName+" is: "+ passedTime);
                    return w;
                }
                Thread.Sleep(interval * 1000);
                passedTime += interval;
            }
            Logger.WriteLine("Cannot find expected window after seconds: " + maxWaitTime);
            return null;
        
        }

        //White: search window from given application by window name keyword
        public static Window getWindow_wait(Application app, string keyword_in_window)
        {
            int passedTime = 0;
            int maxWaitTime = wait_time;
            int interval = 2;            
            List<Window> windows = null;
            while (passedTime < maxWaitTime)
            {
                windows = app.GetWindows();
                foreach (Window w in windows) 
                {
                    if (w.Name.Contains(keyword_in_window))
                    {
                        return w;
                    }
                }
                
                Thread.Sleep(interval * 1000);
                passedTime += interval;
            }
            Logger.WriteLine("Cannot find expected window"+keyword_in_window+" after seconds: " + maxWaitTime);
            return null;
        }


        //UIAutomation: search window from given root's children(not decedant) by window name keyword
        public static AutomationElement getWindow(AutomationElement root, string keyword_in_name)
        {
            AutomationElement temp_win = TreeWalker.ControlViewWalker.GetFirstChild(root);
            string name = null;
            
            while (temp_win != null)
            {
                name = (string)temp_win.GetCurrentPropertyValue(AutomationElement.NameProperty);
                if (name.Contains(keyword_in_name)) return temp_win;
                temp_win = TreeWalker.ControlViewWalker.GetNextSibling(temp_win);
            }

            return null;
        }
      

        //UIAutomation: get window with wait according to specified process
        //public static AutomationElement getWindow_wait( Process p)
        //{
        //    int passedTime = 0;
        //    int maxWaitTime = wait_time;
        //    int interval = 2;
        //    AutomationElement window = null;
            
        //    while (passedTime < maxWaitTime)
        //    {
        //        window = AutomationElement.FromHandle(p.MainWindowHandle);                
        //        if (window != null)
        //        {
        //            return window;
        //        }
        //        Thread.Sleep(interval * 1000);
        //        passedTime += interval;
        //    }
        //    Logger.WriteLine("Cannot find expected window by process after seconds: " + maxWaitTime);
        //    return null;
        //}

       
        //To be deleted
        public static string getType(Window w, SearchCriteria s)
        {
            IUIItem item = w.Get(s);
            if (item == null)
            {
                return "Item not found";
            }
            return item.GetType().Name;
        }
            
    }
}
