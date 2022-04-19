using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class PresenceOfElement
    {
        public static bool IsElementPresent(By locator)
        {
            try
            {
                Browser._Driver.FindElement(locator);

                return true;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

    }
}
