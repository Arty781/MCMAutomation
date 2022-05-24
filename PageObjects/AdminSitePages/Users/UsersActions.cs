using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipAdmin
    {
        [AllureStep("Search User")]

        public MembershipAdmin SearchUser(string email)
        {
            WaitUntil.ElementIsVisible(_searchInput);
            searchInput.SendKeys(email + Keys.Enter);
            

            WaitUntil.WaitSomeInterval(10);
            return this;
        }

        [AllureStep("Edit User")]

        public MembershipAdmin EditUser(string membershipName)
        {
            editBtn.Click();
            WaitUntil.VisibilityOfAllElementsLocatedBy(_emailInput);
            
            addUserMembershipCbbx.SendKeys(membershipName + Keys.Enter);
            addUserMembershipBtn.Click();

            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipItem, 20);

            selectUserActiveMembershipCbbx.SendKeys(membershipName + Keys.Enter);
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipItem, 20);

            return this;
        }
    }
}