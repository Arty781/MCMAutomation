using MCMAutomation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class Button
    {
        public static void Click(IWebElement element)
        {
            WaitUntil.LoaderIsInvisible(Pages.CommonPages.Common.loader, 60);
            WaitUntil.WaitSomeInterval(500);
            WaitUntil.CustomElevemtIsVisible(element, 30);

            element.Click();
        }
        public static void ClickJS(IWebElement element)
        {
            WaitUntil.CustomElevemtIsVisible(element, 10);
            IJavaScriptExecutor ex = (IJavaScriptExecutor)Browser._Driver;
            ex.ExecuteScript("arguments[0].click();", element);
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
                WaitUntil.LoaderIsInvisible(Pages.CommonPages.Common.loader, 10);
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
                WaitUntil.LoaderIsInvisible(Pages.CommonPages.Common.loader, 10);
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
                WaitUntil.LoaderIsInvisible(Pages.CommonPages.Common.loader, 60);
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
            WaitUntil.LoaderIsInvisible(Pages.CommonPages.Common.loader, 60);
           return element.Text;
        }

        public static string GetAttribute(IWebElement element, string attribute)
        {
            WaitUntil.LoaderIsInvisible(Pages.CommonPages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(element);
            return element.GetAttribute(attribute);
            
        }

        public static void CopyTextToBuffer(IWebElement element)
        {
            WaitUntil.LoaderIsInvisible(Pages.CommonPages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(element);
            element.SendKeys(Keyss.Control() + "A");
            element.SendKeys(Keyss.Control() + "C");
        }

        public static void PasteCopiedText(IWebElement element)
        {
            WaitUntil.LoaderIsInvisible(Pages.CommonPages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(element);
            element.SendKeys(Keyss.Control() + "A" + Keyss.Control() + "V");
        }
    }

    public class Element
    {
        public static IWebElement FindElementByXpath(string xpathString)
        {
            IWebElement element = null;
            Task.Delay(TimeSpan.FromMilliseconds(350)).Wait();
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Message = $"Timeout after {10} sec. The search element is not displayed";
            try
            {
                wait.Until(e =>
                {
                    try
                    {
                        if (Browser._Driver.FindElement(By.XPath(xpathString)).Enabled == true)
                        {
                            element = Browser._Driver.FindElement(By.XPath(xpathString));
                            return true;
                        }
                        return false;
                    }
                    catch (Exception) { return false; }

                });

            }
            catch (Exception) { }
            
            return element;
        }

        public static List<IWebElement> FindElementsByXpath(string xpathString)
        {
            WaitUntil.WaitSomeInterval(250);
            var elem = Browser._Driver.FindElements(By.XPath(xpathString)).ToList();
            return elem;
        }
    }
}
