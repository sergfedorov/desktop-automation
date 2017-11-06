using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjects
{
    class Locators
    {
        
        public class InboxPageLocators
        {
            [FindsBy(How = How.XPath, Using = "//div[contains(text(), 'COMPOSE')]")]
            public static IWebElement composeNewEmail;

            [FindsBy(How = How.Name, Using = "to")]
            public static IWebElement recipientField;

            [FindsBy(How = How.Name, Using = "subjectbox")]
            public static IWebElement subjectField;

            [FindsBy(How = How.XPath, Using = "//div[@aria-label='Message Body']")]
            public static IWebElement messageBodyField;

            [FindsBy(How = How.XPath, Using = "//div[contains(text(), 'Send')]")]
            public static IWebElement sendEmailButton;

            [FindsBy(How = How.Id, Using = "link_vsm")]
            public static IWebElement emailSentLabel;

            [FindsBy(How = How.LinkText, Using = "Sent Mail")]
            public static IWebElement sentMailSectionLink;
        }

        public class LoginPageLocators
        {
            [FindsBy(How = How.Id, Using = "identifierId")]
            public static IWebElement EmailField;

            [FindsBy(How = How.Id, Using = "identifierNext")]
            public static IWebElement EmailNextButton;

            [FindsBy(How = How.Name, Using = "password")]
            public static IWebElement PasswordField;

            [FindsBy(How = How.Id, Using = "passwordNext")]
            public static IWebElement PasswordNextButton;

            [FindsBy(How = How.XPath, Using = "//div[@role='progressbar']")]
            public static IWebElement progressbar;
        }

        public class SentMailPageLocators
        {
            [FindsBy(How = How.XPath, Using = "//div[@class='AO']//div[@role='main']//tbody/tr[1]//span[@class='bog']")]
            public static IWebElement lastSentEmailSubject;
        }


    }
}
