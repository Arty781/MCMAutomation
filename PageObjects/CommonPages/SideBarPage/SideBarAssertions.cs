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
            WaitUntil.WaitForElementToAppear(sideBarLogo, 30);
            Assert.IsTrue(sideBarLogo.Enabled);
            WaitUntil.WaitSomeInterval(2500);
            return this;
        }

        public Sidebar VerifyEmailDisplayed(string email)
        {
            WaitUntil.WaitForElementToAppear(textEmail.Where(x=>x.Text.Contains(email)).Select(x=>x).FirstOrDefault(), 10);
            WaitUntil.WaitSomeInterval(1000);
            Assert.IsTrue(textEmail.FirstOrDefault().Text == email);
            

            return this;
        }

       
    }
}
