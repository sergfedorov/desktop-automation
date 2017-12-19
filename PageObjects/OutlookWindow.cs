using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace PageObjects
{
    public class OutlookWindow
    {

        AutomationElement outlookWindow;

        public OutlookWindow()
        {
            outlookWindow = Driver.GetOutlookWindow();
        }


        const string SendReceiveTab = "Send / Receive";
        const string UpdateFolderBtn = "Update Folder";
        const string ReplyToEmailBtn = "Reply";
        const string SendBtn = "Send";
        const string SentItemsSection = "Sent Items";
        const string ByTodayGroup = "Group By: Expanded: Date: Today";
        const string OutlookSendReceiveWindow = "Outlook Send/Receive Progress";


        public void SelectSendReceiveTab()
        {            
            outlookWindow.FindElementByNameWithWait(SendReceiveTab).Select();
        }

        public void ClickUpdateFolderBtn()
        {            
            outlookWindow.FindElementByNameWithWait(UpdateFolderBtn).Click();
            WaitForSendReceiveWindowIsHidden();
        }

        public void ClickMailItemFromList(string mailFrom, string mailSubject)
        {
            AutomationElementCollection list = outlookWindow.FindElementsByClassName("LeafRow");
            int listItemsCounter = 1;
            foreach (AutomationElement element in list)
            {
                string elementNameText = element.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;

                if (elementNameText.Contains(mailFrom) && elementNameText.Contains(mailSubject))
                {
                    element.Select();
                    WaitForSubjectIs(mailSubject); 
                    break;
                }
                listItemsCounter++;
                if (list.Count == listItemsCounter)
                    throw new ElementNotAvailableException(String.Format("Target email \"{0}\" was not found in Inbox folder", mailSubject));
            }
        }

        public void ReplyToEmail(string replyText)
        {            
            outlookWindow.FindElementByNameWithWait(ReplyToEmailBtn).Click();
            SendKeys.SendWait(replyText);            
            outlookWindow.FindElementByNameWithWait(SendBtn).Click();
        }

        public void SelectSentItems()
        {            
            outlookWindow.FindElementByNameWithWait(SentItemsSection).Select();
        }

        public string GetFirstEmailName()
        {            
            AutomationElement firstEmailInList = TreeWalker.ControlViewWalker.GetFirstChild(outlookWindow.FindElementByNameWithWait(ByTodayGroup));
            return firstEmailInList.GetAutomationElementName();
        }

        public void WaitForSendReceiveWindowIsHidden()
        {
            int cycles = 10;
            int sleep = 500;
            int count = 0;

            AutomationElement desktop = AutomationElement.RootElement;            
            AutomationElement element = desktop.FindElementByNameWithWait(OutlookSendReceiveWindow);
            
            while (element.IsElementEnabled() && count < cycles)
            {
                Thread.Sleep(sleep);
                try
                {
                    element = desktop.FindElementByNameWithWait(OutlookSendReceiveWindow);
                    count++;
                }
                catch (ElementNotAvailableException e)
                {
                    break;
                }  

                if (count == cycles - 1)
                {
                    throw new ElementNotAvailableException(String.Format("Send/Receive window was not hidden within specified time range"));
                }
            }
        }

        public void WaitForSubjectIs(string subject)
        {

            int cycles = 10;
            int sleep = 500;
            int count = 0;

            AutomationElement element = outlookWindow.FindElementByNameWithWait("Subject");
            string subjectValue = element.GetValue();

            while (subjectValue != subject && count < cycles)
            {
                Thread.Sleep(sleep);
                element = outlookWindow.FindElementByNameWithWait("Subject");
                subjectValue = element.GetValue();
                count++;

                if (count == cycles - 1)
                {
                    throw new ElementNotAvailableException(String.Format("Email subject \"{0}\" did not appear within specified time range", subjectValue));
                }
            }
        }  
    }
}
