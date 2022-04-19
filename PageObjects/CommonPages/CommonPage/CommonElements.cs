using MCMAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Common
    {
        public IWebElement saveBtn => Browser._Driver.FindElement(_saveBtn);
        public readonly By _saveBtn = By.XPath("//button[@type='submit']");



        #region Alerts

        public IWebElement deleteMessage => Browser._Driver.FindElement(_deleteMessage);
        public readonly By _deleteMessage = By.XPath("//div[contains(text(),'Item has been deleted' )]");

        #endregion
    }
}
