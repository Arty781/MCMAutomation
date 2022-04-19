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

        public IWebElement backBtn => Browser._Driver.FindElement(_backBtn);
        public readonly By _backBtn = By.XPath("//div[@class='btn_back']");







        #endregion
    }
}
