using MCMAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipUser
    {
        #region Client > Membership page

        #region YourNextProgram

        public IWebElement buyBtn => Browser._Driver.FindElement(_buyBtn);
        public readonly By _buyBtn = By.XPath("//a[@class='program-info_btn']");

        #endregion


        public IWebElement backBtn => Browser._Driver.FindElement(_backBtn);
        public readonly By _backBtn = By.XPath("//div[@class='btn_back']");

        





        #endregion
    }
}
