using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObjects
{
    public class Driver
    {
        private static IWebDriver webdriverInstance;

        private Driver() { }

        public static IWebDriver GetDriver()
        {
            if (webdriverInstance == null)
            {
                webdriverInstance = new ChromeDriver();
                
            }

            return webdriverInstance;
        }

        public static void BrowserQuit()
        {
            if (webdriverInstance != null)
            {
                webdriverInstance.Quit();
                webdriverInstance = null;
            }
        }

    }
}
