using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class Button
    {
        public static void Click(IWebElement element, int seconds = 10)
        {
            WaitUntil.WaitSomeInterval(2);
            WaitUntil.CustomElevemtIsVisible(element, seconds);

            element.Click();
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
                element.SendKeys(data);
            }
            catch(InvalidElementStateException) { }


            return element;
        }
        public static IWebElement CbbxElement(IWebElement element, int seconds, string data)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);
            try
            {
                element.SendKeys(data);
            }
            catch (InvalidElementStateException) { }


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

    public class RemoveBtn
    {
        public static void ClickRemoveExerciseBtn(string name, int seconds = 10)
        {
            WaitUntil.WaitSomeInterval(5);

            IWebElement btnRemove = Browser._Driver.FindElement(By.XPath($"//p[text()='{name}']/parent::div/following::div/div[@class='delete']"));
            WaitUntil.CustomElevemtIsVisible(btnRemove, seconds);

            btnRemove.Click();
        }
    }
}
