using OpenQA.Selenium.Appium.Windows;

namespace PageObjects
{
    public static class ExtensionMethods
    {
        public static string GetElementName(this WindowsElement element)
        {
            return element.GetAttribute("Name");
        }


    }
}
