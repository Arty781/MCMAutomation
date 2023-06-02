using MCMAutomation.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class WaitUntil
    {
        public static void WaitSomeInterval(int milliseconds = 2000)
        {
            Task.Delay(TimeSpan.FromMilliseconds(milliseconds)).Wait();
        }

        public static void WaitForElementToAppear(IWebElement element, int seconds = 10)
        {
            Task.Delay(TimeSpan.FromMilliseconds(250)).Wait();
            IWait<IWebDriver> wait = new DefaultWait<IWebDriver>(Browser._Driver)
            {
                Timeout = TimeSpan.FromSeconds(seconds),
                PollingInterval = TimeSpan.FromMilliseconds(100),
                Message = "The search element is not visible"
            };
            try
            {
                wait.Until(driver =>
                {
                    try
                    {
                        if (element.Enabled == true || element.Displayed == true)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch(Exception) { return false; }
                });
                Task.Delay(TimeSpan.FromMilliseconds(350)).Wait();
            }
            catch(Exception) { throw new ArgumentException(wait.Message); }
        }

        public static void WaitForElementToDisappear(IWebElement element, int seconds = 10)
        {
            Task.Delay(TimeSpan.FromMilliseconds(550)).Wait();
            IWait<IWebDriver> wait = new DefaultWait<IWebDriver>(Browser._Driver)
            {
                Timeout = TimeSpan.FromSeconds(seconds),
                PollingInterval = TimeSpan.FromMilliseconds(100),
                Message = "The search element is still visible"
            };
            try
            {
                wait.Until(driver =>
                            {
                                try
                                {
                                    if (element?.Enabled != true || element?.Displayed != true)
                                    {
                                        return true;
                                    }
                                    return false;
                                }
                                catch(Exception) { return true; }
                            });
                Task.Delay(TimeSpan.FromMilliseconds(350)).Wait();
            }
            catch(Exception) { throw new ArgumentException(wait.Message); }
        }



    }
}
