using MCMAutomation.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Common
    {
        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement saveBtn;




        #region Alerts

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Item has been deleted' )]")]
        public IWebElement deleteMessage;
        

        #endregion
    }
}
