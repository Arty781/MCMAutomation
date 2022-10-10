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
            try
            {
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
                WaitUntil.WaitSomeInterval(500);
                WaitUntil.CustomElevemtIsVisible(element, 30);

                element.Click();
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
        public static IWebElement Element(IWebElement element, int seconds, string data)
        {
            
            try 
            {
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 10);
                WaitUntil.CustomElevemtIsVisible(element, seconds);
                element.SendKeys(Keys.Control + "A" + Keys.Delete);
                WaitUntil.WaitSomeInterval(175);
                element.SendKeys(data);
            }
            catch (Exception) { }

            return element;
        }
        public static IWebElement CbbxElement(IWebElement element, int seconds, string data)
        {

            try
            {
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
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
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
           return element.Text;
        }

        public static string GetAttribute(IWebElement element, string attribute)
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(element);
            return element.GetAttribute(attribute);
            
        }
    }

    public class Element
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
