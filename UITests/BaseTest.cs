using NUnit.Framework;
using PageObjects;

namespace UITests
{
    class BaseTest
    {
        [SetUp]
        public void BrowserSetup()
        {
            Driver.GetDriver().Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(5);
            Driver.GetDriver().Manage().Window.Maximize();
        }

        [TearDown]
        public void BrowserClose()
        {
            Driver.BrowserQuit();
        }
    }
}
