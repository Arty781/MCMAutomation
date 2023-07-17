using CONFIG_JSON;
using HtmlAgilityPack;
using MCMAutomation.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextCopy;

namespace MCMAutomation.Helpers
{
    public class Button
    {
        public static void Click(IWebElement element)
        {
            WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 60);
            WaitUntil.WaitSomeInterval(300);
            WaitUntil.WaitForElementToAppear(element, 30);

            element.Click();
        }
        public static void ClickJS(IWebElement element)
        {
            WaitUntil.WaitForElementToAppear(element, 10);
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
                WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 10);
                WaitUntil.WaitForElementToAppear(element, seconds);
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
                WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 10);
                WaitUntil.WaitForElementToAppear(element, seconds);
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
                WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 60);
                WaitUntil.WaitForElementToAppear(element, seconds);
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
            WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 60);
           return element.Text;
        }

        public static string GetAttribute(IWebElement element, string attribute)
        {
            WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 60);
            WaitUntil.WaitForElementToAppear(element);
            return element.GetAttribute(attribute);
            
        }

        public static void CopyTextToBuffer(IWebElement element)
        {
            WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 60);
            WaitUntil.WaitForElementToAppear(element);
            element.SendKeys(Keyss.Control() + "A");
            element.SendKeys(Keyss.Control() + "C");
        }

        public static void PasteCopiedText(IWebElement element)
        {
            WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 60);
            WaitUntil.WaitForElementToAppear(element);
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

    public class ClipboardHelper
    {
        public static void SetText(string text)
        {
            try
            {
                Clipboard clipboard = new();
                clipboard.SetText(text);
                Console.WriteLine("Text added to clipboard: " + text);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur while setting the text
                Console.WriteLine("Error setting text to clipboard: " + ex.Message);
            }
        }
    }

    public class ParseHelper
    {
        public static void AddLinksToList(string html, out List<string> links)
        {
            links = new();
            // Load HTML code into an HtmlDocument object
            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(html);

            HtmlNodeCollection iframeNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='iframe-wrapper']/iframe");
            if (iframeNodes != null)
            {
                foreach (HtmlNode iframeNode in iframeNodes)
                {
                    string src = iframeNode.GetAttributeValue("src", "");
                    links.Add(src);
                }
            }

        }



    }
}
