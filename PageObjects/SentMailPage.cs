using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace PageObjects
{
    public class SentMailPage : BasePage
    {

        //String pageUrl = "https://mail.google.com/mail/u/0/#sent";

        [FindsBy(How = How.XPath, Using = "//div[@class='AO']//div[@role='main']//tbody/tr[1]//span[@class='bog']")]
        public IWebElement lastSentEmailSubject;
        

        public String getSubjectOflastSentEmail()
        {
            return lastSentEmailSubject.Text;
        }


    }
}
