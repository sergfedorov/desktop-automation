using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait customWait;


        public BasePage()
        {
            this.driver = Driver.GetDriver();
            PageFactory.InitElements(driver, this);            
            this.customWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        protected void FillTheField(IWebElement fieldElement, String data)
        {
            fieldElement.SendKeys(data);
        }
                
    }
}
