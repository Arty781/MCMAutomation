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
        public static void Click(IWebElement ElementCtrlA)
        {
            try
            {
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
                WaitUntil.WaitSomeInterval(500);
                WaitUntil.CustomElevemtIsVisible(ElementCtrlA, 30);

                ElementCtrlA.Click();
            }
            catch (Exception) { }

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
        public static IWebElement ElementClear(IWebElement ElementCtrlA, int seconds, string data)
        {
            
            try 
            {
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 10);
                WaitUntil.CustomElevemtIsVisible(ElementCtrlA, seconds);
                ElementCtrlA.Click();
                ElementCtrlA.Clear();
                WaitUntil.WaitSomeInterval(75);
                ElementCtrlA.SendKeys(data);
            }
            catch (Exception) { }

            return ElementCtrlA;
        }

        public static IWebElement ElementCtrlA(IWebElement ElementCtrlA, int seconds, string data)
        {

            try
            {
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 10);
                WaitUntil.CustomElevemtIsVisible(ElementCtrlA, seconds);
                ElementCtrlA.SendKeys(Keys.Control + "A");
                ElementCtrlA.SendKeys(Keys.Delete);
                WaitUntil.WaitSomeInterval(75);
                ElementCtrlA.SendKeys(data);
            }
            catch (Exception) { }

            return ElementCtrlA;
        }

        public static IWebElement CbbxElement(IWebElement ElementCtrlA, int seconds, string data)
        {

            try
            {
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
                WaitUntil.CustomElevemtIsVisible(ElementCtrlA, seconds);
                ElementCtrlA.SendKeys(data + Keys.Enter);
            }
            catch (NoSuchElementException) { }
            catch (StaleElementReferenceException) { }
            return ElementCtrlA;
        }

       
    }
    public class TextBox
    {
        public static string GetText(IWebElement ElementCtrlA)
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
           return ElementCtrlA.Text;
        }

        public static string GetAttribute(IWebElement ElementCtrlA, string attribute)
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(ElementCtrlA);
            return ElementCtrlA.GetAttribute(attribute);
            
        }

        public static void CopyTextToBuffer(IWebElement ElementCtrlA)
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(ElementCtrlA);
            ElementCtrlA.SendKeys(Keys.Control + "A");
            ElementCtrlA.SendKeys(Keys.Control + "C");
        }

        public static void PasteCopiedText(IWebElement ElementCtrlA)
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(ElementCtrlA);
            ElementCtrlA.SendKeys(Keys.Control + "A" + Keys.Control + "V");
        }
    }

    public class ElementCtrlA
    {
        public static IWebElement webElem(string xpathString)
        {
            WaitUntil.WaitSomeInterval(250);
            var elem = Browser._Driver.FindElement(By.XPath(xpathString));
            return elem;
        }

        public static List<IWebElement> webElemList(string xpathString)
        {
            WaitUntil.WaitSomeInterval(250);
            var elem = Browser._Driver.FindElements(By.XPath(xpathString)).ToList();
            return elem;
        }
    }
}
