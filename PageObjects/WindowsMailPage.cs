using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PageObjects
{
    public class WindowsMailPage
    {

        WindowsDriver<WindowsElement> winAppDriver;
        WebDriverWait customWait;

        public WindowsMailPage()
        {
            this.winAppDriver = Driver.GetWinAppDriver();
            this.customWait = new WebDriverWait(winAppDriver, TimeSpan.FromSeconds(5));
        }

        By refreshButton = By.Name("Sync this view");
        By progressBar = By.ClassName("ProgressBar");
        By mailItemContainer = By.ClassName("MailItem");                                             
        By replyButton = By.Name("Reply");
        By emailBody = By.Name("Message");
        By sendButton = By.Name("Send");
        By sentMailLink = By.Name("Sent Items folder, favorite");                                   
        By firstMailContainerFromSentItemsList = By.XPath("//*[@ClassName='MailItem'][1]");
        By refreshStatus = By.Name("Up to date");
                

        public void ClickRefreshButton()
        {            
            winAppDriver.FindElement(refreshButton).Click();
            customWait.Until(ExpectedConditions.ElementIsVisible(refreshStatus));
        }

        public void ClickMailItemContainerFromList(string emailNameContent1, string emailNameContent2)
        {
            IReadOnlyCollection<WindowsElement> list = winAppDriver.FindElements(mailItemContainer);
            int listItemsCounter = 1;
            foreach (WindowsElement element in list)
            {                
                if (element.GetElementName().Contains(emailNameContent1)
                    && element.GetElementName().Contains(emailNameContent2))
                {
                    element.Click();                    
                    break;
                }
                listItemsCounter++;
                if(list.Count() == listItemsCounter)
                    throw new NoSuchElementException("Target email was not found in Inbox folder");
            }
        }

        public void ReplyToEmail(string replyText)
        {
            winAppDriver.FindElement(replyButton).Click();
            winAppDriver.FindElement(emailBody).SendKeys(replyText);
            winAppDriver.FindElement(sendButton).Click();
        }

        public void ClickSentMailLink()
        {
            winAppDriver.FindElement(sentMailLink).Click();
        }

        public string GetNameFirstEmailInSentItems()
        {
            return winAppDriver.FindElement(firstMailContainerFromSentItemsList).GetElementName();
        }

    }

}
