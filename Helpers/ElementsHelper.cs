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
                WaitUntil.WaitSomeInterval(250);
                WaitUntil.CustomElevemtIsVisible(element, 30);

                element.Click();
            }
            catch (Exception) { }
            
        }

        public static void ScrollTo(int xPosition, int yPosition)
        {
            try
            {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Browser._Driver;
            js.ExecuteScript("window.scrollTo({0}, {1})", xPosition, yPosition);
            }
            catch (Exception) { }
        }


    }

    public class InputBox
    {
        public static IWebElement Element(IWebElement element, int seconds, string data)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);
            try 
            {
                
                element.SendKeys(Keys.Control + "A" + Keys.Delete);
                WaitUntil.WaitSomeInterval(200);
                element.SendKeys(data);
            }
            catch(Exception) { }


            return element;
        }
        public static IWebElement CbbxElement(IWebElement element, int seconds, string data)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);
            try
            {
                element.SendKeys(data);
            }
            catch (Exception) { }


            return element;
        }

       
    }
    public class TextBox
    {
        public static string GetText(IWebElement element, int seconds = 30)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);

            return element.Text;
        }

        public static string GetAttribute(IWebElement element, string attribute, int seconds = 30)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);

            return element.GetAttribute(attribute);
        }
    }


    public class ButtonActionsMembership
    {
        

       
    }
}
