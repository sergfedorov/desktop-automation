using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;
using System.Windows.Automation;

namespace PageObjects
{
    public static class ExtensionMethods
    {
        public static string GetElementName(this WindowsElement element)
        {
            return element.GetAttribute("Name");
        }



        /***** MS UI Automation methods *****/

        public static AutomationElement FindElementByNameWithWait(this AutomationElement baseElement, string elementName)
        {
            AutomationElement element = null;

            int sleep = 500;
            int cycles = 5;
            int count = 0;

            while (count < cycles)
            {
                try
                {
                    element = baseElement.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.NameProperty, elementName));
                    element.GetType();
                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(sleep);
                    count++;
                }

                if (count == cycles - 1)
                {
                    throw new ElementNotAvailableException(String.Format("Element \"{0}\" was not found", elementName));
                }
            }

            return element;
        }

        public static AutomationElementCollection FindElementsByClassName(this AutomationElement baseElement, string elementClassName)
        {
            AutomationElementCollection elementCollection = baseElement.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ClassNameProperty, elementClassName));

            return elementCollection;
        }

        public static string GetAutomationElementName(this AutomationElement element)
        {
            return element.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
        }

        public static bool IsElementEnabled(this AutomationElement element)
        {           
            return (bool)element.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty);
        }

        public static void WaitForOutlookWindowIsDisplayed()
        {
            string outlookWindowName = "Inbox - SFedorov@lohika.com - Outlook";

            int cycles = 10;
            int sleep = 500;
            int count = 0;

            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement element = desktop.FindElementByNameWithWait(outlookWindowName);

            while (!element.IsElementEnabled() && count < cycles)
            {
                Thread.Sleep(sleep);
                element = desktop.FindElementByNameWithWait(outlookWindowName);
                count++;

                if (count == cycles - 1)
                {
                    throw new ElementNotAvailableException(String.Format("\"{0}\" window did not appear within specified time range", outlookWindowName));
                }
            }
        }



        /***** Actions *****/

        public static void Click(this AutomationElement element)
        {
            if (element.TryGetCurrentPattern(InvokePattern.Pattern, out object patternObject))
            {
                ((InvokePattern)patternObject).Invoke();
            }
            else
            {
                throw new NotSupportedException("InvokePattern is not supported for the target element");
            }
        }

        public static void Select(this AutomationElement element)
        {
            if (element.TryGetCurrentPattern(SelectionItemPattern.Pattern, out object patternObject))
            {
                ((SelectionItemPattern)patternObject).Select();
            }
            else
            {
                throw new NotSupportedException("SelectionItemPattern is not supported for the target element");
            }
        }

        public static string GetValue(this AutomationElement element)
        {
            if (element.TryGetCurrentPattern(ValuePattern.Pattern, out object patternObject))
            {
                return ((ValuePattern)patternObject).Current.Value;
            }
            else
            {
                throw new NotSupportedException("ValuePattern is not supported for the target element");
            }
        }



        /***** Not used *****/

        public static AutomationElement FindElementByName(this AutomationElement baseElement, string elementName)
        {
            //Thread.Sleep(2000);
            return baseElement.FindFirst(
            TreeScope.Descendants,
            new PropertyCondition(AutomationElement.NameProperty, elementName));
        }

        public static AutomationElementCollection FindElementsByName(this AutomationElement baseElement, string elementName)
        {
            //Thread.Sleep(2000);
            AutomationElementCollection elementCollection = baseElement.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.NameProperty, elementName));

            return elementCollection;
        }               

        public static AutomationElement FindElementByNameInChildren(this AutomationElement baseElement, string elementName)
        {
            return baseElement.FindFirst(
            TreeScope.Children,
            new PropertyCondition(AutomationElement.NameProperty, elementName));
        }

        public static AutomationElement FindElementByClassName(this AutomationElement baseElement, string elementClassName)
        {
            return baseElement.FindFirst(
            TreeScope.Descendants,
            new PropertyCondition(AutomationElement.ClassNameProperty, elementClassName));
        }
    }
}
