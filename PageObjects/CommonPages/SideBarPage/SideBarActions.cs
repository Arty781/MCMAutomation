using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Sidebar
    {
        #region Opening sidebar menu's tabs

        [AllureStep("Open Membership page")]
        public Sidebar OpenMemberShipPage()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipTb);
            membershipTb.Click();

            WaitUntil.ElementIsVisible(Pages.MembershipAdmin._membershipCreateBtn);

            return this;
        }

        [AllureStep("Open Users page")]
        public Sidebar OpenUsersPage()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_usersTb);
            usersTb.Click();

            WaitUntil.VisibilityOfAllElementsLocatedBy(Pages.MembershipAdmin._searchInput);

            return this;
        }



        #endregion


    }
}
