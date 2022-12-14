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
    partial class PopUp
    {
        [FindsBy(How = How.XPath, Using = "//span[text()='Cancel']/parent::button")]
        public IWebElement btnCancel;

        [FindsBy(How = How.XPath, Using = "//span[text() = 'Install']/parent::button]")]
        public IWebElement btnInstall;

        [FindsBy(How = How.XPath, Using = "//div[@class='ant-modal-footer']//button[@type='button']")]
        public IWebElement popupYesNoBtnLinq;
    }
}
