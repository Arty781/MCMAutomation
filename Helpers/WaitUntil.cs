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
            wait.Message = $"Timeout after {seconds}. The search element is not displayed";
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
                    catch (Exception) { return false; }

                });
                
            }
            catch (Exception) { }
            
            Task.Delay(TimeSpan.FromMilliseconds(350)).Wait();

        }

        public static void CustomElevemtIsInvisible(IWebElement element, int seconds = 10)
        {
            Task.Delay(TimeSpan.FromMilliseconds(550)).Wait();
            WebDriverWait wait = new(Browser._Driver, TimeSpan.FromSeconds(seconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100),
                Message = "The search element is displayed"
            };
            try
            {
                wait.Until(e =>
                {
                    try
                    {
                        if (element.Enabled == true && element.Displayed == true)
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
            
            Task.Delay(TimeSpan.FromMilliseconds(350)).Wait();
        }

        public static void LoaderIsInvisible(IWebElement element, int seconds = 10)
        {
            Task.Delay(TimeSpan.FromMilliseconds(150)).Wait();
            WebDriverWait wait = new(Browser._Driver, TimeSpan.FromSeconds(seconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100),
                Message = $"Timeout after {seconds}. The search element is displayed"
            };
            try
            {
                wait.Until(e =>
                {
                    try
                    {
                        if (element.Enabled == true && element.Displayed == true)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (NoSuchElementException) { return true; }
                    catch (StaleElementReferenceException) { return true; }

                });
                wait.Message = $"Timeout after {seconds}. The search element is displayed";
            }
            catch (NoSuchElementException) { }
            catch (StaleElementReferenceException) { }

            Task.Delay(TimeSpan.FromMilliseconds(150)).Wait();
        }



    }
}
