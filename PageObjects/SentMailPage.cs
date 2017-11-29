using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace PageObjects
{
    public class SentMailPage : BaseWebPage
    {

        //String pageUrl = "https://mail.google.com/mail/u/0/#sent";

        [FindsBy(How = How.XPath, Using = "//div[@class='AO']//div[@role='main']//tbody/tr[1]//span[@class='bog']")]
        public IWebElement LastSentEmailSubject;
        

        public String getSubjectOflastSentEmail()
        {
            return LastSentEmailSubject.Text;
        }


    }
}
