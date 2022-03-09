using MCMAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    partial class PopUp
    {
        public IWebElement installBtn => Browser._Driver.FindElement(_installBtn);
        public readonly By _installBtn = By.XPath("//button//span[contains(text(), 'Install')]");

        public IWebElement cancelBtn => Browser._Driver.FindElement(_cancelBtn);
        public readonly By _cancelBtn = By.XPath("//button//span[contains(text(), 'Cancel')]");
    }
}
