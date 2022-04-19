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
    class WaitUntil
    {
        public static void WaitSomeInterval(int seconds = 2)
        {
            Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
        }

        public static void ElementIsClickable(IWebElement element, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static void ElementIsVisible(By element, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(element));
        }

        public static void ElementIsVisibleAndClickable(By element, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(element));
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(element));
        }

        public static void ElementIsInvisible(By element, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.InvisibilityOfElementLocated(element));
        }

        public static void VisibilityOfAllElementsLocatedBy(By element, int seconds = 10)
        {
            new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
        }


    }
}
