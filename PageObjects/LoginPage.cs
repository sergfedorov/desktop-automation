using System;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObjects
{
    public class LoginPage : BaseWebPage
    {

        [FindsBy(How = How.Id, Using = "identifierId")]
        public IWebElement EmailField;

        [FindsBy(How = How.Id, Using = "identifierNext")]
        public IWebElement EmailNextButton;

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement PasswordField;

        [FindsBy(How = How.Id, Using = "passwordNext")]
        public IWebElement PasswordNextButton;

        By progressbar = By.XPath("//div[@role='progressbar']");

        String pageUrl = "https://mail.google.com/";             
           
        
        public void OpenPageUrl()
        {
            WebDriver.Navigate().GoToUrl(pageUrl);
        }

        public void FillOutEmailField(String userEmail)
        {
            EmailField.SendKeys(userEmail);            
        }
              
        public void ClickEmailNextButton()
        {
            EmailNextButton.Click();
        }

        public void FillOutPasswordField(String userPassword)
        {
            PasswordField.SendKeys(userPassword);
        }

        public void ClickPasswordNextButton()
        {
            PasswordNextButton.Click();
        }

        public void LoginToMailbox(String userName, String userPassword)
        {
            EmailField.SendKeys(userName);
            ClickEmailNextButton();
            PasswordField.SendKeys(userPassword);

            CustomWait.
                Until(ExpectedConditions.InvisibilityOfElementLocated(progressbar));                   

            ClickPasswordNextButton();

            CustomWait.
                Until(ExpectedConditions.TitleContains("Inbox"));

        }

    }
}
