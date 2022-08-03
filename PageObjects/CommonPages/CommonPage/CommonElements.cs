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

        [FindsBy(How = How.XPath, Using = "//span[text()='Yes']")]
        public IWebElement btnConfirmationYes;

        [FindsBy(How = How.XPath, Using = "//div[@class='ant-spin ant-spin-spinning']")]
        public IWebElement loader;

        [FindsBy(How = How.XPath, Using = "//p[@class='user-small-title'][text()='Memberships']/parent::div//div[text()='No Data']")]
        public IWebElement itemsNoData;


        #region Alerts

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Item has been deleted' )]")]
        public IWebElement messageDeleted;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Item has been added' )]")]
        public IWebElement messageAdded;


        #endregion
    }
}
