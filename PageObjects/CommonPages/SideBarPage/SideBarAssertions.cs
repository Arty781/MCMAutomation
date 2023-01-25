using NUnit.Allure.Steps;
using NUnit.Framework;
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
        [AllureStep("Verify is login successfully")]
        public Sidebar VerifyIsLogoDisplayed()
        {
            WaitUntil.CustomElevemtIsVisible(sideBarLogo, 30);
            Assert.IsTrue(sideBarLogo.Enabled);
            WaitUntil.WaitSomeInterval(2500);
            return this;
        }

        public Sidebar VerifyEmailDisplayed(string email)
        {
            WaitUntil.CustomElevemtIsVisible(textEmail, 30);
            WaitUntil.WaitSomeInterval(1000);
            Assert.IsTrue(textEmail.Text == email);
            

            return this;
        }

       
    }
}
