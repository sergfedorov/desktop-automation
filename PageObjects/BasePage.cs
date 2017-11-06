using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver WebDriver;
        protected WebDriverWait CustomWait;


        public BasePage()
        {
            this.WebDriver = Driver.GetDriver();
            PageFactory.InitElements(WebDriver, this);            
            this.CustomWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5));
        }

        protected void FillTheField(IWebElement fieldElement, String data)
        {
            fieldElement.SendKeys(data);
        }
                
    }
}
