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
            WaitUntil.CustomElevemtIsVisible(searchInput);
            searchInput.SendKeys(email + Keys.Enter);
            

            WaitUntil.WaitSomeInterval(10);
            return this;
        }

        [AllureStep("Edit User")]

        public MembershipAdmin EditUser(string[] membershipName)
        {
            editBtn.Click();
            WaitUntil.CustomElevemtIsVisible(emailInput);
            
            addUserMembershipCbbx.SendKeys(membershipName[0] + Keys.Enter);
            Button.Click(addUserMembershipBtn);

            WaitUntil.CustomElevemtIsVisible(membershipItem, 20);

            selectUserActiveMembershipCbbx.SendKeys(membershipName[0] + Keys.Enter);
            WaitUntil.CustomElevemtIsVisible(membershipItem, 20);

            return this;
        }
    }
}