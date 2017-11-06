using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObjects
{
    class Driver
    {
        private static IWebDriver webdriverInstance;

        private Driver() { }

        public static IWebDriver GetDriver()
        {
            if (webdriverInstance == null)
            {
                webdriverInstance = new ChromeDriver();
                webdriverInstance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                webdriverInstance.Manage().Window.Maximize();
            }

            return webdriverInstance;
        }

    }
}
