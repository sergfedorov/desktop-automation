using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Shapes;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
using System;
using System.Threading;

namespace PageObjects
{
    public class PaintWindow
    {
        Application paitApp;
        UIA3Automation automation;
        Window paintWindow;

        public void PaintStart()
        {
            paitApp = Application.Launch("mspaint.exe");
            automation = new UIA3Automation();
            paintWindow = paitApp.GetMainWindow(automation);
        }


        public void DrawRectangle()
        {
            AutomationElement mainDrawingPane = paintWindow.FindFirstChild(paintWindow.ConditionFactory.ByAutomationId("59648"));
            Point startingPoint = new Point(mainDrawingPane.BoundingRectangle.X + 100, mainDrawingPane.BoundingRectangle.Y + 100);

            // Draw the rectangle
            Mouse.MoveTo(startingPoint);
            Thread.Sleep(1000);
            Mouse.DragHorizontally(MouseButton.Left, Mouse.Position, 300.0);
            Thread.Sleep(1000);
            Mouse.DragVertically(MouseButton.Left, Mouse.Position, 100.0);
            Thread.Sleep(1000);
            Mouse.DragHorizontally(MouseButton.Left, Mouse.Position, -300.0);
            Thread.Sleep(1000);
            Mouse.DragVertically(MouseButton.Left, Mouse.Position, -100.0);
            
            // Fill the rectangle
            Button fillWithColorBtn = paintWindow.FindFirstDescendant(paintWindow.ConditionFactory.ByName("Fill with color")).AsButton();
            Mouse.MoveTo(fillWithColorBtn.GetClickablePoint());
            Mouse.LeftClick();
            //fillWithColorBtn.Click();

            Mouse.MoveTo((int)startingPoint.X+50, (int)startingPoint.Y+50);
            Mouse.LeftClick();
        }

        public void SaveFile(string fileName)
        {
            // Click Application menu
            Button applicationMenuBtn = paintWindow.FindFirstDescendant(paintWindow.ConditionFactory.ByName("Application menu")).AsButton();
            Mouse.MoveTo(applicationMenuBtn.GetClickablePoint());
            Mouse.LeftClick();
            //applicationMenuBtn.Click();

            // Click Save item
            MenuItem saveItem = paintWindow.FindFirstDescendant(paintWindow.ConditionFactory.ByName("Save")).AsMenuItem();
            saveItem.Invoke();

            // Save As dialog
            Window saveAsDialog = paintWindow.FindFirstChild(paintWindow.ConditionFactory.ByName("Save As")).AsWindow();

            // Enter file name and save
            TextBox filenameField = saveAsDialog.FindFirstDescendant(paintWindow.ConditionFactory.ByAutomationId("1001")).AsTextBox();
            filenameField.Enter(fileName);
            Wait.UntilInputIsProcessed();
            Keyboard.Type(VirtualKeyShort.RETURN);

            // Replace the file in case of duplicate name
            Window[] modalWindows = saveAsDialog.ModalWindows;
            if(modalWindows.Length > 0)
            {
                Button yesButton = modalWindows[0].FindFirstDescendant(modalWindows[0].ConditionFactory.ByName("Yes")).AsButton();
                yesButton.Invoke();
            }
        }
        
        public void ClosePaintWindow()
        {            
            paintWindow.Close();
        }
        
        public bool IsFileExistOnDesktop(string fileName)
        {
            AutomationElement desktop = automation.GetDesktop();            
            AutomationElement desktopProgramManager = desktop.FindFirstChild(desktop.ConditionFactory.ByName("Program Manager"));
            // System.UnauthorizedAccessException : Access is denied. (Exception from HRESULT: 0x80070005 (E_ACCESSDENIED))

            bool isExist = true;
            try
            {
                ListBoxItem myFile = desktopProgramManager.FindFirstChild(desktop.ConditionFactory.ByName(fileName)).AsListBoxItem();
            }catch(Exception e)
            {
                isExist = false;
            }

            return isExist;
        }
    }
}
