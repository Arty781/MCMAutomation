using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chilkat;

namespace MCMAutomation.PageObjects
{
    public partial class Sidebar
    {
        #region Opening Admin sidebar menu's tabs

        [AllureStep("Open Membership page")]
        public Sidebar OpenMemberShipPage()
        {
            var dateBefore = DateTime.Now;
            Button.Click(membershipTb);

            WaitUntil.CustomElevemtIsVisible(Pages.AdminPages.MembershipAdmin.btnCreateMembership, 90);
            var lastMembership = Pages.AdminPages.MembershipAdmin.membershipTitle.Last();
            WaitUntil.CustomElevemtIsVisible(lastMembership, 30);
            var dateAfter = DateTime.Now;
            Console.WriteLine($"Load time for {Browser._Driver.Url} is: " + (dateAfter - dateBefore));

            return this;
        }

        [AllureStep("Open Membership page")]
        public Sidebar OpenMemberShipPageUser()
        {
            
            Button.Click(trainingProgramTb);

            WaitUntil.CustomElevemtIsVisible(Pages.WebPages.MembershipUser.programTitle, 30);
            

            return this;
        }

        [AllureStep("Open Users page")]
        public Sidebar OpenUsersPage()
        {
            
            Button.Click(usersTb);

            return this;
        }

        [AllureStep("Open Exercises page")]
        public Sidebar OpenExercisesPage()
        {
            
            Button.Click(exercisesTb);

            return this;
        }

        #endregion

        #region Opening User sidebar menu's tabs

        [AllureStep("Open Nutrition page")]
        public Sidebar OpenNutritionPage()
        {
            Button.Click(nutritionTb);
            WaitUntil.CustomElevemtIsVisible(Pages.WebPages.Nutrition.btnCalculate, 30);
            
            return this;
        }

        [AllureStep("Open MyAccount")]
        public Sidebar OpenMyAccount(string email)
        {
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Button.Click(btnUserName);

            WaitUntil.CustomElevemtIsVisible(btnMyAccount, 30);
            Button.Click(btnMyAccount);
            WaitUntil.CustomElevemtIsVisible(Pages.WebPages.UserProfile.inputFirstName, 30);

            return this;
        }

        [AllureStep("Open Progress")]
        public Sidebar OpenProgressPage()
        {
            Button.Click(progressTb);
            WaitUntil.CustomElevemtIsVisible(Pages.WebPages.Progress.titleProgressPage, 30);
            return this;
        }


        #endregion


    }
}
