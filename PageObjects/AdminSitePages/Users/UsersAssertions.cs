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
    public partial class UsersAdmin
    {
        [AllureStep("Verify displaying of user")]

        public UsersAdmin VerifyDisplayingOfUser(string email)
        {
            WaitUntil.WaitSomeInterval(1000);
            WaitUntil.WaitForElementToAppear(SwitcherHelper.GetTextForUserEmail(email), 10);
            Assert.AreEqual(email, SwitcherHelper.GetTextForUserEmail(email).GetAttribute("title"));
            return this;
        }
    }
}