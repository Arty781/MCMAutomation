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
        [AllureStep("Verify displaying of user")]

        public MembershipAdmin VerifyDisplayingOfUser(string email)
        {
            WaitUntil.WaitSomeInterval(1000);
            Assert.AreEqual(email, emailRow.GetAttribute("title"));
            return this;
        }
    }
}