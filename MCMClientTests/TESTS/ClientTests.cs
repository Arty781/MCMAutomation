using NUnit.Framework;
using MCMAutomation.Helpers;
using MCMAutomation.PageObjects;
using MCMClientTests.BASE;
using NUnit.Allure.Core;
using OpenQA.Selenium;
using NUnit.Allure.Attributes;
using Allure.Commons;
using System.Collections.Generic;

namespace MCMClientTests.TESTS
{

    [TestFixture]
    [AllureNUnit]
    public class ClientTests : TestBaseClient
    {
        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Client")]
        [AllureSubSuite("Memberships")]

        public void EnterWeight()
        {
            Pages.Login
                .GetLogin(Credentials.login, Credentials.password);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipUser
                .OpenMembership()
                .SelectPhase()
                .SelectWorkout();
            
        }
    }
}
