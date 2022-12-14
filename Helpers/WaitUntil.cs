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

        public static void CustomElevemtIsVisible(IWebElement element, int seconds = 10)
        {
            Task.Delay(TimeSpan.FromMilliseconds(350)).Wait();
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            try
            {
                wait.Until(e =>
                {
                    try
                    {
                        if (element.Enabled == true)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (NoSuchElementException) { return false; }
                    catch (StaleElementReferenceException) { return false; }
                    catch (ArgumentOutOfRangeException) { return false; }

                });
            }
            catch (NoSuchElementException) { }
            catch (StaleElementReferenceException) { }
            catch (ArgumentOutOfRangeException) { }
            wait.Message = $"Timeout after {seconds}. The search element is not displayed";


        }

        public static void CustomElevemtIsInvisible(IWebElement element, int seconds = 10)
        {
            Task.Delay(TimeSpan.FromMilliseconds(250)).Wait();
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            try
            {
                wait.Until(e =>
                {
                    try
                    {
                        if (element.Enabled == true)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (NoSuchElementException) { return true; }
                    catch (StaleElementReferenceException) { return true; }

                });
            }
            catch (NoSuchElementException) { }
            catch (StaleElementReferenceException) { }
            wait.Message = $"Timeout after {seconds}. The search element is displayed";
        }



    }
}
