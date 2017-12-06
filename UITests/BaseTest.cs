using NUnit.Framework;
using PageObjects;
using System;
using System.Configuration;

namespace UITests
{
    class BaseTest
    {
        [OneTimeSetUp]
        public void BrowserSetup()
        {
            Driver.GetWebDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.GetWebDriver().Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void BrowserClose()
        {
            Driver.BrowserQuit();
            Driver.DesktopDriverQuit();
            Driver.WinAppDriverQuit();
        }
    
    }
}
