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
            Driver.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.GetDriver().Manage().Window.Maximize();
        }

        [TearDown]
        public void BrowserClose()
        {
            Driver.BrowserQuit();
        }
    }
}
