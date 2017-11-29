using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.PageObjects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjects
{
    public class OutlookWindowPage
    {
        RemoteWebDriver desktopDriver;

        public OutlookWindowPage()
        {
            this.desktopDriver = Driver.GetDesktopDriver();
            PageFactory.InitElements(desktopDriver, this);
        }
       

        [FindsBy(How = How.ClassName, Using = "rctrl_renwnd32")]
        IWebElement outlookWindow;

        /*[FindsBy(How = How.Name, Using = "New Email")]
        IWebElement newEmailButton;

        [FindsBy(How = How.Name, Using = "Reply")]
        IWebElement replyButton;

        [FindsBy(How = How.Name, Using = "Sent Items")]
        IWebElement sentItemsLink;

        [FindsBy(How = How.ClassName, Using = "LeafRow")]
        IWebElement mailContainerFromList;

        [FindsBy(How = How.XPath, Using = "//*[@ClassName='LeafRow'][1]")]
        IWebElement firstMailContainerFromSentItemsList;*/

        /*New email form*/
        /*[FindsBy(How = How.Id, Using = "4099")]
        IWebElement toField;
        [FindsBy(How = How.Id, Using = "4101")]
        IWebElement subjectField;
        [FindsBy(How = How.ClassName, Using = "_WwG")]
        IWebElement emailTextField;
        [FindsBy(How = How.Id, Using = "4256")]
        IWebElement sendEmailButton;*/

        By outlookWindowBy = By.ClassName("rctrl_renwnd32");

        By newEmailButton = By.Name("New Email");
        By replyButton = By.Name("Reply");        
        By sentItemsLink = By.Name("Sent Items");
        By mailContainerFromList = By.ClassName("LeafRow");
        By firstMailContainerFromSentItemsList = By.XPath("//*[@ClassName='LeafRow'][1]");

        By sendRecieveTab = By.Name("Send / Receive");
        By sendRecieveButton = By.Name("Send/Receive All Folders");

        By updateFolderButton = By.Name("Update Folder");
        By sendReceiveProgressWindow = By.Name("Outlook Send/Receive Progress");  
        
        

        /*New email form*/
        By toField = By.Id("4099");
        By subjectField = By.Id("4101");
        By emailTextField = By.ClassName("_WwG");
        By sendEmailButton = By.Id("4256");


        public void WaitForOutlookToBeVisible()
        {
            new WebDriverWait(desktopDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(outlookWindowBy));            
        }        

        public void ReplyToEmail(String replyText)
        {
            outlookWindow.FindElement(replyButton).Click();            
            IWebElement messageBox = outlookWindow.FindElement(emailTextField);            
            new Actions(desktopDriver).MoveToElement(messageBox, 20, 20).SendKeys(replyText).Build().Perform();
            outlookWindow.FindElement(sendEmailButton).Click();
        }

        public void ClickSpecificEmail(String emailNameContent1, String emailNameContent2)
        {
            IReadOnlyCollection<IWebElement> list = outlookWindow.FindElements(mailContainerFromList);
            foreach (IWebElement element in list)
            {
                if (element.GetAttribute("Name").Contains(emailNameContent1) 
                    && element.GetAttribute("Name").Contains(emailNameContent2))
                {                    
                    element.Click();
                    break;
                }
                throw new NoSuchElementException("Target email was not found in Inbox folder");
            }
        }

        public void OpenSentItems()
        {
            outlookWindow.FindElement(sentItemsLink).Click();
        }

        public String GetNameFirstEmailInSentItems()
        {
            return outlookWindow.FindElement(firstMailContainerFromSentItemsList).GetAttribute("Name");
        }

        public void ClickSendReceiveButton()
        {
            outlookWindow.FindElement(sendRecieveTab).Click();
            outlookWindow.FindElement(updateFolderButton).Click();
            new WebDriverWait(desktopDriver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.InvisibilityOfElementLocated(sendReceiveProgressWindow));
        }

        

    }
}
