using MCMAutomation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class Button
    {
        public static void Click(IWebElement element)
        {
            WaitUntil.LoaderIsInvisible(Pages.Common.loader, 60);
            WaitUntil.WaitSomeInterval(500);
            WaitUntil.CustomElevemtIsVisible(element, 30);

            element.Click();

        }

        public static void ScrollTo(int xPosition, int yPosition)
        {
            try
            {
                IJavaScriptExecutor jsi = (IJavaScriptExecutor)Browser._Driver;
                jsi.ExecuteScript("window.scrollTo({0}, {1})", xPosition, yPosition);
            }
            catch (Exception) { }
        }


    }
    public class InputBox
    {
        public static IWebElement ElementClear(IWebElement element, int seconds, string data)
        {
            
            try 
            {
                WaitUntil.LoaderIsInvisible(Pages.Common.loader, 10);
                WaitUntil.CustomElevemtIsVisible(element, seconds);
                element.Click();
                element.Clear();
                WaitUntil.WaitSomeInterval(75);
                element.SendKeys(data);
            }
            catch (Exception) { }

            return element;
        }

        public static IWebElement ElementCtrlA(IWebElement element, int seconds, string data)
        {

            try
            {
                WaitUntil.LoaderIsInvisible(Pages.Common.loader, 10);
                WaitUntil.CustomElevemtIsVisible(element, seconds);
                element.SendKeys(Keyss.Control() + "A");
                element.SendKeys(Keys.Delete);
                WaitUntil.WaitSomeInterval(75);
                element.SendKeys(data);
            }
            catch (Exception) { }

            return element;
        }

        public static IWebElement CbbxElement(IWebElement element, int seconds, string data)
        {

            try
            {
                WaitUntil.LoaderIsInvisible(Pages.Common.loader, 60);
                WaitUntil.CustomElevemtIsVisible(element, seconds);
                element.SendKeys(data + Keys.Enter);
            }
            catch (NoSuchElementException) { }
            catch (StaleElementReferenceException) { }
            return element;
        }

       
    }
    public class TextBox
    {
        public static string GetText(IWebElement element)
        {
            WaitUntil.LoaderIsInvisible(Pages.Common.loader, 60);
           return element.Text;
        }

        public static string GetAttribute(IWebElement element, string attribute)
        {
            WaitUntil.LoaderIsInvisible(Pages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(element);
            return element.GetAttribute(attribute);
            
        }

        public static void CopyTextToBuffer(IWebElement element)
        {
            WaitUntil.LoaderIsInvisible(Pages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(element);
            element.SendKeys(Keyss.Control() + "A");
            element.SendKeys(Keyss.Control() + "C");
        }

        public static void PasteCopiedText(IWebElement element)
        {
            WaitUntil.LoaderIsInvisible(Pages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(element);
            element.SendKeys(Keyss.Control() + "A" + Keyss.Control() + "V");
        }
    }

    public class Element
    {
        public static IWebElement FindElementByXpath(string xpathString)
        {
            WaitUntil.WaitSomeInterval(350);
            WaitUntil.CustomElevemtIsVisible(Browser._Driver.FindElement(By.XPath(xpathString)));
            var elem = Browser._Driver.FindElement(By.XPath(xpathString));
            return elem;
        }

        public static List<IWebElement> FindElementsByXpath(string xpathString)
        {
            WaitUntil.WaitSomeInterval(250);
            var elem = Browser._Driver.FindElements(By.XPath(xpathString)).ToList();
            return elem;
        }
    }
}
