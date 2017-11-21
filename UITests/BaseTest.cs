using NUnit.Framework;
using PageObjects;
using System;
using System.Configuration;

namespace UITests
{
    class BaseTest
    {
        [SetUp]
        public void BrowserSetup()
        {
            Driver.GetWebDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.GetWebDriver().Manage().Window.Maximize();
        }

        [TearDown]
        public void BrowserClose()
        {
            Driver.BrowserQuit();
        }
    }
}
