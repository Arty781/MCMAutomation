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

        public static void ElementIsClickable(IWebElement ElementCtrlA, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(ElementCtrlA));
        }

        public static void ElementIsVisible(By ElementCtrlA, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(ElementCtrlA));
        }

        public static void ElementIsVisibleAndClickable(By ElementCtrlA, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(ElementCtrlA));
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(ElementCtrlA));
        }

        public static void ElementIsInvisible(By ElementCtrlA, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.InvisibilityOfElementLocated(ElementCtrlA));
        }

        public static void VisibilityOfAllElementsLocatedBy(By ElementCtrlA, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(ElementCtrlA));
        }

        public static void InvisibilityOfAllElementsLocatedBy(By ElementCtrlA, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.InvisibilityOfElementLocated(ElementCtrlA));
        }

        public static void CustomElevemtIsVisible(IWebElement ElementCtrlA, int seconds = 10)
        {
            Task.Delay(TimeSpan.FromMilliseconds(150)).Wait();
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            try
            {
                wait.Until(e =>
                {
                    try
                    {
                        if (ElementCtrlA.Enabled == true)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception) { return false; }

                });
            }
            catch (NoSuchElementException) { }
            catch (StaleElementReferenceException) { }

        }

        public static void CustomElevemtIsInvisible(IWebElement ElementCtrlA, int seconds = 10)
        {
            Task.Delay(TimeSpan.FromMilliseconds(150)).Wait();
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            try
            {
                wait.Until(e =>
                {
                    try
                    {
                        if (ElementCtrlA.Enabled == true)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception) { return true; }
                });
            }
            catch (Exception) { }

        }



    }
}
