using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObjects
{
    public class Driver
    {        
        private static IWebDriver webDriverInstance;
        private static RemoteWebDriver desktopDriverInstance;


        private Driver() { }

        public static IWebDriver GetWebDriver()
        {
            if (webDriverInstance == null)
            {
                webDriverInstance = new ChromeDriver();                
            }

            return webDriverInstance;
        }

        public static RemoteWebDriver GetDesktopDriver()
        {
            if (desktopDriverInstance == null)
            {
                DesiredCapabilities dc = new DesiredCapabilities();
                dc.SetCapability("app", @"C:/Program Files (x86)/Microsoft Office/Office15/OUTLOOK.exe");
                desktopDriverInstance = new RemoteWebDriver(new Uri("http://localhost:9999"), dc);
            }

            return desktopDriverInstance;
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
            if (desktopDriverInstance != null)
                desktopDriverInstance.Quit();
        }

    }
}
