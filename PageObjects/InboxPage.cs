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
        public IWebElement ComposeNewEmail;

        [FindsBy(How = How.Name, Using = "to")]
        public IWebElement RecipientField;

        [FindsBy(How = How.Name, Using = "subjectbox")]
        public IWebElement SubjectField;

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Message Body']")]
        public IWebElement MessageBodyField;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(), 'Send')]")]
        public IWebElement SendEmailButton;

        [FindsBy(How = How.LinkText, Using = "Sent Mail")]
        public IWebElement SentMailSectionLink;

        public By EmailSentLabel = By.Id("link_vsm");


        public void SendNewEmail(String recipientEmail, String emailSubject, String emailBody)
        {
            ComposeNewEmail.Click();
            RecipientField.SendKeys(recipientEmail);
            SubjectField.SendKeys(emailSubject);
            MessageBodyField.SendKeys(emailBody);
            SendEmailButton.Click();

            CustomWait.
               Until(ExpectedConditions.ElementIsVisible(EmailSentLabel));                     

        }

        public void GoToSentMailSection()
        {
            SentMailSectionLink.Click();

            CustomWait.
               Until(ExpectedConditions.TitleContains("Sent Mail"));
            
        }
        
    }
}
