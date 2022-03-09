using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    internal class CustomWaitHelper
    {
        private static IWebElement ElementIfVisible(IWebElement element)
        {
            return element.Displayed ? element : null;
        }
        public static Func<IWebDriver, IWebElement> CustomElementIsClickableByLocator(By locator)
        {
            return (driver) =>
            {
                try
                {
                    IWebElement element = ElementIfVisible(driver.FindElement(locator));
                    if (element != null && element.Enabled)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            };
        }
    }
}
