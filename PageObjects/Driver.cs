using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;

namespace PageObjects
{
    public class Driver
    {        
        private static IWebDriver webDriverInstance;
        private static RemoteWebDriver winiumDesktopDriverInstance;
        private static WindowsDriver<WindowsElement> winAppDriverInstance;

        private Driver() { }

        public static IWebDriver GetWebDriver()
        {
            if (webDriverInstance == null)
            {
                webDriverInstance = new ChromeDriver();                
            }

            return webDriverInstance;
        }

        public static RemoteWebDriver GetWiniumDesktopDriver()
        {
            if (winiumDesktopDriverInstance == null)
            {
                DesiredCapabilities dc = new DesiredCapabilities();
                dc.SetCapability("app", @"C:/Program Files (x86)/Microsoft Office/Office15/OUTLOOK.exe");
                winiumDesktopDriverInstance = new RemoteWebDriver(new Uri("http://localhost:9999"), dc);
            }

            return winiumDesktopDriverInstance;
        }

        public static WindowsDriver<WindowsElement> GetWinAppDriver()
        {
            if (winAppDriverInstance == null)
            {
                winAppDriverInstance = new WindowsDriver<WindowsElement>(new Uri("http://172.24.10.52:4723/wd/hub"), GetCapabilitiesWindows());
                winAppDriverInstance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }
            return winAppDriverInstance;
        }

        private static DesiredCapabilities GetCapabilitiesWindows()
        {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.WindowsCommunicationsApps_8wekyb3d8bbwe!Microsoft.WindowsLive.Mail");            
            //appCapabilities.SetCapability("deviceName", "WindowsPC");
            return appCapabilities;
        }

        public static AutomationElement GetOutlookWindow()
        {            
            AutomationElement desktop = AutomationElement.RootElement;
            return desktop.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.NameProperty, "Inbox - SFedorov@lohika.com - Outlook"));
        }        

        public static void BrowserQuit()
        {
            if (webDriverInstance != null)
            {
                webDriverInstance.Quit();
                webDriverInstance = null;
            }
        }

        public static void DesktopDriverQuit()
        {
            if (winiumDesktopDriverInstance != null)
                winiumDesktopDriverInstance.Quit();
        }

        public static void WinAppDriverQuit()
        {
            if (winAppDriverInstance != null)
                winAppDriverInstance.Quit();
        }

    }
}
