using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObjects
{
    public abstract class BaseWebPage
    {
        protected IWebDriver WebDriver;
        protected WebDriverWait CustomWait;


        public BaseWebPage()
        {
            this.WebDriver = Driver.GetWebDriver();
            PageFactory.InitElements(WebDriver, this);
            this.CustomWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5));
        }        
                
    }
}
