using Allure.Commons;
using MCMAutomation.PageObjects;
using MCMWebTests.BASE;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.WebTests
{
    [TestFixture]
    [AllureNUnit]
    public class WebTests : TestBaseWeb
    {

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void RegisterNewUser()
        {
            Pages.SignUpUser
                .GoToSignUpPage();

            string email = RandomHelper.RandomEmail();
            Pages.SignUpUser
                .EnterData(email)
                .ClickOnSignUpBtn()
                .VerifyDisplayingPopUp()
                .GoToLoginPage();

            Pages.Login
                .GetLogin(email, Credentials.password);
            Pages.MembershipUser
                .VerifyIsBuyBtnDisplayed();
        }


        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CompleteMembershipsWithData()
        {
            
            List<User> email = AppDbContext.GetUserData();
            
            Pages.Login
                .GetUserLogin(email, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipUser
                .OpenMembership()
                .SelectPhase()
                .SelectWeekNumber()
                .OpenWorkouts()
                .AddWeight();
            
        }

    }
}
