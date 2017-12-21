
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using System;
using TestStack.White.UIItems.TreeItems;
using static TestStack.White.WindowsAPI.KeyboardInput;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.TabItems;
using System.Threading;
using TestStack.White.UIItems.ListBoxItems;

namespace PageObjects
{
    public class OutlookWindowWhite
    {
        Application outlookApplication;
        Window outlookWindow;

        public void OutlookStart()
        {
            outlookApplication = Application.Launch("outlook.exe");
            outlookWindow = outlookApplication.GetWindow("Inbox - SFedorov@lohika.com - Outlook");
        }
               

        public void EnterReplyMessage(string replyText)
        {
            Button replyBtn = outlookWindow.Get<Button>(SearchCriteria.ByText("Reply"));

            replyBtn.Click();
            outlookWindow.Keyboard.Enter(replyText);            
        }

        public void AttachFile(string fileName)
        {
            Button attachFile = outlookWindow.Get<Button>(SearchCriteria.ByText("Attach File"));
            attachFile.Click();

            TreeNode desktopItemInsideDialog = outlookWindow.Get<TreeNode>(SearchCriteria.ByText("Desktop"));
            desktopItemInsideDialog.Select();

            TextBox fileNameField = outlookWindow.Get<TextBox>(SearchCriteria.ByText("File name:"));
            fileNameField.Focus();
            fileNameField.SetValue(fileName);
            fileNameField.KeyIn(SpecialKeys.RETURN);

            var modals = outlookWindow.GetMultiple(SearchCriteria.ByAutomationId("CommandButton_1"));
            if(modals.Length > 0)
            {
                throw new AutomationException(String.Format("File \"{0}\" does not exist", fileName), "");
            } 
        }       

        public void ClickSendButton()
        {
            Button sendEmailButton = outlookWindow.Get<Button>(SearchCriteria.ByText("Send"));
            sendEmailButton.Click();           

        }

        public void SelectTargetEmail(string emailFrom)
        {
            ListViewRow firstEmailFromList = outlookWindow.Get<ListViewRow>(SearchCriteria.ByClassName("LeafRow"));

            int counter = 1;
            int sleep = 2000;
            int cycles = 20;
            while (!firstEmailFromList.Name.Contains(emailFrom))
            {
                Thread.Sleep(sleep);
                firstEmailFromList = outlookWindow.Get<ListViewRow>(SearchCriteria.ByClassName("LeafRow"));
                counter++;
                if (counter == cycles)
                {
                    throw new AutomationException(String.Format("Email from \"{0}\" was not found", emailFrom), "");
                }
            }

            firstEmailFromList.Click();
        }

        public string GetFirstEmailText()
        {
            ListViewRow firstEmailFromList = outlookWindow.Get<ListViewRow>(SearchCriteria.ByClassName("LeafRow"));
            return firstEmailFromList.Name;
        }

        public void OpenSentItemsSection()
        {  
            AutomationElement sentItemsAE = outlookWindow.GetElement(SearchCriteria.ByText("Sent Items"));
            UIItem sentItems = new UIItem(sentItemsAE, new NullActionListener());
            sentItems.Click();
        }        

        public void ClickEmail(string emailSubject)
        {
            IUIItem[] inboxEmailList = outlookWindow.GetMultiple(SearchCriteria.ByClassName("LeafRow"));
            int listItemsCounter = 1;
            for(int i = 0; inboxEmailList.Length > i; i++)
            {
                string elementName = inboxEmailList[i].Name;

                if (elementName.Contains(emailSubject))
                {
                    inboxEmailList[i].Click();
                    break;
                }
                listItemsCounter++;
                if (i == inboxEmailList.Length-1)
                {
                    throw new AutomationException(String.Format("Target email \"{0}\" was not found in Inbox folder", emailSubject), "details");
                }

            }
        }
        
    }
}
