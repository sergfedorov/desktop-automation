using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;


namespace PageObjects
{
    public class InboxPage : BasePage
    {

        //String pageUrl = "https://mail.google.com/mail/u/0/#inbox";

        [FindsBy(How = How.XPath, Using = "//div[contains(text(), 'COMPOSE')]")]
        public IWebElement composeNewEmail;

        [FindsBy(How = How.Name, Using = "to")]
        public IWebElement recipientField;

        [FindsBy(How = How.Name, Using = "subjectbox")]
        public IWebElement subjectField;

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Message Body']")]
        public IWebElement messageBodyField;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(), 'Send')]")]
        public IWebElement sendEmailButton;

        [FindsBy(How = How.Id, Using = "link_vsm")]
        public IWebElement emailSentLabel;

        [FindsBy(How = How.LinkText, Using = "Sent Mail")]
        public IWebElement sentMailSectionLink;


        public void sendNewEmail(String recipientEmail, String emailSubject, String emailBody)
        {
            composeNewEmail.Click();
            recipientField.SendKeys(recipientEmail);
            subjectField.SendKeys(emailSubject);
            messageBodyField.SendKeys(emailBody);
            sendEmailButton.Click();

            customWait.
               Until(ExpectedConditions.ElementIsVisible(By.Id("link_vsm")));                     

        }

        public void GoToSentMailSection()
        {
            sentMailSectionLink.Click();

            customWait.
               Until(ExpectedConditions.TitleContains("Sent Mail"));
            
        }
        
    }
}
